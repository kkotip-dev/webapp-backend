using Microsoft.EntityFrameworkCore;

using KkotipAppApi.Entities;
using KkotipAppApi.Objects;

namespace KkotipAppApi.Services;

public class GroupsService(ILogger<GroupsService> logger, ApplicationContext context)
{
    private ILogger<GroupsService> _logger = logger;
    private readonly ApplicationContext _context = context;

    public IEnumerable<string> GetGroupsList()
    {
        return _context.Groups.Select(x => x.Name);
    }

    // TODO: поменять на groupId?
    public IEnumerable<StudentStatistic>? GetStudentStatistics(string groupName)
    {
        var group = _context.Groups
            .Include(x => x.Students)
            .ThenInclude(y => y.Scores)
            .Where(x => x.Name == groupName)
            .SingleOrDefault();
        if (group == null)
        {
            _logger.LogWarning("Group not found: {}", groupName);
            return null;
        }

        return group.Students.Select(x => new StudentStatistic
        {
            Id = x.Id,
            Name = $"{x.LastName} {x.FirstName} {x.MiddleName ?? ""}",
            Email = x.Email,
            LastScores = x.Scores?.OrderByDescending(x => x.Date).Select(x => x.ScoreValue).Take(6) ?? [],
            Visits = Visits.Full,
        });
    }
}
