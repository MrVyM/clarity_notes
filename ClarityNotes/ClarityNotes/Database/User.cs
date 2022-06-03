using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;

public class User
{
    private int id;
    private string username;
    private string email;
    private string hashPassword;
    private bool premium;
    private Color colorTheme;

    public int Id => id;
    public string Username => username;
    public string Email => email;
    public string HashPassword => hashPassword;
    public bool Premium => premium;
    public Color ColorTheme => colorTheme;

    private User(int id, string username, string email, string hashPassword, bool premium, Color colorTheme)
    {
        this.id = id;
        this.username = username;
        this.email = email;
        this.hashPassword = hashPassword;
        this.premium = premium;
        this.colorTheme = colorTheme;
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
        string query = "SELECT * FROM users INNER JOIN usersettings ON usersettings.userId = users.id";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.ExecuteNonQuery();
        using (MySqlDataReader reader = mySqlCommand.ExecuteReader()) {
            while (reader.Read())
            {
                int id = Int32.Parse($"{reader["id"]}");
                string username = $"{reader["username"]}";
                string email = $"{reader["email"]}";
                string hashPassword = $"{reader["hashPassword"]}";
                bool premium = reader["premium"].ToString() == "True";
                Color colorTheme = Color.FromHex($"{reader["colorTheme"]}");
                users.Add(new User(id, username, email, hashPassword, premium, colorTheme));
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
        var test = GetAllUsers();
        foreach (User user in  test) 
            if (user.Username == username || user.Email == email) return false;
        MySqlConnection mySqlConnection = Database.GetConnection();

        string queryUser = "INSERT INTO `users` (username, email, hashPassword) VALUES(@username, @email, @hashPassword)";
        MySqlCommand mySqlCommandUser = new MySqlCommand(queryUser, mySqlConnection);
        mySqlCommandUser.Parameters.AddWithValue("@username", username);
        mySqlCommandUser.Parameters.AddWithValue("@email", email);
        mySqlCommandUser.Parameters.AddWithValue("@hashPassword", GetHashedPassword(password));
        bool resultUser = mySqlCommandUser.ExecuteNonQuery() > 0;

        string querySettings = "INSERT INTO `usersettings` (colorTheme) VALUES(@colorTheme)";
        MySqlCommand mySqlCommandSettings = new MySqlCommand(querySettings, mySqlConnection);
        mySqlCommandSettings.Parameters.AddWithValue("@colorTheme", "57b1eb");
        bool resultSettings = mySqlCommandSettings.ExecuteNonQuery() > 0;
        mySqlConnection.Close();
        return resultUser && resultSettings;
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
        SHA256 sha256 = SHA256.Create();
        byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        StringBuilder sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
            sBuilder.Append(data[i].ToString("x2"));
        return sBuilder.ToString();
    }

    public void UpdateColorTheme(Color color)
    {
        this.colorTheme = color;
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "UPDATE `usersettings` SET colorTheme = @colorTheme WHERE userId = @userId";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.Parameters.AddWithValue("@colorTheme", color.ToHex());
        mySqlCommand.Parameters.AddWithValue("@userId", id);
        mySqlCommand.ExecuteNonQuery();
        mySqlConnection.Close();
    }
}