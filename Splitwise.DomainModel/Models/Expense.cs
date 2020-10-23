using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.Models
{
    public class Expense
    {
        [Key]
        public long Id { get; set; }

        public string ExpenseName { get; set; }

        public DateTime TimeStamp { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }
    }
}
