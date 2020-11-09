﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository.DTOs
{
    public class SettlementDTO
    {
        public long Id { get; set; }
        public string PayeeName { get; set; }
        public string ReceiverName { get; set; }
        public decimal Amount { get; set; }
    }
}
