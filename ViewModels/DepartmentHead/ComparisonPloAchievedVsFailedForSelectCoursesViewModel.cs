using System.Collections.Generic;
using SpmsApp.Models;

namespace SpmsApp.ViewModels.DepartmentHead
{
    public class ComparisonPloAchievedVsFailedForSelectCoursesViewModel
    {
        public TopbarViewModel TopbarViewModel { get; set; }
        public List<Course> Courses { get; set; }
        public Semester Semester { get; set; }

        public List<int> SelectedCourses { get; set; }
        public List<Semester> SelectedSemesters { get; set; }
    }
}