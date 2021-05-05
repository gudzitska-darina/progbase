using System.Xml.Serialization;

    [XmlType("time")]
    public struct Time
    {
        [XmlElement("start_time")]
        public string start;
        [XmlElement("end_time")]
        public string end;
    }
    [XmlType("place")]
    public struct Place
    {
        [XmlElement("building")]
        public string bld;
        [XmlElement("room")]
        public string room;
    }
public class Course
{
    [XmlElement("reg_num")]
    public int id;
    [XmlElement("subj")]
    public string subj;
    public int crse;
    public string sect;
    public string title;
    public double units;
    public string instructor;
    public string days;
    
    [XmlElement("time")]
    public Time newTime = new Time{
        start = "",
        end = ""
    };
    
    [XmlElement("place")]
    public Place newPlace = new Place{
        bld = "",
        room = ""
    };
   
    


    public override string ToString()
    {
        return $"\t|{id}| {subj}\r\nTitle: \"{title}\" — {instructor} | {units}\r\nPlace: {newPlace.bld}/{newPlace.room}\r\nTime: {newTime.start}-{newTime.end}\r\n";
    }
}