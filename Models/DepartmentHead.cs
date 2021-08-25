using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class DepartmentHead : User
    {
        public Department Department { get; set; }
    }
}