using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using StudyMate.Wpf.Integrations.Ai.Interfaces;
using StudyMate.Wpf.Models.Ai;

namespace StudyMate.Wpf.Integrations.Ai
{
    /// <summary>
    /// Communicates with the configured AI service to generate structured
    /// study material from PDF documents.
    /// </summary>
    public class AiClient : IAiClient
    {
        private readonly HttpClient _httpClient;
        private readonly AiSettings _settings;
        private readonly IPdfTextExtractor _pdfTextExtractor;

        /// <summary>
        /// JSON serialization options used for deserializing AI responses.
        /// Configured to be case-insensitive for property name matching.
        /// </summary>
        public static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private static readonly JsonSerializerOptions PrivateJsonOptions = JsonOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="AiClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client for making API requests.</param>
        /// <param name="settings">AI service configuration settings.</param>
        /// <param name="pdfTextExtractor">PDF text extractor for reading document content.</param>
        /// <exception cref="InvalidOperationException">Thrown when AI settings are invalid.</exception>
        public AiClient(HttpClient httpClient, AiSettings settings, IPdfTextExtractor pdfTextExtractor)
        {
            _httpClient = httpClient;
            _settings = settings;
            _pdfTextExtractor = pdfTextExtractor;

            ValidateSettings();

            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settings.ApiKey);
            _httpClient.Timeout = TimeSpan.FromMinutes(5);
        }

        /// <inheritdoc />
        public async Task<AiStudyMaterialResult> GenerateStudyMaterialAsync(string filePath, CancellationToken cancellationToken = default)
        {
            // Build full path since FilePath in DB only stores filename
            var appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "StudyMate",
                "uploads"
            );
            var fullPath = Path.Combine(appDataPath, filePath);
            
            var documentText = await _pdfTextExtractor.ExtractTextAsync(fullPath, cancellationToken);

            // Build request body for chat completion API
            var requestBody = new
            {
                model = _settings.ModelName,
                messages = new object[]
                {
                    new
                    {
                        role = "system",
                        content = AiPromptBuilder.BuildStudyMaterialPrompt()
                    },
                    new
                    {
                        role = "user",
                        content = $"Analyze this study document:\n\n{documentText}"
                    }
                }
            };

            var requestJson = JsonSerializer.Serialize(requestBody);

            using var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            
            using var response = await _httpClient.PostAsync(_settings.AnalysisEndpoint, requestContent, cancellationToken);

            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Lisa API returned HTTP {(int)response.StatusCode}: {responseJson}");
            }

            return ParseChatResponse(responseJson);
        }

        /// <summary>
        /// Parses the raw JSON response from the AI chat completion API into a strongly-typed result.
        /// Extracts the message content from the first choice and deserializes it as AiStudyMaterialResult.
        /// Handles removal of markdown code fences if present.
        /// </summary>
        /// <param name="responseJson">The raw JSON response from the AI API.</param>
        /// <returns>A parsed <see cref="AiStudyMaterialResult"/> object.</returns>
        /// <exception cref="InvalidOperationException">Thrown when response content is empty or cannot be parsed.</exception>
        private static AiStudyMaterialResult ParseChatResponse(string responseJson)
        {
            using var document = JsonDocument.Parse(responseJson);
            
            var content = document.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new InvalidOperationException("Lisa returned empty message content.");
            }

            var cleanedContent = RemoveMarkdownCodeFence(content);
            
            var result = JsonSerializer.Deserialize<AiStudyMaterialResult>(cleanedContent, JsonOptions);

            if (result == null)
            {
                throw new InvalidOperationException("Lisa result could not be parsed.");
            }

            return result;
        }

        /// <summary>
        /// Removes markdown code fence markers (```) from the content string.
        /// Handles both generic code fences and language-specific fences like ```json.
        /// </summary>
        /// <param name="content">The content string potentially containing markdown fences.</param>
        /// <returns>The cleaned content without markdown code fence markers.</returns>
        private static string RemoveMarkdownCodeFence(string content)
        {
            var cleaned = content.Trim();

            if (cleaned.StartsWith("```json", StringComparison.OrdinalIgnoreCase))
            {
                cleaned = cleaned["```json".Length..];
            }
            else if (cleaned.StartsWith("```"))
            {
                cleaned = cleaned[3..];
            }

            if (cleaned.EndsWith("```"))
            {
                cleaned = cleaned[..^3];
            }

            return cleaned.Trim();
        }

        /// <summary>
        /// Validates that all required AI settings are properly configured.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when any required setting is missing or invalid.</exception>
        private void ValidateSettings()
        {
            if (!Uri.TryCreate(_settings.BaseUrl, UriKind.Absolute, out _))
            {
                throw new InvalidOperationException("AI BaseUrl is invalid.");
            }

            if (string.IsNullOrWhiteSpace(_settings.AnalysisEndpoint))
            {
                throw new InvalidOperationException("AI AnalysisEndpoint is missing.");
            }

            if (string.IsNullOrWhiteSpace(_settings.ApiKey))
            {
                throw new InvalidOperationException("AI API key is missing.");
            }

            if (string.IsNullOrWhiteSpace(_settings.ModelName))
            {
                throw new InvalidOperationException("AI ModelName is missing.");
            }
        }
    }
}
