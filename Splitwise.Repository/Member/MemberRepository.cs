using Splitwise.Data;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splitwise.Repository
{
    public class MemberRepository : IMemberRepository<MemberDTO>
    {
        #region Contructor

        public MemberRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion
        #region Private Varibles

        private readonly AppDbContext _dbContext;
        #endregion
        #region Public Methods

        public void DeleteMember(Member member)
        {
            _dbContext.Members.Remove(member);
            _dbContext.SaveChanges();
        }

        public void AddMember(Member member)
        {
            _dbContext.Members.Add(member);
            _dbContext.SaveChanges();
        }

        public IEnumerable<MemberDTO> AllMemberWithBalance(int groupId)
        {
            var listOfSettlement = _dbContext.Settlements.Where(x => x.GroupId == groupId).ToList();

            var listOfGroupMember = from u in _dbContext.ApplicationUsers
                                    join m in _dbContext.Members
                                    on u.UserId equals m.UserId
                                    where m.GroupId == groupId
                                    select new UserDTO
                                    {
                                        Id = u.UserId,
                                        Name = u.Name
                                    };

            var balanceOfEachUser = from e in _dbContext.Expenses
                                  join ed in _dbContext.ExpenseDetails
                                  on e.Id equals ed.ExpenseId
                                  where e.GroupId == groupId
                                  select new
                                  {
                                      userId = ed.UserId,
                                      balance = ed.AmountPaid - ed.AmountOwe
                                  };
            List<MemberDTO> memberDTOs = new List<MemberDTO>();
            foreach (var member in listOfGroupMember)
            {
                var userBalance = (from num in balanceOfEachUser
                                   select num.balance).Sum();

                var userReceivedSettlement = (from rs in listOfSettlement
                                              where member.Id == rs.PayeeUserId
                                              select rs.Amount).Sum();

                var userPaidSettlement = (from ps in listOfSettlement
                                              where member.Id == ps.PayUserId
                                              select ps.Amount).Sum();

                userBalance = userBalance + userPaidSettlement - userReceivedSettlement;

                memberDTOs.Add(new MemberDTO { 
                    Id=member.Id,
                    Name = member.Name,
                    Amount = userBalance
                });
            }


            return memberDTOs;
        }
        
        public IEnumerable<MemberDTO> AllMember(int groupId)
        {
            var listOfSettlement = _dbContext.Settlements.Where(x => x.GroupId == groupId).ToList();

            return from u in _dbContext.ApplicationUsers
                    join m in _dbContext.Members
                    on u.UserId equals m.UserId
                    where m.GroupId == groupId
                    select new MemberDTO
                    {
                        Id = u.UserId,
                        Name = u.Name,
                        Amount = 0
                    };
        }
        #endregion
    }
}
