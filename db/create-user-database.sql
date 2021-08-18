-- database name: spmsdb
-- username: spms
-- password: 


create database spmsdb;
create user 'spms'@'localhost';
grant all on spmsdb.* to 'spms'@'localhost';