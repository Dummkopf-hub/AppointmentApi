using AppointmentSystemAPI.Data;
using AppointmentSystemAPI.Models;
using AppointmentSystemAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AppointmentSystemAPI.Controllers
{
    [ApiController]
    [Route("appointmentapi/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private IAuthService _authService;
        private readonly ILogger<AuthorizationController> _logger;
        public AuthorizationController(IAuthService authService, ILogger<AuthorizationController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateAppUser dto) 
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"{DateTime.UtcNow} : Inputed user model was incorrect.");
                return BadRequest(ModelState);
            }
            var result = _authService.Register(dto);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(new {token = result.Token});
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] AppUserLogin dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"{DateTime.UtcNow} : Inputed login model was incorrect.");
                return BadRequest(ModelState);
            }
            var result = _authService.Login(dto);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(new {token = result.Token});
        }
    }
}
