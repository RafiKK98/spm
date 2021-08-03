package com.teampikachu.spms30.controllers;

import com.teampikachu.spms30.models.LoginCredentials;
import com.teampikachu.spms30.models.SignUpCredentials;
import com.teampikachu.spms30.models.User;

import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class LoginPage {
    @RequestMapping("/")
    public String index() {
        return "HELLO WORLD";
    }

    @RequestMapping(method = RequestMethod.POST, value = "/login")
    public User login(@RequestBody LoginCredentials loginCredentials) {
        System.out.println(loginCredentials);
        
        return User.getUser(loginCredentials);
    }

    @RequestMapping(method = RequestMethod.POST, value = "/signup")
    public boolean signUp(@RequestBody SignUpCredentials signUpCredentials) {
        return User.addUser(signUpCredentials);
    }
}
