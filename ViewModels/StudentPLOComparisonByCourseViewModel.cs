using System.Collections.Generic;
using SpmsApp.Models;

namespace SpmsApp.ViewModels
{
    public class StudentPLOComparisonByCourseViewModel
    {
        public List<Student> Students { get; set; }
        public List<Course> Courses {get; set;}
        public TopbarViewModel TopbarViewModel { get; set; }
    }
}