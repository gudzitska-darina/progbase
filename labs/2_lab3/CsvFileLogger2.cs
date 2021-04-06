using System;
using System.Text;
using static System.IO.File;
public class CsvFileLogger2 : ILogger
{
    public string process;
    public string error;

    public void Log(string message)
    {
        StringBuilder line = new StringBuilder();
        line.Append($"\"{DateTime.UtcNow.ToString("o")}\"").Append(",").Append($"\"{message}\"").AppendLine();
        AppendAllText(process, line.ToString());
    }

    public void LogError(string errorMessage)
    {
        StringBuilder line = new StringBuilder();
        line.Append($"\"{DateTime.UtcNow.ToString("o")}\"").Append(",").Append($"\"{errorMessage}\"").AppendLine();
        AppendAllText(error, line.ToString());
    }
}
