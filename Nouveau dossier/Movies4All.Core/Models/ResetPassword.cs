using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Models
{
    [NotMapped]
    public class ResetPassword
    {
        [Required]
        public string NewPassword { get; set; } = null!;
        [Required]
        public string ConfirmNewPassword { get; set; } = null!;
        public string Token { get; set; }
    }
}
