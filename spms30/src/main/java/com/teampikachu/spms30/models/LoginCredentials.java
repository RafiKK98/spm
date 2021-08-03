package com.teampikachu.spms30.models;

public class LoginCredentials {
    private String userName;
    private String passKey;

    public LoginCredentials(String userName, String passKey) {
        this.userName = userName;
        this.passKey = passKey;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public String getPassKey() {
        return passKey;
    }

    public void setPassKey(String passKey) {
        this.passKey = passKey;
    }

    @Override
    public String toString() {
        return userName + " " + passKey;
    }
}
