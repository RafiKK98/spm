using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class Evaluation
    {
        public Student Student { get; set; }
        public Assessment Assessment { get; set; }
        public float TotalObtainedMark { get; set; }
    }
}