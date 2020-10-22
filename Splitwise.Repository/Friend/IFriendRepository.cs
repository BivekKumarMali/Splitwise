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
        void AddFriend();
        void RemoveFriend();
       IEnumerable<FriendDTO> AllFriends();
    }
}
