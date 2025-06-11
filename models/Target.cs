using Malshinon.models;

public class Target
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SecretCode { get; set; }
    public string? Affiliation { get; set; }

    public List<ReportTarget> ReportTargets { get; set; } = new();

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

    public bool IsDangerous()
    {
        return ReportTargets.Count >= 20;
    }

    public bool HasBurstActivity()
    {
        var timestamps = ReportTargets
            .Select(rt => rt.Report.Timestamp)
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
