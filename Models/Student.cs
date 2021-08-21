using System;

namespace SpmsApp.Models
{
    public class Student{
        public int SID { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string PhoneNum { get; set; }

        public string Email { get; set; }

        public string Address{ get; set; }

        public DateTime DateofBirth { get; set; }

        public Program ProgramID { get; set; }

    }
}