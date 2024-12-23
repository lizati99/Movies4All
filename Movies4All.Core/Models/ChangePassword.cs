using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Models
{
    public class ChangePassword
    {
        [Required]
        public string Email { get; set; } = string.Empty!;
        [Required]
        public string CurrentPassword { get; set; } = string.Empty!;
        [Required]
        public string NewPassword { get; set; } = string.Empty!;
        [Required]
        public string ConfirmNewPassword { get; set; } = string.Empty!;

    }
}
