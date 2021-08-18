using System;

namespace SpmsApp.Models
{
    public class ProgramlearningOutcome
    {
        public int PloID { get; set; }
        public string CoNum { get; set; }
        public string Details { get; set; }
        public Program Program { get; set; }

        internal static ProgramlearningOutcome GetPloByID(int v)
        {
            throw new NotImplementedException();
        }
    }
}