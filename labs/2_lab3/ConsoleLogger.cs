using System;
using static System.Console;
public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        WriteLine(message);
    }

    public void LogError(string errorMessage)
    {
        ForegroundColor = ConsoleColor.Red;
        WriteLine($"~! {errorMessage} !~");
        ResetColor();
    }
}
