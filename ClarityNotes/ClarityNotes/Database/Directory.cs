using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;

public class Directory
{
    private int id;
    private string title;
    private int idOwner;
    private string creationDate;

    public int Id => id;
    public string Title => title;
    public int IdOwner => idOwner;
    public string CreationDate => creationDate;

    private Directory(int id, string title, int idOwner, string creationDate)
    {
        this.id = id;
        this.title = title;
        this.idOwner = idOwner;
        this.creationDate = creationDate;
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
                int idOwner = Int32.Parse($"{reader["idOwner"]}");
                string creationDate = $"{reader["creationDate"]}";
                directories.Add(new Directory(id, title, idOwner, creationDate));
            }
        }
        mySqlConnection.Close();
        return directories.ToArray();
    }

    public static bool CreateDirectory(string title, User user)
    {
        if (GetDirectoryByTitleAndIdOwner(title, user.Id) != null) return false;
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "INSERT INTO `directories` (id, title, idOwner, creationDate) VALUES(@id, @title, @idOwner, @creationDate)";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.Parameters.AddWithValue("@id", 0);
        mySqlCommand.Parameters.AddWithValue("@title", title);
        mySqlCommand.Parameters.AddWithValue("@idOwner", user.Id);
        mySqlCommand.Parameters.AddWithValue("@creationDate", Database.GetCurrentDate());
        bool result = mySqlCommand.ExecuteNonQuery() > 0;
        mySqlConnection.Close();
        return result;
    }

    public static bool DeleteDirectory(int id, User user)
    {
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "DELETE FROM `directories` WHERE id = @id AND idOwner = @idOwner";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.Parameters.AddWithValue("@id", id);
        mySqlCommand.Parameters.AddWithValue("@idOwner", user.Id);
        bool result = mySqlCommand.ExecuteNonQuery() > 0;
        mySqlConnection.Close();
        return result;
    }
    
    public static Directory GetDirectoryByIdAndIdOwner(int id, int idOwner)
    {
        foreach (Directory directory in GetAllDirectories()) 
            if (directory.Id == id && directory.IdOwner == idOwner) return directory;
        return null;
    }
    
    public static Directory GetDirectoryByTitleAndIdOwner(string title, int idOwner)
    {
        foreach (Directory directory in GetAllDirectories()) 
            if (directory.Title == title && directory.IdOwner == idOwner) return directory;
        return null;
    }
}