CREATE Table Guardian_T
(
	GID int not null,
	CONSTRAINT Guardian_PK primary key (GID)
);

CREATE Table GuardianChild_T
(
	GID int not null,
	SID int not null,
	constraint GuardianChild_PK primary key (GID,SID),
	constraint GuardianChild_FK1 foreign key (GID) references Guardian_T(GID),
	constraint GuardianChild_FK2 foreign key (SID) references Student_T(SID)
);

insert into User_T (FName,LName, LoginPass, Email) values
('Borr', 'Burison', '123456', 'borr@asgardmail.com'),
('Zisa', 'Will', '123456', 'meili@lamentismail.com');

insert into Guardian_T values (840),(841);

insert into GuardianChild_T (GID,SID) values
(840, 658),
(840, 178),
(840, 100),
(841,748),
(841,78),
(841,277);

























