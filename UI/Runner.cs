// Runner.cs
namespace Malshinon.UI
{
    internal static class Runner
    {
        public static void Run(string input)
        {
            switch (input)
            {
                case "1":
                    ReportUI.SubmitReport();
                    break;
                case "2":
                    AnalysisUI.ShowPotentialInformants();
                    break;
                case "3":
                    AnalysisUI.ShowDangerousTargets();
                    break;
                default:
                    Console.WriteLine("Invalid selection. Press Enter to continue.");
                    break;
            }
        }
    }
}

