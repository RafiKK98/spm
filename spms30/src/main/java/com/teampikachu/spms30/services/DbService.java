package com.teampikachu.spms30.services;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;

public class DbService {
    private static DbService _dbService;

    private Connection conn;
    private Statement stmt;

    private DbService() {
        try {
            Class.forName("com.mysql.cj.jdbc.Driver");
            this.conn = DriverManager.getConnection("jdbc:mysql://localhost:3306/spms30db", "teampikachu", "wearegroup1");
            stmt = conn.createStatement();
        } catch (ClassNotFoundException cnf) {

        } catch (SQLException se) {

        }
    }

    public static DbService get() {
        if (_dbService == null) {
            _dbService = new DbService();
        }

        return _dbService;
    }

    public Statement getStatement() {
        return stmt;
    }
}
