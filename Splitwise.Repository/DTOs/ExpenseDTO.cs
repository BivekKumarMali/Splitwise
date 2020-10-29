using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository.DTOs
{
    public class ExpenseDTO
    {
        public long Id { get; set; }
        public string ExpenseName { get; set; }
        public string UserName { get; set; }
        public string TimeStamp { get; set; }
       // public long AmountOwed { get; set; }
        // public long AmountPaid { get; set; }
        //  public string AmountPaidBy { get; set; }
        //  public string AmountOwedBy { get; set; }
    }
}
