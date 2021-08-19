using System.Collections.Generic;
using SpmsApp.Models;

namespace SpmsApp.ViewModels
{
    public class CoursePloPerformanceOwnViewModel
    {
        public List<Course> Courses { get; set; }
        public Dictionary<ProgramlearningOutcome, float> PloScores { get; set; }
    }
}