using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class DepartmentHead : User
    {
        public int DepartmentHeadID { get; set; }
        public Department Department { get; set; }
    }
}