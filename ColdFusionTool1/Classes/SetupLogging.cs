using Serilog;
using static System.DateTime;

namespace ColdFusionTool1.Classes;

public class SetupLogging
{

    public static void Development(string logfileName = "Log.txt")
    {

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles", $"{Now.Year}-{Now.Month:D2}-{Now.Day:D2}", logfileName),
                rollingInterval: RollingInterval.Infinite,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();
    }
}

