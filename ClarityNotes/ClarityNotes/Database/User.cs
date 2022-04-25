using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

public class User
{
    private int id;
    private string email;
    private string username;
    private string password;

    public int Id => id;
    public string Email => email;
    public string Username => username;
    public string Password => password;

    private User(int id, string email, string username, string password)
    {
        this.id = id;
        this.email = email;
        this.username = username;
        this.password = password;
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
                string email = $"{reader["email"]}";
                string name = $"{reader["username"]}";
                string password = $"{reader["password"]}";
                users.Add(new User(id, email, name, password));
            }
        }
        mySqlConnection.Close();
        return users.ToArray();
    }

    public static void Change(int id, string arg, string value)
    {
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "UPDATE `users` SET "+arg+" = @value WHERE id = @id";
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
        string query = "INSERT INTO `users` (username, email, password) VALUES(@username, @email, @password)";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.Parameters.AddWithValue("@username", username);
        mySqlCommand.Parameters.AddWithValue("@email", email);
        mySqlCommand.Parameters.AddWithValue("@password", password);
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
            if (user.username == username && user.password == password) return user;
        return null;
    }
}