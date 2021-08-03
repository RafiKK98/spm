use spms30db;

drop table User_T;

create table User_T(
user_id integer not null,
fname varchar(255),
lname varchar(255),
age integer,
constraint PK_User primary key (user_id)
);

show tables;

insert into User_T values(1, 'Mahfuzur', 'Rahman', 22);
insert into User_T values(2, 'MD. Hasibul', 'Haque', 22);

select * from User_T;

drop table Login_T;

create table Login_T(
username varchar(255) not null,
passkey varchar(255) not null,
user_id integer not null,
constraint PK_Login primary key(username),
constraint FK_user_id foreign key (user_id) references User_T(user_id)
);

insert into Login_T values('mahfuz226','password1', 1);
insert into Login_T values('hasib226','password2', 2);

select * from Login_T;

select fname,lname,age from User_T
inner join Login_T on User_T.user_id=Login_T.user_id
where username="mahfuz226" and passkey="password1";


