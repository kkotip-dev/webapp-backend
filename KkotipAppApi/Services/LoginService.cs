using System.Security.Cryptography;
using System.Text;
using KkotipAppApi.Entities;
using KkotipAppApi.Extensions;
using KkotipAppApi.Objects;

namespace KkotipAppApi.Services;

public class LoginService(ApplicationContext context, ILogger<LoginService> logger)
{
    private readonly ILogger<LoginService> _logger = logger;
    private readonly ApplicationContext _context = context;

    public LoginStatus Login(string login, string password)
    {
        var user = _context.Users.Where(user => user.Email == login).SingleOrDefault();
        if (user == null)
        {
            _logger.LogDebug("User not found {}", login);
            return new LoginStatus
            {
                Success = false,
                ErrorMessage = "Неправильные логин или пароль"
            };
        }

        if(string.IsNullOrEmpty(user.PasswordHash))
        {
            return new LoginStatus
            {
                Success = false,
                ErrorMessage = "Необходимо сбросить пароль"
            };
        }

        var hash = SHA256.HashData(Encoding.Default.GetBytes(password + user.PasswordSalt)).ToHexString();
        if (user.PasswordHash != hash)
        {
            _logger.LogDebug("Hash not matches");
            return new LoginStatus
            {
                Success = false,
                ErrorMessage = "Неправильные логин или пароль"
            };
        }

        var token = new UserToken
        {
            User = user,
            Token = StringExtensions.RandomString(64),
            Date = DateOnly.FromDateTime(DateTime.Now),
            ExpireDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
        };

        _context.Tokens.Add(token);
        _context.SaveChanges();

        return new LoginStatus
        {
            Success = true,
            Token = token.Token
        };
    }
}