using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Dto
{
    public class MovieDetailsDto
    {
        [NotMapped]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string NameGenre { get; set; } = null!;
        public string NameRating { get; set; } = null!;
        public ICollection<ImageDto> Images { get; set; }
    }
}
