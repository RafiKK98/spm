namespace SpmsApp.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int numberOfCredits { get; set; }
        public int programID { get; set; }
        public int CoofferedCourseID { get; set; }
    }
}