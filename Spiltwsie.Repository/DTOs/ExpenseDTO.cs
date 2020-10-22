using System;
using System.Collections.Generic;
using System.Text;

namespace Spiltwise.Repository.DTOs
{
    class ExpenseDTO
    {
        public long Id { get; set; }
        public string ExpenseName { get; set; }
        public string[] PayeeName { get; set; }
        public long AmountPaid { get; set; }
        public long AmountOwed { get; set; }
    }
}
