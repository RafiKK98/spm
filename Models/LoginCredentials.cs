using System;

namespace SpmsApp.Models
{
    public enum UserType
    {
        Admin,
        Student,
        Faculty,
        Head,
        Dean,
        VC,
        UGCIEB,
        Gaurdian
    }

    [Serializable]
    public class LoginCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserID { get; set; }
        public UserType UserType { get; set; }
    }
}