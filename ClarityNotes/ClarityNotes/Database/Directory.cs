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
    private bool readOnly;

    public int Id => id;

    public bool ReadOnly => readOnly;
    public string Title => title;
    public int IdOwner => idOwner;
    public string CreationDate => creationDate;

    private Directory(int id, string title, int idOwner, string creationDate, bool ReadOnly = false)
    {
        this.id = id;
        this.title = title;
        this.idOwner = idOwner;
        this.creationDate = creationDate;
        this.readOnly = ReadOnly;
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
        using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                int id = Int32.Parse($"{reader["id"]}");
                string title = $"{reader["title"]}";
                int idOwner = Int32.Parse($"{reader["idOwner"]}");
                string creationDate = $"{reader["creationDate"]}";
                bool ReadOnly = reader["ReadOnly"].ToString() == "True";
                directories.Add(new Directory(id, title, idOwner, creationDate, ReadOnly));
            }
        }
        mySqlConnection.Close();
        return directories.ToArray();
    }

    public static void ChangeRootOwner(int idOldRoot, int idNewRoot, int idDirectory)
    {
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = $"UPDATE `directories` SET idOwner = {-1} WHERE id = {idDirectory} AND idOwner = {idOldRoot}";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.ExecuteNonQuery();
        mySqlConnection.Close();

        mySqlConnection = Database.GetConnection();
        query = $"UPDATE `directories` SET idOwner = {idOldRoot} WHERE id = {idDirectory} AND idOwner = {idNewRoot}";
        mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.ExecuteNonQuery();
        mySqlConnection.Close();

        mySqlConnection = Database.GetConnection();
        query = $"UPDATE `directories` SET idOwner = {idNewRoot} WHERE id = {idDirectory} AND idOwner = {-1}";
        mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.ExecuteNonQuery();
        mySqlConnection.Close();
    }


    public static void ChangeReadOnly(int idDirectory, User Owner)
    {
        MySqlConnection mySqlConnection = Database.GetConnection();
        int change = Directory.GetReadOnlyByDirectoryAndIdOwner(idDirectory, Owner) ? 0 : 1;
        string query = $"UPDATE `directories` SET ReadOnly = {change} WHERE id = {idDirectory} AND idOwner = {Owner.Id}";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.ExecuteNonQuery();
        mySqlConnection.Close();
    }

    public static Directory[] GetUserDirectories(User user)
    {
        List<Directory> directories = new List<Directory>();
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "SELECT * FROM `directories` WHERE idOwner = @idOwner";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.Parameters.AddWithValue("@idOwner", user.Id);
        mySqlCommand.ExecuteNonQuery();
        using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                int id = Int32.Parse($"{reader["id"]}");
                string title = $"{reader["title"]}";
                int idOwner = Int32.Parse($"{reader["idOwner"]}");
                string creationDate = $"{reader["creationDate"]}";
                bool ReadOnly = reader["ReadOnly"].ToString() == "True";
                directories.Add(new Directory(id, title, idOwner, creationDate, ReadOnly));
            }
        }
        mySqlConnection.Close();
        return directories.ToArray();
    }

    public static bool CreateDirectory(string title, User user)
    {
        if (GetDirectoryByTitleAndIdOwner(title, user) != null) return false;
        MySqlConnection mySqlConnection = Database.GetConnection();

        string queryMax = "SELECT COALESCE(MAX(id), 0) FROM directories";
        MySqlCommand mySqlCommandMax = new MySqlCommand(queryMax, mySqlConnection);
        mySqlCommandMax.ExecuteNonQuery();
        int resultMax = 0;
        using (MySqlDataReader reader = mySqlCommandMax.ExecuteReader())
        {
            while (reader.Read())
                resultMax = Int32.Parse($"{reader["COALESCE(MAX(id), 0)"]}");
        }
        string queryCreate = "INSERT INTO `directories` (id, title, idOwner, creationDate) VALUES(@id, @title, @idOwner, @creationDate)";
        MySqlCommand mySqlCommandCreate = new MySqlCommand(queryCreate, mySqlConnection);
        mySqlCommandCreate.Parameters.AddWithValue("@id", resultMax + 1);
        mySqlCommandCreate.Parameters.AddWithValue("@title", title);
        mySqlCommandCreate.Parameters.AddWithValue("@idOwner", user.Id);
        mySqlCommandCreate.Parameters.AddWithValue("@creationDate", Database.GetCurrentDate());
        bool result = mySqlCommandCreate.ExecuteNonQuery() > 0;
        mySqlConnection.Close();
        return result;
    }

    public static bool ShareDirectory(int idDirectory, User owner, string rep)
    {
        User shared;
        if (rep == null || rep == "") return false;
        if (rep.Contains("@"))
            shared = User.GetUserByMail(rep);
        else
            shared = User.GetUserByUsername(rep);
        if (shared == null) return false;

        string queryCreate = "INSERT INTO `directories` (id, title, idOwner, creationDate) VALUES(@id, @title, @idOwner, @creationDate)";
        MySqlConnection mySqlConnection = Database.GetConnection();
        MySqlCommand mySqlCommandCreate = new MySqlCommand(queryCreate, mySqlConnection);
        mySqlCommandCreate.Parameters.AddWithValue("@id", idDirectory);
        mySqlCommandCreate.Parameters.AddWithValue("@title", Directory.GetDirectoryByIdAndIdOwner(idDirectory, owner).Title);
        mySqlCommandCreate.Parameters.AddWithValue("@idOwner", shared.Id);
        mySqlCommandCreate.Parameters.AddWithValue("@creationDate", Database.GetCurrentDate());
        mySqlCommandCreate.ExecuteNonQuery();
        mySqlConnection.Close();
        return true;
    }


    public static int GetLastestDirectorybyUser(User user)
    {
        Directory[] list = Directory.GetUserDirectories(user);
        if (list.Count() == 0)
            return -1;
        return list.Last().Id;
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

    public static bool GetReadOnlyByNoteAndIdOwner(Note note, User user)
    {
        Directory[] list = GetUserDirectories(user);
        foreach (Directory directory in list)
            if (directory.Id == note.IdDirectory && directory.idOwner == user.Id) return directory.ReadOnly;
        return false;
    }

    public static bool GetReadOnlyByDirectoryAndIdOwner(int idDirectory, User user)
    {
        Directory[] list = GetUserDirectories(user);
        foreach (Directory directory in list)
            if (directory.Id == idDirectory && directory.idOwner == user.Id) return directory.ReadOnly;
        return false;
    }

    public static Directory GetDirectoryByIdAndIdOwner(int id, User user)
    {
        foreach (Directory directory in GetUserDirectories(user))
            if (directory.Id == id) return directory;
        return null;
    }

    public static User[] GetUsersByDirectory(int idDirectory)
    {
        List<User> final = new List<User>();

        foreach (Directory dir in GetAllDirectories())
        {
            if (dir.id == idDirectory)
                final.Add(User.GetUserById(dir.idOwner));
        }
        return final.ToArray();
    }

    public static Directory GetDirectoryByTitleAndIdOwner(string title, User user)
    {
        foreach (Directory directory in GetUserDirectories(user))
            if (directory.Title == title) return directory;
        return null;
    }
}