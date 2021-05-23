using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System;

public class ActyvityRepository
{
    private SqliteConnection connection;
    public ActyvityRepository(SqliteConnection connection)
    {
        this.connection = connection;
    }
    public Activity GetById(long id)
    {
        this.connection.Open(); 

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT * FROM activitys WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
    
        SqliteDataReader reader = command.ExecuteReader();
        Activity pr = new Activity();
        if (reader.Read())
        {   
            pr.id = long.Parse(reader.GetString(0));
            pr.type = reader.GetString(1);
            pr.name = reader.GetString(2);
            pr.comment = reader.GetString(3);
            pr.distance = int.Parse(reader.GetString(4));
            pr.createdAt = DateTime.Now;
        }

        reader.Close();
        this.connection.Close(); 
        return pr;  
    }
    public int DeleteById(long id) 
    {   
        this.connection.Open();          
        SqliteCommand command = connection.CreateCommand();
        command.CommandText = @"DELETE FROM activitys WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        
        int nChanged = command.ExecuteNonQuery();
        this.connection.Close();
        return nChanged;
    }
    public long Insert(Activity ord) 
    {   
        this.connection.Open(); 

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = @"
        INSERT INTO activitys (type, name, comment, distance, createdAt)
        VALUES ($type, $name, $comment, $distance, $createdAt);
        SELECT last_insert_rowid();
        ";
        command.Parameters.AddWithValue("$type", ord.type);
        command.Parameters.AddWithValue("$name", ord.name);
        command.Parameters.AddWithValue("$comment", ord.comment);
        command.Parameters.AddWithValue("$distance", ord.distance);
        command.Parameters.AddWithValue("$createdAt", DateTime.Now);
        
        long newId = (long)command.ExecuteScalar();
        
        this.connection.Close(); 
        return newId;
    }

    public int UpdateById(long id, Activity act)
    {
        this.connection.Open();          
        SqliteCommand command = connection.CreateCommand();
        command.CommandText = @"UPDATE activitys 
        SET type = $type, name = $name, comment = $comment, distance = $distance 
        WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$type", act.type);
        command.Parameters.AddWithValue("$name", act.name);
        command.Parameters.AddWithValue("$comment", act.comment);
        command.Parameters.AddWithValue("$distance", act.distance);

        int nChanged = command.ExecuteNonQuery();
        this.connection.Close();
        return nChanged;
    }

    public long GetCountBooks()
    {   
        this.connection.Open();
        SqliteCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT COUNT(*) FROM activitys";       
        return (long)command.ExecuteScalar();
    }
    public int GetTotalPages(int pageSize)
    {
        return (int)Math.Ceiling(this.GetCountBooks() / (double)pageSize);
    }
        
    public List<Activity> GetPage(int pageNumber, int pageSize) 
    { 
        this.connection.Open();  
        SqliteCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT * FROM activitys LIMIT $pageSize OFFSET $pageSize * ($pageNumber - 1)";
        command.Parameters.AddWithValue("$pageSize", pageSize);
        command.Parameters.AddWithValue("$pageNumber", pageNumber);
        SqliteDataReader reader = command.ExecuteReader();
        List<Activity> providers = new List<Activity>();
        while(reader.Read())
        {
            Activity pr = new Activity();
            pr.id = long.Parse(reader.GetString(0));
            pr.type = reader.GetString(1);
            pr.name = reader.GetString(2);
            pr.comment = reader.GetString(3);
            pr.distance = int.Parse(reader.GetString(4));
            pr.createdAt = DateTime.Parse(reader.GetString(5));

            providers.Add(pr);
        }
        reader.Close();
        this.connection.Close();  
        return providers;
    }
}   
