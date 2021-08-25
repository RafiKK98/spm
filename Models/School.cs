using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class School
    {
        public int SchoolID { get; set; }
        public string SchoolName { get; set; }
        public University University { get; set; }
    }
}