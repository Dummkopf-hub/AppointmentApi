using System.ComponentModel.DataAnnotations;

namespace AppointmentSystemAPI.Models
{
    public enum Status 
    { 
        Sheduled, 
        Cancelled, 
        Completed 
    }

    public class Appointment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatingTime { get; set; }
        public Status Status { get; set; }
        public int ExpertId { get; set; }
        public int UserId { get; set; }
    }

    public class CreateAppointment
    {
        [Required(ErrorMessage = "Description is required!")]
        [StringLength(250)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Time of starting is required!")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "Time of ending is required!")]
        public DateTime EndTime { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Id of Expert can`t be 0!")]
        [Required(ErrorMessage = "Id of Expert is required!")]
        public int ExpertId { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Id of User can`t be 0!")]
        [Required(ErrorMessage = "Id of User is required!")]
        public int UserId { get; set; }
    }

    public class UpdateAppointment
    {
        [Required(ErrorMessage = "Description is required!")]
        [StringLength(250)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Time of starting is required!")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "Time of ending is required!")]
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "Status number is required!")]
        [Range(0, 2, ErrorMessage = "Out of range!")]
        public Status Status { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Id of Expert can`t be 0!")]
        [Required(ErrorMessage = "Id of Expert is required!")]
        public int ExpertId { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Id of User can`t be 0!")]
        [Required(ErrorMessage = "Id of User is required!")]
        public int UserId { get; set; }
    }

    public class GetAppointment
    {
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatingTime { get; set; }
        public Status Status { get; set; }
    }
}
