using System;
using System.Collections.Generic;
using System.Text;

namespace Spiltwise.Repository.DTOs
{
    class SettlementDTO
    {
        public long Id { get; set; }
        public string PayeeName { get; set; }
        public string ReceiverName { get; set; }
        public long Amount { get; set; }
    }
}
