using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.Models
{
    public class ExpenseDetail
    {
        [Key]
        public long Id { get; set; }

        public long AmountOwe { get; set; }

        public long AmountPaid { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Expense")]
        public long ExpenseId { get; set; }
    }
}
