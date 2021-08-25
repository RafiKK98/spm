using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SpmsApp.Models;

namespace SpmsApp.Services
{
    [Serializable]
    public class Data
    {
        public float Obtained { get; set; }
        public float Achievable { get; set; }
        public string CourseCode { get; set; }
        public string PloName { get; set; }
        public float PassMark { get; set; }
    }

    public class DataServices
    {
        public static DataServices dataServices = new DataServices();

        public List<University> universities = new List<University>();
        public List<School> schools = new List<School>();
        public List<Department> departments = new List<Department>();
        public List<Faculty> faculties = new List<Faculty>();
        public List<Program> programs = new List<Program>();
        public List<Student> students = new List<Student>();
        public List<ProgramlearningOutcome> plos = new List<ProgramlearningOutcome>();
        public List<Course> courses = new List<Course>();
        public List<CourseOutcome> cos = new List<CourseOutcome>();
        public List<Section> sections = new List<Section>();
        public List<CourseRegistration> courseRegistrations = new List<CourseRegistration>();
        public List<Assessment> assessments = new List<Assessment>();
        public List<Evaluation> evaluations = new List<Evaluation>();
        public List<LoginCredentials> loginCredentialsList = new List<LoginCredentials>();

        private MySqlConnection connection;
        private MySqlCommand command;

        private DataServices()
        {
            connection = new MySqlConnection("server=localhost;database=spmsdb;userid=spms;password=");
            connection.Open();
            command = new MySqlCommand("Select * from University_T;", connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var uni = new University()
                {
                    UniversityID = reader.GetInt32(0),
                    UniversityName = reader.GetString(1),
                    UniversityDomain = reader.GetString(2),
                };

                universities.Add(uni);
            }
            reader.Close();
            connection.Close();

            connection.Open();
            command = new MySqlCommand("Select * from School_T;", connection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                var school = new School()
                {
                    SchoolID = reader.GetInt32(0),
                    SchoolName = reader.GetString(1),
                };

                int uniID = reader.GetInt32(2);

                school.University = universities.Where(u => u.UniversityID == uniID).First();

                schools.Add(school);
            }
            reader.Close();
            connection.Close();

            connection.Open();
            command = new MySqlCommand("Select * from Department_T;", connection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                var department = new Department()
                {
                    DepartmentID = reader.GetInt32(0),
                    DepartmentName = reader.GetString(1),
                };

                int schoolID = reader.GetInt32(2);

                department.School = schools.Where(s => s.SchoolID == schoolID).First();

                departments.Add(department);
            }
            reader.Close();
            connection.Close();

            connection.Open();
            command = new MySqlCommand("Select * from Program_T;", connection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                var program = new Program()
                {
                    ProgramID = reader.GetInt32(0),
                    ProgramName = reader.GetString(1),
                    TotalCreditCount = reader.GetInt32(2)
                };

                int deptID = reader.GetInt32(3);

                program.Department = departments.Where(d => d.DepartmentID == deptID).First();

                programs.Add(program);
            }
            reader.Close();
            connection.Close();

            connection.Open();
            command = new MySqlCommand("Select * from Course_T;", connection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                var course = new Course()
                {
                    CourseID = reader.GetInt32(0),
                    CourseCode = reader.GetString(1),
                    CourseName = reader.GetString(2),
                    CourseCreditCount = reader.GetInt32(3),
                };

                int programID = reader.GetInt32(4);

                course.Program = programs.Where(p => p.ProgramID == programID).First();
                course.PrerequisiteCourses = new List<Course>();

                courses.Add(course);
            }
            reader.Close();
            connection.Close();

            connection.Open();
            command = new MySqlCommand("Select CourseID,CoOfferedCourseID from Course_T;", connection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Course course;
                int cid, coOfferedId;

                cid = reader.GetInt32(0);
                if (!reader.IsDBNull(1))
                {
                    coOfferedId = reader.GetInt32(1);
                    course = courses.Find(c => c.CourseID == cid);
                    course.CoofferedCourse = courses.Find(c => c.CourseID == coOfferedId);
                }
            }
            reader.Close();
            connection.Close();

            connection.Open();
            command = new MySqlCommand("Select * from PrereqCourse_T;", connection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Course course = courses.Find(c => c.CourseID == reader.GetInt32(0));
                Course prereq = courses.Find(c => c.CourseID == reader.GetInt32(1));

                course.PrerequisiteCourses.Add(prereq);
            }

            reader.Close();
            connection.Close();

            connection.Open();
            command = new MySqlCommand("select * from User_T inner join Faculty_T on UserID=FID;", connection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Faculty faculty = new Faculty()
                {
                    ID = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    ContactNumber = reader.GetString(4),
                    EmailAddress = reader.GetString(5),
                    Address = reader.GetString(6),
                    HiringDate = reader.GetDateTime(9),
                    FacultyID = reader.GetInt32(10)
                };

                int deptID = reader.GetInt32(8);
                faculty.Department = departments.Find(d => d.DepartmentID == deptID);
                faculties.Add(faculty);
            }
            reader.Close();
            connection.Close();

            connection.Open();
            command = new MySqlCommand("select * from User_T inner join Student_T on UserID=SID;", connection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Student student = new Student()
                {
                    ID = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    ContactNumber = reader.GetString(4),
                    EmailAddress = reader.GetString(5),
                    StudentID = reader.GetInt32(8),
                    DateofBirth = reader.GetDateTime(9)
                };

                int programID = reader.GetInt32(10);
                student.Program = programs.Find(p => p.ProgramID == programID);
                students.Add(student);
            }
            reader.Close();
            connection.Close();

            connection.Open();
            command = new MySqlCommand("select * from PLO_T;", connection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                ProgramlearningOutcome plo = new ProgramlearningOutcome()
                {
                    PloID = reader.GetInt32(0),
                    PloName = reader.GetString(1),
                    Details = reader.GetString(2)
                };

                int programID = reader.GetInt32(3);
                plo.Program = programs.Find(p => p.ProgramID == programID);
                plos.Add(plo);
            }
            reader.Close();
            connection.Close();

            connection.Open();
            command = new MySqlCommand("select * from CO_T;", connection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CourseOutcome co = new CourseOutcome()
                {
                    CoID = reader.GetInt32(0),
                    CoName = reader.GetString(1),
                    Details = reader.GetString(4)
                };

                int courseID = reader.GetInt32(2);
                int ploID = reader.GetInt32(3);

                co.Course = courses.Find(c => c.CourseID == courseID);
                co.PLO = plos.Find(plo => plo.PloID == ploID);

                cos.Add(co);
            }
            reader.Close();
            connection.Close();

            connection.Open();
            command = new MySqlCommand("select * from Section_T;", connection);

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Section section = new Section()
                {
                    SectionID = reader.GetInt32(0),
                    SectionNumber = reader.GetInt32(1),
                    Semester = reader.GetString(2),
                    Year = reader.GetInt32(3),
                    MaximumCapacity = reader.GetInt32(4),
                    PassMark = reader.GetFloat(5)
                };

                int FID = reader.GetInt32(6);
                int courseID = reader.GetInt32(7);

                section.Faculty = faculties.Find(f => f.ID == FID);
                section.Course = courses.Find(c => c.CourseID == courseID);

                sections.Add(section);
            }
            reader.Close();
            connection.Close();

            connection.Open();
            command = new MySqlCommand("Select * from CourseRegistration_T;", connection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CourseRegistration courseRegistration = new CourseRegistration()
                {
                    RegistrationDate = reader.GetDateTime(2)
                };

                courseRegistration.Student = students.Find(s => s.ID == reader.GetInt32(0));
                courseRegistration.Section = sections.Find(s => s.SectionID == reader.GetInt32(1));
                courseRegistrations.Add(courseRegistration);
            }
            reader.Close();
            connection.Close();

            connection.Open();
            command = new MySqlCommand("Select * from Assessment_T;", connection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Assessment assessment = new Assessment()
                {
                    AssessmentID = reader.GetInt32(0),
                    QuestionNumber = reader.GetInt32(1),
                    AssessmentType = reader.GetString(2),
                    TotalMark = reader.GetInt32(3)
                };

                assessment.Section = sections.Find(s => s.SectionID == reader.GetInt32(4));
                assessment.CourseOutcome = cos.Find(co => co.CoID == reader.GetInt32(5));
                assessments.Add(assessment);
            }

            reader.Close();
            connection.Close();

            connection.Open();
            command = new MySqlCommand("Select * from Evaluation_T;", connection);

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Evaluation evaluation = new Evaluation()
                {
                    TotalObtainedMark = reader.GetFloat(2)
                };

                evaluation.Student = students.Find(s => s.ID == reader.GetInt32(0));
                evaluation.Assessment = assessments.Find(a => a.AssessmentID == reader.GetInt32(1));
                evaluations.Add(evaluation);
            }

            reader.Close();
            connection.Close();

            connection.Open(); // loading login credentials
            command = new MySqlCommand("Select UserID,Email,LoginPass from User_T;", connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                LoginCredentials loginCredentials = new LoginCredentials()
                {
                    UserID = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2)
                };

                loginCredentialsList.Add(loginCredentials);
            }

            reader.Close();
            connection.Close();
        }

        public Faculty GetFaculty(string username, string password)
        {
            int userID = loginCredentialsList.Find(lc => lc.Username == username && lc.Password == password).UserID;
            return faculties.Find(f => f.ID == userID);
        }

        public ArrayList PloAchievementTableData(Student student)
        {
            var studentCourseRegistrations = courseRegistrations.Where(cr => cr.Student == student);

            var sectionAssessments = studentCourseRegistrations.Join
            (
                assessments,
                scr => scr.Section,
                assessment => assessment.Section,
                (scr, assessement) => new
                {
                    CourseRegistration = scr,
                    Assessment = assessement
                }
            );

            var studentEvaluations = sectionAssessments.Join
            (
                evaluations,
                sa => sa.Assessment,
                eval => eval.Assessment,
                (sa, eval) => new
                {
                    CourseRegistration = sa.CourseRegistration,
                    Evaluation = eval
                }
            ).Where(o => o.Evaluation.Student == student);

            var courseWiseStudentEvaluations = studentEvaluations.GroupBy(o => o.Evaluation.Assessment.Section.Course.CourseID);

            ArrayList data = new ArrayList();

            foreach (var courseEval in courseWiseStudentEvaluations)
            {
                // var recentRegistrationDate = courseEval.Max(o => o.CourseRegistration.RegistrationDate);                
                var courseToDisplay = courseEval.ToList();

                var dt = new { CourseCode = courseEval.Key, Data = new List<Data>() };
                foreach (var course in courseToDisplay)
                {
                    dt.Data.Add(new Data()
                    {
                        Obtained = course.Evaluation.TotalObtainedMark,
                        Achievable = course.Evaluation.Assessment.TotalMark,
                        CourseCode = course.Evaluation.Assessment.Section.Course.CourseCode,
                        PloName = course.Evaluation.Assessment.CourseOutcome.PLO.PloName,
                        PassMark = course.Evaluation.Assessment.Section.PassMark
                    });
                }

                data.Add(dt);
            }

            return data;
        }
    }
}