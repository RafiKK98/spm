using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpmsApp.ViewModels;
using SpmsApp.Models;
using SpmsApp.Services;

namespace SpmsApp.Controllers
{
    public class VCController : Controller
    {
        public static DataServices ds = DataServices.dataServices;

        static CoursePloPerformanceOwnViewModel viewModel = new CoursePloPerformanceOwnViewModel();
        static StudentwisePloComparisonCourseViewModel viewStudentModel = new StudentwisePloComparisonCourseViewModel();

        public static VC ActiveVC = ds.vcs.First();
        // public static VC ActiveVC = new VC()
        // {
        //     ID = 11000,
        //     FirstName = "Tanweer",
        //     LastName = "Hasan",
        //     ContactNumber = "01923456789",
        //     EmailAddress = "tanweer@iub.edu.bd",
        //     Address = "Bashundhara R/A",
        //     VCID = 234,
        //     University = ds.universities.First()
        // };

        [HttpGet("/vc/")]
        public IActionResult Index()
        {
            return View(new TopbarViewModel() {Name = ActiveVC.FullName, ID = ActiveVC.VCID});
        }

        [HttpGet("/vc/spcc/")]
        public IActionResult StudentPLOComparisonByCourse()
        {
            viewStudentModel.Courses = new List<Course>()
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

            viewStudentModel.Students = new List<Student>()
            {
                new Student()
                {
                    StudentID = 1830411,
                    FirstName = "Rafi",
                    LastName = "Khan"
                }
            };
            return View(new TopbarViewModel() {Name = ActiveVC.FullName, ID = ActiveVC.VCID});
        }

        [HttpGet("/vc/spcc/{courseID}/{studentID}")]
        public IActionResult StudentPLOComparisonByCourse(int courseID, int studentID)
        {
            return Json(new {labels = new List<string>(){"PLO-01", "PLO-02", "PLO-03"}, data = new List<float>(){99, 93, 97}});
        }

        [HttpGet("/vc/spcp/")]
        public IActionResult StudentPLOComparisonByProgram()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
        [HttpGet("/vc/spat/")]
        public IActionResult StudentPLOAchievementTable()
        {
            PloAchievementTableViewModel ploAchievementTableViewModel = new PloAchievementTableViewModel();
            ploAchievementTableViewModel.TopbarViewModel = new TopbarViewModel()
            {
                Name = ActiveVC.FullName,
                ID = ActiveVC.VCID
            };

            return View(ploAchievementTableViewModel);
        }

        [HttpGet("/vc/spat/{studentID}")]
        public IActionResult StudentPloAchievementTable(int studentID)
        {
            var student = ds.students.Find(s => s.StudentID == studentID);

            if (student == null) return Json(null);

            var _data = ds.PloAchievementTableData(student);

            if (_data.Count <= 0) return Json(null);

            var plos = ds.plos.Where(o => o.Program == ds.students.First().Program);

            if (plos.Count() <= 0) return Json(null);

            var mydata = new { studentName = student.FullName, ploList = plos, data = _data };

            return Json(mydata);
        }
        [HttpGet("/vc/cppf/")]
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

            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }

        [HttpGet("/vc/pcp/")]
        public IActionResult PLOwiseCoursePerformance()
        {
            return View(new TopbarViewModel() {Name = ActiveVC.FullName, ID = ActiveVC.VCID});
        }

        [HttpGet("/vc/afpp/")]
        public IActionResult AchievedVsFailedPLOPerformance()
        {
            return View(new TopbarViewModel() {Name = ActiveVC.FullName, ID = ActiveVC.VCID});
        }

        [HttpGet("/vc/aac/")]
        public IActionResult AttemptedVsAchievedComparison()
        {
            return View(new TopbarViewModel() {Name = ActiveVC.FullName, ID = ActiveVC.VCID});
        }

        [HttpGet("/vc/pafap/")]
        public IActionResult PLOAchievementForAProgram()
        {
            return View(new TopbarViewModel() {Name = ActiveVC.FullName, ID = ActiveVC.VCID});
        }

        [HttpGet("/vc/sgaap/")]
        public IActionResult StudentsGraduatesAchievingAllPLOS()
        {
            return View(new TopbarViewModel() {Name = ActiveVC.FullName, ID = ActiveVC.VCID});
        }

        [HttpGet("/vc/apa/")]
        public IActionResult AveragePLOAchievement()
        {
            return View(new TopbarViewModel() {Name = ActiveVC.FullName, ID = ActiveVC.VCID});
        }

        [HttpGet("/vc/ippc/")]
        public IActionResult InstructorwisePLOPerformanceComparison()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
    }
}
