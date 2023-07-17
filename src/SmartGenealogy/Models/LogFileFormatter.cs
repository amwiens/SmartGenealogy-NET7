using System.Globalization;
using System.IO;

using Serilog.Events;
using Serilog.Formatting;

namespace SmartGenealogy.Models;

/// <summary>
/// Log file formatter.
/// </summary>
public class LogFileFormatter : ITextFormatter
{
    /// <summary>
    /// Formats the string that gets entered into the log file.
    /// </summary>
    /// <param name="logEvent">Log event data.</param>
    /// <param name="output">Output string.</param>
    public void Format(LogEvent logEvent, TextWriter output)
    {
        output.Write($"[{logEvent.Timestamp:HH:mm:ss.fff zzz}] [{logEvent.Level}]");
        output.WriteLine(logEvent.MessageTemplate.Render(logEvent.Properties, CultureInfo.InvariantCulture));
        if (logEvent.Exception is not null)
        {
            output.WriteLine("------------------------------");
            output.WriteLine(logEvent.Exception.ToString());
            output.WriteLine("------------------------------");
        }
    }
}