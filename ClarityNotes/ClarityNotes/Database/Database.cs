using MySql.Data.MySqlClient;
using System;

public static class Database
{
    private static string ip = "localhost"; // 83.150.217.50
    private static string database = "clarity_notes";
    private static string username = "root";
    private static string password = ""; // tAP4kN4SLEpita

    public static MySqlConnection GetConnection()
    {
        string parameters = $"Data Source={ip};Initial Catalog={database};User id={username};Password={password};";
        MySqlConnection mySqlConnection = new MySqlConnection();
        mySqlConnection.ConnectionString = parameters;
        mySqlConnection.Open();
        return mySqlConnection;
    }

    public static string GetCurrentDate()
    {
        return DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
    }
}