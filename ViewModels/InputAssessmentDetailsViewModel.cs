using System.Collections.Generic;
using SpmsApp.Models;

namespace SpmsApp.ViewModels
{
    public class InputAssessmentDetailsViewModel
    {
        public TopbarViewModel TopbarViewModel { get; set; }
        public List<Course> Courses { get; set; }
        public List<Assessment> Assessments { get; set; }
        public Course SelectedCourse { get; set; }
        public List<Course> CourseList { get; set; }
        // public bool ShowAssessment { get; set; }
    }
}