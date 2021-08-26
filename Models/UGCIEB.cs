using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class UGCIEB : User
    {
        public int UgciebID { get; set; }
        public University University{ get; set;}

    }
}