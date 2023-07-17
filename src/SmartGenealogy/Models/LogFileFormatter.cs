﻿using System.Globalization;
using System.IO;

using Serilog.Events;
using Serilog.Formatting;

namespace SmartGenealogy.Models;

public class LogFileFormatter : ITextFormatter
{
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