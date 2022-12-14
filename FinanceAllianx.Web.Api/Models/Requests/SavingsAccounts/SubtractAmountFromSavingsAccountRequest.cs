using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FinanceAllianx.Web.Api.Models.Requests.SavingsAccounts
{
    /// <summary>
    /// Represents a request to remove subtract an amount to a savings account.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SubtractAmountFromSavingsAccountRequest
    {
        /// <summary>
        /// The amount of money that will be subtracted from the savings account in the account's currency.
        /// </summary>
        [Required]
        [JsonProperty("amount", Required = Required.Always)]
        public double Amount { get; set; }
    }
}