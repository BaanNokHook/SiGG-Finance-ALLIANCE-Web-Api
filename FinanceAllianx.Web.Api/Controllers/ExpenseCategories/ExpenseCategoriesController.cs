using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using FinanceAllianx.Web.Api.Models.Responses.ExpenseCategories;
using FinanceAllianx.Web.Api.Managers;
using FinanceAllianx.Web.Api.Models.Requests.ExpenseCategories;
using FinanceAllianx.Web.Api.Models.Responses.ExpenseCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.WebApi.Core.Errors;
using Microsoft.AspNetCore.Identity;

namespace FinanceAllianx.Web.Api.Controllers.ExpenseCategories
{
    [Authorize]
    [Route("expenseCatagories")]
    public class ExpenseCategoriesController : ControllerBase
    {
        private readonly IExpenseCategoriesManager _expenseCategoriesManager;
        private readonly ILogger<ExpenseCategoriesController> _logger;

        public ExpenseCategoriesController(
            IExpenseCategoriesManager expenseCategoriesManager,
            ILogger<ExpenseCategoriesController> logger)
        {
            _expenseCategoriesManager = expenseCategoriesManager;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetExpenseCategoriesResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetExpenseCategories()
        {
            _logger.LogInformation("GetExpenseCategories start");
            var response = await _expenseCategoriesManager.GetExpenseCategoriesAsync();
            return Ok(response);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(GetExpenseCategoriesForUserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetExpenseCategoriesForUser([Required][FromRoute] Guid userId)
        {
            _logger.LogInformation($"GetExpenseCategoriesForUser start, UserId: {userId}");
            var response = await _expenseCategoriesManager.GetExpenseCategoriesForUserAsync(userId);
            _logger.LogInformation($"GetExpenseCategoriesForUser end. UserId: {userId}")
            return Ok(response);
        }

        [HttpPost("user/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddExpenseCategoryForUser(
            [Required][FromRoute] Guid userId,
            [Required][FromBody] AddExpenseCategoryRequest request)
        {
            _logger.LogInformation($"AddExpenseCategoryForUser start. UserId: {userId}");
            await _expenseCategoriesManager.AddExpenseCategoryForUserAsync(userId, request);
            _logger.LogInformation($"AddExpenseCategoryForUser end. UserId: {userId}");
            return Accepted();
        }

       
        [HttpDelete("user/{userId}/expense/{expenseCategoryId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteExpenseCategoryForUser(
            [Required][FromRoute] Guid userId,
            [Required][FromRoute] Guid expenseCategoryId)
        {
            _logger.LogInformation($"DeleteExpenseCategoryForUser start. UserId: {userId}. ExpenseCategoryId: {expenseCategoryId}");
            await _expenseCategoriesManager.DeleteExpenseCategoryForUserAsync(userId, expenseCategoryId);
            _logger.LogInformation($"DeleteExpenseCategoryForUser start. UserId: {userId}. ExpenseCategoryId: {expenseCategoryId}");
            return Ok();
        }
    }
}