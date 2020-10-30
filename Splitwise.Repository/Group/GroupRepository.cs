using Splitwise.Data;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splitwise.Repository
{
    public class GroupRepository : IGroupRepository<GroupDTO>
    {
        #region Contructors

        public GroupRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion
        #region Private Variable

        private readonly AppDbContext _dbContext;
        #endregion

        #region Private Method

        private DomainModel.Models.Group FindGroup(int groupId)
        {
            return _dbContext.Groups.Find(groupId);
        }
        #endregion
        #region Public method
        public IEnumerable<GroupDTO> AllGroups(string userId)
        {
            var listOfGroups = _dbContext.Groups.ToList().Where(x => x.UserId == userId);
            return listOfGroups.Select(g => new GroupDTO
            {
                Id = g.Id,
                GroupName = g.GroupName
            }).ToList();

        }

        public void AddGroup(DomainModel.Models.Group group)
        {
            _dbContext.Groups.Add(group);
            _dbContext.Members.Add(new DomainModel.Models.Member { GroupId = group.Id, UserId = group.UserId });
            _dbContext.SaveChanges();
        }

        public void UpdateGroup(DomainModel.Models.Group group)
        {
            _dbContext.Groups.Update(group);
            _dbContext.SaveChanges();
        }

        public void DeleteGroup(int groupId)
        {
            var group = FindGroup(groupId);
            _dbContext.Groups.Remove(group);
            _dbContext.SaveChanges();
        }

        public bool GroupExist(int groupId)
        {
            return _dbContext.Groups.Find(groupId) != null ? true : false;
        }

        #endregion
    }
}
