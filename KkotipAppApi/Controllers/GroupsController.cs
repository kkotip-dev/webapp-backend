using Microsoft.AspNetCore.Mvc;

using KkotipAppApi.Entities;
using KkotipAppApi.Objects;
using KkotipAppApi.Services;

namespace KkotipAppApi.Controllers;

[Route("api")]
[ApiController]
public class GroupsController(ILogger<GroupsController> logger, GroupsService service) : ControllerBase
{
    private readonly ILogger<GroupsController> _logger = logger;

    private readonly GroupsService _service = service;

    [HttpGet("groups")]
    public ActionResult<IEnumerable<string>> GetGroupsList()
    {
        if (HttpContext.Items["User"] is not User user)
        {
            _logger.LogWarning("User not found");
            return Unauthorized();
        }

        if(user.Type != UserType.Administrator)
        {
            _logger.LogWarning("Group list requested not by administrator");
            return BadRequest();
        }

        return Ok(_service.GetGroupsList());
    }

    [HttpGet("group/{groupName}")]
    public ActionResult<IEnumerable<StudentStatistic>> GetStudentStatistics(string groupName)
    {
        if (HttpContext.Items["User"] is not User user)
        {
            _logger.LogWarning("User not found");
            return Unauthorized();
        }

        if(user.Type != UserType.Administrator)
        {
            _logger.LogWarning("Student statictic requested not by administrator");
            return BadRequest();
        }

        var statistic = _service.GetStudentStatistics(groupName);
        if (statistic == null)
        {
            _logger.LogInformation("Group not found");
            return BadRequest();
        }

        return Ok(statistic);
    }
}
