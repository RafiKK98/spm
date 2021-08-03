package com.teampikachu.spms30.models;

import java.sql.ResultSet;
import java.sql.SQLException;

import com.teampikachu.spms30.services.DbService;

public class User {
    private String fname;
    private String lname;
    private int age;

    public User() {
    }

    public User(String fname, String lname, int age) {
        this.fname = fname;
        this.lname = lname;
        this.age = age;
    }

    public String getFname() {
        return fname;
    }

    public void setFname(String fname) {
        this.fname = fname;
    }

    public String getLname() {
        return lname;
    }

    public void setLname(String lname) {
        this.lname = lname;
    }

    public int getAge() {
        return age;
    }

    public void setAge(int age) {
        this.age = age;
    }

    public static User getUser(LoginCredentials cred) {
        DbService dbService = DbService.get();

        try {
            ResultSet resultSet = dbService.getStatement().executeQuery(
                    "select fname,lname,age from User_T inner join Login_T on User_T.user_id=Login_T.user_id where username=\"" + cred.getUserName() + "\" and passkey=\"" + cred.getPassKey() + "\";");

            if (resultSet.next()) {
                // System.out.println("hello");

                User user = new User();
                user.setFname(resultSet.getString("fname"));
                user.setLname(resultSet.getString("lname"));
                user.setAge(resultSet.getInt("age"));

                return user;
            }
        } catch (SQLException ex) {
            ex.printStackTrace();
        }

        return null;
    }

    public static boolean addUser(SignUpCredentials signUpCredentials) {
        DbService dbService = DbService.get();
        int numberOfUser;

        try {
            ResultSet resultSet = dbService.getStatement().executeQuery("Select Count(*) from User_T;");
            if (resultSet.next()) {
                numberOfUser = resultSet.getInt(1);

                return dbService.getStatement().execute(String.format("insert into User_T values(%d, '%s', '%s', %d);", numberOfUser + 1, signUpCredentials.getFname(), signUpCredentials.getLname(), signUpCredentials.getAge())) &&
                    dbService.getStatement().execute(String.format("insert into Login_T values('%s','%s', %d);", signUpCredentials.getUsername(), signUpCredentials.getPasskey(), numberOfUser + 1));
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }

        return false;
    }
}
