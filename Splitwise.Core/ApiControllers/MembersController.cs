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
    public class MembersController : ControllerBase
    {
        #region Constructors
        public MembersController(
            IMemberRepository<MemberDTO> memberRepository,
            IGroupRepository<GroupDTO> groupRepository
            )
        {
            _memberRepository = memberRepository;
            _groupRepository = groupRepository;
        }

        #endregion

        #region Private variables

        private readonly IMemberRepository<MemberDTO> _memberRepository;
        private readonly IGroupRepository<GroupDTO> _groupRepository;
        #endregion

        #region Public methods
        //GET : api/Members
        [HttpGet]
        public IActionResult Get(int groupid)
        {
            if (_groupRepository.GroupExist(groupid))
            {
                return Ok(_memberRepository.AllMember(groupid));
            }
            return NotFound();

        }
        
        //GET : api/Members
        [Route("Balance")]
        [HttpGet]
        public IActionResult GetWithBalance(int groupid)
        {
            if (_groupRepository.GroupExist(groupid))
            {
                return Ok(_memberRepository.AllMemberWithBalance(groupid));
            }
            return NotFound();

        }


        //POST : api/Members
        [HttpPost]
        public IActionResult Add(Member member)
        {
            if (member != null)
            {
                _memberRepository.AddMember(member);
                return Ok();
            }
            return NotFound();

        }

        //DELTE : api/Members
        [HttpDelete]
        public IActionResult Delete(Member member)
        {
            if (member != null)
            {
                _memberRepository.DeleteMember(member);
                return Ok();
            }
            return NotFound();

        }
        #endregion
    }
}
