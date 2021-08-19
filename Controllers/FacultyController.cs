using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SpmsApp.Models;
using SpmsApp.ViewModels;

namespace SpmsApp.Controllers
{
    public class FacultyController : Controller
    {
        public static Faculty activeFaculty;

        [HttpGet("/faculty/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/faculty/mcp")]
        public IActionResult MapCoPlo()
        {
            List<Course> courses = new List<Course>()
            {
                new Course()
                {
                    CourseID = 0,
                    CourseName = "Abc"
                },
                new Course()
                {
                    CourseID = 1,
                    CourseName = "Def"
                }
            };

            return View(new MapCoPloViewModel(){
                Courses = courses
            });
        }

        [HttpPost("/faculty/mcp")]
        public IActionResult MapCoPlo(int selectedCourse)
        {
            return Content(selectedCourse.ToString());
        }

        [HttpGet("/faculty/cppo")]
        public IActionResult CoursePloPerformanceOwn()
        {
            return View();
        }
    }
}