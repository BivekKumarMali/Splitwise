using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        public virtual ActionResult<IEnumerable<GroupDTO>> GetGroups(string userId)
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
        [Route("ByID")]
        [HttpGet]
        public virtual ActionResult<GroupDTO> GetGroupById(int groupid)
        {
            if (_groupRepository.GroupExist(groupid))
            {
                return _groupRepository.GroupById(groupid);
            }
            else
            {
                return BadRequest();
            }

        }

        // POST: api/Groups
        [HttpPost]
        public ActionResult<int> AddGroup(Group group)
        {
            if (group != null)
            {
                return Ok(_groupRepository.AddGroup(group));
            }
            else
            {
                return BadRequest();
            }

        }

        // PUT: api/Groups
        [HttpPut]
        public virtual IActionResult EditGroup(Group group)
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
        public virtual IActionResult DeleteGroup(int groupId)
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
