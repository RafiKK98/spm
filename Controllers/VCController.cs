using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpmsApp.ViewModels;
using SpmsApp.ViewModels.VC;
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

        [HttpGet("/vc/ispscc")]
        public IActionResult IndividualStudentPloScoreComparisonCourse() // 1
        {
            // return Content("HELLO WORLD");

            var viewModel = new IndividualStudentPLOScoreComparisonCourseViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = ActiveVC.FullName,
                    ID = ActiveVC.VCID
                },
                Courses = ds.courses.Where(c => c.Program.Department.School.University == ActiveVC.University && c.CoofferedCourse == null).ToList()
            };

            return View(viewModel);
        }

        [HttpGet("/vc/ispscc/{studentID}/{courseID}")]
        public IActionResult IndividualStudentPloScoreComparisonCourse(int studentID, int courseID) // 1 continued
        {
            var student = ds.students.Find(s => s.StudentID == studentID && s.Program.Department.School.University == ActiveVC.University);
            var course = ds.courses.Find(c => c.CourseID == courseID);

            if (student == null) return NotFound();

            var courseRegistrations = ds.courseRegistrations.Where(cr => cr.Student == student && cr.Section.Course == course).ToList();

            if (courseRegistrations.Count <= 0) return NotFound();

            var sections = courseRegistrations.Select((cr, idx) => cr.Section).ToList();

            var section = sections.First();

            for (int i = 1; i < sections.Count; i++)
            {
                if (section.Semester.CompareTo(sections[i].Semester) > 0)
                {
                    section = sections[i];
                }
            }

            var semester = section.Semester;

            var studentEvaluation = ds.evaluations.Where(e => e.Assessment.Section == section && e.Student == student);

            var studentEvalGroup = studentEvaluation.GroupBy(se => se.Assessment.CourseOutcome.PLO.PloName);

            List<string> studentPloList = new List<string>();
            List<float> studentScoreList = new List<float>();

            foreach (var evalGroup in studentEvalGroup)
            {
                studentPloList.Add(evalGroup.Key);

                float score = 0;

                foreach (var eval in evalGroup)
                {
                    score += eval.TotalObtainedMark;
                }

                studentScoreList.Add(score);
            }

            var courseEval = ds.evaluations.Where(ev => ev.Assessment.Section.Semester.Equals(semester) && ev.Assessment.Section.Course == course && ev.Assessment.CourseOutcome.PLO.Program == student.Program);

            var courseEvalGroupbyPlo = courseEval.GroupBy(ce => ce.Assessment.CourseOutcome.PLO.PloName);

            var coursePloList = new List<string>();
            var courseAvgScoreList = new List<float>();

            foreach (var evalGroup in courseEvalGroupbyPlo)
            {
                coursePloList.Add(evalGroup.Key);

                var evalGroupbySt = evalGroup.GroupBy(eg => eg.Student);
                var stCount = evalGroupbySt.Count();

                float score = 0;

                foreach (var eval in evalGroup)
                {
                    score += eval.TotalObtainedMark;
                }

                courseAvgScoreList.Add(score / stCount);
            }

            return Json(new { StData = studentScoreList, StLabel = studentPloList, CourseData = courseAvgScoreList });
        }

        [HttpGet("/vc/ispscp")]
        public IActionResult IndividualStudentPloScoreComparisonProgram() // 2
        {
            var viewModel = new IndividualStudentPLOScoreComparisonProgramViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = ActiveVC.FullName,
                    ID = ActiveVC.VCID
                },
                Programs = ds.programs.Where(p => p.Department.School.University == ActiveVC.University).ToList()
            };

            return View(viewModel);
        }

        [HttpGet("/vc/ispscp/{studentID}/{programID}")]
        public IActionResult IndividualStudentPloScoreComparisonProgram(int studentID, int programID) // 2 continued
        {
            var student = ds.students.Find(s => s.StudentID == studentID);

            if (student == null) return NotFound();

            var program = ds.programs.Find(p => p.ProgramID == programID);
            var programPlos = ds.plos.Where(plo => plo.Program == program);

            var evaluations = ds.evaluations.Where(ev => ev.Assessment.CourseOutcome.PLO.Program == program);

            List<float> programScores = new List<float>();
            List<float> studentScores = new List<float>();

            foreach (var plo in programPlos)
            {
                var ploScoreProgram = evaluations.Where(ev => ev.Assessment.CourseOutcome.PLO == plo);
                var stCount = ploScoreProgram.GroupBy(psp => psp.Student).Count();
                // var ploScoreStudent = ploScoreProgram.Where(ev => ev.Student == student);

                float programScore = 0;
                float studentScore = 0;

                foreach (var p in ploScoreProgram)
                {
                    programScore += p.TotalObtainedMark;

                    if (p.Student == student)
                    {
                        studentScore += p.TotalObtainedMark;
                    }
                }

                programScores.Add(programScore / stCount);
                studentScores.Add(studentScore);
            }

            var data = new { PloList = programPlos.Select(p => p.PloName), StudentScores = studentScores, ProgramScores = programScores };

            return Json(data);
        }

        [HttpGet("/vc/spcc/")]
        public IActionResult StudentPLOComparisonByCourse()
        {
            int UniversityID = 0;

            StudentPLOComparisonByCourseViewModel studentPLOComparisonByCourseViewModel = new StudentPLOComparisonByCourseViewModel();
            List<int> SchoolIDs = new List<int>();
            List<int> deptID = new List<int>();
            List<int> progID = new List<int>();
            List<Course> cou = new List<Course>();

                foreach (VC vc in ds.vcs)
                {
                    if(vc.VCID == ActiveVC.VCID)
                    {
                        UniversityID = vc.University.UniversityID;
                    }
                }

                foreach (School school in ds.schools)
                {
                    if(UniversityID == school.University.UniversityID)
                    {
                        SchoolIDs.Add(school.SchoolID);
                    }
                }


                foreach(Department d in ds.departments)
                {
                    for(int i = 0; i<SchoolIDs.Count; i++)
                    {
                        if(SchoolIDs[i] == d.School.SchoolID)
                        {
                            deptID.Add(d.DepartmentID);
                        }
                    }
                }

                foreach(Program p in ds.programs)
                {
                    for(int i=0; i< deptID.Count;i++)
                    {
                        if(deptID[i] == p.Department.DepartmentID)
                        {
                            progID.Add(p.ProgramID);
                        }
                    }
                }

                foreach(Course c in ds.courses){
                    for(int i=0; i< progID.Count;i++)
                    {
                        if(progID[i] == c.Program.ProgramID)
                        {
                            cou.Add(c);
                        }
                    }
                }
            
            studentPLOComparisonByCourseViewModel.Courses = cou;
            studentPLOComparisonByCourseViewModel.TopbarViewModel = new TopbarViewModel()
            {
                Name = ActiveVC.FullName,
                ID = ActiveVC.VCID
            };

            return View(studentPLOComparisonByCourseViewModel);
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

            return View(new TopbarViewModel() {Name = ActiveVC.FullName, ID = ActiveVC.VCID});
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
            return View(new TopbarViewModel() {Name = ActiveVC.FullName, ID = ActiveVC.VCID});
        }
    }
}
