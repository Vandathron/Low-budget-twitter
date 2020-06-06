using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Tweeter.Domain
{
    public class User
    {
        [Key]
        public  int Id { get; set; }

        [Required]
        public  string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public  string Email { get; set; }

        public string Bio { get; set; }
    }
}
