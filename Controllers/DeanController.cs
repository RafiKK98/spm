using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpmsApp.ViewModels;
using SpmsApp.Models;
using SpmsApp.Services;
using SpmsApp.ViewModels.Dean;
using System.Collections;

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

        [HttpGet("/dean/pccsp")]
        public IActionResult PloComparisonCourseWithSelectPlos() // 5
        {
            var viewModel = new PLOComparisonCourseWithSelectPlosViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = ActiveDean.FullName,
                    ID = ActiveDean.DeanID
                },
                Courses = ds.courses.Where(c => c.Program.Department.School == ActiveDean.School).ToList()
            

            };

            return View(viewModel);
        }

        public class Dummy
        {
            public string Name { get; set; }
        }

        [HttpPost]
        public IActionResult DummyAction([FromBody] PLOComparisonCourseWithSelectPlosViewModel dummy)
        {
            var courses = ds.courses.Where(c => dummy.SelectedCoursesID.Contains(c.CourseID)).ToList();
            return Json(dummy);
        }


        [HttpPost("/dean/pccsp/{startSemester}/{startYear}/{endSemester}/{endYear}")]
        public IActionResult PloComparisonCourseWithSelectPlos([FromBody] PLOComparisonCourseWithSelectPlosViewModel viewModel, int startSemester, int startYear, int endSemester, int endYear) // 5 continued
        {
            var start = new Semester(startSemester, startYear);
            var end = new Semester(endSemester, endYear);

            // var courses = ds.courses.Where(c => viewModel.SelectedCoursesID.Contains(c.CourseID)).ToList();

            // var plos = ds.plos.Where(plo => plo.Program.Department == activeFaculty.Department && viewModel.SelectedPlosName.Contains(plo.PloName)).ToList();



            List<float> scores = new List<float>();
            List<string> ploNames = new List<string>();

            List<Course> courses = new List<Course>();

            foreach (var course in ds.courses)
            {
                if (viewModel.SelectedCoursesID.Contains(course.CourseID))
                {
                    bool available = true;
                    var plos = ds.cos.Where(co => co.Course == course).Select(co => co.PLO.PloName);

                    foreach (var ploName in viewModel.SelectedPlosName)
                    {
                        if (!plos.Contains(ploName))
                            available = false;
                    }

                    if (available) courses.Add(course);
                }
            }

            var evaluations = ds.evaluations.Where(ev => courses.Contains(ev.Assessment.Section.Course)
                                                    && viewModel.SelectedPlosName.Contains(ev.Assessment.CourseOutcome.PLO.PloName)
                                                    && ev.Assessment.Section.Semester.CompareTo(start) >= 0
                                                    && ev.Assessment.Section.Semester.CompareTo(end) <= 0)
                                            .GroupBy(ev => ev.Assessment.CourseOutcome.PLO.PloName);

            foreach (var evg in evaluations)
            {
                ploNames.Add(evg.Key);
                int count = 0;

                foreach (var ev in evg)
                {
                    var percent = ev.TotalObtainedMark / ev.Assessment.TotalMark * 100;
                    if (percent >= ev.Assessment.Section.PassMark)
                    {
                        count++;
                    }
                }

                scores.Add((float)count / evg.Count() * 100);
            }

            var myData = new { labels = ploNames, data = scores };

            return Json(myData);
        }



        [HttpGet("/dean/spcc/")]
        public IActionResult StudentPLOComparisonByCourse()
        {
            int SchoolId = 0;

            StudentPloComparisonByCourseViewModel studentPloComparisonByCourseViewModel = new StudentPloComparisonByCourseViewModel();
            List<int> deptID = new List<int>();
            List<int> progID = new List<int>();
            List<Course> cou = new List<Course>();

                foreach(SchoolDean s in ds.schoolDeans){
                    if(s.DeanID == ActiveDean.DeanID)
                    {
                        SchoolId = s.School.SchoolID;
                    }
                }

                foreach(Department d in ds.departments){
                    if(SchoolId == d.School.SchoolID)
                    {
                       deptID.Add(d.DepartmentID);
                    }
                }

                foreach(Program p in ds.programs){
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
            
            studentPloComparisonByCourseViewModel.Courses = cou;
            studentPloComparisonByCourseViewModel.TopbarViewModel = new TopbarViewModel()
            {
                Name = ActiveDean.FullName,
                ID = ActiveDean.DeanID
            };

            return View(studentPloComparisonByCourseViewModel);
        }

        [HttpGet("/dean/spcc/{startSemester}/{startYear}/{endSemester}/{endYear}/{selectedCourse}")]
        public IActionResult StudentPLOComparisonByCourse(int startSemester, int startYear, int endSemester, int endYear, int selectedCourse)
        {
            Semester start = new Semester(startSemester, startYear);
            Semester end = new Semester(endSemester, endYear);

            StudentPloComparisonCourseViewModel viewModel = new StudentPloComparisonCourseViewModel();
            viewModel.Courses = ds.courses.Where(c => c.Program.Department.School == ActiveDean.School && c.CoofferedCourse == null).ToList();
            viewModel.TopbarViewModel = new TopbarViewModel()
            {
                Name = ActiveDean.FullName,
                ID = ActiveDean.DeanID
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

                // System.Console.WriteLine(group);
            }

            return Json(new { Plos = plos, Counts = counts });
        }

        [HttpGet("/dean/spcp/")]
        public IActionResult StudentPLOComparisonByProgram()
        {
            return View(new TopbarViewModel(){Name = "Mr Dean", ID = 1234});
        }
        [HttpGet("/dean/spat/")]
        public IActionResult StudentPLOAchievementTable()
        {
            StudentPLOAchievementTableViewModel studentPLOAchievementTableViewModel = new StudentPLOAchievementTableViewModel();
            studentPLOAchievementTableViewModel.TopbarViewModel = new TopbarViewModel()
            {
                Name = ActiveDean.FullName,
                ID = ActiveDean.DeanID
            };
            
            return View(studentPLOAchievementTableViewModel);
        }

        [HttpGet("/dean/spat/{studentID}")]
        public IActionResult StudentPLOAchievementTable(int studentID)
        {
            var student = ds.students.Find(s => s.StudentID == studentID && s.Program.Department.School == ActiveDean.School);

            if (student == null) return Json(null);

            var _data = ds.PloAchievementTableData(student);

            if (_data.Count <= 0) return Json(null);

            var plos = ds.plos.Where(o => o.Program == ds.students.First().Program);

            if (plos.Count() <= 0) return Json(null);

            var mydata = new { studentName = student.FullName, ploList = plos, data = _data };

            return Json(mydata);
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
            var viewModel = new InstructorwisePLOPerformanceViewModel()
            {
                TopbarViewModel = new TopbarViewModel() { Name = ActiveDean.FullName, ID = ActiveDean.DeanID },
                Courses = ds.courses.Where(c => c.Program.Department.School == ActiveDean.School).ToList()
            };

            return View(viewModel);
        }

        [HttpGet("/dean/ippc/{selectedCourse}/{startSemester}/{startYear}/{endSemester}/{endYear}")]
        public IActionResult InstructorwisePLOPerformanceSelectCourses(int selectedCourse, int startSemester, int startYear, int endSemester, int endYear) // 4 continued
        {
            var course = ds.courses.Find(c => c.CourseID == selectedCourse);
            var start = new Semester(startSemester, startYear);
            var end = new Semester(endSemester, endYear);

            var sections = ds.sections.Where(s => (s.Course == course || s.Course == course.CoofferedCourse) && s.Semester.CompareTo(start) >= 0 && s.Semester.CompareTo(end) <= 0)
                                .GroupBy(s => s.Faculty);

            // Dictionary<Faculty, List<float>> scores = new Dictionary<Faculty, List<float>>();
            var scores = new ArrayList();
            var plos = ds.plos.Where(plo => plo.Program == course.Program);

            foreach (var facultySection in sections)
            {
                List<float> _scores = new List<float>();

                foreach (var plo in plos)
                {
                    float count = 0;
                    int total = 0;

                    foreach (var section in facultySection)
                    {
                        var evaluations = ds.evaluations.Where(ev => ev.Assessment.Section == section && ev.Assessment.CourseOutcome.PLO == plo);
                        total += evaluations.Count();

                        foreach (var eval in evaluations)
                        {
                            var percent = eval.TotalObtainedMark / eval.Assessment.TotalMark * 100;
                            count = percent > eval.Assessment.Section.PassMark ? count + 1 : count;
                        }
                    }

                    if (total > 0) _scores.Add(count / total * 100);
                    else _scores.Add(0);
                }

                var d = new
                {
                    faculty = facultySection.Key,
                    data = _scores
                };

                scores.Add(d);
            }

            var myData = new
            {
                Course = course,
                Data = scores,
                ploList = plos.Select(p => p.PloName)
            };

            return Json(myData);
        }

        [HttpGet("/dean/ispscc/")]
        public IActionResult IndividualStudentPloScoreComparisonCourse()
        {
            var viewModel = new IndividualStudentPLOScoreComparisonCourseViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = ActiveDean.FullName,
                    ID = ActiveDean.DeanID
                },
                Courses = ds.courses.Where(c => c.Program.Department.School == ActiveDean.School && c.CoofferedCourse == null).ToList()
            };

            return View(viewModel);
        }

        [HttpGet("/dean/ispscc/{studentID}/{courseID}")]
        public IActionResult IndividualStudentPLOScoreComparisonCourse(int studentID, int courseID)
        {
            var student = ds.students.Find(s => s.StudentID == studentID && s.Program.Department.School == ActiveDean.School);
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

        [HttpGet("/dean/ispscp")]
        public IActionResult IndividualStudentPLOScoreComparisonProgram() // 2
        {
            var viewModel = new IndividualStudentPLOScoreComparisonProgramViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = ActiveDean.FullName,
                    ID = ActiveDean.DeanID
                },
                Programs = ds.programs.Where(p => p.Department.School == ActiveDean.School).ToList()
            };

            return View(viewModel);
        }

        [HttpGet("/dean/ispscp/{studentID}/{programID}")]
        public IActionResult IndividualStudentPLOScoreComparisonProgram(int studentID, int programID) // 2 continued
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


    }
}
