using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpmsApp.ViewModels;
using SpmsApp.Models;
using SpmsApp.Services;
using SpmsApp.ViewModels.Dean;

namespace SpmsApp.Controllers
{
    public class DeanController : Controller
    {
        public static DataServices ds = DataServices.dataServices;

        static CoursePloPerformanceOwnViewModel viewModel = new CoursePloPerformanceOwnViewModel();
        static StudentwisePloComparisonCourseViewModel viewStudentModel = new StudentwisePloComparisonCourseViewModel();

        public static SchoolDean ActiveDean = ds.schoolDeans.First();

        [HttpGet("/dean/")]
        public IActionResult Index()
        {
            return View(new TopbarViewModel(){Name = ActiveDean.FullName, ID = ActiveDean.DeanID});
        }

        [HttpGet("/dean/spcc/")]
        public IActionResult StudentPLOComparisonByCourse()
        {
            StudentPloComparisonByCourseViewModel studentPloComparisonByCourseViewModel = new StudentPloComparisonByCourseViewModel();
            //studentPloComparisonByCourseViewModel
            studentPloComparisonByCourseViewModel.TopbarViewModel = new TopbarViewModel()
            {
                Name = ActiveDean.FullName,
                ID = ActiveDean.DeanID
            };

            return View(studentPloComparisonByCourseViewModel);
        }

        [HttpGet("/dean/spcc/{courseID}/{studentID}")]
        public IActionResult StudentPLOComparisonByCourse(int courseID, int studentID)
        {
            return Json(new {labels = new List<string>(){"PLO-01", "PLO-02", "PLO-03"}, data = new List<float>(){99, 93, 97}});
        }

        [HttpGet("/dean/spcp/")]
        public IActionResult StudentPLOComparisonByProgram()
        {
            return View(new TopbarViewModel(){Name = "Mr Dean", ID = 1234});
        }
        [HttpGet("/dean/spat/")]
        public IActionResult StudentPLOAchievementTable()
        {
            return View(new TopbarViewModel(){Name = "Mr Dean", ID = 1234});
        }
        [HttpGet("/dean/cppf/")]
        public IActionResult CoursePLOPerformanceByFaculty()
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

            viewModel.TopbarViewModel = new TopbarViewModel(){Name = "Mr Dean", ID = 1234};

            return View(viewModel);
        }

        [HttpGet("/dean/cppf/{courseID}")]
        public IActionResult CoursePLOPerformanceByFaculty(int courseID)
        {
            return Json(new {labels = new List<string>(){"PLO-01", "PLO-02", "PLO-03"}, data = new List<float>(){99, 63, 97}});
        }

        [HttpGet("/dean/pcp/")]
        public IActionResult PLOwiseCoursePerformance()
        {
            return View(new TopbarViewModel(){Name = "Mr Dean", ID = 1234});
        }

        [HttpGet("/dean/afpp/")]
        public IActionResult AchievedVsFailedPLOPerformance()
        {
            return View(new TopbarViewModel(){Name = "Mr Dean", ID = 1234});
        }

        [HttpGet("/dean/aac/")]
        public IActionResult AttemptedVsAchievedComparison()
        {
            return View(new TopbarViewModel(){Name = "Mr Dean", ID = 1234});
        }

        [HttpGet("/dean/pafap/")]
        public IActionResult PLOAchievementForAProgram()
        {
            //
            return View(new TopbarViewModel(){Name = "Mr Dean", ID = 1234});
        }

        [HttpGet("/dean/sgaap/")]
        public IActionResult StudentsGraduatesAchievingAllPLOS()
        {
            return View(new TopbarViewModel(){Name = "Mr Dean", ID = 1234});
        }

        [HttpGet("/dean/apa/")]
        public IActionResult AveragePLOAchievement()
        {
            return View(new TopbarViewModel(){Name = "Mr Dean", ID = 1234});
        }

        [HttpGet("/dean/ippc/")]
        public IActionResult InstructorwisePLOPerformanceComparison()
        {
            return View(new TopbarViewModel(){Name = "Mr Dean", ID = 1234});
        }
    }
}
