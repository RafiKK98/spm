using System.Collections.Generic;

namespace SpmsApp.Models
{
    public class Program
    {
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        public int TotalCreditCount { get; set; }
        public Department Department { get; set; }

        public List<ProgramlearningOutcome> ProgramLearningOutcomes
        {
            get
            {
                var ploList = ProgramlearningOutcome.GetPlosByProgramID(this.ProgramID);
                ploList.ForEach(plo => plo.Program = this);
                return ploList;
            }
        }
    }
}