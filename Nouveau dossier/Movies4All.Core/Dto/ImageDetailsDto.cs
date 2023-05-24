using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Dto
{
    public class ImageDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null;
        public string MovieName { get; set; } = null;
        public int MovieId { get; set; }
    }
}
