using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using KkotipAppApi.Entities;
using KkotipAppApi.Extensions;
using KkotipAppApi.Objects;
using KkotipAppApi.Services;

namespace KkotipAppApi.Controllers;

[Route("api/")]
[ApiController]
public class LoginController(ILogger<LoginController> logger, LoginService service) : ControllerBase
{
    private readonly ILogger<LoginController> _logger = logger;

    private readonly LoginService _service = service;

    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<LoginStatus> Login([FromForm] string login, [FromForm] string password)
    {
        if (login == null || password == null)
        {
            return Unauthorized();
        }

        var status = _service.Login(login, password);
        if(status.Success)
        {
            return Ok(status);
        }
        else
        {
            return Unauthorized(status);
        }
    }
}
