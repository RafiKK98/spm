using System;

namespace SpmsApp.Models
{
    public abstract class User
    {
        public int ID { get; set; } // identifier used for the database
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }

        public string FullName
        {
            get => $"{FirstName} {LastName}";
        }
    }
}