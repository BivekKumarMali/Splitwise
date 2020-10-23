using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.Models
{
    public class Friend
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("User")]
        public long GroupId { get; set; }
    }
}
