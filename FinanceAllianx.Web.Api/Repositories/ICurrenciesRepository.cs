using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceAllianx.Web.Api.Models;
using FinanceAllianx.Web.Api.Models.Requests.Currencies;

namespace FinanceAllianx.Web.Api.Repositories
{
    /// <summary>
    /// Defines a contract for interacting with currencies in the database.
    /// </summary>
    public interface ICurrenciesRepository
    {
        /// <summary>
        /// Gets all currently listed currencies from the database.
        /// </summary>
        /// <returns>An enumeration of currencies.</returns>
        Task<IEnumerable<Currency>> GetCurrenciesAsync();

        /// <summary>
        /// Attempts to persist a new currency to the database.
        /// </summary>
        /// <param name="request">The details of the currency to be added.</param>
        Task AddCurrencyAsync(AddCurrencyRequest request);
    }
}