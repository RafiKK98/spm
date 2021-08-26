using System.Collections.Generic;
using SpmsApp.Models;

namespace SpmsApp.ViewModels
{
    public class AddNewAssessmentViewModel
    {
        public TopbarViewModel TopbarViewModel { get; set; }
        public Course SelectedCourse { get; set; }
        public List<Section> Sections { get; set; }
        public List<CourseOutcome> CourseOutcomes { get; set; }
        public Assessment NewAssessment { get; set; }
    }
}