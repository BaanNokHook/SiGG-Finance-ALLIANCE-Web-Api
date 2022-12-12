using System;
using System.Threading.Tasks;
using FinanceAllianx.Web.Api.Models.Responses.ExpenseCategories;
using FinanceAllianx.Web.Api.Repositories;
using FinanceAllianx.Web.Api.Models.Requests.ExpenseCategories;
using FinanceAllianx.Web.Api.Models.Responses.ExpenseCategories;
using FinanceAllianx.Web.Api.Repositories;
using Microsoft.Extensions.Logging;

namespace FinanceAllianx.Web.Api.Managers
{
    public class ExpenseCategoriesManager : IExpenseCategoriesManager
    {
        private readonly IExpenseCategoriesRepository expenseCategoriesRepository1;
        private readonly ILogger<ExpenseCategoriesManager> _logger;   

        public ExpenseCategoriesManager(   
            IExpenseCategoriesRepository expenCategoriesRepository, 
            ILogger<ExpenseCategoriesManager> logger)
        {
            _expenseCategoriesRepository = expenseCategoriesRepository;
            _logger = logger;  
        }   

        public async Task<GetExpenseCategoriesForUserResponse> GetExpenseCategoriesAsync()
        {
            _logger.LogInformation("GetExpenseCategoriesAsync start");
            var expenseCategories = await _expenseCategoriesRepository.GetExpenseCategoriesAsync();
            _logger.LogInformation("GetExpenseCategoriesAsync end");    
            return new GetExpenseCategoriesResponse
            {
                ExpenseCategories = expenseCategories 
            };    
        }

        public async Task<GetExpenseCategoriesForUserResponse > GetExpenseCategoriesForUserAsync(Guid userId)
        {
            _logger.LogInformation($"GetExpenseCategoriesForUserAsync start. UserId: {userId}");
            var expenseCategories = await _expenseCategoriesRepository.GetExpenseCategoriesForUserAsync(userId);
            _logger.LogInformation($"GetExoenseCategoriesForUserAsync end. UserId: {userId}");
            return new GetExpenseCategoriesForUserResponse
            {
                ExpenseCategories = expenseCategories,
                UserId = userId
            };  
        }  

        public Task AddExpenseCategoryForUserAsync(Guid userId, AddExpenseCategoryRequest request)
        {
            _logger.LogInformation($"AddExpenseCategoryForUserAsync start. UserId: {userId}");
            var responseTask = _expenseCategoriesRepository.AddExpenseCategoryForUserAsync(userId, request);
            _logger.LogInformation($"AddExpenseCategoryForUserAsync end. UserId: {userId}");
            return responseTask;
        }

        public Task DeleteExpenseCategoryForUserAsync(Guid userId, Guid expenseCategoryId)
        {
            _logger.LogInformation($"DeleteExpenseCategoryForUserAsync start. UserId: {userId}. ExpenseCategoryId: {expenseCategoryId}");
            var responseTask = _expenseCategoriesRepository.DeleteExpenseCategoryForUserAsync(userId, expenseCategoryId);
            _logger.LogInformation($"DeleteExpenseCategoryForUserAsync end. UserId: {userId}. ExpenseCategoryId: {expenseCategoryId}");
            return responseTask;
        }
    }
}