using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FinanceAllianx.Web.Api.Models.Responses.Currencies;
using FinanceAllianx.Web.Api.Repositories;
using FinanceAllianx.Web.Api.Managers;
using FinanceAllianx.Web.Api.Models.Requests.Currencies;
using FinanceAllianx.Web.Api.Models.Responses.Currencies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.WebApi.Core.Errors;

namespace FinanceAllianx.Web.Api.Controllers.Currencies
{
    [Authorize]
    [Route("currencies")]  
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrenciesManager currenciesManager;
        private readonly ILogger<CurrenciesController> _logger;
        private object _currenciesManager;

        public CurrenciesController(  
            ICurrenciesManager currenciesManager,  
            ILogger<CurrenciesController> logger)
        {
            _currenciesManager = currenciesManager;
            _logger = logger;

            [HttpGet]
            [ProducesResponseType(typeof(GetCurrencyResponse), (int)HttpStatusCode.OK)]
            [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
            [ProducesResponseType((int)HttpStatusCode.Forbidden)]
            [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]  
            public async Task<IActionResult> GetCurrencies()
            {
                _logger.LogInformation("GetCurrencies start");
                var response = await _currenciesManager.GetCurrenciesAsync();
                _logger.LogInformation("GetCurrencies end");
                return Ok(response);  
            }

            [HttpPost]
            [ProducesResponseType((int)HttpStatusCode.Accepted)]
            [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
            [ProducesResponseType((int)HttpStatusCode.Forbidden)]
            [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]   
            public async Task<IActionResult> AddCurrency([Required][FromBody] AddCurrencyRequest request)
            {
                await _currenciesManager.AddCurrency(request);
                return Accepted();    
            }

        }  

    }
}





