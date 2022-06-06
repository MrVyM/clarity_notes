using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;

public class Note
{
    private int id;
    private int idDirectory;
    private string title;
    private string content;
    private string creationDate;
    private string updateDate;
    private int idCreator;
    private int idUpdater;

    public int Id => id;
    public int IdDirectory => idDirectory;
    public string Title => title;
    public string Content => content;
    public string CreationDate => creationDate;
    public string UpdateDate => updateDate;
    public int IdCreator => idCreator;
    public int IdUpdater => idUpdater;

    private Note(int id, int idDirectory, string title, string content, string creationDate, string updateDate, int idCreator, int idUpdater)
    {
        this.id = id;
        this.idDirectory = idDirectory;
        this.title = title;
        this.content = content;
        this.creationDate = creationDate;
        this.updateDate = updateDate;
        this.idCreator = idCreator;
        this.idUpdater = idUpdater;
    }

    public override string ToString()
    {
        string result = $"[{GetType().Name}]\n";
        foreach (var p in GetType().GetProperties()
                     .Where(p => !p.GetGetMethod().GetParameters().Any()))
            result += $"    {p.Name} : {p.GetValue(this, null)}\n";
        return result.Substring(0, result.Length - 1);
    }

    public static Note[] GetAllNotes()
    {
        List<Note> notes = new List<Note>();
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "SELECT * FROM `notes`";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.ExecuteNonQuery();
        using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                int id = Int32.Parse($"{reader["id"]}");
                int idDirectory = Int32.Parse($"{reader["idDirectory"]}");
                string title = $"{reader["title"]}";
                string content = $"{reader["content"]}";
                string creationDate = $"{reader["creationDate"]}";
                string updateDate = $"{reader["updateDate"]}";
                int idCreator = Int32.Parse($"{reader["idCreator"]}");
                int idUpdater = Int32.Parse($"{reader["idUpdater"]}");
                notes.Add(new Note(id, idDirectory, title, content, creationDate, updateDate, idCreator, idUpdater));
            }
        }
        mySqlConnection.Close();
        return notes.ToArray();
    }

    public static Note GetNoteById(int id)
    {
        foreach (Note note in GetAllNotes())
            if (note.Id == id) return note;
        return null;
    }

    public static Note GetNoteByTitleAndIdDirectory(string title, int idDirectory)
    {
        foreach (Note note in GetAllNotes())
            if (note.title == title && note.IdDirectory == idDirectory) return note;
        return null;
    }

    public static Note[] GetNotesByIdDirectory(int idDirectory)
    {
        List<Note> notes = new List<Note>();
        foreach (Note note in GetAllNotes())
            if (note.idDirectory == idDirectory) notes.Add(note);
        return notes.ToArray();
    }

    public static bool CreateNote(string title, int idDirectory, User user)
    {
        if (GetNoteByTitleAndIdDirectory(title, idDirectory) != null) return false;
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "INSERT INTO `notes` (idDirectory, title, content, creationDate, updateDate, "
                       + "idCreator, idUpdater) VALUES(@idDirectory, @title, @content,"
                       + " @creationDate, @updateDate, @idCreator, @idUpdater)";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.Parameters.AddWithValue("@idDirectory", idDirectory);
        mySqlCommand.Parameters.AddWithValue("@title", title);
        mySqlCommand.Parameters.AddWithValue("@content", "");
        mySqlCommand.Parameters.AddWithValue("@creationDate", Database.GetCurrentDate());
        mySqlCommand.Parameters.AddWithValue("@updateDate", Database.GetCurrentDate());
        mySqlCommand.Parameters.AddWithValue("@idCreator", user.Id);
        mySqlCommand.Parameters.AddWithValue("@idUpdater", user.Id);
        bool result = mySqlCommand.ExecuteNonQuery() > 0;
        mySqlConnection.Close();
        return result;
    }

    public static bool DeleteNote(int id)
    {
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "DELETE FROM `notes` WHERE id = @id";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.Parameters.AddWithValue("@id", id);
        bool result = mySqlCommand.ExecuteNonQuery() > 0;
        mySqlConnection.Close();
        return result;
    }

    public bool Update(string newContent, User user)
    {
        if (content == newContent) return false;
        content = newContent;
        updateDate = Database.GetCurrentDate();
        idUpdater = user.Id;
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "UPDATE `notes` SET content = @content, updateDate = @updateDate, idUpdater = @idUpdater WHERE id = @id";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.Parameters.AddWithValue("@content", content);
        mySqlCommand.Parameters.AddWithValue("@updateDate", updateDate);
        mySqlCommand.Parameters.AddWithValue("@idUpdater", idUpdater);
        mySqlCommand.Parameters.AddWithValue("@id", id);
        bool result = mySqlCommand.ExecuteNonQuery() > 0;
        mySqlConnection.Close();
        return result;
    }
}