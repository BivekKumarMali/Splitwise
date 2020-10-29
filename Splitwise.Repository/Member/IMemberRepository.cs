using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository
{
    public interface IMemberRepository<T> where T: MemberDTO
    {
        void DeleteMember(Member member);
        void AddMember(Member member);
        IEnumerable<T> AllMember(int groupId);
        IEnumerable<T> AllMemberWithBalance(int groupId);
    }
}
