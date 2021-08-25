using System;
using System.Diagnostics.CodeAnalysis;

namespace SpmsApp.Models
{
    public enum SemesterName
    {
        SPRING, SUMMER, AUTUMN
    }

    public class Semester : IComparable<Semester>
    {
        public SemesterName SemesterName { get; set; }
        public int Year { get; set; }

        public Semester(string semesterName, int year)
        {
            switch (semesterName.ToLower())
            {
                case "spring":
                    this.SemesterName = SemesterName.SPRING;
                    break;
                case "summer":
                    this.SemesterName = SemesterName.SUMMER;
                    break;
                case "autumn":
                    this.SemesterName = SemesterName.AUTUMN;
                    break;
            }

            this.Year = year;
        }

        public int CompareTo([AllowNull] Semester other)
        {
            if (other == null) return 1;
            
            if (this.Year > other.Year)
            {
                return 1;
            }
            else if (this.Year < other.Year)
            {
                return -1;
            }
            else
            {
                return this.SemesterName.CompareTo(other.SemesterName);
            }
        }

        public override string ToString()
        {
            return $"{this.SemesterName} - {this.Year}";
        }
    }
}