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
    class FriendRepository : IFriendRepository<FriendDTO>
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
            List<FriendDTO> friendDTOs = new List<FriendDTO>();

            var listOfFriends = _dbContext.Friends.Where
                (x =>
                    x.FriendId == userId || x.UserId == userId
            ).ToList();

            foreach (var friend in listOfFriends)
            {
                if (friend.UserId == userId) listOfFriendUserId.Add(friend.FriendId);
                else listOfFriendUserId.Add(friend.UserId);
            }

            var applicationUsers = _dbContext.ApplicationUsers.ToList();

            return applicationUsers.Join
                (
                    listOfFriendUserId,
                    u => u.UserId,
                    f => f,
                    (u, f) => new FriendDTO
                    {
                        Name = u.Name,
                        Id = f
                    }
                    ).ToList();
        }

        #endregion
    }
}
