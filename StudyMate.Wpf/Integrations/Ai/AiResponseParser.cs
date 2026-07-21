using System.Text.Json;
using StudyMate.Wpf.Models.Ai;

namespace StudyMate.Wpf.Integrations.Ai
{
    public static class AiResponseParser
    {
        public static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static AiStudyMaterialResult ParseAiResponse(string responseContent)
        {
            System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\ai_debug.log", $"[{System.DateTime.Now:HH:mm:ss}] ParseAiResponse: Input length={responseContent.Length}\n");
            
            if (string.IsNullOrWhiteSpace(responseContent))
            {
                throw new InvalidOperationException("the Ai API returned an empty response");
            }

            var cleanedJson = RemoveMarkDownCodeFence(responseContent);
            System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\ai_debug.log", $"[{System.DateTime.Now:HH:mm:ss}] ParseAiResponse: Cleaned JSON={cleanedJson}\n");
            
            try
            {
                var result = JsonSerializer.Deserialize<AiStudyMaterialResult>(cleanedJson, JsonOptions);
                if(result == null)
                {
                    System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\ai_debug.log", $"[{System.DateTime.Now:HH:mm:ss}] ParseAiResponse: Result is null after deserialization\n");
                    throw new InvalidOperationException("Failed to parse Ai response");
                }
                System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\ai_debug.log", $"[{System.DateTime.Now:HH:mm:ss}] ParseAiResponse: Summary={result.Summary?.Substring(0, Math.Min(50, result.Summary.Length))}...\n");
                System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\ai_debug.log", $"[{System.DateTime.Now:HH:mm:ss}] ParseAiResponse: Quiz count={result.QuizQuestions?.Count}\n");
                ValidateResult(result);
                return result;
            }
            catch(JsonException ex)
            {
                System.IO.File.AppendAllText("C:\\Users\\T490s\\AppData\\Local\\Temp\\ai_debug.log", $"[{System.DateTime.Now:HH:mm:ss}] ParseAiResponse: JsonException={ex.Message}\n");
                throw new InvalidOperationException("Failed to parse Ai response", ex);
            }

        }

        private static string RemoveMarkDownCodeFence(string responseContent)
        {
            var content = responseContent.Trim();

            if (content.StartsWith("```json", StringComparison.OrdinalIgnoreCase))
            {
                content = content["```json".Length..];
            }
            else if (content.StartsWith("```"))
            {
                content = content[3..];
            }

            if (content.EndsWith("```"))
            {
                content = content[..^3];
            }

            return content.Trim();
        }

        private static void ValidateResult(AiStudyMaterialResult result)
        {
            if (string.IsNullOrWhiteSpace(result.Summary))
            {
                throw new InvalidOperationException("The Ai reponse does not contain a summary");
            }

            if (result.StructuredContent == null)
            {
                throw new InvalidOperationException("The Ai reponse does not contain structured content");
            }

            if(result.QuizQuestions == null || result.QuizQuestions.Count == 0)
            {
                throw new InvalidOperationException("The Ai reponse does not contain any quiz questions");
            }

            foreach (var question in result.QuizQuestions)
            {
                if (string.IsNullOrWhiteSpace(question.Question))
                {
                    throw new InvalidOperationException("The Ai reponse contains a quiz question without content");
                }

                if (question.Options == null || question.Options.Count != 4)
                {
                    throw new InvalidOperationException("Each quiz question must have exactly 4 options");
                }
                if(question.CorrectOptionIndex < 0 || question.CorrectOptionIndex >= question.Options.Count)
                {
                    throw new InvalidOperationException("Each quiz question must have a valid correct option index");
                }
                if(string.IsNullOrWhiteSpace(question.Explanation))
                {
                    throw new InvalidOperationException("Each quiz question must have an explanation");
                }
            }
        }
    }
}
