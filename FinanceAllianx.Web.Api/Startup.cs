using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Dapper;
using FinanceAllianx.Web.Api.Errors;
using FinanceAllianx.Web.Api.Helpers;
using FinanceAllianx.Web.Api.Managers;
using FinanceAllianx.Web.Api.Repositories;
using FinanceAllianx.Web.Api.Repositories.Connection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using Shared.WebApi.Core.Builders;
using Shared.WebApi.Core.Errors;
using Shared.WebApi.Core.Extensions;
using Shared.WebApi.Core.Security;

#pragma warning disable 1591

namespace FinanceAllianx.Web.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private const string ApiTitle = "FinanceAllianx";
        private const int ApiVersion = 1;
        private const string ApiDescription = "A RESTful API that exposes functionality to manage your way to FinanceAllianx.";
        
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Setup the API
            services.AddCors();
            services.AddControllers();
            services.AddTokenAuthentication(Configuration); 
            SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));

            // Setup the Swagger docs.
            var apiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);

            new SwaggerServicesBuilder()
                .WithApiTitle(ApiTitle)
                .WithApiVersion(ApiVersion)
                .WithApiDescription(ApiDescription)
                .WithXmlComments(apiXmlPath)
                .WithCoreXmlDocs(true)
                .WithJwtAuthentication(true)
                .BuildSwaggerServices(services);
            
            // Register implementations
            services.TryAddTransient<IErrorMessageSelector, ErrorMessageSelector>();
            services.TryAddTransient<IJwtService, JwtService>();
            services.TryAddTransient<IDbConnection>(_ => new MySqlConnection(Configuration["ConnectionStrings:FreedomDb"]));
            services.TryAddTransient<ISqlConnectionProvider, SqlConnectionProvider>();
            services.TryAddTransient<IBudgetsRepository, BudgetsRepository>();
            services.TryAddTransient<ICurrenciesRepository, CurrenciesRepository>();
            services.TryAddTransient<ISavingsAccountRepository, SavingsAccountRepository>();
            services.TryAddTransient<IDebtAccountsRepository, DebtAccountsRepository>();
            services.TryAddTransient<IExpenseCategoriesRepository, ExpenseCategoriesRepository>();
            services.TryAddTransient<IBudgetsManager, BudgetsManager>();
            services.TryAddTransient<IExpenseCategoriesManager, ExpenseCategoriesManager>();
            services.TryAddTransient<ICurrenciesManager, CurrenciesManager>();
            services.TryAddTransient<ISavingsAccountManager, SavingsAccountManager>();
            services.TryAddTransient<IDebtAccountsManager, DebtAccountsManager>();
            
            // Register singletons
            services.TryAddSingleton<ISqlConnectionWrapper, SqlConnectionWrapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());
            app.UseSwaggerDocs(ApiVersion, ApiTitle);
            app.UseGlobalExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();  
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}