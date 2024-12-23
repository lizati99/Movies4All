using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Dto
{
    public class RestPassword
    {
        public string token { get; set; }
        [Required]
        public string Password { get; set; }
        //[Required,Compare("Password")]
        //public string ConfiremPassword { get; set; }
    }
}
