using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewsAsArt.DTO
{
    public class LoginDTO
    {
        [Required]
        [StringLength(200)]
        public string Username { get; set; }
        

        [Required]
        public string Password { get; set; }
    }
}
