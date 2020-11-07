﻿using Splitwise.Data;
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
            var name = _dbContext.ApplicationUsers.Find(userId).Name;
            return listOfGroups.Select(g => new GroupDTO
            {
                Id = g.Id,
                GroupName = g.GroupName,
                UserName = name
            }).ToList();

        }

        public int AddGroup(DomainModel.Models.Group group)
        {
            _dbContext.Groups.Add(group);
            _dbContext.SaveChanges();
            return group.Id;
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

        public GroupDTO GroupById(int groupid)
        {
            var group = _dbContext.Groups.Find(groupid);
            var user = _dbContext.ApplicationUsers.Find(group.UserId);
            return new GroupDTO
            {
                GroupName = group.GroupName,
                Id = group.Id,
                UserName = user.Name
            };
        }

        #endregion
    }
}
