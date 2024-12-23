using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Dto
{
    public class PutMovieDto
    {
        public int Id { get; set; } = 1;
        public string Title { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public int RatingId { get; set; }
    }
}
