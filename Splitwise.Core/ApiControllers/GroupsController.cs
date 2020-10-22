using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Splitwise.DomainModel.Models;
using Splitwise.Repository;
using Splitwise.Repository.DTOs;

namespace Splitwise.Core.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {

        #region Constructors
        public GroupsController(
            IGroupRepository<GroupDTO> groupRepository,
            IUserRepository userRepository
            )
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }
        #endregion

        #region Private variables

        private readonly IGroupRepository<GroupDTO> _groupRepository;
        private readonly IUserRepository _userRepository;
        #endregion

        #region Public methods

        // GET: api/Groups
        [HttpPost]
        public virtual IActionResult Groups(long userId)
        {
            if (_userRepository.UserExist(userId))
            {
                return Ok(_groupRepository.AllGroups(userId));
            }
            else
            {
                return BadRequest();
            }

        }

        // POST: api/Groups
        [HttpPost]
        public virtual IActionResult Add(Group group)
        {
            if (group != null)
            {
                _groupRepository.AddGroup(group);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        // PUT: api/Groups
        [HttpPut]
        public virtual IActionResult Edit(Group group)
        {
            if (group != null)
            {
                _groupRepository.UpdateGroup(group);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        // DELETE: api/Groups
        [HttpDelete]
        public virtual IActionResult Delete(int groupId)
        {
            if (_groupRepository.GroupExist(groupId))
            {
                _groupRepository.DeleteGroup(groupId);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        #endregion
    }
}
