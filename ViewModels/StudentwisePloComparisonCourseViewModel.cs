using System.Collections.Generic;
using SpmsApp.Models;

namespace SpmsApp.ViewModels
{
    public class StudentwisePloComparisonCourseViewModel
    {
        public List<Course> Courses { get; internal set; }
        public List<Student> Students { get; internal set; }
        public TopbarViewModel TopbarViewModel { get; internal set; }
    }
}