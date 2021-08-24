using System.Collections.Generic;
using SpmsApp.Services;

namespace SpmsApp.Models
{
    public class Program
    {
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        public int TotalCreditCount { get; set; }
        public int DepartmentID { get; set; }

        public List<ProgramlearningOutcome> ProgramLearningOutcomes
        {
            get
            {
                var ploList = ProgramlearningOutcome.GetPlosByProgramID(this.ProgramID);
                ploList.ForEach(plo => plo.ProgramID = this.ProgramID);
                return ploList;
            }
        }

        public static Program GetProgram(int programID)
        {
            return DataServices.GetProgram(programID);
        }
    }
}