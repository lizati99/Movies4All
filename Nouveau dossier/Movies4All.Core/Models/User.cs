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
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string Lastname { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = null!;
        //[Required]
        //public string PasswordSalt { get; set; } = null!;
        public string? VerificationToken { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ResetTokenExpires { get; set; }

        public virtual ICollection<Favorite> Favories { get; set; }
    }
}
