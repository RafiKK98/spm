using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class University
    {
        public int UniversityID { get; set; }
        public string UniversityName { get; set; }
        public string UniversityDomain { get; set; }
    }
}