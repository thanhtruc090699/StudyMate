namespace StudyMate.Wpf.Models.Ai
{
    /// <summary>
    /// Configuration settings for AI service integration.
    /// Contains credentials, endpoints, and model configuration required to communicate with the AI API.
    /// </summary>
    public class AiSettings
    {
        /// <summary>
        /// Gets or sets the base URL of the AI service API (e.g., "https://api.example.com/").
        /// Must be a valid absolute URI.
        /// </summary>
        public string BaseUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the endpoint path for chat completion requests (e.g., "/v1/chat/completions").
        /// Appended to the BaseUrl to form the complete request URL.
        /// </summary>
        public string AnalysisEndpoint { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the API key used for authentication with the AI service.
        /// Sent as a Bearer token in the Authorization header.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the AI model to use for generating study materials
        /// (e.g., "gpt-4", "claude-3").
        /// </summary>
        public string ModelName { get; set; } = string.Empty;
    }
}
