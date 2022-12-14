using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FinanceAllianx.Web.Api.Models.Requests.DebtAccounts
{
    /// <summary>
    /// Represents a request to reduce the amount owed on a debt account.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SubtractAmountFromDebtAccountRequest
    {
        /// <summary>
        /// The amount of money that will be subtracted from the debt account in the account's currency.
        /// </summary>
        [Required]
        [JsonProperty("amount", Required = Required.Always)]
        public double Amount { get; set; }
    }
}