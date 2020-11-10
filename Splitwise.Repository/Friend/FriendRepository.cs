using Microsoft.AspNetCore.Identity;
using Splitwise.Data;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public class FriendRepository : IFriendRepository<FriendDTO>
    {

        #region Contructor

        public FriendRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion
        #region Private Varibles

        private readonly AppDbContext _dbContext;

        #endregion
        #region Private Method

        private Friend SetFriend(string userId, string friendId)
        {
            return new Friend { UserId = userId, FriendId = friendId };
        }

        private Friend GetFriend(string userId, string friendId)
        {
            return _dbContext.Friends.Where
                (x =>
                    (x.FriendId == friendId && x.UserId == userId) ||
                    (x.FriendId == userId && x.UserId == friendId)
            ).First();
        }
        #endregion

        #region Public methods
        public void AddFriend(string userId, string friendId)
        {
            Friend friend = SetFriend(userId, friendId);

            _dbContext.Friends.Add(friend);
            _dbContext.SaveChanges();
        }

        public void RemoveFriend(string userId, string friendId)
        {
            Friend friend = GetFriend(userId, friendId);
            _dbContext.Friends.Remove(friend);
            _dbContext.SaveChanges();
        }

        public IEnumerable<FriendDTO> AllFriends(string userId)
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

        public bool friendExist(string userId, string friendId)
        {
            return _dbContext.Friends.FirstOrDefault(
                x =>
                (x.FriendId == userId && x.UserId == friendId) ||
                (x.FriendId == friendId && x.UserId == userId)
                ) == null;
        }

        public IEnumerable<FriendDTO> AllFriendWithBalance(string userId)
        {
            var OweExpenseDetail = from ed in _dbContext.ExpenseDetails.ToList()
                                   where ed.UserId == userId & ed.AmountPaid - ed.AmountOwe < 0
                                   select new
                                   {
                                       expenseId = ed.ExpenseId,
                                       amount = ed.AmountPaid - ed.AmountOwe
                                   };
            var ListOfPeopleToBePaid = from oed in OweExpenseDetail
                                       join ed in _dbContext.ExpenseDetails.ToList()
                                       on oed.expenseId equals ed.ExpenseId
                                       join u in _dbContext.ApplicationUsers.ToList()
                                       on ed.UserId equals u.UserId
                                       where ed.AmountPaid - ed.AmountOwe >0
                                       select new FriendDTO
                                       {
                                           Amount = oed.amount * -1,
                                           Id = ed.UserId,
                                           Name = u.Name
                                       };
            var OwedExpenseDetail = from ed in _dbContext.ExpenseDetails.ToList()
                                   where ed.UserId == userId & ed.AmountPaid - ed.AmountOwe > 0
                                   select new
                                   {
                                       expenseId = ed.ExpenseId,
                                       userid = ed.UserId,
                                       amount = ed.AmountPaid - ed.AmountOwe
                                   };
            var ListOfPeopleToGetPaid = from oed in OwedExpenseDetail
                                       join ed in _dbContext.ExpenseDetails.ToList()
                                       on oed.expenseId equals ed.ExpenseId
                                       join u in _dbContext.ApplicationUsers.ToList()
                                       on ed.UserId equals u.UserId
                                       where ed.UserId != oed.userid
                                       select new FriendDTO
                                       {
                                           Amount = ed.AmountPaid - ed.AmountOwe,
                                           Id = ed.UserId,
                                           Name = u.Name
                                       };
            var list1 = ListOfPeopleToBePaid.ToList();
            var list2 = ListOfPeopleToGetPaid.ToList();
            IList<FriendDTO> List1 = new List<FriendDTO>();
            IList<FriendDTO> List2 = new List<FriendDTO>();

            for (int i = 0; i < list1.Count; i++)
            {
                var element = list1[i];
                for (int j = i; j < list1.Count; j++)
                {
                    var alreadyPresent = List1.Where(x => x.Id == element.Id).ToList();
                    if(alreadyPresent.Count != 0)
                    {
                        element = null;
                        break;
                    }
                    if(list1[j].Id == element.Id && i != j)
                    {
                        element.Amount += list1[j].Amount;
                    }
                    
                }
                if (element != null)
                {
                    List1.Add(element);
                }

            }for (int i = 0; i < list2.Count; i++)
            {
                var element = list2[i];
                for (int j = i; j < list2.Count; j++)
                {
                    var alreadyPresent = List2.Where(x => x.Id == element.Id).ToList();
                    if(alreadyPresent.Count != 0)
                    {
                        element = null;
                        break;
                    }
                    if(list2[j].Id == element.Id && i != j)
                    {
                        element.Amount += list2[j].Amount;
                    }
                    
                }
                if (element != null)
                {
                    List2.Add(element);
                }

            }

            return List1.Concat(List2);
        }
        #endregion
    }
}
