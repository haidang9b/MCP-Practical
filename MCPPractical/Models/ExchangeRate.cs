namespace MCPPractical.Models;

public class ExchangeRate
{
    public double Amount { get; set; }

    public string? Base { get; set; }

    public string? Date { get; set; }

    public Dictionary<string, double> Rates { get; set; } = new Dictionary<string, double>();

}
