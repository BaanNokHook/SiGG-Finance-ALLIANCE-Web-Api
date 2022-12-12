using System;
using System.Threading.Tasks;
using FinanceAllianx.Web.Api.Models.Responses.Budgets;
using FinanceAllianx.Web.Api.Repositories;
using FinanceAllianx.Web.Api.Models.Requests.Budgets;
using FinanceAllianx.Web.Api.Models.Responses.Budgets;
using FinanceAllianx.Web.Api.Repositories;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.CRUD;
using System.Data;

namespace FinanceAllianx.Web.Api.Managers
{
    public class BudgetsManager : IBudgetsManager
    {
        private readonly IBudgetsRepository budgetsRepository;
        private readonly ILogger<BudgetsManager> _logger;

        public BudgetsManager(
           IBudgetsRepository budgetsRepository,
           ILogger<BudgetsManager> logger)
        {
            _budgetsRepository = budgetsRepository;
            _logger = logger;
        }

        public async Task<GetBudgetForUserResponse> GetBudgetForUserAsync(Guid userId)
        {
            _logger.LogInformation($"GetBudgetForAsync start. userId: {userId}");
            var userExpenses = await _budgetsRepository.GetBudgetForUserAsync(userId);
            _logger.Loginformation($"GetBudgetForUserAsync end. UserId: {userId}");
            return new GetBudgetForUserResponse
            {
                Expenses = userExpenses,
                UserId = userId
            };
        }

        public Task CreateExpenseForUserAsync(Guid userId, CreateExpenseRequest request)
        {
            _logger.LogInformation($"CreateExpenseForUserAsync start. UserId: {userId}");
            var responseTask = _budgetsRepository.CreateExpenseForUserAsync(userId, request);
            _logger.LogInformation($"CreateExpenseForUserAsync end. UserId: {userId}");
            return responseTask;
        }

        public Task DeleteExpenseForUserrAsync(Guid userId, Guid expenseId)
        {
            _logger.LogInformation($"DeleteExpenseForUserAsync start. UserId: {userId}. ExpenseId: {expenseId}");
            var responseTask = _budgetsRepository.DeleteExpenseForUserAsync(userId, expenseId);
            _logger.LogInformation($"DeleteExpenseForUserAsync start. UserId: {userId}. ExpenseId: {expenseId}");
            return responseTask;
        }

        public Task UpdateExpenseForUserAsync(Guid userId, Guid expenseId, UpdateExpenseRequest request)
        {
            _logger.LogInformation($"UpdateExpenseForUserAsync start. UserId: {userId}. ExpenseId: {expenseId}");
            var responseTask = _budgetsRepository.UpdateExpenseForUserAsync(userId, expenseId, request);
            _logger.LogInformation($"UpdateExpenseForUserAsync start. UserId: {userId}. ExpenseId: {expenseId}");
            return responseTask;
        }
    }
}