using System.ComponentModel.DataAnnotations;

namespace Identity.DTOs
{
    public class LoginDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [StringLength(8,MinimumLength = 4)]
        public string Password{get;set;}
        
    }
}