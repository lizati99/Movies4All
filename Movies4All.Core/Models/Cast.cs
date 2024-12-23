using System;
using System.Collections.Generic;

namespace Movies4All.App.Models
{
    public partial class Cast
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public string CharacterName { get; set; } = null!;

        public virtual Actor Actor { get; set; } = null!;
        public virtual Movie Movie { get; set; } = null!;
    }
}
