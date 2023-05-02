using Movies4All.Core.Models;
using System;
using System.Collections.Generic;

namespace Movies4All.App.Models
{
    public partial class Movie
    {
        public Movie()
        {
            Casts = new HashSet<Cast>();
            Images = new HashSet<Image>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public int RatingId { get; set; }
        public virtual Director Director { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
        public virtual Rating Rating { get; set; } = null!;
        public virtual ICollection<Cast> Casts { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
