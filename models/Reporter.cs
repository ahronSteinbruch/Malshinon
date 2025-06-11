using Malshinon.models;

public class Reporter
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SecretCode { get; set; }

    public double Rating { get; set; } = 50.0;
    public bool IsRecruit { get; set; } = false;

    public Reporter() { }

    public Reporter(string name)
    {
        Name = name;
        SecretCode = GenerateSecretCode();
    }

    private string GenerateSecretCode()
    {
        return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
    }

    public bool IsRecruitCandidate()
    {
        /* if (Reports.Count < 10) return false;
         double avgLength = Reports.Average(r => r.Text.Length);
         return avgLength >= 100;*/
        return true;
    }
}

