using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Dto
{
    public class RegistreUserDto
    {
        [Required(ErrorMessage = "FirstName")]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "LastName")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
