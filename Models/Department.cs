namespace SpmsApp.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public School School { get; set; }
    }
}