using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class SchoolDean : User
    {

        public int DeanID { get; set; }
        public School School { get; set; }
    }
}