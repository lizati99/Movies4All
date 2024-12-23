using System;
using System.Collections.Generic;

namespace Movies4All.App.Models
{
    public partial class Director
    {
        public Director()
        {
            Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
