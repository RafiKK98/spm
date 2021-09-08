using System.Collections.Generic;
using SpmsApp.Models;

namespace SpmsApp.ViewModels.VC
{
    public class InstructorwisePloPerformanceComparisonViewModel
    {
        public TopbarViewModel TopbarViewModel { get; set; }
        public List<Faculty> Faculties { get; set; }

        public int FacultyID { get; set; }
        public int StartSemester { get; set; }
        public int StartYear { get; set; }
        public int EndSemester { get; set; }
        public int EndYear { get; set; }
    }
}