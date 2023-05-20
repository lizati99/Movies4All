using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Dto
{
    public class ImageDto
    {
        public int Id { get; set; }
        public int movieId { get; set; }
        public string Image { get; set; }
    }
}
