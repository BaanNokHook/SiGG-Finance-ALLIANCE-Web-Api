using System.Threading.Tasks;
using FinanceAllianx.Web.Api.Models.Requests.Currencies;
using FinanceAllianx.Web.Api.Models.Responses.Currencies;

namespace FinanceAllianx.Web.Api.Managers
{
    /// <summary>
    /// Provides a contract for a manager between the repository and controller layer.
    /// </summary>
    public interface ICurrenciesManager
    {
        /// <summary>
        /// Gets the currently listed currencies from the database.
        /// </summary>
        /// <returns>A collection of currencies.</returns>
        Task<GetCurrencyResponse> GetCurrenciesAsync();

        /// <summary>
        /// Attempts to persist a new currency to the database.
        /// </summary>
        /// <param name="request">The details of the new currency.</param>
        Task AddCurrency(AddCurrencyRequest request);
    }
}