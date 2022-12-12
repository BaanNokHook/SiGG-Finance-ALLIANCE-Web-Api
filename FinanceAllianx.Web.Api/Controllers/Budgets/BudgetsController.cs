using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using FinanceAllianx.Web.Api.Models.Responses.Budgets;
using FinanceAllianx.Web.Api.Managers;
using FinanceAllianx.Web.Api.Models.Requests.Budgets;
using FinanceAllianx.Web.Api.Models.Responses.Budgets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.WebApi.Core.Errors;

namespace FinanceAllianx.Web.Api.Controllers.Budgets
{

    [Authorize]
    [Route("budgets")]

    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetsManager budgetsManager;
        private IBudgetManager _budgetsManager;
        private readonly ILogger<BudgetsController> _logger;

        public BudgetsController(
            IBudgetManager budgetsManager,
            ILogger<BudgetsController> logger)

        {
            _budgetsManager = budgetsManager;
            _logger = logger;
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(GetBudgetForUserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetBudgetForUser([Required][FromRoute] Guid userId)
        {
            _logger.LogInformation($"GetBudgetForUser start. UserId: {userId}");
            var response = await _budgetsManager.GetBudgetForUser(userId);
            _logger.LogInformation($"GetBudgetForUser end. UserId: {userId}");
            return Ok(response);
        }


        [HttpPost("user/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateExpenseForUser(
            [Required][FromRoute] Guid userId,
            [Required][FromBody] CreateExpenseRequest request)
        {
            _logger.LogInformation($"CreateExpenseForUser start. UserId: {userId}");
            object value = await _budgetsManager.CreateExpenseForUserAsync(userId, request);
            _logger.LogInformation($"CreateExpenseForUser end. UserId: {userId}");
            return Accepted();
        }

        [HttpDelete("user/{userId}/expense/{expenseId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteExpenseForUser(
            [Required][FromRoute] Guid userId,
            [Required][FromRoute] Guid expenseId)
        {
            _logger.LogInformation($"DeleteExpenseForUser start. UserId: {userId}. ExpenseId: {expenseId}");
            await _budgetsManager.DeleteExpenseForUserAsync(userId, expenseId);
            _logger.LogInformation($"DeleteExpenseForUser end. Userid: {userId}. ExpenseId: {expenseId}");
            return Ok();
        }

        [HttpPatch("user/{userId}/expense/{expenseId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateExpenseForUser(
            [Required][FromRoute] Guid userId,
            [Required][FromRoute] Guid expenseId,
            [Required][FromBody] UpdateExpenseRequest request)

        {
            _logger.LogInformation($"UpdateExpenseForUser start. UserId: {userId}. ExpenseId: {expenseId}");
            await _budgetsManager.UpdateExpenseForUserAsync(userId, expenseId, request);
            _logger.LogInformation($"UpdateExpenseForUser end. UserId: {userId}. ExpenseId: {expenseId}");
            return Ok();
        }
    }

    public interface IBudgetManager
    {
        Task<object> CreateExpenseForUserAsync(Guid userId, CreateExpenseRequest request);
        Task DeleteExpenseForUserAsync(Guid userId, Guid expenseId);
        Task GetBudgetForUser(Guid userId);
        Task UpdateExpenseForUserAsync(Guid userId, Guid expenseId, UpdateExpenseRequest request);
    }

}
