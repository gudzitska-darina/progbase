using System;
using System.IO;
using System.Xml.Serialization;
public class Serializer
{
    public LibCourse Deserialize(string filepath)
    {
        LibCourse cors = null;
        string path = "reed.xml";

        XmlSerializer serializer = new XmlSerializer(typeof(LibCourse));

        StreamReader reader = new StreamReader(path);
        cors = (LibCourse)serializer.Deserialize(reader);
        reader.Close();
        return cors;
    }

    public void Serialize(string filepath, LibCourse courses)
    {
        XmlSerializer ser = new XmlSerializer(typeof(LibCourse));
 
        System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
        settings.Indent = true;
        settings.NewLineHandling = System.Xml.NewLineHandling.Entitize;
        System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(filepath, settings);
        
        ser.Serialize(writer, courses);
        writer.Close();
    }
}