using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.WebApp.Interfaces.Common;
using BeautySalonBooking.WebApp.Interfaces.Login;
using BeautySalonBooking.WebApp.Services.Login;
using BeautySalonBooking.WebApp.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BeautySalonBooking.WebApp.Controller
{
    [ApiController]
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly IApiClient _apiClient;
        private readonly ILogger<LoginService> _logger;
        private readonly ApiSettings _settings;
        private readonly ITokenService _tokenService;
        private readonly NavigationManager _navigationManager;
        private readonly ILoginService _loginService;

        public LoginController(
            IApiClient apiClient,
            IOptions<ApiSettings> settings,
            ILoginService loginService,
            NavigationManager navigationManager,
            ITokenService tokenService,
            ILogger<LoginService> logger)
        {
            _apiClient = apiClient;
            _settings = settings.Value;
            _logger = logger;
            _tokenService = tokenService;
            _navigationManager = navigationManager;
            _loginService = loginService;
        }

        [HttpPost("api/logout")]
        public async Task<IActionResult> Logout()
        {

            var result = _loginService.Logout();
            return Ok(result);
        }
    }
}
