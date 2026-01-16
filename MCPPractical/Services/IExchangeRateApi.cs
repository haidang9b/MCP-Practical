using MCPPractical.Models;
using Refit;

namespace MCPPractical.Services;

public interface IExchangeRateApi
{
    [Get("/v1/latest")]
    Task<ApiResponse<ExchangeRate>> GetExchangeRatesAsync([Query("base")] string baseCurrency = "USD");
}
