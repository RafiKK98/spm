namespace SpmsApp.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CourseCreditCount { get; set; }
        public Program Program { get; set; }
        public int CoofferedCourseID { get; set; }
    }
}