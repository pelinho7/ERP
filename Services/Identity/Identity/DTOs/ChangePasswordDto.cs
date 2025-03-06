using System.ComponentModel.DataAnnotations;

namespace Identity.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        public string ActualPassword { get; set; }
        [Required]
        [MinLength(4)]
        public string NewPassword { get; set; }

    }
}
