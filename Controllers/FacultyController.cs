using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
// using Newtonsoft.Json;
using SpmsApp.Models;
using SpmsApp.Services;
using SpmsApp.ViewModels;

namespace SpmsApp.Controllers
{
    public class FacultyController : Controller
    {
        static DataServices ds = DataServices.dataServices;
        public static Faculty activeFaculty = ds.faculties.First();
        // MapCoPloViewModel mapCoPloViewModel = new MapCoPloViewModel();
        // CoursePloPerformanceOwnViewModel coursePloPerformanceOwnViewModel = new CoursePloPerformanceOwnViewModel();

        [HttpGet("/faculty/")]
        public IActionResult Index()
        {
            TopbarViewModel tbvm = new TopbarViewModel()
            {
                Name = activeFaculty.FullName,
                ID = activeFaculty.FacultyID
            };

            return View(tbvm);
        }

        [HttpGet("/faculty/pat")]
        public IActionResult PloAchievementTable()
        {
            PloAchievementTableViewModel ploAchievementTableViewModel = new PloAchievementTableViewModel();
            ploAchievementTableViewModel.TopbarViewModel = new TopbarViewModel()
            {
                Name = activeFaculty.FullName,
                ID = activeFaculty.FacultyID
            };

            return View(ploAchievementTableViewModel);
        }

        [HttpGet("/faculty/pat/{studentID}")]
        public IActionResult PloAchievementTable(int studentID)
        {
            var student = ds.students.Find(s => s.StudentID == studentID);

            var _data = ds.PloAchievementTableData(student);

            var plos = ds.plos.Where(o => o.Program == ds.students.First().Program);

            var mydata = new {studentName = student.FullName, ploList = plos, data = _data };

            return Json(mydata);
        }

        // [HttpGet("/faculty/mcp")]
        // public IActionResult MapCoPlo()
        // {
        //     // mapCoPloViewModel.Courses = Course.GetCoursesByDepartment(activeFaculty.DepartmentID);
        //     // mapCoPloViewModel.TopbarViewModel = new TopbarViewModel()
        //     // {
        //     //     Name = activeFaculty.FullName,
        //     //     ID = activeFaculty.FacultyID
        //     // };

        //     // var mcpvm = new MapCoPloViewModel()
        //     // {
        //     //     Courses = Course.GetCoursesByDepartment(activeFaculty.DepartmentID)
        //     // };
        //     // mcpvm.TopbarViewModel = new TopbarViewModel()
        //     // {
        //     //     Name = activeFaculty.FullName,
        //     //     ID = activeFaculty.FacultyID
        //     // };

        //     // return View(mcpvm);

        //     return Content("To be implemented...");
        // }

        // [HttpPost("/faculty/mcp")]
        // public IActionResult MapCoPlo(int selectedCourse)
        // {
        //     // var mcpvm = new MapCoPloViewModel()
        //     // {
        //     //     Courses = Course.GetCoursesByDepartment(activeFaculty.DepartmentID)
        //     // };
        //     // mcpvm.SelectedCourse = Course.GetCourse(selectedCourse);
        //     // mcpvm.TopbarViewModel = new TopbarViewModel()
        //     // {
        //     //     Name = activeFaculty.FullName,
        //     //     ID = activeFaculty.FacultyID
        //     // };

        //     // return View(mcpvm);

        //     return Content("To be implemented...");
        // }

        // [HttpGet("/faculty/mcp/edit/{coID}")]
        // public IActionResult EditCoPloMapping(int coID)
        // {
        //     // EditCoPloMapping ecpm = new EditCoPloMapping();
        //     // ecpm.CO = CourseOutcome.GetCourseOutcome(coID);
        //     // ecpm.PLO = ProgramlearningOutcome.GetPLO(ecpm.CO.PloID);
        //     // ecpm.TopbarViewModel = new TopbarViewModel()
        //     // {
        //     //     Name = activeFaculty.FullName,
        //     //     ID = activeFaculty.FacultyID
        //     // };

        //     // return View(ecpm);

        //     return Content("To be implemented...");
        // }

        // [HttpPost("/faculty/mcp/edit/{coId}")]
        // public IActionResult EditCoPloMapping(int coId, EditCoPloMapping mapping)
        // {
        //     // CourseOutcome.ModifyMapping(coId, mapping.PloID, mapping.Details);
        //     // return Redirect("/faculty/mcp");

        //     return Content("To be implemented...");
        // }

        // [HttpPost("/faculty/mcp/delete/{coId}")]
        // public IActionResult DeleteCoPloMapping(int coId)
        // {
        //     // CourseOutcome.DeleteCoPloMapping(coId);
        //     // // CourseOutcome.ModifyMapping(coId, mapping.PloID, mapping.Details);
        //     // return Redirect("/faculty/mcp");

        //     return Content("To be implemented...");
        // }

        // [HttpGet("/faculty/cppo")]
        // public IActionResult CoursePloPerformanceOwn()
        // {
        //     CoursePloPerformanceOwnViewModel viewModel = new CoursePloPerformanceOwnViewModel();

        //     viewModel.Courses = activeFaculty.CoursesTaught;

        //     viewModel.TopbarViewModel = new TopbarViewModel();
        //     viewModel.TopbarViewModel.Name = activeFaculty.FullName;
        //     viewModel.TopbarViewModel.ID = activeFaculty.FacultyID;

        //     return View(viewModel);
        // }

        // [HttpGet("/faculty/cppo/{courseID}")]
        // public IActionResult CoursePloPerformanceOwn(int courseID)
        // {
        //     // example
        //     // TODO: to be implemented
        //     // viewModel.Labels = JsonConvert.SerializeObject();
        //     // viewModel.Data = JsonConvert.SerializeObject();

        //     return Json(new { labels = new List<string>() { "PLO-01", "PLO-02", "PLO-03" }, data = new List<float>() { 69, 57, 87 } });
        //     // return Content(JsonConvert.SerializeObject(viewModel.Data));
        // }

        // [HttpGet("/faculty/pat")]
        // public IActionResult PloAchievementTable()
        // {
        //     PloAchievementTableViewModel ploAchievementTableViewModel = new PloAchievementTableViewModel();
        //     ploAchievementTableViewModel.TopbarViewModel = new TopbarViewModel()
        //     {
        //         Name = activeFaculty.FullName,
        //         ID = activeFaculty.FacultyID
        //     };

        //     return View(ploAchievementTableViewModel);
        // }

        // [HttpPost("/faculty/pat/")]
        // public IActionResult PloAchievementTable(PloAchievementTableViewModel input)
        // {
        //     PloAchievementTableViewModel ploAchievementTableViewModel = new PloAchievementTableViewModel();
        //     ploAchievementTableViewModel.TopbarViewModel = new TopbarViewModel()
        //     {
        //         Name = activeFaculty.FullName,
        //         ID = activeFaculty.FacultyID
        //     };

        //     DataServices.GetStudentPloHistory(input.StudentID, activeFaculty.DepartmentID);

        //     return View(ploAchievementTableViewModel);
        // }
    }
}