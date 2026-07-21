namespace StudyMate.Wpf.Models.Ai
{
    public class QuizQuestion
    {
        public string Question { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectOptionIndex { get; set; }

        public string Explanation { get; set; } = string.Empty;
    }
}
