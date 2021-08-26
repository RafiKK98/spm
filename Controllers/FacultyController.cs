using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
// using Newtonsoft.Json;
using SpmsApp.Models;
using SpmsApp.Services;
using SpmsApp.ViewModels;
using System.Collections;

namespace SpmsApp.Controllers
{
    public class FacultyController : Controller
    {
        static DataServices ds = DataServices.dataServices;
        public static Faculty activeFaculty = ds.faculties.First();

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

        struct PatData
        {
            public string studentName;
            public ArrayList data;
            public IEnumerable<ProgramlearningOutcome> ploList;
        }

        [HttpGet("/faculty/pat/{studentID}")]
        public IActionResult PloAchievementTable(int studentID)
        {
            var student = ds.students.Find(s => s.StudentID == studentID);

            if (student == null) return Json(null);

            // var mydata = new { studentName = student.FullName, ploList = plos, data = _data };

            var _data = ds.PloAchievementTableData(student);

            if (_data.Count <= 0) return Json(null);

            var plos = ds.plos.Where(o => o.Program == ds.students.First().Program);

            if (plos.Count() <= 0) return Json(null);

            var mydata = new { studentName = student.FullName, ploList = plos, data = _data };

            return Json(mydata);
        }

        [HttpGet("/faculty/spcc")]
        public IActionResult StudentPloComparisonCourse()
        {
            StudentPloComparisonCourseViewModel viewModel = new StudentPloComparisonCourseViewModel();
            viewModel.Courses = ds.courses.Where(c => c.Program.Department == activeFaculty.Department && c.CoofferedCourse == null).ToList();
            viewModel.TopbarViewModel = new TopbarViewModel()
            {
                Name = activeFaculty.FullName,
                ID = activeFaculty.FacultyID
            };

            return View(viewModel);
        }

        [HttpGet("/faculty/spcc/{startSemester}/{startYear}/{endSemester}/{endYear}/{selectedCourse}")]
        public IActionResult StudentPloComparisonCourse(int startSemester, int startYear, int endSemester, int endYear, int selectedCourse)
        {
            Semester start = new Semester(startSemester, startYear);
            Semester end = new Semester(endSemester, endYear);

            StudentPloComparisonCourseViewModel viewModel = new StudentPloComparisonCourseViewModel();
            viewModel.Courses = ds.courses.Where(c => c.Program.Department == activeFaculty.Department && c.CoofferedCourse == null).ToList();
            viewModel.TopbarViewModel = new TopbarViewModel()
            {
                Name = activeFaculty.FullName,
                ID = activeFaculty.FacultyID
            };

            var courses = ds.courses.Where(c => (c.CourseID == selectedCourse) || (c.CoofferedCourse != null ? c.CoofferedCourse.CourseID == selectedCourse : false)).ToList();
            var sections = ds.sections.Where(s =>
            {
                return courses.Contains(s.Course) && s.Semester.CompareTo(start) >= 0 && s.Semester.CompareTo(end) <= 0;
            }).ToList();

            var sectionEvaluations = ds.evaluations.Where(e => sections.Contains(e.Assessment.Section)).ToList();

            var groupedSectionEvaluation = sectionEvaluations.GroupBy(se => se.Assessment.CourseOutcome.PLO.PloName).ToList();

            List<string> plos = new List<string>();
            List<int> counts = new List<int>();

            foreach (var group in groupedSectionEvaluation)
            {
                int count = 0;

                foreach (var ev in group)
                {
                    var convertedMark = (ev.TotalObtainedMark / ev.Assessment.TotalMark) * 100;
                    if (convertedMark >= ev.Assessment.Section.PassMark)
                    {
                        count++;
                    }
                }

                plos.Add(group.Key);
                counts.Add(count);

                System.Console.WriteLine(group);
            }

            return Json(new { Plos = plos, Counts = counts });
        }

        [HttpGet("/faculty/iad/")]
        public IActionResult InputAssessmentDetails()
        {
            InputAssessmentDetailsViewModel viewModel = new InputAssessmentDetailsViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = activeFaculty.FullName,
                    ID = activeFaculty.FacultyID
                },
                Courses = ds.courses.Where(c => c.Program.Department == activeFaculty.Department && c.CoofferedCourse == null).ToList()
            };

            return View(viewModel);
        }

        [HttpPost("/faculty/iad/")]
        public IActionResult InputAssessmentDetails(int selectedCourse)
        {
            InputAssessmentDetailsViewModel viewModel = new InputAssessmentDetailsViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = activeFaculty.FullName,
                    ID = activeFaculty.FacultyID
                },
                Courses = ds.courses.Where(c => c.Program.Department == activeFaculty.Department && c.CoofferedCourse == null).ToList()
            };

            viewModel.SelectedCourse = ds.courses.Find(c => c.CourseID == selectedCourse);

            var sections = ds.sections.Where(s => s.Course == viewModel.SelectedCourse && s.Semester.CompareTo(activeFaculty.Department.School.University.CurrentSemester) == 0);

            viewModel.Assessments = ds.assessments.Where(a => sections.Contains(a.Section)).ToList();

            return View(viewModel);
        }

        public IActionResult AddNewAssessment(int courseID)
        {
            AddNewAssessmentViewModel viewModel = new AddNewAssessmentViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = activeFaculty.FullName,
                    ID = activeFaculty.FacultyID
                },
                SelectedCourse = ds.courses.Find(c => c.CourseID == courseID),
                Sections = ds.sections.Where(s => s.Semester == activeFaculty.Department.School.University.CurrentSemester && s.Course.CourseID == courseID).ToList(),
                CourseOutcomes = ds.cos.Where(co => co.Course.CourseID == courseID).ToList()
            };

            return View(viewModel);
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