using System.Collections.Generic;
using SpmsApp.Services;

namespace SpmsApp.Models
{
    public class CourseOutcome
    {
        public int CoID { get; set; }
        public string CoName { get; set; }
        public int CourseID { get; set; }
        public string Details { get; set; }
        public int PloID { get; set; }

        public static List<CourseOutcome> GetCourseOutcomesWithCourseID(int courseID)
        {
            // var ds = DataServices.Get;
            // var result = ds.RunQuery(
            //     "select * from CO_T" +
            //     $"where CourseID={courseID};"
            // );

            // List<CourseOutcome> coList = new List<CourseOutcome>();

            // while (result.Read())
            // {
            //     CourseOutcome co = new CourseOutcome()
            //     {
            //         CoID = result.GetInt32("CoID"),
            //         CoName = result.GetString("CoName"),
            //         Details = result.GetString("Details"),
            //         PLO = ProgramlearningOutcome.GetPloByID(result.GetInt32("PloID"))
            //     };

            //     coList.Add(co);
            // }

            return DataServices.GetCoListForCourse(courseID);
        }
    }
}