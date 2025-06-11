using Malshinon.models;

public class Report
{
    public int Id { get; set; }

    public int ReporterId { get; set; }

    public List<string> tergetsNames { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }

    public string Tag;
}
