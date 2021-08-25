using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class Assessment
    {
        public int AssessmentID { get; set; }
        public int QuestionNumber { get; set; }
        public string AssessmentType { get; set; }
        public int TotalMark { get; set; }
        public Section Section { get; set; }
        public CourseOutcome CourseOutcome { get; set; }
    }
}