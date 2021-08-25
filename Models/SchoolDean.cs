using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class SchoolDean : User
    {
        public School School { get; set; }
    }
}