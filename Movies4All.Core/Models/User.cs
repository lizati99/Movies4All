using Movies4All.App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Models
{
    public class User
    {
        public User()
        {
            Favories = new HashSet<Favorite>();
        }
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string Lastname { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? VerificationToken { get; set; }
        public string? RestPassword { get; set; }
        public DateTime? VirefieAT { get; set; }
        public DateTime? RestTokenExpires { get; set; }
        public virtual ICollection<Favorite> Favories { get; set; }
    }
}
