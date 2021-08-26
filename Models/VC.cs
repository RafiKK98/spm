using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class VC : User
    {
        public University University { get; set; }
    }
}