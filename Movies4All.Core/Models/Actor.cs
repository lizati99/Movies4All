using System;
using System.Collections.Generic;

namespace Movies4All.App.Models
{
    public partial class Actor
    {
        public Actor()
        {
            Casts = new HashSet<Cast>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public virtual ICollection<Cast> Casts { get; set; }
    }
}
