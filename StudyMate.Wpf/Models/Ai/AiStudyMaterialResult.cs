
namespace StudyMate.Wpf.Models.Ai
{
    public class AiStudyMaterialResult
    {
        public string Name { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public StructuredContent StructuredContent { get; set; } = new StructuredContent();

        public List<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();

        public string ModelName { get; set; } = string.Empty;
    }
}
