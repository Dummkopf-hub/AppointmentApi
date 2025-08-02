using AppointmentSystemAPI.Data;
using AppointmentSystemAPI.Models;

namespace AppointmentSystemAPI.Services
{
    public interface IAppointmentService
    {
        GetAppointment GetById(int id);
        bool Create(CreateAppointment dto);
        bool Delete(int id);
        bool Update(int id, UpdateAppointment dto);
    }
    public class AppointmentService : IAppointmentService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AppointmentService> _logger;
        public AppointmentService(AppDbContext context, ILogger<AppointmentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public GetAppointment GetById(int id)
        {
            var appointment = _context.Appointments
                .Where(a => a.Id == id)
                .Select(appointment => new GetAppointment 
                {
                    Description = appointment.Description,
                    CreatingTime = appointment.CreatingTime,
                    StartTime = appointment.StartTime,
                    EndTime = appointment.EndTime,
                    Status = appointment.Status 
                })
                .FirstOrDefault();
            if (appointment == null) 
            {
                _logger.LogWarning($"{DateTime.UtcNow} : Appointment not found.");
                return null;
            }
            _logger.LogInformation($"{DateTime.UtcNow} : Appointment with ID {id} received.");
            return appointment;
        }

        public bool Create(CreateAppointment dto)
        {
            var appointment = new Appointment
            {
                Description = dto.Description,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                CreatingTime = DateTime.UtcNow,
                Status = 0,
                ExpertId = dto.ExpertId,
                UserId = dto.UserId
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            _logger.LogInformation($"{DateTime.UtcNow} : Appointment created.");
            return true;
        }

        public bool Delete(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                _logger.LogWarning($"{DateTime.UtcNow} : Appointment with ID {id} not found.");
                return false;
            }
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
            _logger.LogInformation($"{DateTime.UtcNow} : Appointment with ID {id} deleted.");
            return true;
        }

        public bool Update(int id, UpdateAppointment dto)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                _logger.LogWarning($"{DateTime.UtcNow} : Appointment with ID {id} not found.");
                return false;
            }
            appointment.Description = dto.Description;
            appointment.StartTime = dto.StartTime;
            appointment.EndTime = dto.EndTime;
            appointment.Status = dto.Status;
            appointment.ExpertId = dto.ExpertId;
            appointment.UserId = dto.UserId;

            _context.SaveChanges();
            _logger.LogInformation($"{DateTime.UtcNow} : Appointment with ID {id} updated.");
            return true;
        }
    }
}
