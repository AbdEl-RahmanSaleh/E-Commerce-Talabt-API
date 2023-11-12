using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.UserService.Dto
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^A-Za-z0-9])(?!.*\\s).*$"
            ,ErrorMessage = "This Password must conatains 1 upperCasr , 1 LowerCase , 1 Number ,1 non alphanumeric and at least 6 characters ")]
        public string Password { get; set; }
    }
}
