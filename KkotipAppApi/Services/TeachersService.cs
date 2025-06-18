using Microsoft.EntityFrameworkCore;

using KkotipAppApi.Entities;

namespace KkotipAppApi.Services;

public class TeachersService(ILogger<TeachersService> logger, ApplicationContext context)
{
    private readonly ILogger<TeachersService> _logger = logger;
    private readonly ApplicationContext _context = context;

    public IEnumerable<string> GetTeachers()
    {
        return _context.Lessons
            .Include(l => l.Teacher)
            .Select(l => l.Teacher)
            .Distinct()
            .OrderBy(t => string.Join(" ", t.LastName,t.FirstName,t.MiddleName ?? ""))
            .Select(t => $"{t.LastName} {t.FirstName} {t.MiddleName ?? ""}");
    }
}
