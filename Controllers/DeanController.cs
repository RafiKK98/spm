﻿using Microsoft.AspNetCore.Mvc;
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
            return View(new TopbarViewModel(){Name = "Mr Dean", ID = 1234});
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
