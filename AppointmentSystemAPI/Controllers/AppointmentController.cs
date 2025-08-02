using AppointmentSystemAPI.Data;
using AppointmentSystemAPI.Models;
using AppointmentSystemAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystemAPI.Controllers
{
    [Route("appointmentapi/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<AppointmentController> _logger;
        public AppointmentController(IAppointmentService appointmentService, ILogger<AppointmentController> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }

        [Authorize(Roles = "User")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var appointment = _appointmentService.GetById(id);
            if (appointment == null) return NotFound(); 
            return Ok(appointment);
        }

        [Authorize(Roles = "User,Expert")]
        [HttpPost]
        public IActionResult Create([FromBody] CreateAppointment dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"{DateTime.UtcNow} : Appointment model was inputed incorrect.");
                return BadRequest(ModelState);
            }
            var result = _appointmentService.Create(dto);
            if (!result) return BadRequest(); 
            return Ok();
        }

        [Authorize(Roles = "User,Expert")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _appointmentService.Delete(id);
            if (!result) return BadRequest(); 
            return Ok();
        }

        [Authorize(Roles = "Expert")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UpdateAppointment dto) 
        {
            if (!ModelState.IsValid) 
            {
                _logger.LogWarning($"{DateTime.UtcNow} : Appointment model was inputed incorrect.");
                return BadRequest(ModelState); 
            }
            var result = _appointmentService.Update(id, dto);
            if (!result) return BadRequest(); 
            return Ok();
        }
    }
}
