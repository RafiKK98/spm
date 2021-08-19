using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using SpmsApp.Services;

namespace SpmsApp.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CourseCreditCount { get; set; }
        public Program Program { get; set; }
        public int CoofferedCourseID { get; set; }

        public List<CourseOutcome> CourseOutcomes
        {
            get
            {
                var coList = CourseOutcome.GetCourseOutcomesWithCourseID(this.CourseID);
                coList.ForEach(co => co.Course = this);
                return coList;
            }
        }
    }
}