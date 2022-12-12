using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FinanceAllianx.Web.Api.Models.Requests.SavingsAccounts
{
    /// <summary>
    /// Represents a request to add an amount to a savings account.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AddAmountToSavingsAccountRequest
    {
        /// <summary>
        /// The amount of money that will be added to the savings account in the account's currency.
        /// </summary>
        [Required]
        [JsonProperty("amount", Required = Required.Always)]
        public double Amount { get; set; }
    }
}