using ModelContextProtocol.Server;
using System.ComponentModel;

namespace MCPPractical.Tools;

[McpServerToolType]
public class DateTimeMcpTools
{
    [McpServerTool, Description("Get current date and time")]
    public Task<DateTime> GetCurrentDateTimeAsync()
    {
        return Task.FromResult(DateTime.Now);
    }
}
