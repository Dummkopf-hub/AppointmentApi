using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AppointmentSystemAPI.Models
{
    public enum Role
    {
        User,
        Expert
    }
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Specialization { get; set; }
        public string? Description { get; set; }
        public Role Role { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = [];
    }

    public class AppUserLogin
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }

    public class CreateAppUser
    {
        [Required(ErrorMessage = "Username is required.")]
        [MinLength(3, ErrorMessage = "Not enough symbols. (Minimum 3)")]
        [MaxLength(12, ErrorMessage = "Too many symbols. (Maximum 12)")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Not enough symbols. (Minimum 8)")]
        [MaxLength(24, ErrorMessage = "Too many symbols. (Maximum 24)")]
        public string Password { get; set; }
        [EmailAddress(ErrorMessage = "Incorrect format, input email.")]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Firstname is required.")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Lastname is required.")]
        public string Lastname { get; set; }
        public string? Specialization { get; set; }
        [MaxLength(350)]
        public string? Description { get; set; }
        [Range(0, 1, ErrorMessage = "Out of range.")]
        [Required(ErrorMessage = "Role is required.")]
        public Role Role { get; set; }
    }
    public class UpdateAppUser
    {
        [EmailAddress(ErrorMessage = "Incorrect format, input email.")]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Firstname is required.")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Lastname is required.")]
        public string Lastname { get; set; }
        public string? Specialization { get; set; }
        [MaxLength(350)]
        public string? Description { get; set; }
    }

    public class GetAppUser
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Specialization { get; set; }
        public string? Description { get; set; }
    }
}
