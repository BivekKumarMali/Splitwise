using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.Models
{
    public class Message
    {
        [Key]
        public long Id { get; set; }

        public string Information { get; set; }

        public string TimeStamp { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }

        [ForeignKey("Expense")]
        public long ExpenseId { get; set; }
    }
}
