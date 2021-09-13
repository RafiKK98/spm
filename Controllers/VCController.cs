using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpmsApp.ViewModels;
using SpmsApp.ViewModels.VC;
using SpmsApp.Models;
using SpmsApp.Services;
using System.Collections;

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
            return View(new TopbarViewModel() { Name = ActiveVC.FullName, ID = ActiveVC.VCID });
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

        [HttpGet("/vc/ippsc")]
        public IActionResult InstructorwisePLOPerformanceComparisonSelectCourse() // 4
        {
            var viewModel = new InstructorwisePLOPerformanceViewModelSelectCourse()
            {
                TopbarViewModel = new TopbarViewModel() { Name = ActiveVC.FullName, ID = ActiveVC.VCID },
                Courses = ds.courses.Where(c => c.Program.Department.School.University == ActiveVC.University).ToList()
            };

            return View(viewModel);
        }

        [HttpGet("/vc/ippsc/{selectedCourse}/{startSemester}/{startYear}/{endSemester}/{endYear}")]
        public IActionResult InstructorwisePLOPerformanceComparisonSelectCourse(int selectedCourse, int startSemester,
        int startYear, int endSemester, int endYear) // 4 continued
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

        [HttpGet("/vc/pccsp")]
        public IActionResult PloComparisonCourseWithSelectPlos() // 5
        {
            var viewModel = new PLOComparisonCourseWithSelectPlosViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = ActiveVC.FullName,
                    ID = ActiveVC.VCID
                },
                Courses = ds.courses.Where(c => c.Program.Department.School.University == ActiveVC.University).ToList()


            };

            return View(viewModel);
        }

        [HttpPost("/vc/pccsp/{startSemester}/{startYear}/{endSemester}/{endYear}")]
        public IActionResult PloComparisonCourseWithSelectPlos([FromBody] PLOComparisonCourseWithSelectPlosViewModel viewModel,
        int startSemester, int startYear, int endSemester, int endYear) // 5 continued
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

        [HttpGet("/vc/cpafc")]
        public IActionResult ComparisonPloAchievedFailedSelectCourses() // 6
        {
            var viewModel = new ComparisonPLOAchievedFailedSelectCoursesViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = ActiveVC.FullName,
                    ID = ActiveVC.VCID
                },
                Courses = ds.courses.Where(c => c.Program.Department.School.University == ActiveVC.University && c.CoofferedCourse == null).ToList(),
                Semester = new Semester(1, 2001)
            };

            return View(viewModel);
        }

        [HttpPost("/vc/cpafc")]
        public IActionResult ComparisonPloAchievedFailedSelectCourses([FromBody] ComparisonPLOAchievedFailedSelectCoursesViewModel viewModel) // 6 continued...
        {
            var evaluations = ds.evaluations.Where(ev => viewModel.SelectedCourses.Contains(ev.Assessment.Section.Course.CourseID))
                                            .Where(ev => viewModel.SelectedSemesters.Contains(ev.Assessment.Section.Semester));
            var evaluationsPloGroups = evaluations.GroupBy(ev => ev.Assessment.CourseOutcome.PLO.PloName);

            var ploNameList = new List<string>();
            var achievedList = new List<float>();
            var failedList = new List<float>();

            foreach (var evGroup in evaluationsPloGroups)
            {
                ploNameList.Add(evGroup.Key);

                var passedCount = 0;

                foreach (var ev in evGroup)
                {
                    var percent = ev.TotalObtainedMark / ev.Assessment.TotalMark * 100;

                    if (percent > ev.Assessment.Section.PassMark)
                    {
                        passedCount++;
                    }
                }

                var passPercent = (float)passedCount / evGroup.Count() * 100;

                achievedList.Add(passPercent);
                failedList.Add(100 - passPercent);
            }

            var myData = new { label = ploNameList, passData = achievedList, failData = failedList };

            return Json(myData);
        }

        [HttpGet("/vc/cpaap")]
        public IActionResult ComparisonPloAchievedAttemptedSelectPrograms() // 7
        {
            var viewModel = new ComparisonPLOAchievedAttemptedSelectProgramsViewModel()
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

        [HttpPost("/vc/cpaap")]
        public IActionResult ComparisonPloAchievedAttemptedSelectPrograms([FromBody] ComparisonPloAchievedAttemptedSelectProgramsViewModel viewModel) // 7 continued...
        {
            var evaluations = ds.evaluations.Where(e => viewModel.SelectedPrograms.Contains(e.Assessment.CourseOutcome.PLO.Program.ProgramID))
                                            .Where(e => viewModel.SelectedSemesters.Contains(e.Assessment.Section.Semester));

            var evaluationsPloGroups = evaluations.GroupBy(ev => ev.Assessment.CourseOutcome.PLO.PloName);

            var ploNameList = new List<string>();
            var achievedList = new List<float>();
            var attemptedList = new List<float>();

            foreach (var evGroup in evaluationsPloGroups)
            {
                ploNameList.Add(evGroup.Key);

                var passedCount = 0;

                foreach (var ev in evGroup)
                {
                    var percent = ev.TotalObtainedMark / ev.Assessment.TotalMark * 100;

                    if (percent > ev.Assessment.Section.PassMark)
                    {
                        passedCount++;
                    }
                }

                achievedList.Add(passedCount);
                attemptedList.Add(evGroup.Count());
            }

            var myData = new { label = ploNameList, passData = achievedList, attemptData = attemptedList };

            return Json(myData);
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
                if (vc.VCID == ActiveVC.VCID)
                {
                    UniversityID = vc.University.UniversityID;
                }
            }

            foreach (School school in ds.schools)
            {
                if (UniversityID == school.University.UniversityID)
                {
                    SchoolIDs.Add(school.SchoolID);
                }
            }


            foreach (Department d in ds.departments)
            {
                for (int i = 0; i < SchoolIDs.Count; i++)
                {
                    if (SchoolIDs[i] == d.School.SchoolID)
                    {
                        deptID.Add(d.DepartmentID);
                    }
                }
            }

            foreach (Program p in ds.programs)
            {
                for (int i = 0; i < deptID.Count; i++)
                {
                    if (deptID[i] == p.Department.DepartmentID)
                    {
                        progID.Add(p.ProgramID);
                    }
                }
            }

            foreach (Course c in ds.courses)
            {
                for (int i = 0; i < progID.Count; i++)
                {
                    if (progID[i] == c.Program.ProgramID)
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
            return Json(new { labels = new List<string>() { "PLO-01", "PLO-02", "PLO-03" }, data = new List<float>() { 99, 93, 97 } });
        }

        [HttpGet("/vc/spcp/")]
        public IActionResult StudentPLOComparisonByProgram()
        {
            return View(new TopbarViewModel() { Name = "No Name Set", ID = 0000 });
        }
        [HttpGet("/vc/spat/")]
        public IActionResult StudentPLOAchievementTable() // 3
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
        public IActionResult StudentPloAchievementTable(int studentID) // 3 continued...
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

            return View(new TopbarViewModel() { Name = ActiveVC.FullName, ID = ActiveVC.VCID });
        }

        [HttpGet("/vc/pcp/")]
        public IActionResult PLOwiseCoursePerformance()
        {
            return View(new TopbarViewModel() { Name = ActiveVC.FullName, ID = ActiveVC.VCID });
        }

        [HttpGet("/vc/afpp/")]
        public IActionResult AchievedVsFailedPLOPerformance()
        {
            return View(new TopbarViewModel() { Name = ActiveVC.FullName, ID = ActiveVC.VCID });
        }

        [HttpGet("/vc/aac/")]
        public IActionResult AttemptedVsAchievedComparison()
        {
            return View(new TopbarViewModel() { Name = ActiveVC.FullName, ID = ActiveVC.VCID });
        }

        [HttpGet("/vc/pafap/")]
        public IActionResult PLOAchievementForAProgram()
        {
            return View(new TopbarViewModel() { Name = ActiveVC.FullName, ID = ActiveVC.VCID });
        }

        [HttpGet("/vc/sgaap/")]
        public IActionResult StudentsGraduatesAchievingAllPLOS()
        {
            return View(new TopbarViewModel() { Name = ActiveVC.FullName, ID = ActiveVC.VCID });
        }

        [HttpGet("/vc/apa/")]
        public IActionResult AveragePLOAchievement()
        {
            return View(new TopbarViewModel() { Name = ActiveVC.FullName, ID = ActiveVC.VCID });
        }

        [HttpGet("/vc/ippc")]
        public IActionResult InstructorwisePloPerformanceComparison()
        {
            var viewModel = new InstructorwisePloPerformanceComparisonViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = ActiveVC.FullName,
                    ID = ActiveVC.VCID
                },
                Faculties = ds.faculties.Where(f => f.Department.School.University == ActiveVC.University).ToList()
            };

            return View(viewModel);
        }

        // [HttpPost("/vc/ippc")]
        // public IActionResult InstructorwisePloPerformanceComparison(InstructorwisePloPerformanceComparisonViewModel viewModel)
        // {
        //     Semester start = new Semester(viewModel.StartSemester, viewModel.StartYear);
        //     Semester end = new Semester(viewModel.EndSemester, viewModel.EndYear);
        //     Faculty faculty = ds.faculties.Find(f => f.ID == viewModel.FacultyID);
        // }

        [HttpPost("/vc/ippc")]
        public IActionResult InstructorwisePloPerformanceComparison(InstructorwisePloPerformanceComparisonViewModel viewModel)
        {
            Semester start = new Semester(viewModel.StartSemester, viewModel.StartYear);
            Semester end = new Semester(viewModel.EndSemester, viewModel.EndYear);
            Faculty faculty = ds.faculties.Find(f => f.ID == viewModel.FacultyID);

            var sections = ds.sections.Where(s => s.Faculty == faculty && s.Semester.CompareTo(start) >= 0 && end.CompareTo(s.Semester) >= 0);
            var evaluationStudentGroups = ds.evaluations.Where(e => sections.Contains(e.Assessment.Section) && e.Assessment.Section.Semester.CompareTo(start) >= 0 && e.Assessment.Section.Semester.CompareTo(end) <= 0)
                                            .GroupBy(e => e.Student);

            string[] ploname = { "PLO-01", "PLO-02", "PLO-03", "PLO-04", "PLO-05", "PLO-06", "PLO-07", "PLO-08", "PLO-09", "PLO-10", "PLO-11", "PLO-12" };
            int[] ploAttempted = new int[12];
            int[] ploAchieved = new int[12];

            foreach (var evStGroup in evaluationStudentGroups)
            {
                var evStSectionGroups = evStGroup.GroupBy(ev => ev.Assessment.Section);
                bool[] hasPlo = null;
                bool[] hasAchieved = null;

                foreach (var evStSectionGroup in evStSectionGroups)
                {
                    hasPlo = new bool[12];
                    hasAchieved = new bool[12];

                    var evStSecPloGroups = evStSectionGroup.GroupBy(ev => ev.Assessment.CourseOutcome.PLO.PloName);

                    foreach (var evStSecPloGroup in evStSecPloGroups)
                    {
                        float totalObtainable = 0;
                        float totalObtained = 0;

                        switch (evStSecPloGroup.Key)
                        {
                            case "PLO-01":
                                hasPlo[0] = true;
                                break;
                            case "PLO-02":
                                hasPlo[1] = true;
                                break;
                            case "PLO-03":
                                hasPlo[2] = true;
                                break;
                            case "PLO-04":
                                hasPlo[3] = true;
                                break;
                            case "PLO-05":
                                hasPlo[4] = true;
                                break;
                            case "PLO-06":
                                hasPlo[5] = true;
                                break;
                            case "PLO-07":
                                hasPlo[6] = true;
                                break;
                            case "PLO-08":
                                hasPlo[7] = true;
                                break;
                            case "PLO-09":
                                hasPlo[8] = true;
                                break;
                            case "PLO-10":
                                hasPlo[9] = true;
                                break;
                            case "PLO-11":
                                hasPlo[10] = true;
                                break;
                            case "PLO-12":
                                hasPlo[11] = true;
                                break;
                        }

                        foreach (var evStSecPlo in evStSecPloGroup)
                        {
                            totalObtainable += evStSecPlo.Assessment.TotalMark;
                            totalObtained += evStSecPlo.TotalObtainedMark;
                        }

                        float percent = totalObtained / totalObtainable * 100;

                        if (percent >= evStSecPloGroup.First().Assessment.Section.PassMark)
                        {
                            switch (evStSecPloGroup.Key)
                            {
                                case "PLO-01":
                                    hasAchieved[0] = true;
                                    break;
                                case "PLO-02":
                                    hasAchieved[1] = true;
                                    break;
                                case "PLO-03":
                                    hasAchieved[2] = true;
                                    break;
                                case "PLO-04":
                                    hasAchieved[3] = true;
                                    break;
                                case "PLO-05":
                                    hasAchieved[4] = true;
                                    break;
                                case "PLO-06":
                                    hasAchieved[5] = true;
                                    break;
                                case "PLO-07":
                                    hasAchieved[6] = true;
                                    break;
                                case "PLO-08":
                                    hasAchieved[7] = true;
                                    break;
                                case "PLO-09":
                                    hasAchieved[8] = true;
                                    break;
                                case "PLO-10":
                                    hasAchieved[9] = true;
                                    break;
                                case "PLO-11":
                                    hasAchieved[10] = true;
                                    break;
                                case "PLO-12":
                                    hasAchieved[11] = true;
                                    break;
                            }
                        }
                        else
                        {
                            switch (evStSecPloGroup.Key)
                            {
                                case "PLO-01":
                                    hasAchieved[0] = false;
                                    break;
                                case "PLO-02":
                                    hasAchieved[1] = false;
                                    break;
                                case "PLO-03":
                                    hasAchieved[2] = false;
                                    break;
                                case "PLO-04":
                                    hasAchieved[3] = false;
                                    break;
                                case "PLO-05":
                                    hasAchieved[4] = false;
                                    break;
                                case "PLO-06":
                                    hasAchieved[5] = false;
                                    break;
                                case "PLO-07":
                                    hasAchieved[6] = false;
                                    break;
                                case "PLO-08":
                                    hasAchieved[7] = false;
                                    break;
                                case "PLO-09":
                                    hasAchieved[8] = false;
                                    break;
                                case "PLO-10":
                                    hasAchieved[9] = false;
                                    break;
                                case "PLO-11":
                                    hasAchieved[10] = false;
                                    break;
                                case "PLO-12":
                                    hasAchieved[11] = false;
                                    break;
                            }
                        }
                    }
                }

                if (hasPlo != null)
                {
                    for (int i = 0; i < hasPlo.Length; i++)
                    {
                        ploAttempted[i]++;
                    }
                }

                if (hasAchieved != null)
                {
                    for (int i = 0; i < hasAchieved.Length; i++)
                    {
                        ploAchieved[i]++;
                    }
                }
            }

            List<float> ploAchievePercent = new List<float>();

            for (int i = 0; i < ploAchieved.Length; i++)
            {
                ploAchievePercent.Add((float)ploAchieved[i] / ploAttempted[i] * 100);
            }

            var data = new ViewModels.VC.Data { PloList = ploname, AchieveData = ploAchievePercent };

            viewModel.TopbarViewModel = new TopbarViewModel()
            {
                Name = ActiveVC.FullName,
                ID = ActiveVC.VCID
            };
            viewModel.Faculties = ds.faculties.Where(f => f.Department.School.University == ActiveVC.University).ToList();
            viewModel.Data = data;

            return View(viewModel);
        }
        [HttpGet("/vc/upasp/")]
        public IActionResult UniversityPloAchievementSelectProgram()
        {
            var viewModel = new SpmsApp.ViewModels.VC.UniversityPloAchievementSelectProgram()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = ActiveVC.FullName,
                    ID = ActiveVC.VCID
                },
                Programs = ds.programs
            };

            return View(viewModel);

        }
        
        [HttpPost("/vc/upasp/")]
        public IActionResult UniversityPloAchievementSelectProgram([FromBody] SpmsApp.ViewModels.VC.UniversityPloAchievementSelectProgram viewModel)
        {
            viewModel.TopbarViewModel = new TopbarViewModel()
            {
                Name = ActiveVC.FullName,
                ID = ActiveVC.VCID
            };
            viewModel.Programs = ds.programs;

            var program = ds.programs.Find(p => p.ProgramID == viewModel.SelectedProgram);

            var programEval = ds.evaluations.Where(ev => ev.Assessment.CourseOutcome.PLO.Program == program);
            var evalPloGroups = programEval.GroupBy(ev => ev.Assessment.CourseOutcome.PLO.PloName);

            List<string> labels = new List<string>();
            List<int> passCount = new List<int>();

            foreach (var evalPloGroup in evalPloGroups)
            {
                labels.Add(evalPloGroup.Key);

                int passed = 0;

                foreach (var eval in evalPloGroup)
                {
                    float percent = (float)eval.TotalObtainedMark / eval.Assessment.TotalMark * 100;

                    if (percent >= eval.Assessment.Section.PassMark)
                    {
                        passed++;
                    }
                }

                passCount.Add(passed);
            }

            SpmsApp.ViewModels.VC.Dataset dset = new SpmsApp.ViewModels.VC.Dataset()
            {
                Data = passCount,
                Label = "No. of Students",
                BackgroundColor = "rgba(255, 99, 132, 0.2)",
                BorderColor = "rgb(255, 99, 132)",
                PointBackgroundColor = "rgb(255, 99, 132)",
                PointBorderColor = "#fff",
                PointHoverBackgroundColor = "#fff",
                PointHoverBorderColor = "rgb(255, 99, 132)"
            };

            ViewModels.VC.VC_Data data = new ViewModels.VC.VC_Data()
            {
                Labels = labels,
                Datasets = new List<ViewModels.VC.Dataset>() { dset }
            };

            viewModel.Data = data;

            return Json(viewModel);
        }

        [HttpGet("/vc/logout")]
        public IActionResult Logout()
        {
            return Redirect("/");
        }
    }
}
