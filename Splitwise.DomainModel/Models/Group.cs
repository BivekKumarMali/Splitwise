using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        public string GroupName { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }

    }
}