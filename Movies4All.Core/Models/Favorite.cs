using Movies4All.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movies { get; set; } = null!;
        public int UserId { get; set; }
        public virtual User Users { get; set; } = null!;
    }
}
