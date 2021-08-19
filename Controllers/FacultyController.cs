using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
            viewModel.PloScores = null;

            return View(viewModel);
        }

        [HttpPost("/faculty/cppo")]
        public IActionResult CoursePloPerformanceOwn(int selectedCourse)
        {
            // example
            // TODO: to be implemented
            var ploScores = new Dictionary<ProgramlearningOutcome, float>();
            ploScores.Add(new ProgramlearningOutcome()
            {
                PloID = 0,
                PloName = "PLO-01"
            }, 58);
            ploScores.Add(new ProgramlearningOutcome()
            {
                PloID = 1,
                PloName = "PLO-02"
            }, 67);
            ploScores.Add(new ProgramlearningOutcome()
            {
                PloID = 2,
                PloName = "PLO-03"
            }, 51);

            viewModel.PloScores = ploScores;

            return View(viewModel);
        }
    }
}