using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.Models
{
    public class Member
    {
        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Group")]
        public long GroupId { get; set; }
    }
}
