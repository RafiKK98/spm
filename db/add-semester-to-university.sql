ALTER table University_T
add column SemesterName VARCHAR(6) default 'Spring';

ALTER table University_T
add column `Year` INT default 1;

UPDATE University_T 
set SemesterName = 'Summer', `Year`=2006;