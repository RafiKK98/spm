using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class VC : User
    {
        public int VCID { get; set; }
        public University University { get; set; }
    }
}