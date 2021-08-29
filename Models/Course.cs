using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MySql.Data.MySqlClient;
using SpmsApp.Services;

namespace SpmsApp.Models
{
    [Serializable]
    public class Course : IEquatable<Course>, IEqualityComparer<Course>
    {
        public int CourseID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CourseCreditCount { get; set; }
        public Program Program { get; set; }
        public Course CoofferedCourse { get; set; }
        public List<Course> PrerequisiteCourses { get; set; }

        public bool Equals([AllowNull] Course other)
        {
            if (other == null) return false;

            return other.CourseID == CourseID;
        }

        public bool Equals([AllowNull] Course x, [AllowNull] Course y)
        {
            return x.Equals(y);
        }

        public int GetHashCode([DisallowNull] Course obj)
        {
            return obj.CourseID.GetHashCode();
        }
    }
}