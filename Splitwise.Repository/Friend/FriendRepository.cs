using Splitwise.Data;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
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
        #endregion
    }
}
