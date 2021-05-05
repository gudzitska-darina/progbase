using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("root")]
public class LibCourse
{
    [XmlElement("course")]
    public List<Course> courses;
}