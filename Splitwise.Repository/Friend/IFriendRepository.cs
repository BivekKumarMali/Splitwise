using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public interface IFriendRepository<T>
    {
        void AddFriend(string userId, string friendId);
        void RemoveFriend(string userId, string friendId);
       IEnumerable<FriendDTO> AllFriends(string userId);
       IEnumerable<FriendDTO> AllFriendWithBalance(string userId);
        bool friendExist(string userId, string friendId);
    }
}
