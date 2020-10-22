using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository.DTOs
{
    public class ExpenseDTO
    {
        public long Id { get; set; }
        public string ExpenseName { get; set; }
        public DataTime TimeStamp { get; set; }
        public long AmountPaid { get; set; }
        public long AmountOwed { get; set; }
    }
}
