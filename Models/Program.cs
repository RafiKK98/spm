using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using SpmsApp.Services;

namespace SpmsApp.Models
{
    [Serializable]
    public class Program : IEquatable<Program>, IEqualityComparer<Program>
    {
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        public string ProgramCode { get; set; }
        public int TotalCreditCount { get; set; }
        public Department Department { get; set; }

        public bool Equals([AllowNull] Program other)
        {
            return ProgramID == other.ProgramID;
        }

        public bool Equals([AllowNull] Program x, [AllowNull] Program y)
        {
            return x.Equals(y);
        }

        public int GetHashCode([DisallowNull] Program obj)
        {
            return ProgramID.GetHashCode() | ProgramName.GetHashCode() | ProgramCode.GetHashCode() | TotalCreditCount.GetHashCode() | Department.GetHashCode();
        }

        // public List<ProgramlearningOutcome> ProgramLearningOutcomes
        // {
        //     get
        //     {
        //         var ploList = ProgramlearningOutcome.GetPlosByProgramID(this.ProgramID);
        //         ploList.ForEach(plo => plo.ProgramID = this.ProgramID);
        //         return ploList;
        //     }
        // }

        // public static Program GetProgram(int programID)
        // {
        //     return DataServices.GetProgram(programID);
        // }
    }
}