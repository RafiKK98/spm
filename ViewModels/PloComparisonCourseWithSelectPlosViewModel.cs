using System.Collections.Generic;
using SpmsApp.Models;

namespace SpmsApp.ViewModels
{
    public class PloComparisonCourseWithSelectPlosViewModel
    {
        public TopbarViewModel TopbarViewModel { get; set; }
        public List<Course> Courses { get; set; }

        public List<int> SelectedCoursesID { get; set; }
        public List<string> SelectedPlosName { get; set; }
        public Semester StartSemester { get; set; }
        public Semester EndSemester { get; set; }
    }
}