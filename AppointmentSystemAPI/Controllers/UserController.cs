using AppointmentSystemAPI.Data;
using AppointmentSystemAPI.Models;
using AppointmentSystemAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace AppointmentSystemAPI.Controllers
{
    [Route("appointmentapi/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger) 
        {
            _userService = userService;
            _logger = logger;
        }

        //_logger.LogWarning($"{DateTime.UtcNow} : ");
        //_logger.LogInformation($"{DateTime.UtcNow} : ");

        [Authorize(Roles = "User,Expert")]
        [HttpGet("my-profile")]
        public IActionResult GetMyProfile() 
        {
            var result = _userService.GetMyProfile();
            if (result == null) 
            {
                _logger.LogWarning($"{DateTime.UtcNow} : User not found.");
                return NotFound(); 
            }
            return Ok(result);
        }
        [Authorize(Roles = "User,Expert")]
        [HttpPut("update-profile")]
        public IActionResult UpdateMyProfile([FromBody] UpdateAppUser dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"{DateTime.UtcNow} : Inputed model was incorrect.");
                return BadRequest(ModelState);
            }
            var success = _userService.UpdateMyProfile(dto);
            if (!success) 
            {
                _logger.LogWarning($"{DateTime.UtcNow} : Updating of profile was unsuccessful.");
                return NotFound(); 
            }
            return Ok();
        }
        [HttpGet("experts")]
        [Authorize(Roles = "User")]
        public IActionResult GetAllExperts()
        {
            var experts = _userService.GetAllExperts();
            if (experts == null) return NotFound();
            return Ok(experts);
        }
        [HttpGet("experts/{id}")]
        [Authorize(Roles = "User")]
        public IActionResult GetExpertById(int id)
        {
            var expert = _userService.GetExpertById(id);
            if (expert == null) return NotFound();
            return Ok(expert);
        }
        [Authorize(Roles = "User,Expert")]
        [HttpGet("appointments")]
        public IActionResult GetMyAppointments()
        {
            var appointments = _userService.GetMyAppointments();
            if (appointments == null) return NotFound();
            return Ok(appointments);
        }
    }
}
