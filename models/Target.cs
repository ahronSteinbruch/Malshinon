using Malshinon.DAL;
using Malshinon.models;

public class Target
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SecretCode { get; set; }
    public string? Affiliation { get; set; }


    public Target() { }

    public Target(string name, string? affiliation = null)
    {
        Name = name;
        Affiliation = affiliation;
        SecretCode = GenerateSecretCode();
    }

    private string GenerateSecretCode()
    {
        return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
    }

    public static bool IsDangerous(int id)
    {
        return ReportRepository.GetReportCountByTargetId(id) >= 20;
    }

    public static bool HasBurstActivity(int id)
    {
        var timestamps = ReportRepository.GetReportsByTargetId(id)
            .Select(rt => rt.Timestamp)
            .OrderBy(t => t)
            .ToList();

        for (int i = 0; i < timestamps.Count - 2; i++)
        {
            if ((timestamps[i + 2] - timestamps[i]).TotalMinutes <= 15)
                return true;
        }

        return false;
    }
}
