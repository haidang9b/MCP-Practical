namespace MCPPractical.Models;

/// <summary>
/// Student record with ID, Name, and three subject scores
/// </summary>
public record Student(int Id, string Name, int Math, int Science, int English);
