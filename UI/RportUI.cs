using Malshinon.models;
using Malshinon.models.Services;
using Malshinon.Services;
using System;
using System.Collections.Generic;

namespace Malshinon.UI
{
    internal static class ReportUI
    {
        public static void SubmitReport()
        {
            Console.WriteLine("Enter your code name:");
            string codeOrName = Console.ReadLine();
            int reporterId = PersonRegistry.GetOrCreateReporter(codeOrName);

            Console.WriteLine("Enter your message:");
            string message = Console.ReadLine();

            Console.WriteLine("Enter target names or codes (separated by spaces):");
            string targetsLine = Console.ReadLine();
            List<string> targetIdentifiers = new(targetsLine.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            ReportProcessor.ProcessNewReport(reporterId, message, targetIdentifiers);

            Console.WriteLine("Report submitted successfully.");
        }
    }
}
