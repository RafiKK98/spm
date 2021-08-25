using System;
using System.Collections.Generic;
using SpmsApp.Services;

namespace SpmsApp.Models
{
    [Serializable]
    public class ProgramlearningOutcome
    {
        public int PloID { get; set; }
        public string PloName { get; set; }
        public string Details { get; set; }
        public Program Program { get; set; }
    }
}