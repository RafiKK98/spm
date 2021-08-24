using System.Collections.Generic;
using SpmsApp.Models;

namespace SpmsApp.ViewModels
{
    public class MapCoPloViewModel
    {
        public List<Course> Courses { get; set; }
        public Course SelectedCourse { get; set; }
        public TopbarViewModel TopbarViewModel { get; set; }
    }
}