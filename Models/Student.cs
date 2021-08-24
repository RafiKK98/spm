using System;

namespace SpmsApp.Models
{
    public class Student : User
    {
        public int StudentID { get; set; }

        public DateTime DateofBirth { get; set; }

        public int ProgramID { get; set; }

    }
}