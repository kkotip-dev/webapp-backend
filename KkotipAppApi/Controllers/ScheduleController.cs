using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using KkotipAppApi.Entities;
using KkotipAppApi.Objects;
using KkotipAppApi.Services;

using ScheduleEntryObject = KkotipAppApi.Objects.ScheduleEntry;

namespace KkotipAppApi.Controllers;

[Route("api")]
[ApiController]
public class ScheduleController(ILogger<ScheduleController> logger, ScheduleService service) : ControllerBase
{
    private ILogger<ScheduleController> _logger = logger;

    private ScheduleService _service = service;

    [HttpGet("schedule/{date}")]
    public ActionResult<IEnumerable<SchedulePairEntry>> GetSchduleEntries(DateOnly date)
    {
        if (HttpContext.Items["User"] is not User user)
        {
            _logger.LogWarning("User not found");
            return Unauthorized();
        }

        if (user.Type == UserType.Student)
        {
            var schedule = _service.GetStudentPairEntries(user, date);

            return Ok(schedule);
        }
        else
        {
            var schedule = _service.GetTeacherPairEntries(user, date);

            return Ok(schedule);
        }
    }
}
