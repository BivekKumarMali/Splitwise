using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository
{
    public interface ISettlementRepository<T> where T: SettlementDTO
    {
        IEnumerable<T> SettlementByGroupId(int groupid);
        void AddSettlement(Settlement settlement);
        void DeleteSettlement(long id);
        IEnumerable<T> SettlementByUserId(Friend friend);
        bool SettlementExists(long id);
    }
}
