using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using StudyMate.Wpf.Integrations.Ai.Interfaces;
using StudyMate.Wpf.Models.Ai;

namespace StudyMate.Wpf.Integrations.Ai
{
    public class AiClient : IAiClient
    {
        private readonly HttpClient _httpClient;
        private readonly AiSettings _settings;
        private readonly IPdfTextExtractor _pdfTextExtractor;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

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
