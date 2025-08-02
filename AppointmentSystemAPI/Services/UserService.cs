using AppointmentSystemAPI.Controllers;
using AppointmentSystemAPI.Data;
using AppointmentSystemAPI.Models;
using AppointmentSystemAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Security.Claims;

namespace AppointmentSystemAPI.Services
{
    public interface IUserService
    {
        GetAppUser GetMyProfile();
        bool UpdateMyProfile(UpdateAppUser dto);
        List<GetAppUser> GetAllExperts();
        GetAppUser GetExpertById(int id);
        List<GetAppointment> GetMyAppointments();
    }
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(IHttpContextAccessor httpContextAccessor, AppDbContext context, ILogger<UserService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _logger = logger;
        }

        public GetAppUser GetMyProfile()
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _context.AppUsers.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                _logger.LogWarning($"{DateTime.UtcNow} : User with ID {userId} does not exist.");
                return null;
            }

            _logger.LogInformation($"{DateTime.UtcNow} : User {user.Username} (ID: {userId}) received his profile.");
            return new GetAppUser
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Specialization = user.Specialization,
                Description = user.Description
            };
        }

        public bool UpdateMyProfile(UpdateAppUser dto)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _context.AppUsers.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                _logger.LogWarning($"{DateTime.UtcNow} : User with ID {userId} not found.");
                return false;
            }
            user.Firstname = dto.Firstname;
            user.Lastname = dto.Lastname;
            user.Email = dto.Email;
            user.Specialization = dto.Specialization;
            user.Description = dto.Description;

            _context.SaveChanges();
            _logger.LogInformation($"{DateTime.UtcNow} : User {user.Username} (ID: {userId}) updated his profile.");
            return true;
        }

        public List<GetAppUser> GetAllExperts()
        {
            var experts = _context.AppUsers
            .Where(u => u.Role == Role.Expert)
            .Select(expert => new GetAppUser
            {
                Firstname = expert.Firstname,
                Lastname = expert.Lastname,
                Email = expert.Email,
                Specialization = expert.Specialization,
                Description = expert.Description
            })
            .ToList();
            if (experts == null || experts.Count == 0)
            {
                _logger.LogWarning($"{DateTime.UtcNow} : Experts not found.");
                return null;
            }
            _logger.LogInformation($"{DateTime.UtcNow} : {experts.Count} experts received.");
            return experts;
        }
        public GetAppUser GetExpertById(int id)
        {
            var expert = _context.AppUsers
                .Where(u => u.Id == id)
                .Select(e => new GetAppUser {
                    Firstname = e.Firstname, 
                    Lastname = e.Lastname, 
                    Email = e.Email, 
                    Description = e.Description, 
                    Specialization = e.Specialization
                }).FirstOrDefault();
            if (expert == null)
            {
                _logger.LogWarning($"{DateTime.UtcNow} : Expert with ID {id} not found.");
                return null;
            }
            _logger.LogInformation($"{DateTime.UtcNow} : Expert with ID {id} received.");
            return expert;
        }
        public List<GetAppointment> GetMyAppointments()
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role).ToString();

            List<GetAppointment> appointments = new List<GetAppointment>();

            if (role == "User")
            {
                appointments = _context.Appointments
                    .Where(a => a.UserId == userId)
                    .Select(appointment => new GetAppointment
                    {
                        Description = appointment.Description,
                        CreatingTime = appointment.CreatingTime,
                        StartTime = appointment.StartTime,
                        EndTime = appointment.EndTime,
                        Status = appointment.Status
                    })
                    .ToList();
            }
            else if (role == "Expert")
            {
                appointments = _context.Appointments
                    .Where(a => a.ExpertId == userId)
                    .Select(appointment => new GetAppointment
                    {
                        Description = appointment.Description,
                        CreatingTime = appointment.CreatingTime,
                        StartTime = appointment.StartTime,
                        EndTime = appointment.EndTime,
                        Status = appointment.Status
                    })
                    .ToList();
            }
            if(appointments == null || appointments.Count == 0)
            {
                _logger.LogWarning($"{DateTime.UtcNow} : Appointments of user with ID {userId} not found.");
                return null;
            }
            _logger.LogInformation($"{DateTime.UtcNow} : User {_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name).ToString()} (ID: {userId}) received his appointments.");
            return appointments;
        }
    }
}
