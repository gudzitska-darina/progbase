using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
public class DataProcessing
{
    public int GetTotalPages(LibCourse cor)
    {
        const int pageSize = 5;
        return (int)Math.Ceiling(cor.courses.Count / (double)pageSize);
    }
    public void GetPage(int pagenum, LibCourse cor)
    {
        const int pageSize = 5;
        for(int i = (pagenum - 1) * pageSize; i < (((pagenum - 1) * pageSize) + pageSize); i++)
        {
            Console.WriteLine(cor.courses[i].ToString());
        }
    }
    public LibCourse MaxUnit(int maxu, LibCourse cors)
    {
        var delcors = cors.courses.OrderByDescending(x => x.units).ToList();

        for(int i = maxu; i < delcors.Count; i++)
        {
            delcors.Remove(delcors[i]);
            i--;
        }

        LibCourse maxLibUn = new LibCourse();
        maxLibUn.courses = delcors;
        return maxLibUn;
    }
        
    public IEnumerable<Course> UniqSubj(LibCourse cors)
    {
        var res = cors.courses.GroupBy(x => x.subj).Select(x => x.First());
        return res;
    }
    public IEnumerable<Course> UniqInstr(LibCourse cors)
    {
        var res = cors.courses.GroupBy(x => x.instructor).Select(x => x.First());
        return res;
    }

    public HashSet<string> ListSubj(LibCourse cors, string subject)
    {
        List<string> listTitle = new List<string>();
        foreach(Course item in cors.courses)
        {
            if(item.subj == subject)
            {
                listTitle.Add(item.title);
            }
        }
        HashSet<string> res = new HashSet<string>(listTitle);
        return res;
    }
}
