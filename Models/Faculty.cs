using System;
using SpmsApp.Services;

namespace SpmsApp.Models
{
    public class Faculty : User
    {
        public int FacultyID { get; set; }
        public DateTime HiringDate { get; set; }
        public int DepartmentID { get; set; }

        public static Faculty GetFaculty(string email, string password)
        {
            return DataServices.GetFaculty(email, password);
        }
    }
}