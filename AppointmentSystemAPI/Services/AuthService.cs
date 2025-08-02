using AppointmentSystemAPI.Data;
using AppointmentSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;

namespace AppointmentSystemAPI.Services
{
    public interface IAuthService
    {
        AuthResult Login(AppUserLogin dto);
        AuthResult Register(CreateAppUser dto);
    }
    public class AuthResult
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
    }
    public class AuthService : IAuthService
    {
        private readonly JwtService _jwtService;
        private readonly AppDbContext _context;
        private readonly ILogger<AuthService> _logger;

        public AuthService(JwtService jwtService, AppDbContext context, ILogger<AuthService> logger)
        {
            _jwtService = jwtService;
            _context = context;
            _logger = logger;
        }

        public static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public static bool VerifyPassword(string rawPassword, string hashedPassword)
        {
            var hashOfInput = HashPassword(rawPassword);
            return hashOfInput == hashedPassword;
        }

        public AuthResult Login(AppUserLogin dto)
        {
            var user = _context.AppUsers.FirstOrDefault(u => u.Username == dto.Username);
            if (user == null)
            {
                _logger.LogWarning($"{DateTime.UtcNow} : Unsuccessful login attempt. Username not found.");
                return new AuthResult { Success = false, Message = "User not found." };
            }
            if (!VerifyPassword(dto.Password, user.Password))
            {
                _logger.LogWarning($"{DateTime.UtcNow} : Unsuccessful login attempt for {user.Username}. Incorrect password.");
                return new AuthResult { Success = false, Message = "Password is incorrect." };
            }
            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Role.ToString(), user.Username);
            _logger.LogInformation($"{DateTime.UtcNow} : Successful login for {user.Username}. (ID: {user.Id})");
            return new AuthResult { Success = true, Token = token };
        }
        public AuthResult Register(CreateAppUser dto)
        {
            if (_context.AppUsers.Any(u => u.Username == dto.Username)) 
            {
                _logger.LogWarning($"{DateTime.UtcNow} : Unsuccessful register attempt. Username \"{dto.Username}\" already exists.");
                return new AuthResult { Success = false, Message = "Username already exists." }; 
            }
            if(_context.AppUsers.Any(u => u.Email == dto.Email))
            {
                _logger.LogWarning($"{DateTime.UtcNow} : Unsuccessful register attempt. Account with Email \"{dto.Email}\" already exists.");
                return new AuthResult { Success = false, Message = "Account with equal Email already exists." };
            }
            var newUser = new AppUser
            {
                Username = dto.Username,
                Password = HashPassword(dto.Password),
                Email = dto.Email,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Specialization = dto.Specialization,
                Description = dto.Description,
                Role = dto.Role
            };
            _context.AppUsers.Add(newUser);
            _context.SaveChanges();
            var token = _jwtService.GenerateToken(newUser.Id.ToString(), newUser.Role.ToString(), newUser.Username);
            _logger.LogInformation($"{DateTime.UtcNow} : Successful register for {newUser.Username}. (ID: {newUser.Id})");
            return new AuthResult { Success = true, Token = token };
        }
    }
}
