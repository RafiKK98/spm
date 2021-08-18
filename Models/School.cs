namespace SpmsApp.Models
{
    public class School
    {
        public int SchoolID { get; set; }
        public string SchoolName { get; set; }
        public University University { get; set; }
    }
}