using System;
using System.Collections.Generic;
using SpmsApp.Services;

namespace SpmsApp.Models
{
    [Serializable]
    public class CourseOutcome
    {
        public int CoID { get; set; }
        public string CoName { get; set; }
        public Course Course { get; set; }
        public string Details { get; set; }
        public ProgramlearningOutcome PLO { get; set; }
    }
}