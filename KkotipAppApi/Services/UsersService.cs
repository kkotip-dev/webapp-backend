using Microsoft.EntityFrameworkCore;

using KkotipAppApi.Entities;
using KkotipAppApi.Objects;

namespace KkotipAppApi.Services;

public class UsersService(ApplicationContext context, ILogger<UsersService> logger)
{
    private readonly ILogger<UsersService> _logger = logger;
    private readonly ApplicationContext _context = context;

    public Profile GetProfile(User user)
    {
        var group = _context.Groups.Where(x => x.Students.Any(x => x == user)).SingleOrDefault();
        if (group == null)
        {
            _logger.LogDebug("GetProfile: group not found");
        }

        var scores = _context.Scores
            .Include(x => x.Student)
            .Include(x => x.Lesson)
            .Where(x => x.Student == user);

        return new Profile
        {
            AvatarUrl = user.AvatarUrl ?? "",
            LastName = user.LastName,
            FirstName = user.FirstName,
            MiddleName = user.MiddleName,
            Group = group?.Name ?? "",
            Type = user.Type,
            AverageScore = user.Type == UserType.Student ? (float)Math.Round(scores.Select(x => x.ScoreValue).Average(), 2) : 0,
            AverageScores = user.Type == UserType.Student ?
                            scores
                                .GroupBy(x => x.Lesson)
                                .Select(x =>
                                        new LessonScore
                                        {
                                            Lesson = x.Key.Name,
                                            Score = (int)Math.Round(x.Select(y => y.ScoreValue).Average())
                                        }
                                )
                                .ToList() : null!,
            LastScores = user.Type == UserType.Student ?
                         scores
                            .OrderByDescending(x => x.Date)
                            .Take(15)
                            .Select(x =>
                                new LessonScore
                                {
                                    Lesson = x.Lesson.Name,
                                    Score = x.ScoreValue,
                                }
                            )
                            .ToList() : null!
        };
    }

    public void AddStudent(NewStudent student)
    {
        var group = _context.Groups
            .Include(x => x.Students)
            .Where(x => x.Id == student.GroupId)
            .SingleOrDefault();
        if(group == null)
        {
            _logger.LogWarning("Group {} not found", student.GroupId);
            return;
        }

        var user = _context.Users.Add(new User
        {
            LastName = student.LastName,
            FirstName = student.FirstName,
            MiddleName = student.MiddleName,
            Email = student.Email,
            PasswordHash = "",
            PasswordSalt = "",
            Type = UserType.Student,
        });

        group.Students.Add(user.Entity);

        _context.SaveChanges();
    }
}
