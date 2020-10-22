using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository
{
    public class SettlementRepository : ISettlementRepository<SettlementDTO>
    {
        public void AddSettlement(Settlement settlement)
        {
            throw new NotImplementedException();
        }

        public void DeleteSettlement(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SettlementDTO> SettlementByGroupId(int groupid)
        {
            throw new NotImplementedException();
        }

        public object SettlementByUserId(long userid)
        {
            throw new NotImplementedException();
        }

        public bool SettlementExists(long id)
        {
            throw new NotImplementedException();
        }
    }
}
