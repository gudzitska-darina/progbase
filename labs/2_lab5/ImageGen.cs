using System.Linq;
using ScottPlot;
using System;
using System.Collections.Generic;


public class ImageGen
{
    public void GenerationImg(IEnumerable<Course> list, LibCourse cors, string filepath)
    {
        var plt = new ScottPlot.Plot(700, 900);

        int pointCount = CreateListSbj(list).Count;

        double[] xs = DataGen.Consecutive(pointCount);
        double[] ys = SumUnit(CreateListSbj(list), cors);

        plt.PlotBar(xs, ys, horizontal: true);

        plt.Grid(enableHorizontal: false, lineStyle: LineStyle.Dot);

        string[] labels = CreateListSbj(list).Cast<string>().ToArray();
        plt.YTicks(xs, labels);

        plt.SaveFig(filepath);
    } 

    public List<string> CreateListSbj(IEnumerable<Course> list)
    {
        List<string> listsbj = new List<string>();
        foreach(Course item in list)
        {
           listsbj.Add(item.subj);
        }
        return listsbj;
    }

    public double[] SumUnit(List<string> listsbj, LibCourse cors)
    {
        double[] sumun = new double[listsbj.Count];
        double sum = 0;
        int i = 0;
        foreach(var item in listsbj)
        {
            foreach(var cours in cors.courses)
            {
                if(item == cours.subj)
                {
                    sum += cours.units;
                }
            }
            sumun[i] = sum;
            sum = 0;
            i++;
        }
        return sumun;
    }
}
