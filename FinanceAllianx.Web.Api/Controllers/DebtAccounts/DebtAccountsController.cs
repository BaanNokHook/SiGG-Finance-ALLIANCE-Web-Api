using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using FinanceAllianx.Web.Api.Models.Responses.DebtAccounts;
using FinanceAllianx.Web.Api.Managers;
using FinanceAllianx.Web.Api.Models.Requests.DebtAccounts;
using FinanceAllianx.Web.Api.Models.Responses.DebtAccounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.WebApi.Core.Errors;
using FinanceAllianx.Web.Api.Repositories;
using System.Net.NetworkInformation;

namespace FinanceAllianx.Web.Api.Controllers.DebtAccounts
{
    [Authorize]
    [Route("debtAccounts")]  
    public class DebtAccountsController : ControllerBase
    {
        private readonly IDebtAccountsManager _debtAccountsManager;
        private readonly ILogger<DebtAccountsController> _logger;   

        public DebtAccountsController(  
            IDebtAccountsManager debtAccountsManager, 
            ILogger<DebtAccountsController> logger)
        {
            _debtAccountsManager = debtAccountsManager;
            _logger = logger; 
        }


        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(GetDebtAccountsForUserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]   
        public async Task<IActionResult> GetDebtAccountsForUser([Required][FromRoute] Guid userId)
        {
            _logger.LogInformation($"GetDebtAccountsForUser start. UserId: {userId}");
            var response = await _debtAccountsManager.GetDebtAccountForUserAsync(UserId);
            _logger.LogInformation($"GetDebtAccountsForUser end. UserId: {userId}");
            return Ok(response);  
        }

        [HttpPost("user/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]   
        public async Task<IActionResult> AddDebtAccountForUser(
            [Required][FromBody] Guid userId,
            [Required][FromBody] AddDebtAccountRequest request)
        {
            _logger.LogInformation($"AddDebtAccountForUser start.UserId: {userId}");
            await _debtAccountsManager.AddDebtAccountForUserAsync(userId, request);
            _logger.LogInformation($"AddDebtAccountForUser end. UserId: {userId}");
            return Accepted();   
        }
  
        [HttpPatch("user/{userId}/debtAccount/{debtAccountId}/addAmount")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddAmountToDebtAccount(
            [Required][FromRoute] Guid userId,
            [Required][FromRoute] Guid debtAccountId,
            [Required][FromBody] AddAmountToDebtAccountRequest request)
        {
            _logger.LogInformation($"AddAmountToDebtAccount start. UserId: {userId}. DebtAccountId: {debtAccountId}");
            await _debtAccountsManager.AddAmountToDebtAccountForUserAsync(userId, debtAccountId, request);
            _logger.LogInformation($"AddAmountToDebtAccount end. UserId: {userId}. DebtAccountId: {debtAccountId}");
            return Ok();
        }

        
        [HttpPatch("user/{userId}/debtAccount/{debtAccountId}/subtractAmount")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SubtractAmountFromDebtAccountForUser(
            [Required][FromRoute] Guid userId,
            [Required][FromRoute] Guid debtAccountId,
            [Required][FromBody] SubtractAmountFromDebtAccountRequest request)
        {
            _logger.LogInformation($"SubtractAmountFromDebtAccountForUser start. UserId: {userId}. DebtAccountId: {debtAccountId}");
            await _debtAccountsManager.SubtractAmountFromDebtAccountForUserAsync(userId, debtAccountId, request);
            _logger.LogInformation($"SubtractAmountFromDebtAccountForUser end. UserId: {userId}. DebtAccountId: {debtAccountId}");
            return Ok();
        }

        
        [HttpDelete("user/{userId}/debtAccount/{debtAccountId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteDebtAccountForUser(
            [Required][FromRoute] Guid userId,
            [Required][FromRoute] Guid debtAccountId)
        {
            _logger.LogInformation($"DeleteDebtAccountForUser start. UserId: {userId}. DebtAccountId: {debtAccountId}");
            await _debtAccountsManager.DeleteDebtAccountForUserAsync(userId, debtAccountId);
            _logger.LogInformation($"DeleteDebtAccountForUser end. UserId: {userId}. DebtAccountId: {debtAccountId}");
            return Ok();
        }

       
        [HttpPatch("user/{userId}/debtAccount/{debtAccountId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateDebtAccountForUser(
            [Required][FromRoute] Guid userId,
            [Required][FromRoute] Guid debtAccountId,
            [Required][FromBody] UpdateDebtAccountRequest request)
        {
            _logger.LogInformation($"UpdateDebtAccountForUser start. UserId: {userId}. DebtAccountId: {debtAccountId}");
            await _debtAccountsManager.UpdateDebtAccountForUserAsync(userId, debtAccountId, request);
            _logger.LogInformation($"UpdateDebtAccountForUser end. UserId: {userId}. DebtAccountId: {debtAccountId}");
            return Ok();
        }
    }
}