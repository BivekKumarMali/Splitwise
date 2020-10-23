using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.Models
{
    public class ApplicationUser
    {
        [Key][ForeignKey("AspNetUsers")]
        public string UserId { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
