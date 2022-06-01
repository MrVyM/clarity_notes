using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;

public class User
{
    private int id;
    private string username;
    private string email;
    private string hashPassword;
    private bool premium;

    public int Id => id;
    public string Username => username;
    public string Email => email;
    public string HashPassword => hashPassword;
    public bool Premium => premium;

    private User(int id, string username, string email, string hashPassword, bool premium)
    {
        this.id = id;
        this.username = username;
        this.email = email;
        this.hashPassword = hashPassword;
        this.premium = premium;
    }

    public override string ToString()
    {
        string result = $"[{GetType().Name}]\n";
        foreach (var p in GetType().GetProperties()
                     .Where(p => !p.GetGetMethod().GetParameters().Any()))
            result += $"    {p.Name} : {p.GetValue(this, null)}\n";
        return result.Substring(0, result.Length - 1);
    }

    public static User[] GetAllUsers()
    {
        List<User> users = new List<User>();
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "SELECT * FROM `users`";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.ExecuteNonQuery();
        using (MySqlDataReader reader = mySqlCommand.ExecuteReader()) {
            while (reader.Read())
            {
                int id = Int32.Parse($"{reader["id"]}");
                string username = $"{reader["username"]}";
                string email = $"{reader["email"]}";
                string hashPassword = $"{reader["hashPassword"]}";
                bool premium = $"{reader["premium"]}" == "1";
                users.Add(new User(id, username, email, hashPassword, premium));
            }
        }
        mySqlConnection.Close();
        return users.ToArray();
    }

    public static void Change(int id, string field, string value)
    {
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "UPDATE `users` SET " + field + " = @value WHERE id = @id";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.Parameters.AddWithValue("@value", value);
        mySqlCommand.Parameters.AddWithValue("@id", id);
        mySqlCommand.ExecuteNonQuery();
        mySqlConnection.Close();
    }

    public static bool CreateUser(string email, string username, string password)
    {
        foreach (User user in GetAllUsers()) 
            if (user.Username == username || user.Email == email) return false;
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "INSERT INTO `users` (username, email, hashPassword) VALUES(@username, @email, @hashPassword)";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.Parameters.AddWithValue("@username", username);
        mySqlCommand.Parameters.AddWithValue("@email", email);
        mySqlCommand.Parameters.AddWithValue("@hashPassword", GetHashedPassword(password));
        bool result = mySqlCommand.ExecuteNonQuery() > 0;
        mySqlConnection.Close();
        return result;
    }
    
    public static bool DeleteUser(int id)
    {
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "DELETE FROM `users` WHERE id = @id";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.Parameters.AddWithValue("@id", id);
        bool result = mySqlCommand.ExecuteNonQuery() > 0;
        mySqlConnection.Close();
        return result;
    }
    
    public static User GetUserById(int id)
    {
        foreach (User user in GetAllUsers()) 
            if (user.Id == id) return user;
        return null;
    }

    public static User Connexion(string username, string password)
    {
        foreach (User user in GetAllUsers())
            if (user.username == username && user.hashPassword == GetHashedPassword(password)) return user;
        return null;
    }

    public static string GetHashedPassword(string password)
    {
        return password;
    }
}