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
        #region Private Method


        private void DeleteMemberDetail(Member member)
        {
            var listExpenseToDelete = from ed in _dbContext.ExpenseDetails.ToList()
                                      join e in _dbContext.Expenses.ToList()
                                      on ed.ExpenseId equals e.Id
                                      where e.GroupId == member.GroupId && ed.UserId == member.UserId
                                      select e;
            _dbContext.Expenses.RemoveRange(listExpenseToDelete);

            var listOfSettlementToDelete = _dbContext.Settlements.Where(
                                            x => x.GroupId == member.GroupId &&
                                            (x.PayeeUserId == member.UserId || x.PayUserId == member.UserId))
                                            .ToList();
            _dbContext.Settlements.RemoveRange(listOfSettlementToDelete);
        }


        #endregion

        #region Public Methods

        public void DeleteMember(int memberId)
        {
            var member = _dbContext.Members.Find(memberId);

            DeleteMemberDetail(member);

            _dbContext.Members.Remove(member);
            _dbContext.SaveChanges();
        }

        public void AddMember(Member member)
        {
            if (!memberExist(member))
            {
                _dbContext.Members.Add(member);
                _dbContext.SaveChanges();
            }
        }


        public IEnumerable<MemberDTO> AllMemberWithBalance(int groupId)
        {
            var listOfSettlement = _dbContext.Settlements.Where(x => x.GroupId == groupId).ToList();

            var listOfGroupMember = from m in _dbContext.Members
                                    join u in _dbContext.ApplicationUsers
                                    on m.UserId equals u.UserId
                                    where m.GroupId == groupId
                                    select new UserDTO
                                    {
                                        Id = u.UserId,
                                        Name = u.Name
                                    };
            var list = _dbContext.Members.Where(x => x.GroupId == groupId).ToList();
            var IQuerybalanceOfEachUser = from e in _dbContext.Expenses
                                  join ed in _dbContext.ExpenseDetails
                                  on e.Id equals ed.ExpenseId
                                  where e.GroupId == groupId
                                  select new MemberDTO
                                  {
                                      Id = ed.UserId,
                                      Amount = ed.AmountPaid - ed.AmountOwe
                                  };
            var balanceOfEachUser = IQuerybalanceOfEachUser.ToList();
            List<MemberDTO> memberDTOs = new List<MemberDTO>();
            foreach (var member in listOfGroupMember)
            {
                List<decimal> listOfUserBalance = (from num in balanceOfEachUser where num.Id == member.Id
                                   select num.Amount).ToList();

                var userBalance = listOfUserBalance.Sum();

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
        
        public IEnumerable<MemberDTO> AllMemberWithID(int groupId)
        {
            var listOfSettlement = _dbContext.Settlements.Where(x => x.GroupId == groupId).ToList();

            return from u in _dbContext.ApplicationUsers
                    join m in _dbContext.Members
                    on u.UserId equals m.UserId
                    where m.GroupId == groupId
                    select new MemberDTO
                    {
                        MemberId = m.Id,
                        Name = u.Name,
                        Id = u.UserId,
                        Amount = 0
                    };
        }

        public bool memberExist(Member member)
        {
            return _dbContext.Members.FirstOrDefault(x => x.GroupId == member.GroupId && x.UserId == member.UserId) != null ? true : false;
        }

        public void AddMemberInBulk(Member[] member)
        {
            if (member.Count() != 0)
            {
                _dbContext.Members.AddRange(member);
                _dbContext.SaveChanges();
            }
        }
        #endregion
    }
}
