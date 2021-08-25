using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using SpmsApp.Services;

namespace SpmsApp.Models
{
    [Serializable]
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CourseCreditCount { get; set; }
        public Program Program { get; set; }
        public Course CoofferedCourse { get; set; }
        public List<Course> PrerequisiteCourses { get; set; }
    }
}