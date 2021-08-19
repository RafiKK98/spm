using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpmsApp.Models;
using SpmsApp.ViewModels;

namespace SpmsApp.Controllers
{
    public class FacultyController : Controller
    {
        public static Faculty activeFaculty;
        static CoursePloPerformanceOwnViewModel viewModel = new CoursePloPerformanceOwnViewModel();

        [HttpGet("/faculty/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/faculty/mcp")]
        public IActionResult MapCoPlo()
        {
            // example
            // TODO: to be implemented
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

            return View(new MapCoPloViewModel()
            {
                Courses = courses
            });
        }

        [HttpPost("/faculty/mcp")]
        public IActionResult MapCoPlo(int selectedCourse)
        {
            // example
            // TODO: to be implemented

            return Content(selectedCourse.ToString());
        }

        [HttpGet("/faculty/cppo")]
        public IActionResult CoursePloPerformanceOwn()
        {
            viewModel.Courses = new List<Course>()
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

            return View(viewModel);
        }

        [HttpGet("/faculty/cppo/{courseID}")]
        public IActionResult CoursePloPerformanceOwn(int courseID)
        {
            // example
            // TODO: to be implemented
            // viewModel.Labels = JsonConvert.SerializeObject();
            // viewModel.Data = JsonConvert.SerializeObject();

            return Json(new {labels = new List<string>(){"PLO-01", "PLO-02", "PLO-03"}, data = new List<float>(){69, 57, 87}});
            // return Content(JsonConvert.SerializeObject(viewModel.Data));
        }
    }
}