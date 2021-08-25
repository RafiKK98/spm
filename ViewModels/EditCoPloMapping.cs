using SpmsApp.Models;

namespace SpmsApp.ViewModels
{
    public class EditCoPloMapping
    {
        public TopbarViewModel TopbarViewModel { get; set; }
        public CourseOutcome CO { get; set; }
        public ProgramlearningOutcome PLO { get; set; }
        public int PloID { get; set; }
        public string Details { get; set; }
    }
}