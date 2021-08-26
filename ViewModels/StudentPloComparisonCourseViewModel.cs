using System.Collections.Generic;
using SpmsApp.Models;

namespace SpmsApp.ViewModels
{
    public class StudentPloComparisonCourseViewModel
    {
        public List<Course> Courses { get; set; }
        public TopbarViewModel TopbarViewModel { get; set; }
    }
}