using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using KkotipAppApi.Entities;
using KkotipAppApi.Objects;
using KkotipAppApi.Services;

using JournalEntryObject = KkotipAppApi.Objects.JournalEntry;

namespace KkotipAppApi.Controllers;

[Route("api")]
[ApiController]
public class JournalController(ILogger<JournalController> logger, JournalSerivce service) : ControllerBase
{
    private readonly ILogger<JournalController> _logger = logger;

    private readonly JournalSerivce _service = service;

    [HttpGet("journal/{date}")]
    public ActionResult<IEnumerable<JournalEntryObject>> GetJournalEntries(DateOnly date)
    {
        if (HttpContext.Items["User"] is not User user)
        {
            _logger.LogWarning("User not found");
            return Unauthorized();
        }

        if (user.Type != UserType.Student)
        {
            _logger.LogWarning("Not student requested journal entries");
            return Ok("Not supported");
        }

        var journal = _service.GetJournalEntries(user, date);

        return Ok(journal);
    }

}
