using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Dto
{
    public class MovieImageDto
    {
        public int MovieId { get; set; }
        public IFormFile Image { get; set; }
    }
}
