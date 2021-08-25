using System;

namespace SpmsApp.Models
{
    [Serializable]
    public class CourseRegistration
    {
        public Student Student { get; set; }
        public Section Section { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}