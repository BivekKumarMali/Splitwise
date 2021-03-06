﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository.DTOs
{
    public class ExpenseDetailDTO
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountOwed { get; set; }
    }
}
