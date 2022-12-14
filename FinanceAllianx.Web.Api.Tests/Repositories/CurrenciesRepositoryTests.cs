using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FinanceAllianx.Web.Api.Models;
using FinanceAllianx.Web.Api.Models.Requests.Currencies;
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
    public class CurrenciesRepositoryTests
    {
        private struct Stubs
        {
            public ISqlConnectionProvider SqlConnectionProvider { get; set; }
            public ISqlConnectionWrapper SqlConnectionWrapper { get; set; }
            public static ILogger<CurrenciesRepository> Logger => Substitute.For<ILogger<CurrenciesRepository>>();
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

        private static CurrenciesRepository GetSystemUnderTest(Stubs stubs)
        {
            return new CurrenciesRepository(stubs.SqlConnectionProvider, Stubs.Logger);
        }

        [Test]
        public async Task GetCurrencies_OnRequest_ShouldReturnExpectedCurrencies()
        {
            // Arrange
            var expectedCurrencies = new List<Currency>
            {
                new Currency
                {
                    Country = "South Africa",
                    Name = "South African Rand",
                    CurrencyId = Guid.NewGuid(),
                    CountryCurrencyCode = "ZAR",
                    RandExchangeRate = 1.0
                }
            };

            var stubs = GetStubs();
            stubs.SqlConnectionWrapper.QueryAsync<Currency>(
                    Arg.Any<string>(),
                    commandType: Arg.Any<CommandType>())
                .Returns(expectedCurrencies);
            var repository = GetSystemUnderTest(stubs);

            // Act
            var actualCurrencies = await repository.GetCurrenciesAsync();

            // Assert
            actualCurrencies.Should().BeEquivalentTo(expectedCurrencies);
        }

        [Test]
        public void AddCurrency_GivenCreateCurrencyRequest_ShouldCompleteTransactionWithoutError()
        {
            // Arrange
            var stubs = GetStubs();
            var repository = GetSystemUnderTest(stubs);

            var request = new AddCurrencyRequest
            {
                Country = "South Africa",
                Name = "South African Rand",
                CountryCurrencyCode = "ZAR",
                RandExchangeRate = 1.0
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await repository.AddCurrencyAsync(request));
        }
    }
}