namespace SpmsApp.Models
{
    public class Program
    {
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        public int TotalCreditCount { get; set; }
        public Department Department { get; set; }
    }
}