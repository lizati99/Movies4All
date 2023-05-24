using Movies4All.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
