using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.Models
{
    public class Settlement
    {
        [Key]
        public long Id { get; set; }

        public long Amount { get; set; }

        public DateTime TimeStamp { get; set; }

        public string PayUserId { get; set; }
        [ForeignKey("PayUserId")]
        public ApplicationUser User1 { get; set; }

        public string PayeeUserId { get; set; }
        [ForeignKey("PayeeUserId")]
        public ApplicationUser User2 { get; set; }

        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
    }
}
