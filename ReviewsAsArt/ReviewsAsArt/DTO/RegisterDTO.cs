using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewsAsArt.DTO
{
    public class RegisterDTO : LoginDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Compare("Password")]
        [RegularExpression("(?=.*?[^A-Za-z0-9])(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$",
            ErrorMessage = "Passwords must be at least 8 characters in length and have at least one digit, one uppercase letter, one lowercase letter and one non-alphanumeric character.")]
        public string PasswordConfirmation { get; set; }
    }
}
