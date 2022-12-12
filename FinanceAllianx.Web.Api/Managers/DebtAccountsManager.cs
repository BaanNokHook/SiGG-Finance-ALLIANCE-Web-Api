using System;
using System.Threading.Tasks;
using FinanceAllianx.Web.Api.Models.Responses.DebtAccounts;
using FinanceAllianx.Web.Api.Repositories;
using FinanceAllianx.Web.Api.Models.Requests.DebtAccounts;
using FinanceAllianx.Web.Api.Models.Responses.DebtAccounts;
using FinanceAllianx.Web.Api.Repositories;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Configuration;
using System.Data;

namespace FinanceAllianx.Web.Api.Managers
{
    public class DebtAccountsManager : IDebtAccountsManager
    {
        private readonly IDebtAccountsRepository _debtAccountsRepository;
        private readonly ILogger<DebtAccountsManager> _logger;   

        public DebtAccountsManager(
            IDebtAccountsRepository debtAccountsRepository,  
            ILogger<DebtAccountsManager> logger)
        {
            _debtAccountsRepository = debtAccountsRepository;
            _logger = logger;  
        }  
        
        public async Task<GetDebtAccountsForUserResponse> GetDebtAccountForUserAsync(Guid userId)
        {
            _logger.LogInformation($"GetDebtAccountForUserAsync start. UserId: {userId}");
            var debtAccounts = await _debtAccountsRepository.GetDebtAccountsForUserAsync(userId);
            _logger.LogInformation($"GetDebtAccountForUserAsync end. UserId: {userId}");
            return new GetDebtAccountsForUserResponse
            {
                DebtAccounts = debtAccounts,
                UserId = userId
            };  
        }

        public Task AddDebtAccountForUserAsync(Guid userId, AddingAccountRequest request)
        {
            _logger.LogInformation($"AddDebtAccountForUser start. UserId: {userId}");
            var responseTask = _debtAccountsRepository.AddDebtAccountForUserAsync(userId, request);
            _logger.LogInformation($"AddDebtAccountForUser end. UserId: {userId}");
            return responseTask;    
        }  

        public Task AddAmountToDebtAccountForUserAsync(     
            Guid userId, 
            Guid debtAccountId,   
            AddAmountTobeDebtAccountRequest request)
        {
            _logger.LogInformation($"AddAmountToDebtAccountForUserAsync start. UserId: {userId}. DebtAccountIdซ {debtAccountId}");
            var responseTask = _debtAccountsRepository.AddAmountToDebtAccountForUserAsync(userId, debtAccountId, request);
            _logger.LogInformation($"AddAmountToDebtAccountForUserAsync end. UserId: {userId}. DebtAccountId: {debtAccountId}");
            return responseTask;     
        }

        public Task SubtractAmountFromDebtAccountForUserAsync(
            Guid userId,  
            Guid debtAccountId,   
            SubtractAmountFromDebtAccountRequest request)
        {
            _logger.LogInformation($"SubtractAmountFromDebtAccountForUserAsync start. UserId: {userId}. DebtAccountId: {debtAccountId}");
            var responseTask = _debtAccountsRepository.SubtractAmountFromDebtAccountForUserAsync(userId, debtAccountId, request);
            _logger.LogInformation($"SubtractAmountFromDebtaccountForUserAsnc end. UserId: {userId}. DebtAccountId: {debtAccountId}");
            return responseTask;   
        }  

        public Task DeleteDebtAccountForUserAsync(Guid userId, Guid debtAccountId)
        {
            _logger.LogInformation($"DeleteDebtAccountForUserAsync start. UserId: {userId}. DebtAccountId: {debtAccountId}");
            var responseTask = _debtAccountsRepository.DeleteDebtAccountForUserAsync(userId, debtAccountId);
            _logger.LogInformation($"DeleteDebtAccountForUserAsync end. UserId: {userId}. DebtAccountId: {debtAccountId}");
            return responseTask;   
        }   

        public Task UpdateDebtAccountForUserAsync(Guid userId, Guid debtAccountId, UpdateDebtAccountRequest request)
        {
            _logger.LogInformation($"UpdatedebtAccountForAsync start. UserId: {userId}. DebtAccountId: {debtAccountId}");
            var responseTask = _debtAccountsRepository.UpdateDebtAccountForUserAsync(userId, debtAccountId, request);
            _logger.LogInformation($"UpdateDebtAccountForUserAsync end. UserId: {userId}. DebtAccountId: {debtAccountId}");
            return responseTask;   
        }  
    }
}
