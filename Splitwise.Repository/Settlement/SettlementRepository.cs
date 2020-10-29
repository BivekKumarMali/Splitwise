using Splitwise.Data;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splitwise.Repository
{
    public class SettlementRepository : ISettlementRepository<SettlementDTO>
    {

        #region Contructors

        public SettlementRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion
        #region Private Variable

        private readonly AppDbContext _dbContext;
        #endregion

        #region Private Method

        #endregion
        #region Public method
        public void AddSettlement(Settlement settlement)
        {
            _dbContext.Settlements.Add(settlement);
            _dbContext.SaveChanges();
        }

        public void DeleteSettlement(long id)
        {
            Settlement settlement = _dbContext.Settlements.Find(id);
            _dbContext.Settlements.Add(settlement);
            _dbContext.SaveChanges();
        }

        public IEnumerable<SettlementDTO> SettlementByGroupId(int groupid)
        {
            var listOfSettlement = _dbContext.Settlements.Where(x => x.GroupId == groupid).ToList();
            var listOfGroupmember = from u in _dbContext.ApplicationUsers
                                    join m in _dbContext.Members
                                    on u.UserId equals m.UserId
                                    where m.GroupId == groupid
                                    select new UserDTO
                                    {
                                        Id = u.UserId,
                                        Name = u.Name
                                    };

            return from s in listOfSettlement
                   select new SettlementDTO
                   {
                       Amount = s.Amount,
                       PayeeName = listOfGroupmember.Where(x => x.Id == s.PayeeUserId).Single().Name,
                       ReceiverName = listOfGroupmember.Where(x => x.Id == s.PayUserId).Single().Name,
                       Id = s.Id
                   };
        }

        public IEnumerable<SettlementDTO> SettlementByUserId(Friend friend)
        {
            var listofSettlement = _dbContext.Settlements.Where
                (x =>
                    (x.PayeeUserId == friend.UserId && x.PayUserId == friend.FriendId) ||
                    (x.PayeeUserId == friend.FriendId && x.PayUserId == friend.UserId)
                ).ToList();
            ApplicationUser friendDetails = _dbContext.ApplicationUsers.Find(friend.FriendId);
            return from s in listofSettlement
                   select new SettlementDTO
                   {
                       Id = s.Id,
                       Amount = s.Amount,
                       PayeeName = friend.UserId == s.PayeeUserId ? "You" : friendDetails.Name,
                       ReceiverName = friend.UserId == s.PayeeUserId ? "You" : friendDetails.Name
                   };
        }

        public bool SettlementExists(long id)
        {
            return _dbContext.Settlements.Find(id) != null ? true : false;
        }
        #endregion
    }
}
