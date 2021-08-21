USE spmsdb;

-- Section_T 
-- Faculty_T

-- drop table Section_T;
-- drop table Faculty_T;
-- drop table User_T;


CREATE TABLE University_T(
	UniversityID INTEGER NOT NULL AUTO_INCREMENT ,
	UniversityName VARCHAR(255) NOT NULL,
    UniversityDomain varchar(255) not null,
	CONSTRAINT University_PK PRIMARY KEY(UniversityID)
);


CREATE TABLE School_T(
    SchoolID INTEGER NOT NULL AUTO_INCREMENT,
    SchoolName VARCHAR(255) NOT NULL,
    UniversityID INTEGER NOT NULL,
    CONSTRAINT School_PK PRIMARY KEY (SchoolID),
    CONSTRAINT School_FK FOREIGN KEY (UniversityID) REFERENCES University_T(UniversityID)
);


CREATE TABLE Department_T(
    DepartmentID INTEGER NOT NULL AUTO_INCREMENT,
    DepartmentName VARCHAR(255) NOT NULL,
    SchoolID INTEGER NOT NULL,
    CONSTRAINT Department_PK PRIMARY KEY (DepartmentID),
    CONSTRAINT Department_FK FOREIGN KEY (SchoolID) REFERENCES School_T(SchoolID)
);


CREATE TABLE Program_T(
    ProgramID INTEGER NOT NULL AUTO_INCREMENT ,
    ProgramName VARCHAR(255) NOT NULL,
    TotalNumCredits INTEGER NOT NULL,
    DepartmentID INTEGER NOT NULL,
    CONSTRAINT Program_PK PRIMARY KEY (ProgramID),
    CONSTRAINT Program_FK FOREIGN KEY (DepartmentID) REFERENCES Department_T(DepartmentID)
);


create table User_T(
	UserID int not null AUTO_INCREMENT,
    FName varchar(255) not null,
    LName varchar(255) not null,
    LoginPass varchar(50) not null,
    PhoneNumber varchar(14) default 'NOT AVAILABLE',
    Email varchar(50) not null,
    Address varchar(255) default 'NOT AVAILABLE',
    constraint User_PK primary key (UserID)
);


create table Faculty_T(
	FID int not null,
    DepartmentID INTEGER NOT NULL,
    HiringDate DATE,
    FacultyID INTEGER NOT NULL,
    constraint Faculty_PK primary key (FID),
    constraint Faculty_FK1 foreign key (FID) references User_T(UserID),
    constraint Faculty_FK2 foreign key (DepartmentID) references Department_T(DepartmentID)
);


CREATE TABLE Student_T(
    SID INTEGER NOT NULL AUTO_INCREMENT,
    StudentID INTEGER NOT NULL,
    DateofBirth DATE,
    ProgramID INTEGER NOT NULL, 
    CONSTRAINT Student_PK PRIMARY KEY (SID),
    CONSTRAINT Student_FK1 FOREIGN KEY (SID) REFERENCES User_T(UserID),
    CONSTRAINT Student_FK2 FOREIGN KEY (ProgramID) REFERENCES Program_T(ProgramID)
);


CREATE TABLE PLO_T(
     PloID INTEGER NOT NULL AUTO_INCREMENT,
     PloName VARCHAR(6) NOT NULL,
     Details VARCHAR(255) NOT NULL,
     ProgramID INTEGER NOT NULL,
     CONSTRAINT PLO_PK PRIMARY KEY (PloID),
     CONSTRAINT PLO_FK FOREIGN KEY (ProgramID) REFERENCES Program_T(ProgramID)
);


CREATE TABLE Course_T(
     CourseID INTEGER NOT NULL AUTO_INCREMENT,
     CourseCode VARCHAR(8) NOT NULL,
     CourseName VARCHAR(255) NOT NULL,
     NumOfCredits INTEGER NOT NULL,
     ProgramID INTEGER NOT NULL,
     CoOfferedCourseID INTEGER DEFAULT NULL,
     CONSTRAINT Course_PK PRIMARY KEY (CourseID),
     CONSTRAINT Course_FK FOREIGN KEY (ProgramID) REFERENCES Program_T(ProgramID)
);


CREATE TABLE CO_T(
     CoID INTEGER NOT NULL AUTO_INCREMENT,
     CoName VARCHAR(6) NOT NULL,
     CourseID INTEGER NOT NULL,
     PloID INTEGER NOT NULL,
     Details VARCHAR(255) NOT NULL,
     CONSTRAINT CO_PK PRIMARY KEY (CoID),
     CONSTRAINT CO_FK1 FOREIGN KEY (CourseID) REFERENCES Course_T(CourseID),
     CONSTRAINT CO_FK2 FOREIGN KEY (PloID) REFERENCES PLO_T(PloID)
);


create table Section_T(
	SectionID int not null auto_increment,
    SectionNum int not null,
    Semester varchar(6) not null,
    `Year` int not null,
    MaximumCapacity int not null,
    PassMark float(5,2) default 40,
    FID int not null,
    CourseID int not null,
    constraint Section_PK primary key (SectionID),
    constraint Section_FK1 foreign key (FID) references Faculty_T(FID),
    constraint Section_FK2 foreign key (CourseID) references Course_T(CourseID)
);


CREATE TABLE PrereqCourse_T(
    CourseID INTEGER NOT NULL,
	PrereqID  INTEGER NOT NULL,
    CONSTRAINT PrereqCourse_PK PRIMARY KEY(CourseID,PrereqID),
    CONSTRAINT PrereqCourse_FK1 FOREIGN KEY (CourseID) REFERENCES Course_T(CourseID),
    CONSTRAINT PrereqCourse_FK2 FOREIGN KEY (PrereqID) REFERENCES Course_T(CourseID)
);


CREATE TABLE CourseRegistration_T(
     SID INTEGER NOT NULL,
     SectionID INTEGER NOT NULL,
     RegistrationDate DATE DEFAULT (DATE_FORMAT(NOW(), '%Y-%m-%d')),
     CONSTRAINT CourseRegistration_PK PRIMARY KEY (SID,SectionID),
     CONSTRAINT CourseRegistration_FK1 FOREIGN KEY (SID) REFERENCES Student_T(SID),
     CONSTRAINT CourseRegistration_FK2 FOREIGN KEY (SectionID) REFERENCES Section_T(SectionID)
);


CREATE TABLE Assessment_T(
    AssessmentID INTEGER NOT NULL AUTO_INCREMENT,
    QuestionNumber INTEGER NOT NULL,
    AssessmentType VARCHAR(50) NOT NULL,
    TotalMarks INTEGER NOT NULL,
    SectionID int not null,
    CoID INTEGER NOT NULL,
    CONSTRAINT Assessment_PK PRIMARY KEY (AssessmentID),
    CONSTRAINT Assessment_FK1 FOREIGN KEY (SectionID) REFERENCES Section_T(SectionID),
    CONSTRAINT Assessment_FK2 FOREIGN KEY (CoID) REFERENCES CO_T(CoID)
);


CREATE TABLE Evaluation_T(
    SID INTEGER NOT NULL,
    AssessmentID INTEGER NOT NULL,
    TotalObtainedMark FLOAT(5,2) NOT NULL,
    CONSTRAINT Evaluation_PK PRIMARY KEY (SID,AssessmentID),
    CONSTRAINT Evaluation_FK1 FOREIGN KEY (SID) REFERENCES Student_T(SID),
    CONSTRAINT Evaluation_FK2 FOREIGN KEY (AssessmentID) REFERENCES Assessment_T(AssessmentID)
);


-- CREATE TABLE Appointment_T(
--     FID INTEGER NOT NULL,
--     DepartmentID INTEGER NOT NULL,
--     HiringDate DATE,
--     FacultyID INTEGER NOT NULL,  
--     CONSTRAINT Appointment_PK PRIMARY KEY (FID,DepartmentID),
--     CONSTRAINT Appointment_FK1 FOREIGN KEY (FID) REFERENCES Faculty_T(FID),
--     CONSTRAINT Appointment_FK2 FOREIGN KEY (DepartmentID) REFERENCES Department_T(DepartmentID)
-- );

