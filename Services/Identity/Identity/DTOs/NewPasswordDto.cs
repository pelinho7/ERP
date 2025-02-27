using System.ComponentModel.DataAnnotations;

namespace Identity.DTOs
{
    public class NewPasswordDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [MinLength(4)]
        public string NewPassword { get; set; }
        [Required]
        public string ResetPasswordToken { get; set; }
    }
}
