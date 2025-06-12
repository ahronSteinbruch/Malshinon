// ReportProcessor.cs
using Malshinon.DAL;
using Malshinon.Services;



namespace Malshinon.models.Services
{
    public static class ReportProcessor
    {
        public static void ProcessNewReport(int reporterId, string message, List<string> targetCodesOrNames)
        {
            var tagRepo = new TagsRepository();
            var allTags = tagRepo.GetConnectedTagNamesRecursive("", 2) // Load all if needed
                .Select(t => new Tag { Name = t }).ToList();
            var tagAnalyzer = new TagAnalyzer(allTags);
            var alertGenerator = new AlertGenerator();
            var alertRepo = new AlertRepository();

            // Get or create targets
            List<int> targets = new();
            foreach (var targetName in targetCodesOrNames)
            {
                targets.Add(PersonRegistry.GetOrCreateTarget(targetName));
            }
          

            // Analyze primary tag
            var primaryTag = tagAnalyzer.AnalyzeTextAndFindPrimaryTag(message);

            var report = new Report
            {
                ReporterId = reporterId,
                Message = message,
                Timestamp = DateTime.Now,
                Tag = primaryTag?.Name ?? "Uncategorized"
            };

            // Save report
            ReportRepository.AddReport(report);

            // Get last inserted report id (assuming auto increment)
            int reportId = PersonRegistry.ReporteridGenerator;

            foreach (int targetId in targets)
            {

                string query = "INSERT INTO ReportTargets (ReportId, TargetId) VALUES (@rid, @tid);";
                var parameters = new Dictionary<string, object>
                {
                    {"@rid", reportId },
                    {"@tid", targetId }
                };
                ConnectionWrapper.getInstance().ExecuteNoneQuery(query, parameters);

                // Fetch all reports for this target
                var allReports = ReportRepository.GetReportsByTargetId(targetId);
                var alert = alertGenerator.GenerateAlertForTarget(targetId, allReports);
                if (alert != null)
                {
                    alertRepo.AddAlert(alert);
                    Console.WriteLine("ALERT! " + alert.Reason);
                }
            }
        }
    }
}