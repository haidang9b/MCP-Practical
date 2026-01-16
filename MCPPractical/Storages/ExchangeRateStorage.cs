using MCPPractical.Services;
using System.Collections.Concurrent;

namespace MCPPractical.Storages;

public class ExchangeRateStorage
{
    private readonly ConcurrentDictionary<string, double> _exchangeRates = new();

    private readonly IExchangeRateApi _exchangeRateApi;

    private readonly SemaphoreSlim _lock = new(1, 1);

    public ExchangeRateStorage(IExchangeRateApi exchangeRateApi)
    {
        _exchangeRateApi = exchangeRateApi;
    }

    public async Task<double> GetExchangeRateAsync(string currency)
    {
        if (_exchangeRates.TryGetValue(currency, out var rate))
        {
            return rate;
        }
        await _lock.WaitAsync();
        try
        {
            // Double-check locking
            if (_exchangeRates.TryGetValue(currency, out rate))
            {
                return rate;
            }
            var response = await _exchangeRateApi.GetExchangeRatesAsync();
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                foreach (var kvp in response.Content.Rates)
                {
                    _exchangeRates[kvp.Key] = kvp.Value;
                }
                if (_exchangeRates.TryGetValue(currency, out rate))
                {
                    return rate;
                }
            }
            throw new KeyNotFoundException($"Exchange rate for currency '{currency}' not found.");
        }
        finally
        {
            _lock.Release();
        }
    }
}
