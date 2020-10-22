using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository.Group
{
    class GroupRepository : IGroupRepository<GroupDTO>
    {
        public IEnumerable<GroupDTO> AllGroups(long userId)
        {
            throw new NotImplementedException();
        }

        public void AddGroup(DomainModel.Models.Group group)
        {
            throw new NotImplementedException();
        }

        public void UpdateGroup(DomainModel.Models.Group group)
        {
            throw new NotImplementedException();
        }

        public void DeleteGroup(int groupId)
        {
            throw new NotImplementedException();
        }

        public bool GroupExist(int groupId)
        {
            throw new NotImplementedException();
        }
    }
}
