using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Splitwise.DomainModel.Models
{
    public class ApplicationUser
    {
        [Key]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser IdentityUser { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
