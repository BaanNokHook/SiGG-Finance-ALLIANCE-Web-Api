using System.Threading.Tasks;
using FinanceAllianx.Web.Api.Models.Responses.Currencies;
using FinanceAllianx.Web.Api.Repositories;
using FinanceAllianx.Web.Api.Models.Requests.Currencies;
using FinanceAllianx.Web.Api.Models.Responses.Currencies;
using FinanceAllianx.Web.Api.Repositories;
using Microsoft.Extensions.Logging;

namespace FinanceAllianx.Web.Api.Managers
{
    public class CurrenciesManager : ICurrenciesManager
    {
        private readonly ICurrenciesRepository _currenciesRepository;
        private readonly ILogger<CurrenciesManager> _logger;

        public CurrenciesManager(
            ICurrenciesRepository currenciesRepository,
            ILogger<CurrenciesManager> logger)
        {
            _currenciesRepository = currenciesRepository;
            _logger = logger;
        }

        public async Task<GetCurrencyResponse> GetCurrenciesAsync()
        {
            _logger.LogInformation("GetCurrenciesAsync start");
            var currencies = await _currenciesRepository.GetCurrenciesAsync();
            _logger.LogInformation("GetCurrenciesAsync end");
            return new GetCurrencyResponse
            {
                Currencies = currencies
            };
        }

        public Task AddCurrency(AddCurrencyRequest request)
        {
            _logger.LogInformation("AddCurrency start");
            var responseTask = _currenciesRepository.AddCurrencyAsync(request);
            _logger.LogInformation("AddCurrency end");
            return responseTask;
        }
    }
}