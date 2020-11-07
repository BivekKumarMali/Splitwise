using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository
{
    public interface IGroupRepository<T> where T : GroupDTO
    {
        IEnumerable<T> AllGroups(string userId);
        int AddGroup(DomainModel.Models.Group group);
        void UpdateGroup(DomainModel.Models.Group group);
        void DeleteGroup(int groupId);
        bool GroupExist(int groupId);
        T GroupById(int groupid);
    }
}
