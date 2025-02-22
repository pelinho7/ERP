using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Identity.DTOs
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}