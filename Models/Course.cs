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
        public int ProgramID { get; set; }
        public int? CoofferedCourseID { get; set; }
        public List<int> PrerequisiteCourses { get; set; }

        public List<CourseOutcome> CourseOutcomes
        {
            get
            {
                return CourseOutcome.GetCourseOutcomesWithCourseID(this.CourseID);
                // return coList;
            }
        }

        public static List<Course> GetCoursesByDepartment(int departmentID)
        {
            return DataServices.GetCoursesByDepartment(departmentID);
        }
    }
}