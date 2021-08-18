namespace SpmsApp.Models
{
    public abstract class User
    {
        public int ID { get; set; } // identifier used for the database
        public string fname { get; set; }
        public string lname { get; set; }
        public string contactNumber { get; set; }
        public string emailAddress { get; set; }
        public string address { get; set; }
    }

    public class Faculty : User
    {
        public int DepartmentID { get; set; }
    }
}