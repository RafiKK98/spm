namespace SpmsApp.Models
{
    public class CourseOutcome
    {
        public int CoID { get; set; }
        public string CoNum { get; set; }
        public Course Course { get; set; }
        public string Details { get; set; }
        public ProgramlearningOutcome PLO { get; set; }
    }
}