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
        public decimal Amount { get; set; }
        public int GroupId { get; set; }
    }
}
