using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using FinanceAllianx.Web.Api.Models;
using FinanceAllianx.Web.Api.Models.Requests.Budgets;
using FinanceAllianx.Web.Api.Repositories;
using FinanceAllianx.Web.Api.Repositories.Connection;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace FinanceAllianx.Web.Api.Tests.Repositories
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class BudgetsRepositoryTests
    {
        private struct Stubs
        {
            public ISqlConnectionProvider SqlConnectionProvider { get; set; }
            public ISqlConnectionWrapper SqlConnectionWrapper { get; set; }
            public static ILogger<BudgetsRepository> Logger => Substitute.For<ILogger<BudgetsRepository>>();
        }

        private static Stubs GetStubs()
        {
            var stubs = new Stubs
            {
                SqlConnectionProvider = Substitute.For<ISqlConnectionProvider>(),
                SqlConnectionWrapper = Substitute.For<ISqlConnectionWrapper>()
            };
            stubs.SqlConnectionProvider.Open().Returns(stubs.SqlConnectionWrapper);

            return stubs;
        }

        private static BudgetsRepository GetSystemUnderTest(Stubs stubs)
        {
            return new BudgetsRepository(stubs.SqlConnectionProvider, Stubs.Logger);
        }

        [Test]
        public async Task GetBudgetForUserAsync_GivenUserId_ShouldReturnExpectedExpenses()
        {
            // Arrange
            var expenses = new List<Expense>
            {
                new Expense
                {
                    BudgetId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    ExpenseId = Guid.NewGuid(),
                    DisplayName = "Groceries",
                    CountryCurrencyCode = "ZAR",
                    Value = 3500.0
                }
            };

            var stubs = GetStubs();
            stubs.SqlConnectionWrapper.QueryAsync<Expense>(
                    Arg.Any<string>(),
                    Arg.Any<DynamicParameters>(),
                    commandType: Arg.Any<CommandType>())
                .Returns(expenses);
            var repository = GetSystemUnderTest(stubs);

            // Act
            var actualExpenses = await repository.GetBudgetForUserAsync(Guid.NewGuid());

            // Assert
            actualExpenses.Should().BeEquivalentTo(expenses);
        }

        [Test]
        public void CreateExpenseForUserAsync_GivenUserIdAndCreateRequest_ShouldCompleteTransactionWithoutError()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            var request = new CreateExpenseRequest
            {
                ExpenseCategoryName = "Groceries",
                CountryCurrencyCode = "ZAR",
                Value = 3500.0
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await repository.CreateExpenseForUserAsync(Guid.NewGuid(), request));
        }

        [Test]
        public void DeleteExpenseForUserAsync_GivenUserIdAndExpenseId_ShouldCompleteTransactionWithoutError()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await repository.DeleteExpenseForUserAsync(
                Guid.NewGuid(),
                Guid.NewGuid()));
        }

        [Test]
        public void UpdateExpenseForUserAsync_GivenUserIdAndExpenseIdAndUpdateRequest_ShouldCompleteTransactionWithoutError()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            var request = new UpdateExpenseRequest
            {
                ExpenseCategoryName = "Rent",
                CountryCurrencyCode = "ZAR",
                Value = 7500.0
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await repository.UpdateExpenseForUserAsync(
                Guid.NewGuid(), 
                Guid.NewGuid(),
                request));
        }
    }
}