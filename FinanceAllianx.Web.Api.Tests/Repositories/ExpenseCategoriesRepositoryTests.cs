using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using FinanceAllianx.Web.Api.Models;
using FinanceAllianx.Web.Api.Models.Requests.ExpenseCategories;
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
    public class ExpenseCategoriesRepositoryTests
    {
        private struct Stubs
        {
            public ISqlConnectionProvider SqlConnectionProvider { get; set; }
            public ISqlConnectionWrapper SqlConnectionWrapper { get; set; }
            public static ILogger<ExpenseCategoriesRepository> Logger => Substitute.For<ILogger<ExpenseCategoriesRepository>>();
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

        private static ExpenseCategoriesRepository GetSystemUnderTest(Stubs stubs)
        {
            return new ExpenseCategoriesRepository(stubs.SqlConnectionProvider, Stubs.Logger);
        }

        [Test]
        public async Task GetExpenseCategories_OnRequest_ShouldReturnExpectedCategories()
        {
            // Arrange
            var expectedResponse = new List<ExpenseCategory>
            {
                new ExpenseCategory
                {
                    ExpenseCategoryId = Guid.NewGuid(),
                    ExpenseCategoryName = "Rent"
                },
                new ExpenseCategory
                {
                    ExpenseCategoryId = Guid.NewGuid(),
                    ExpenseCategoryName = "Groceries"
                },
                new ExpenseCategory
                {
                    ExpenseCategoryId = Guid.NewGuid(),
                    ExpenseCategoryName = "Utilities"
                }
            };

            var stubs = GetStubs();
            stubs.SqlConnectionWrapper.QueryAsync<ExpenseCategory>(
                    Arg.Any<string>(),
                    commandType: Arg.Any<CommandType>())
                .Returns(expectedResponse);
            var repository = GetSystemUnderTest(stubs);

            // Act
            var actualResponse = await repository.GetExpenseCategoriesAsync();

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task GetExpenseCategoriesForUser_GivenUserId_ShouldReturnExpectedCategories()
        {
            // Arrange
            var expectedResponse = new List<ExpenseCategory>
            {
                new ExpenseCategory
                {
                    ExpenseCategoryId = Guid.NewGuid(),
                    ExpenseCategoryName = "Rent"
                },
                new ExpenseCategory
                {
                    ExpenseCategoryId = Guid.NewGuid(),
                    ExpenseCategoryName = "Groceries"
                },
                new ExpenseCategory
                {
                    ExpenseCategoryId = Guid.NewGuid(),
                    ExpenseCategoryName = "Utilities"
                }
            };

            var stubs = GetStubs();
            stubs.SqlConnectionWrapper.QueryAsync<ExpenseCategory>(
                    Arg.Any<string>(),
                    Arg.Any<DynamicParameters>(),
                    commandType: Arg.Any<CommandType>())
                .Returns(expectedResponse);
            var repository = GetSystemUnderTest(stubs);

            // Act
            var actualResponse = await repository.GetExpenseCategoriesForUserAsync(Guid.NewGuid());

            // Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void AddExpenseCategoryForUser_GivenUserIdAndRequest_ShouldCompleteTransactionWithoutError()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            var request = new AddExpenseCategoryRequest
            {
                ExpenseCategoryName = "Test"
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async() => await repository.AddExpenseCategoryForUserAsync(
                Guid.NewGuid(), 
                request));
        }
        
        [Test]
        public void DeleteExpenseCategoryForUser_GivenUserIdAndExpenseCategoryId_ShouldCompleteTransactionWithoutError()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            // Act & Assert
            Assert.DoesNotThrowAsync(async() => await repository.DeleteExpenseCategoryForUserAsync(
                Guid.NewGuid(), 
                Guid.NewGuid()));
        }
    }
}