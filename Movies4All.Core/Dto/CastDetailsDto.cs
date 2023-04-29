using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Dto
{
    public class CastDetailsDto: CastDto
    {
        public string MovieName { get; set; } = null;
        public string ActorName { get; set; } = null;
        public int MovieId { get; set; } 
        public int ActorId { get; set; } 

    }
}
