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
        MapCoPloViewModel mapCoPloViewModel = new MapCoPloViewModel();
        CoursePloPerformanceOwnViewModel coursePloPerformanceOwnViewModel = new CoursePloPerformanceOwnViewModel();

        [HttpGet("/faculty/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/faculty/mcp")]
        public IActionResult MapCoPlo()
        {
            // mapCoPloViewModel.Courses = Course.GetCoursesByDepartment(activeFaculty.DepartmentID);
            // mapCoPloViewModel.TopbarViewModel = new TopbarViewModel()
            // {
            //     Name = activeFaculty.FullName,
            //     ID = activeFaculty.FacultyID
            // };

            var mcpvm = new MapCoPloViewModel()
            {
                Courses = Course.GetCoursesByDepartment(1)
            };

            return View(mcpvm);
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
            coursePloPerformanceOwnViewModel.Courses = new List<Course>()
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

            return View(coursePloPerformanceOwnViewModel);
        }

        [HttpGet("/faculty/cppo/{courseID}")]
        public IActionResult CoursePloPerformanceOwn(int courseID)
        {
            // example
            // TODO: to be implemented
            // viewModel.Labels = JsonConvert.SerializeObject();
            // viewModel.Data = JsonConvert.SerializeObject();

            return Json(new { labels = new List<string>() { "PLO-01", "PLO-02", "PLO-03" }, data = new List<float>() { 69, 57, 87 } });
            // return Content(JsonConvert.SerializeObject(viewModel.Data));
        }
    }
}