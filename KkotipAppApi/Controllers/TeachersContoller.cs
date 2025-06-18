using Microsoft.AspNetCore.Mvc;

using KkotipAppApi.Entities;
using KkotipAppApi.Services;

namespace KkotipAppApi.Controllers;

[Route("api/")]
[ApiController]
public class TeachersContoller(ILogger<TeachersContoller> logger, TeachersService service) : ControllerBase
{
    private readonly ILogger<TeachersContoller> _logger = logger;
    private readonly TeachersService _service = service;

    [HttpGet("teachers")]
    public ActionResult<IEnumerable<string>> GetTeachers()
    {
        if (HttpContext.Items["User"] is not User user)
        {
            _logger.LogWarning("User is not found");
            return Unauthorized();
        }

        if(user.Type != UserType.Administrator)
        {
            _logger.LogWarning("Teachers list requested by not administator");
            return BadRequest();
        }

        return Ok(_service.GetTeachers());
    }
}
