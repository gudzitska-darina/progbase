using System;

public class Activity
{
    public long id;
    public string type;
    
    public string name;
    public string comment;
    public int distance;
    public DateTime createdAt;
    public override string ToString()
    {
        return $"[{type}] — {name}";
    }
}
