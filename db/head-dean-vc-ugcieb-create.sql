use spmsdb;

create table DepartmentHead_T(
	HID int not null,
    DepartmentID INTEGER NOT NULL,
    HiringDate DATE,
    HeadID INTEGER NOT NULL,
    constraint DepartmentHead_PK primary key (HID),
    constraint DepartmentHead_FK1 foreign key (HID) references User_T(UserID),
    constraint DepartmentHead_FK2 foreign key (DepartmentID) references Department_T(DepartmentID)
);


INSERT INTO User_T (UserID, FName, LName, LoginPass, Email) VALUES
(825, "Skylar", "Palmer", "123456", "9841@uni1.edu.bd"),
(826, "Tiffany", "Bradley", "123456", "9842@uni1.edu.bd"),
(827, "Carter", "Austin", "123456", "9843@uni1.edu.bd"),
(828, "Arden", "Burns", "123456", "9844@uni1.edu.bd"),
(829, "Colton", "Perez", "123456", "9845@uni2.edu.bd"),
(830, "Adelyn", "Bryan", "123456", "9846@uni2.edu.bd"),
(831, "Davian", "Zimmerman", "123456", "9847@uni2.edu.bd"),
(832, "Anna", "Ho", "123456", "9848@uni2.edu.bd");


INSERT INTO DepartmentHead_T (HID, DepartmentID,HeadID) VALUES
(825, 1, 9841),
(826, 2, 9842),
(827, 3, 9843),
(828, 4, 9844),
(829, 5, 9845),
(830, 6, 9846),
(831, 7, 9847),
(832, 8, 9848);


create table SchoolDean_T(
	DID int not null,
    SchoolID INTEGER NOT NULL,
    HiringDate DATE,
    DeanID INTEGER NOT NULL,
    constraint SchoolDean_PK primary key (DID),
    constraint SchoolDean_FK1 foreign key (DID) references User_T(UserID),
    constraint SchoolDean_FK2 foreign key (SchoolID) references School_T(SchoolID)
);


INSERT INTO User_T (UserID, FName, LName, LoginPass, Email) VALUES
(833, "Brenden", "Sweeney", "123456", "9849@uni1.edu.bd"),
(834, "Avah", "Smith", "123456", "9850@uni1.edu.bd"),
(835, "Boden", "Patton", "123456", "9851@uni2.edu.bd"),
(836, "Borat", "Sagdiyev", "123456", "9852@uni2.edu.bd");


INSERT INTO SchoolDean_T (DID, SchoolID, DeanID) VALUES
(833, 1, 9849),
(834, 2, 9850),
(835, 3, 9851),
(836, 4, 9852);


create table VC_T(
	VID int not null,
    UniversityID INTEGER NOT NULL,
    HiringDate DATE,
    VcID INTEGER NOT NULL,
    constraint VC_PK primary key (VID),
    constraint VC_FK1 foreign key (VID) references User_T(UserID),
    constraint VC_FK2 foreign key (UniversityID) references University_T(UniversityID)
);

INSERT INTO User_T (UserID, FName, LName, LoginPass, Email) VALUES
(837, "Johnny", "Sharp", "123456", "9853@uni1.edu.bd"),
(838, "Melanie", "Durham", "123456", "9854@uni1.edu.bd");

INSERT INTO VC_T (VID, UniversityID, VcID) VALUES
(837, 1, 9853),
(838, 2, 9854);


create table UGCIEB_T(
	UIID int not null,
    UgcIebID INTEGER NOT NULL,
    constraint UGCIEB_PK primary key (UIID),
    constraint UGCIEB_FK1 foreign key (UIID) references User_T(UserID)
);

INSERT INTO User_T (UserID, FName, LName, LoginPass, Email) VALUES
(839, "Karsyn", "Murphy", "123456", "9855@uni1.edu.bd");

INSERT INTO UGCIEB_T VALUES (839, 9855);