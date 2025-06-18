using Microsoft.EntityFrameworkCore;
using KkotipAppApi.Entities;
using KkotipAppApi.Objects;

using ScheduleEntryObject = KkotipAppApi.Objects.ScheduleEntry;

namespace KkotipAppApi.Services;

public class ScheduleService(ApplicationContext context)
{
    private readonly ApplicationContext _context = context;

    private record class SchedulePairProjection
    {
        public int FirstCall { get; set; }
        public TimeOnly FirstTime { get; set; }
        public int SecondCall { get; set; }
        public TimeOnly SecondTime { get; set; }
        public string FirstLessonName { get; set; }
        public string FirstLessonTeacher { get; set; }
        public string FirstLessonLocation { get; set; }
        public string SecondLessonName { get; set; }
        public string SecondLessonTeacher { get; set; }
        public string SecondLessonLocation { get; set; }
    }

    public IEnumerable<SchedulePairEntry> GetStudentPairEntries(User user, DateOnly date)
    {
        var lessons = _context.Lessons.Where(x => x.Groups.Any(x => x.Students.Contains(user)));

        var schedule = _context.Database.SqlQueryRaw<SchedulePairProjection>("""
                SELECT c1."Time" as "FirstTime", 
                        c1."Id" as "FirstCall", 
                        c2."Time" as "SecondTime", 
                        c2."Id" as "SecondCall",
                        MAX(CASE WHEN s."CallId" = c1."Id" THEN l."Name" END) FirstLessonName,
                        MAX(CASE WHEN s."CallId" = c1."Id" THEN t."Name" END) FirstLessonTeacher,
                        MAX(CASE WHEN s."CallId" = c1."Id" THEN loc."Name" END) FirstLessonLocation,
                        MAX(CASE WHEN s."CallId" = c2."Id" THEN l."Name" END) SecondLessonName,
                        MAX(CASE WHEN s."CallId" = c2."Id" THEN t."Name" END) SecondLessonTeacher,
                        MAX(CASE WHEN s."CallId" = c2."Id" THEN loc."Name" END) SecondLessonLocation
                FROM "Pairs" p
                LEFT JOIN "Calls" c1 ON p."FirstCall" = c1."Id"
                LEFT JOIN "Calls" c2 ON p."SecondCall" = c2."Id"
                LEFT JOIN "ScheduleEntries" s ON s."CallId" = c1."Id" OR s."CallId" = c2."Id"
                LEFT JOIN "Users" u ON u."GroupId" = s."GroupId"
                LEFT JOIN "Lessons" l ON l."Id" = s."LessonId"
                LEFT JOIN "Users" t ON t."Id" = l."TeacherId"
                LEFT JOIN "Locations" loc ON loc."Id" = s."LocationId"
                WHERE u."Id" = {0} AND s."Date" = {1}
                GROUP BY c1."Time", c1."Id", c2."Time", c2."Id"
                """, user.Id, date).ToList()
            .Select(p => new SchedulePairEntry
            {
                Lessons = [
                    new ScheduleEntryObject {
                            CallNumber = p.FirstCall,
                            CallTime = p.FirstTime.ToString("hh:mm"),
                            LessonName = p.FirstLessonName,
                            Location = p.FirstLessonLocation,
                            ScheduleEntryName = p.FirstLessonTeacher
                        },
                        new ScheduleEntryObject {
                            CallNumber = p.SecondCall,
                            CallTime = p.SecondTime.ToString("hh:mm"),
                            LessonName = p.SecondLessonName,
                            Location = p.SecondLessonLocation,
                            ScheduleEntryName = p.SecondLessonTeacher
                        },
                ],
                Current = new TimeOnly[] { TimeOnly.FromDateTime(DateTime.Now) }.Any(t => (t - p.FirstTime).TotalMinutes < 40 || (t - p.SecondTime).TotalMinutes < 40)
            });

        return schedule;
    }

    public IEnumerable<SchedulePairEntry> GetTeacherPairEntries(User user, DateOnly date)
    {
        return null;
    }

}
