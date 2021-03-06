using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json;
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
        public IActionResult PloAchievementTable() // 3
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
        public IActionResult PloAchievementTable(int studentID) // 3 continued...
        {
            var student = ds.students.Find(s => s.StudentID == studentID);

            if (student == null) return Json(null);

            var _data = ds.PloAchievementTableData(student);

            if (_data.Count <= 0) return Json(null);

            var plos = ds.plos.Where(o => o.Program == student.Program);

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

                // System.Console.WriteLine(group);
            }

            return Json(new { Plos = plos, Counts = counts });
        }

        [HttpGet("/faculty/iad/")]
        public IActionResult InputAssessmentDetails()
        {
            activeFaculty.Department.School.University.CurrentSemester = new Semester("Summer", 2003);

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

            viewModel.CourseList = ds.courses.Where(c => c == viewModel.SelectedCourse || c.CoofferedCourse == viewModel.SelectedCourse).ToList();

            return View(viewModel);
        }

        [HttpGet("/faculty/iad/add/{courseID}")]
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
                Sections = ds.sections.Where(s => s.Semester.Equals(activeFaculty.Department.School.University.CurrentSemester)
                                            && (s.Course.CourseID == courseID || s.Course == ds.courses.Find(c => c.CourseID == courseID).CoofferedCourse)).ToList(),
                CourseOutcomes = ds.cos.Where(co => co.Course.CourseID == courseID).ToList()
            };

            return View(viewModel);
        }

        [HttpPost("/faculty/iad/add/{courseID}")]
        public IActionResult AddNewAssessment(AddNewAssessmentViewModel viewModel)
        {
            // ds.assessments.Find(a => a.Section.Semester.Equals(activeFaculty.Department.School.University.CurrentSemester) &&
            //                             a.Section.SectionID == viewModel.SectionID &&
            //                             (a.Section.Course == viewModel.SelectedCourse || a.Section.Course == viewModel.SelectedCourse.CoofferedCourse))

            var section = ds.sections.Find(s => s.SectionID == viewModel.SectionID);
            var co = ds.cos.Find(co => co.CoID == viewModel.CoID);
            var assessment = new Assessment()
            {
                AssessmentID = ds.assessments.Count + 1,
                QuestionNumber = viewModel.QuestionNumber,
                AssessmentType = viewModel.AssessmentType,
                TotalMark = viewModel.TotalMark,
                Section = section,
                CourseOutcome = co
            };

            ds.AddAssessment(assessment);

            return Redirect("/faculty/iad");
        }

        [HttpGet("/faculty/ispscc")]
        public IActionResult IndividualStudentPloScoreComparisonCourse() // 1
        {
            // return Content("HELLO WORLD");

            var viewModel = new IndividualStudentPloScoreComparisonCourseViewModel()
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

        [HttpGet("/faculty/ispscc/{studentID}/{courseID}")]
        public IActionResult IndividualStudentPloScoreComparisonCourse(int studentID, int courseID) // 1 continued
        {
            var student = ds.students.Find(s => s.StudentID == studentID && s.Program.Department == activeFaculty.Department);
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

            var courseEval = ds.evaluations.Where(ev => ev.Assessment.Section.Semester.Equals(semester) &&
                                                        ev.Assessment.Section.Course == course &&
                                                        ev.Assessment.CourseOutcome.PLO.Program == student.Program);

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

        [HttpGet("/faculty/ispscp")]
        public IActionResult IndividualStudentPloScoreComparisonProgram() // 2
        {
            var viewModel = new IndividualStudentPloScoreComparisonProgramViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = activeFaculty.FullName,
                    ID = activeFaculty.FacultyID
                },
                Programs = ds.programs.Where(p => p.Department == activeFaculty.Department).ToList()
            };

            return View(viewModel);
        }

        [HttpGet("/faculty/ispscp/{studentID}/{programID}")]
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

        [HttpGet("/faculty/ippsc")]
        public IActionResult InstructorwisePLOPerformanceSelectCourses() // 4
        {
            var viewModel = new InstructorwisePLOPerformanceSelectCoursesViewModel()
            {
                TopbarViewModel = new TopbarViewModel() { Name = activeFaculty.FullName, ID = activeFaculty.FacultyID },
                Courses = ds.courses.Where(c => c.Program.Department == activeFaculty.Department).ToList()
            };

            return View(viewModel);
        }

        [HttpGet("/faculty/ippsc/{selectedCourse}/{startSemester}/{startYear}/{endSemester}/{endYear}")]
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

        [HttpGet("/faculty/pccsp")]
        public IActionResult PloComparisonCourseWithSelectPlos() // 5
        {
            var viewModel = new PloComparisonCourseWithSelectPlosViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = activeFaculty.FullName,
                    ID = activeFaculty.FacultyID
                },
                Courses = ds.courses.Where(c => c.Program.Department == activeFaculty.Department).ToList()
            };

            return View(viewModel);
        }

        public class Dummy
        {
            public string Name { get; set; }
        }

        [HttpPost]
        public IActionResult DummyAction([FromBody] PloComparisonCourseWithSelectPlosViewModel dummy)
        {
            var courses = ds.courses.Where(c => dummy.SelectedCoursesID.Contains(c.CourseID)).ToList();
            return Json(dummy);
        }

        [HttpPost("/faculty/pccsp/{startSemester}/{startYear}/{endSemester}/{endYear}")]
        public IActionResult PloComparisonCourseWithSelectPlos([FromBody] PloComparisonCourseWithSelectPlosViewModel viewModel, int startSemester, int startYear, int endSemester, int endYear) // 5 continued
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

        [HttpGet("/faculty/cpafc")]
        public IActionResult ComparisonPloAchievedFailedSelectCourses() // 6
        {
            var viewModel = new ComparisonPloAchievedFailedSelectCoursesViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = activeFaculty.FullName,
                    ID = activeFaculty.FacultyID
                },
                Courses = ds.courses.Where(c => c.Program.Department == activeFaculty.Department && c.CoofferedCourse == null).ToList(),
                Semester = new Semester(1, 2001)
            };

            return View(viewModel);
        }

        [HttpPost("/faculty/cpafc")]
        public IActionResult ComparisonPloAchievedFailedSelectCourses([FromBody] ComparisonPloAchievedFailedSelectCoursesViewModel viewModel) // 6 continued...
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

            var myData = new {label = ploNameList, passData = achievedList, failData = failedList};

            return Json(myData);
        }

        [HttpGet("/faculty/cpaap")]
        public IActionResult ComparisonPloAchievedAttemptedSelectPrograms() // 7
        {
            var viewModel = new ComparisonPloAchievedAttemptedSelectProgramsViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = activeFaculty.FullName,
                    ID = activeFaculty.FacultyID
                },
                Programs = ds.programs.Where(p => p.Department == activeFaculty.Department).ToList()
            };

            return View(viewModel);
        }

        [HttpPost("/faculty/cpaap")]
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

            var myData = new {label = ploNameList, passData = achievedList, attemptData = attemptedList};

            return Json(myData);
        }

        [HttpGet("/faculty/cpaaps")]
        public IActionResult ComparisonPloAchievedAttemptedSelectProgramsSegment() // 8
        {
            var viewModel = new ComparisonPloAchievedAttemptedSelectProgramsViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = activeFaculty.FullName,
                    ID = activeFaculty.FacultyID
                },
                Programs = ds.programs.Where(p => p.Department == activeFaculty.Department).ToList()
            };

            return View(viewModel);
        }

        [HttpPost("/faculty/cpaaps")]
        public IActionResult ComparisonPloAchievedAttemptedSelectProgramsSegment([FromBody] ComparisonPloAchievedAttemptedSelectProgramsViewModel viewModel) // 7 continued...
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

            var myData = new {label = ploNameList, passData = achievedList, attemptData = attemptedList};

            return Json(myData);
        }

        [HttpGet]
        public IActionResult Dummy2()
        {
            return View();
        }

        [HttpPost]
        public void Dummy2([FromBody] Semester semester)
        {
            System.Console.WriteLine(semester);
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

        [HttpGet("/faculty/logout")]
        public IActionResult Logout()
        {
            return Redirect("/");
        }
    }
}