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
        public ActionResult<IEnumerable<MemberDTO>> GetMembers(int groupid)
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
        public ActionResult<IEnumerable<MemberDTO>> GetMemberWithBalance(int groupid)
        {
            if (_groupRepository.GroupExist(groupid))
            {
                return Ok(_memberRepository.AllMemberWithBalance(groupid));
            }
            return NotFound();

        }


        //POST : api/Members
        [HttpPost]
        public IActionResult AddMember(Member member)
        {
            if (member != null)
            {
                if (!_memberRepository.memberExist(member))
                {
                    _memberRepository.AddMember(member);
                    return Ok();
                }
                return BadRequest(new { message = "MemberLaredy exists" });
            }
            return NotFound();

        }
        //POST : api/Members/bulk
        [Route("Bulk")]
        [HttpPost]
        public IActionResult AddMemberInBulk(Member[] member)
        {
            if (member.Count() != 0)
            {
                    _memberRepository.AddMemberInBulk(member);
                    return Ok();
            }
            return BadRequest();

        }

        //DELTE : api/Members
        [HttpDelete]
        public IActionResult DeleteMember(int memberId)
        {
            if (memberId > 0)
            {
                _memberRepository.DeleteMember(memberId);
                return Ok();
            }
            return NotFound();

        }
        #endregion
    }
}
