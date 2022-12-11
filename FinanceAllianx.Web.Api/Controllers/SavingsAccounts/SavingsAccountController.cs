using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using FinanceAllianx.Web.Api.Models.Responses.SavingsAccounts;
using FinanceAllianx.Web.Api.Managers;
using FinanceAllianx.Web.Api.Models.Requests.SavingsAccounts;
using FinanceAllianx.Web.Api.Models.Responses.SavingsAccounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.WebApi.Core.Errors;

namespace FinanceAllianx.Web.Api.Controllers.SavingsAccounts
{

    [Authorize]
    [Route("savingsAccounts")]
    public class SavingsAccountController : ControllerBase
    {
        private readonly ISavingsAccountManager _savingsAccountManager;
        private readonly ILogger<SavingsAccountController> _logger;

        
        public SavingsAccountController(
            ISavingsAccountManager savingsAccountManager,
            ILogger<SavingsAccountController> logger)
        {
            _savingsAccountManager = savingsAccountManager;
            _logger = logger;
        }

      
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(GetSavingsAccountForUserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetSavingsAccountsForUser([Required][FromRoute] Guid userId)
        {
            _logger.LogInformation($"GetSavingsAccountsForUser start. UserId: {userId}");
            var response = await _savingsAccountManager.GetSavingsAccountForUserAsync(userId);
            _logger.LogInformation($"GetSavingsAccountsForUser end. UserId: {userId}");
            return Ok(response);
        }

        
        [HttpPost("user/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddSavingsAccountForUser(
            [Required][FromRoute] Guid userId,
            [Required][FromBody] AddSavingsAccountRequest request)
        {
            _logger.LogInformation($"AddSavingsAccountForUser start. UserId: {userId}");
            await _savingsAccountManager.AddSavingsAccountForUserAsync(userId, request);
            _logger.LogInformation($"AddSavingsAccountForUser end. UserId: {userId}");
            return Accepted();
        }

       
        [HttpPatch("user/{userId}/savingsAccount/{savingsAccountId}/addAmount")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddAmountToSavingsAccountForUser(
            [Required][FromRoute] Guid userId,
            [Required][FromRoute] Guid savingsAccountId,
            [Required][FromBody] AddAmountToSavingsAccountRequest request)
        {
            _logger.LogInformation($"AddAmountToSavingsAccountForUser start. UserId: {userId}. SavingsAccountId: {savingsAccountId}");
            await _savingsAccountManager.AddAmountToSavingsAccountForUserAsync(userId, savingsAccountId, request);
            _logger.LogInformation($"AddAmountToSavingsAccountForUser end. UserId: {userId}. SavingsAccountId: {savingsAccountId}");
            return Ok();
        }

       
        [HttpPatch("user/{userId}/savingsAccount/{savingsAccountId}/subtractAmount")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SubtractAmountFromSavingsAccountForUser(
            [Required][FromRoute] Guid userId,
            [Required][FromRoute] Guid savingsAccountId,
            [Required][FromBody] SubtractAmountFromSavingsAccountRequest request)
        {
            _logger.LogInformation($"SubtractAmountFromSavingsAccountForUser start. UserId: {userId}. SavingsAccountId: {savingsAccountId}");
            await _savingsAccountManager.SubtractAmountFromSavingsAccountForUserAsync(userId, savingsAccountId, request);
            _logger.LogInformation($"SubtractAmountFromSavingsAccountForUser end. UserId: {userId}. SavingsAccountId: {savingsAccountId}");
            return Ok();
        }

 
        [HttpDelete("user/{userId}/savingsAccount/{savingsAccountId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteSavingsAccountForUser(
            [Required][FromRoute] Guid userId,
            [Required][FromRoute] Guid savingsAccountId)
        {
            _logger.LogInformation($"DeleteSavingsAccountForUser start. UserId: {userId}. SavingsAccountId: {savingsAccountId}");
            await _savingsAccountManager.DeleteSavingsAccountForUserAsync(userId, savingsAccountId);
            _logger.LogInformation($"DeleteSavingsAccountForUser end. UserId: {userId}. SavingsAccountId: {savingsAccountId}");
            return Ok();
        }

  
        [HttpPatch("user/{userId}/savingsAccount/{savingsAccountId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateSavingsAccountForUser(
            [Required][FromRoute] Guid userId,
            [Required][FromRoute] Guid savingsAccountId,
            [Required][FromBody] UpdateSavingsAccountRequest request)
        {
            _logger.LogInformation($"UpdateSavingsAccountForUser start. UserId: {userId}. SavingsAccountId: {savingsAccountId}");
            await _savingsAccountManager.UpdateSavingsAccountForUserAsync(userId, savingsAccountId, request);
            _logger.LogInformation($"UpdateSavingsAccountForUser end. UserId: {userId}. SavingsAccountId: {savingsAccountId}");
            return Ok();
        }
    }
}