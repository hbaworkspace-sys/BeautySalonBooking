using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Contracts.Authentication.Requests;
using Microsoft.AspNetCore.Authorization;

//using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    //private readonly IValidator<LoginInitiateRequest> _validator;

    public AuthenticationController(IAuthenticationService authenticationService/*, IValidator<LoginInitiateRequest> validator*/)
    {
        _authenticationService = authenticationService;
        //  _validator = validator;
    }

    [HttpPost("register/request-otp")]
    public async Task<IActionResult> RegisterInitiate([FromBody] RegisterInitiateRequest request, CancellationToken cancellationToken)
    {

        var result = await _authenticationService.RequestRegisterOtpAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("register/verify-otp")]
    public async Task<IActionResult> VerifyRegistrationOtp([FromBody] VerifyOtpRequest request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.VerifyRegisterOtpAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("login/request-otp")]
    public async Task<IActionResult> LoginInitiate([FromBody] LoginInitiateRequest request, CancellationToken cancellationToken)
    {
        //var validationResult = await _validator.ValidateAsync(request);
        //if (!validationResult.IsValid)
        //{
        //    return BadRequest(validationResult.Errors);
        //}
        var result = await _authenticationService.RequestLoginOtpAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("login/verify-otp")]
    public async Task<IActionResult> VerifyLoginOtp([FromBody] VerifyOtpRequest request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.VerifyLoginOtpAsync(request, cancellationToken);
        return Ok(result);
    }
}