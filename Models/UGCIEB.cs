using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class UGCIEB : User
    {
        public int UGCIEB { get; set; }
        public University University{get; set;}

    }
}