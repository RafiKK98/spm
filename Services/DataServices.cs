using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SpmsApp.Models;

namespace SpmsApp.Services
{
    public static class DataServices
    {
        // public static DataServices Get { get; private set; }
        // private static DataServices _ds = new DataServices();

        private static MySqlConnection connection = new MySqlConnection("server=localhost;database=spmsdb;userid=spms;password=");
        private static MySqlCommand command;

        public static List<CourseOutcome> GetCoListForCourse(int courseID)
        {
            List<CourseOutcome> coList = new List<CourseOutcome>();

            connection.Open();
            command = new MySqlCommand($"select * from CO_T where CourseID={courseID};", connection);
            var result = command.ExecuteReader();

            while (result.Read())
            {
                CourseOutcome co = new CourseOutcome()
                {
                    CoID = result.GetInt32("CoID"),
                    CoName = result.GetString("CoName"),
                    Details = result.GetString("Details"),
                    PloID = result.GetInt32("PloID")
                };

                coList.Add(co);
            }

            result.Close();
            connection.Close();

            return coList;
        }

        internal static Program GetProgram(int programID)
        {
            Program program = null;
            connection.Open();
            command = new MySqlCommand($"Select * from Program_T where ProgramID={programID};", connection);
            var result = command.ExecuteReader();

            if (result.Read())
            {
                program = new Program()
                {
                    ProgramID = result.GetInt32("ProgramID"),
                    ProgramName = result.GetString("ProgramName"),
                    TotalCreditCount = result.GetInt32("TotalNumCredits"),
                    DepartmentID = result.GetInt32("DepartmentID")
                };
            }
            result.Close();
            connection.Close();

            return program;
        }

        internal static List<Course> GetCoursesByDepartment(int departmentID)
        {
            List<Course> courses = new List<Course>();
            connection.Open();
            command = new MySqlCommand(
                "Select CourseID, CourseCode, CourseName, NumOfCredits, C.ProgramID, CoOfferedCourseID " +
                "from Course_T as C inner join Program_T as P " +
                "on C.ProgramID=P.ProgramID " +
                $"where DepartmentID={departmentID};", connection);
            var result = command.ExecuteReader();

            while (result.Read())
            {
                var course = new Course()
                {
                    CourseID = result.GetInt32("CourseID"),
                    CourseCode = result.GetString("CourseCode"),
                    CourseName = result.GetString("CourseName"),
                    CourseCreditCount = result.GetInt32("NumOfCredits"),
                    ProgramID = result.GetInt32("ProgramID")
                };

                if (!result.IsDBNull(5))
                {
                    course.CoofferedCourseID = result.GetInt32("CoOfferedCourseID");
                }

                courses.Add(course);
            }
            result.Close();
            connection.Close();

            return courses;
        }

        public static List<ProgramlearningOutcome> GetPlosByProgram(int programID)
        {
            List<ProgramlearningOutcome> plos = new List<ProgramlearningOutcome>();
            connection.Open();
            command = new MySqlCommand($"Select * from PLO_T where ProgramID={programID};", connection);
            var result = command.ExecuteReader();

            while (result.Read())
            {
                plos.Add(new ProgramlearningOutcome()
                {
                    PloID = result.GetInt32("PloID"),
                    PloName = result.GetString("PloName"),
                    Details = result.GetString("Details"),
                    ProgramID = result.GetInt32("ProgramID")
                });
            }

            result.Close();
            connection.Close();

            return plos;
        }

        public static Faculty GetFaculty(string email, string password)
        {
            Faculty faculty = null;
            connection.Open();
            command = new MySqlCommand(
                "Select UserID,FName,LName,PhoneNumber,Email,Address,DepartmentID,FacultyID,HiringDate " +
                "from User_T as U inner join Faculty_T as F on U.UserID=F.FID " +
                $"where Email={email} and LoginPass={password};"
            );
            var result = command.ExecuteReader();
            if (result.Read())
            {
                faculty = new Faculty()
                {
                    ID = result.GetInt32("UserID"),
                    FirstName = result.GetString("FName"),
                    LastName = result.GetString("LName"),
                    ContactNumber = result.GetString("PhoneNumber"),
                    EmailAddress = result.GetString("Email"),
                    Address = result.GetString("Address"),
                    FacultyID = result.GetInt32("FacultyID"),
                    HiringDate = result.GetDateTime("HiringDate"),
                    DepartmentID = result.GetInt32("DepartmentID")
                };
            }
            result.Close();
            connection.Close();

            return faculty;
        }

        // private DataServices()
        // {
        //     connection = new MySqlConnection("server=localhost;database=spmsdb;userid=spms;password=");
        // }

        // public static DataServices Get { get => _ds; }

        // public MySqlDataReader RunQuery(string query)
        // {
        //     connection.Open();
        //     command = connection.CreateCommand();
        //     command.CommandText = query;
        //     var reader = command.ExecuteReader();
        //     connection.Close();
        //     return reader;
        // }

        // public int RunUpdate(string query)
        // {
        //     connection.Open();
        //     command = connection.CreateCommand();
        //     command.CommandText = query;
        //     var result = command.ExecuteNonQuery();
        //     connection.Close();
        //     return result;
        // }
    }
}