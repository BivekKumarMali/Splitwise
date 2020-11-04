using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Splitwise.Repository.DTOs;
using Splitwise.Repository;
using Splitwise.DomainModel.Models;

namespace Splitwise.Core.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettlementsController : ControllerBase
    {
        #region Constructors
        public SettlementsController(
            ISettlementRepository<SettlementDTO> settlementRepository,
            IGroupRepository<GroupDTO> groupRepository,
            IUserRepository userRepository
            )
        {
            _settlementRepository = settlementRepository;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }
        #endregion

        #region Private variables

        private readonly ISettlementRepository<SettlementDTO> _settlementRepository;
        private readonly IGroupRepository<GroupDTO> _groupRepository;
        private readonly IUserRepository _userRepository;
        #endregion

        #region Public methods

        //GET : api/Settlement
        [HttpGet]
        public IActionResult GetByGroupId(int groupid)
        {
            if (_groupRepository.GroupExist(groupid))
            {
                return Ok(_settlementRepository.SettlementByGroupId(groupid));
            }
            return NotFound();

        }

        //POST : api/Settlement
        [HttpPost]
        public IActionResult Add(Settlement settlement)
        {
            if (_groupRepository.GroupExist(settlement.GroupId))
            {
                _settlementRepository.AddSettlement(settlement);
                return Ok();
            }
            return BadRequest();

        }

        //DELETE : api/Settlement
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            if (_settlementRepository.SettlementExists(id))
            {
                _settlementRepository.DeleteSettlement(id);
                return Ok();
            }
            return BadRequest();

        }

        //GET : api/Setlement/{userid}
        [Route("")]
        [HttpGet]
        public IActionResult GetByUserID(string userId)
        {
            if (_userRepository.UserExist(userId))
            {
                return Ok(_settlementRepository.AllTransaction(userId));
            }
            return NotFound();

        }


        #endregion
    }
}
