using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;

using KkotipAppApi.Entities;
using KkotipAppApi.Objects;
using KkotipAppApi.Services;

namespace KkotipAppApi.Controllers;

[Route("api/")]
[ApiController]
public class UsersController(ILogger<UsersController> logger, UsersService service) : ControllerBase
{
    private readonly ILogger<UsersController> _logger = logger;

    private readonly UsersService _service = service;

    [HttpGet("profile")]
    public ActionResult<Profile> GetProfile()
    {
        if (HttpContext.Items["User"] is not User user)
        {
            _logger.LogWarning("User not found");
            return Unauthorized();
        }

        var profile = _service.GetProfile(user);

        return Ok(profile);
    }

    [HttpPost("student")]
    public ActionResult AddStudent(NewStudent student)
    {
        if (HttpContext.Items["User"] is not User user)
        {
            _logger.LogWarning("User not found");
            return Unauthorized();
        }

        if(user.Type != UserType.Administrator)
        {
            _logger.LogWarning("Tried to add student by not an administrator");
            return BadRequest();
        }

        try
        {
            _service.AddStudent(student);
        }
        catch(Exception)
        {
            return BadRequest();
        }

        return Ok();
    }
}
