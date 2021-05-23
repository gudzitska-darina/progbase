using System;
using Microsoft.Data.Sqlite;
using Terminal.Gui;
using System.Collections.Generic;



namespace _2_lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "./progdb.db";
            SqliteConnection connection = new SqliteConnection($"Data Source={file}");
            ActyvityRepository repo = new ActyvityRepository(connection);
            Application.Init();
            Toplevel top = Application.Top;

            MainWindow win = new MainWindow();
            win.SetRepository(repo);
            top.Add(win);

            Application.Run();

        }
    }
}
