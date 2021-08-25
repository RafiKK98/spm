using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class Section
    {
        public int SectionID { get; set; }
        public int SectionNumber { get; set; }
        public string Semester { get; set; }
        public int Year { get; set; }
        public int MaximumCapacity { get; set; }
        public float PassMark { get; set; }
        public Faculty Faculty { get; set; }
        public Course Course { get; set; }
    }
}