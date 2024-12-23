using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Dto
{
    public class CastDto
    {
        public int Id { get; set; }
        public string CharacterName { get; set; } = null!;
        public int MovieId { get; set; }
        public int ActorId { get; set; }
    }
}
