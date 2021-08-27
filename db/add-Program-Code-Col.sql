use spmsdb;

ALTER TABLE spmsdb.Program_T ADD ProgramCode VARCHAR(6) NOT NULL;

UPDATE spmsdb.Program_T
SET ProgramName='Electrical and Telecommunication Engineering', TotalNumCredits=120, DepartmentID=1, ProgramCode='ETE'
WHERE ProgramID=1;
UPDATE spmsdb.Program_T
SET ProgramName='Electrical and Electronic Engineering', TotalNumCredits=120, DepartmentID=1, ProgramCode='EEE'
WHERE ProgramID=2;
UPDATE spmsdb.Program_T
SET ProgramName='Computer Engineering', TotalNumCredits=120, DepartmentID=2, ProgramCode='CEN'
WHERE ProgramID=3;
UPDATE spmsdb.Program_T
SET ProgramName='Computer Science and Engineering', TotalNumCredits=120, DepartmentID=2, ProgramCode='CSE'
WHERE ProgramID=4;
UPDATE spmsdb.Program_T
SET ProgramName='English Literature', TotalNumCredits=120, DepartmentID=3, ProgramCode='ENG'
WHERE ProgramID=5;
UPDATE spmsdb.Program_T
SET ProgramName='English Language Teaching', TotalNumCredits=120, DepartmentID=3, ProgramCode='ELT'
WHERE ProgramID=6;
UPDATE spmsdb.Program_T
SET ProgramName='Anthropology', TotalNumCredits=120, DepartmentID=4, ProgramCode='ANT'
WHERE ProgramID=7;
UPDATE spmsdb.Program_T
SET ProgramName='Sociology', TotalNumCredits=120, DepartmentID=4, ProgramCode='SOC'
WHERE ProgramID=8;
UPDATE spmsdb.Program_T
SET ProgramName='Electrical and Telecommunication Engineering', TotalNumCredits=120, DepartmentID=5, ProgramCode='ETE'
WHERE ProgramID=9;
UPDATE spmsdb.Program_T
SET ProgramName='Electrical and Electronic Engineering', TotalNumCredits=120, DepartmentID=5, ProgramCode='EEE'
WHERE ProgramID=10;
UPDATE spmsdb.Program_T
SET ProgramName='Computer Engineering', TotalNumCredits=120, DepartmentID=6, ProgramCode='CEN'
WHERE ProgramID=11;
UPDATE spmsdb.Program_T
SET ProgramName='Computer Science and Engineering', TotalNumCredits=120, DepartmentID=6, ProgramCode='CSE'
WHERE ProgramID=12;
UPDATE spmsdb.Program_T
SET ProgramName='English Literature', TotalNumCredits=120, DepartmentID=7, ProgramCode='ENG'
WHERE ProgramID=13;
UPDATE spmsdb.Program_T
SET ProgramName='English Language Teaching', TotalNumCredits=120, DepartmentID=7, ProgramCode='ELT'
WHERE ProgramID=14;
UPDATE spmsdb.Program_T
SET ProgramName='Anthropology', TotalNumCredits=120, DepartmentID=8, ProgramCode='ANT'
WHERE ProgramID=15;
UPDATE spmsdb.Program_T
SET ProgramName='Sociology', TotalNumCredits=120, DepartmentID=8, ProgramCode='SOC'
WHERE ProgramID=16;

