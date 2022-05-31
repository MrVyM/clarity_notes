using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;

public class Note
{
    private int id;
    private string title;
    private int idDirectory;
    private string content;
    private string creationDate;
    private string updateDate;
    private int creationIdAuthor;
    private int updateIdAuthor;

    public int Id => id;
    public string Title => title;
    public int IdDirectory => idDirectory;
    public string Content => content;
    public string CreationDate => creationDate;
    public string UpdateDate => updateDate;
    public int CreationIdAuthor => creationIdAuthor;
    public int UpdateIdAuthor => updateIdAuthor;
    
    private Note(int id, string title, int idDirectory, string content, string creationDate,
        string updateDate, int creationIdAuthor, int updateIdAuthor)
    {
        this.id = id;
        this.title = title;
        this.idDirectory = idDirectory;
        this.content = content;
        this.creationDate = creationDate;
        this.updateDate = updateDate;
        this.creationIdAuthor = creationIdAuthor;
        this.updateIdAuthor = updateIdAuthor;
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
        using (MySqlDataReader reader = mySqlCommand.ExecuteReader()) {
            while (reader.Read())
            {
                int id = Int32.Parse($"{reader["id"]}");
                string title = $"{reader["title"]}";
                int idDirectory = Int32.Parse($"{reader["id_directory"]}");
                string content = $"{reader["content"]}";
                string creationDate = $"{reader["creation_date"]}";
                string updateDate = $"{reader["update_date"]}";
                int creationIdAuthor = Int32.Parse($"{reader["creation_id_author"]}");
                int updateIdAuthor = Int32.Parse($"{reader["update_id_author"]}");
                notes.Add(new Note(id, title, idDirectory, content, creationDate, updateDate, creationIdAuthor, updateIdAuthor));
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
    
    public static Note GetNoteByTitle(string title)
    {
        foreach (Note note in GetAllNotes()) 
            if (note.title == title) return note;
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
        if (GetNoteByTitle(title) != null) return false;
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "INSERT INTO `notes` (title, id_directory, content, creation_date, update_date, " 
                       + "creation_id_author, update_id_author) VALUES(@title, @idDirectory, @content," 
                       + " @creationDate, @updateDate, @creationIdAuthor, @updateIdAuthor)";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.Parameters.AddWithValue("@title", title);
        mySqlCommand.Parameters.AddWithValue("@idDirectory", idDirectory);
        mySqlCommand.Parameters.AddWithValue("@content", "");
        mySqlCommand.Parameters.AddWithValue("@creationDate", Database.GetCurrentDate());
        mySqlCommand.Parameters.AddWithValue("@updateDate", Database.GetCurrentDate());
        mySqlCommand.Parameters.AddWithValue("@creationIdAuthor", user.Id);
        mySqlCommand.Parameters.AddWithValue("@updateIdAuthor", user.Id);
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
        updateIdAuthor = user.Id;
        MySqlConnection mySqlConnection = Database.GetConnection();
        string query = "UPDATE `notes` SET content = @content, update_date = @updateDate, update_id_author = @updateIdAuthor WHERE id = @id";
        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
        mySqlCommand.Parameters.AddWithValue("@content", content);
        mySqlCommand.Parameters.AddWithValue("@updateDate", updateDate);
        mySqlCommand.Parameters.AddWithValue("@updateIdAuthor", updateIdAuthor);
        mySqlCommand.Parameters.AddWithValue("@id", id);
        bool result = mySqlCommand.ExecuteNonQuery() > 0;
        mySqlConnection.Close();
        return result;
    }
}