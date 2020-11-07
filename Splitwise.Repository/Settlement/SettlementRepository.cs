using Splitwise.Data;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
        private IEnumerable<FriendDTO> ListOfFriendByUserID(string userId)
        {
            List<string> listOfFriendUserId = new List<string>();

            var listOfFriends = _dbContext.Friends.Where
                (x =>
                    x.FriendId == userId || x.UserId == userId
            ).ToList();

            // Filter Outing my userId from lisOfFriends to get the list of my frineds userId
            foreach (var friend in listOfFriends)
            {
                if (friend.UserId == userId) listOfFriendUserId.Add(friend.FriendId);
                else listOfFriendUserId.Add(friend.UserId);
            }

            return from u in _dbContext.ApplicationUsers.ToList()
                   join f in listOfFriendUserId
                   on u.UserId equals f
                   select new FriendDTO
                   {
                       Id = u.UserId,
                       Name = u.Name
                   };
        }

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
            _dbContext.Settlements.Remove(settlement);
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
                       PayeeName = friend.UserId == s.PayUserId ? "You" : friendDetails.Name,
                       ReceiverName = friend.UserId == s.PayeeUserId ? "You" : friendDetails.Name
                   };
        }

        public bool SettlementExists(long id)
        {
            return _dbContext.Settlements.Find(id) != null ? true : false;
        }

        public IEnumerable<SettlementDTO> AllTransaction(string userId)
        {

            return from s in _dbContext.Settlements.ToList()
                   join pay in _dbContext.ApplicationUsers.ToList()
                   on s.PayUserId equals pay.UserId
                   join payee in _dbContext.ApplicationUsers.ToList()
                   on s.PayeeUserId equals payee.UserId
                   select new SettlementDTO
                   {
                       Amount = s.Amount,
                       Id = s.Id,
                       PayeeName = userId == pay.UserId ? "You" : pay.Name,
                       ReceiverName = userId == payee.UserId ? "You" : payee.Name,
                   };

            throw new NotImplementedException();
        }
        #endregion
    }
}
