using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.WebApi.Core.Security;

namespace FinanceAllianx.Web.Api.
{
    [Route("tokens")]
    [ExcludeFromCodeCoverage]  
    public class TestTokensController : ControllerBase
    {
        private readonly IJwtService jwtService;
        private readonly ILogger<TestTokensController> _logger;   

        public TestTokensController(IJwtService jwtService, ILogger<TestTokensController> logger)
        {
            _jwtService = jwtService;
            _logger = logger;  
        }

        [HttpGet]
        [AllowAnonymous]   
        public IActionResult GetRandomTokenAsync()
        {
            _logger.LogInformation("GetRandomTokenAsync start");
            var token = _jwtService.GenerateSecurityToken("fake@mail.com", DateTime.Now);
            return Ok(token);   
        }  
    }
}



