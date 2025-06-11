using Malshinon.models;

namespace Malshinon.Services
{
    public class AlertGenerator
    {
        public Alert? GenerateAlertForTarget(Target target, List<Report> allReportsOnTarget)
        {
            DateTime now = DateTime.Now;

            // תנאי 1: 20 דוחות או יותר
            if (target.IsDangerous())
            {
                return new Alert
                {
                    Target = target,
                    TriggerTime = now,
                    Reason = "Target mentioned in 20+ reports"
                };
            }

            // תנאי 2: 3 דוחות תוך 15 דקות
            var sortedTimestamps = allReportsOnTarget
                .Select(r => r.Timestamp)
                .OrderBy(t => t)
                .ToList();

            for (int i = 0; i < sortedTimestamps.Count - 2; i++)
            {
                if ((sortedTimestamps[i + 2] - sortedTimestamps[i]).TotalMinutes <= 15)
                {
                    return new Alert
                    {
                        Target = target,
                        TriggerTime = now,
                        Reason = "3+ reports within 15 minutes"
                    };
                }
            }

            return null; // לא נוצרה התראה
        }
    }
}
