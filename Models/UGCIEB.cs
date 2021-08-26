using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class UGCIEB : User
    {
        public int UGCIEBID { get; set; }
        public University University{ get; set;}

    }
}