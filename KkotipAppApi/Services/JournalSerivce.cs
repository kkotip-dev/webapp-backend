using KkotipAppApi.Entities;
using KkotipAppApi.Objects;

using JournalEntryObject = KkotipAppApi.Objects.JournalEntry;

namespace KkotipAppApi.Services;

public class JournalSerivce(ApplicationContext context)
{
    private readonly ApplicationContext _context = context;

    public IEnumerable<JournalEntryObject> GetJournalEntries(User user, DateOnly date)
    {
        var journal = _context.JournalEntries
            .Where(x => x.Group.Students.Contains(user))
            .Where(x => x.Date == date)
            .Select(x =>
                new JournalEntryObject
                {
                    Teacher = $"{x.Lesson.Teacher.LastName} {x.Lesson.Teacher.FirstName[0]}. {(x.Lesson.Teacher.MiddleName ?? " ")[0]}{(x.Lesson.Teacher.MiddleName == null ? "" : ".")}",
                    Lesson = x.Lesson.Name,
                    Date = x.Date,
                    Description = x.Description
                }
            );

        return journal;
    }
}
