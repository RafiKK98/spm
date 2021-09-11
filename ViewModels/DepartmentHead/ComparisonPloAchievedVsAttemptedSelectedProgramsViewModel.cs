using System.Collections.Generic;
using SpmsApp.Models;

namespace SpmsApp.ViewModels.DepartmentHead
{
    public class ComparisonPloAchievedVsAttemptedSelectedProgramsViewModel
    {
        public TopbarViewModel TopbarViewModel { get; set; }
        public List<Program> Programs { get; set; }

        public List<int> SelectedPrograms { get; set; }
        public List<Semester> SelectedSemesters { get; set; }
    }
}