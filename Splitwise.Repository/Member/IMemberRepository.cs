using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository
{
    public interface IMemberRepository<T> where T: MemberDTO
    {
        void DeleteMember();
        void AddMember();
        IEnumerable<T> AllMember();
    }
}
