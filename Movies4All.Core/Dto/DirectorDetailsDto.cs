using Movies4All.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Dto
{
    public class DirectorDetailsDto:DirectorDto
    {
        public ICollection<MovieDto> Movies { get; set; }
    }
}
