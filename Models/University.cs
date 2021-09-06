using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class University
    {
        public int UniversityID { get; set; }
        public string UniversityName { get; set; }
        public string UniversityDomain { get; set; }
        public Semester CurrentSemester { get; set; }

        public override int GetHashCode()
        {
            return UniversityID.GetHashCode() | UniversityName.GetHashCode() | UniversityDomain.GetHashCode() | CurrentSemester.GetHashCode();
        }
    }
}