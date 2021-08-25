using System;
using System.Collections.Generic;
using SpmsApp.Services;

namespace SpmsApp.Models
{
    [Serializable]
    public class Faculty : User
    {
        public int FacultyID { get; set; }
        public DateTime HiringDate { get; set; }
        public Department Department { get; set; }
    }
}