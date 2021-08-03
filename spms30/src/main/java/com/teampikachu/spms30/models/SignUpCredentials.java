package com.teampikachu.spms30.models;

public class SignUpCredentials {
    private String username;
    private String passkey;
    private String fname;
    private String lname;
    private int age;

    public SignUpCredentials(String username, String passkey, String fname, String lname, int age) {
        this.username = username;
        this.passkey = passkey;
        this.fname = fname;
        this.lname = lname;
        this.age = age;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPasskey() {
        return passkey;
    }

    public void setPasskey(String passkey) {
        this.passkey = passkey;
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
}
