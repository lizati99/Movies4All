using System;
using System.Collections.Generic;

namespace Movies4All.App.Models
{
    public partial class Rating
    {
        public Rating()
        {
            Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
