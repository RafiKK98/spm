using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public School School { get; set; }

        public override int GetHashCode()
        {
            return DepartmentID.GetHashCode() | DepartmentName.GetHashCode() | School.GetHashCode();
        }
    }
}