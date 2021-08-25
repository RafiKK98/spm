using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class Student : User
    {
        public int StudentID { get; set; }

        public DateTime DateofBirth { get; set; }

        public Program Program { get; set; }

    }
}