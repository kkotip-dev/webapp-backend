using KkotipAppApi;
using KkotipAppApi.Filters;
using KkotipAppApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<AuthenticationFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddDbContext<ApplicationContext>();

builder.Services.AddScoped<GroupsService>();
builder.Services.AddScoped<JournalSerivce>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<ScheduleService>();
builder.Services.AddScoped<TeachersService>();
builder.Services.AddScoped<UsersService>();

var app = builder.Build();


app.UseCors(
    (cors) => cors
        .WithOrigins("http://localhost:1234", "http://192.168.172.239:1234")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.MapControllers();

app.Run();
