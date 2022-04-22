using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

public class Directory
{
    private int id;
    private string title;
    private string creationDate;
    private int creationIdAuthor;

    public int Id => id;
    public string Title => title;
    public string CreationDate => creationDate;
    public int CreationIdAuthor => creationIdAuthor;
    
    private Directory(int id, string title, string creationDate, int creationIdAuthor)
    {
        this.id = id;
        this.title = title;
        this.creationDate = creationDate;
        this.creationIdAuthor = creationIdAuthor;
    }
    
    public override string ToString()
    {
        string result = $"[{GetType().Name}]\n";
        foreach (var p in GetType().GetProperties()
                     .Where(p => !p.GetGetMethod().GetParameters().Any()))
            result += $"    {p.Name} : {p.GetValue(this, null)}\n";
        return result.Substring(0, result.Length - 1);
    }
    
    public static Directory[] GetAllDirectories()
    {
        List<Directory> directories = new List<Directory>();
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "SELECT * FROM `directories`";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.ExecuteNonQuery();
        using (MySqlDataReader reader = mySqlCommand.ExecuteReader()) {
            while (reader.Read())
            {
                int id = Int32.Parse($"{reader["id"]}");
                string title = $"{reader["title"]}";
                string creationDate = $"{reader["creation_date"]}";
                int creationIdAuthor = Int32.Parse($"{reader["creation_id_author"]}");
                directories.Add(new Directory(id, title, creationDate, creationIdAuthor));
            }
        }
        mySqlConnection.Close();
        return directories.ToArray();
    }

    public static bool CreateDirectory(string title, User user)
    {
        if (GetDirectoryByTitle(title) != null) return false;
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "INSERT INTO `directories` (title, creation_date, creation_id_author) VALUES(@title, @date, @id)";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.Parameters.AddWithValue("@title", title);
        mySqlCommand.Parameters.AddWithValue("@date", Database.GetCurrentDate());
        mySqlCommand.Parameters.AddWithValue("@id", user.Id);
        bool result = mySqlCommand.ExecuteNonQuery() > 0;
        mySqlConnection.Close();
        return result;
    }

    public static bool DeleteDirectory(int id)
    {
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "DELETE FROM `directories` WHERE id = @id";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.Parameters.AddWithValue("@id", id);
        bool result = mySqlCommand.ExecuteNonQuery() > 0;
        mySqlConnection.Close();
        return result;
    }
    
    public static Directory GetDirectoryById(int id)
    {
        foreach (Directory directory in GetAllDirectories()) 
            if (directory.Id == id) return directory;
        return null;
    }
    
    public static Directory GetDirectoryByTitle(string title)
    {
        foreach (Directory directory in GetAllDirectories()) 
            if (directory.Title == title) return directory;
        return null;
    }
}