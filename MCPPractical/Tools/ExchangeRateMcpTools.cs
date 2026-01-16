using MCPPractical.Storages;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace MCPPractical.Tools;

[McpServerToolType]
public class ExchangeRateMcpTools
{
    private readonly ExchangeRateStorage _exchangeRateStorage;

    public ExchangeRateMcpTools(ExchangeRateStorage exchangeRateStorage)
    {
        _exchangeRateStorage = exchangeRateStorage;
    }

    [McpServerTool, Description("Get current exchange rate base on currency USD")]
    public async Task<double> GetCurrentExchangeRateAsync(
        [Description("Currency code")]
        string currency
    )
    {
        return await _exchangeRateStorage.GetExchangeRateAsync(currency);
    }


}