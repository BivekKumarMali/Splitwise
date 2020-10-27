using Splitwise.Data;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splitwise.Repository
{
    public class ExpenseRepository : IExpenseRepository<ExpenseDTO>
    {
        #region Contructor

        public ExpenseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion
        #region Private Varibles

        private readonly AppDbContext _dbContext;

        #endregion
        #region Private Method

        
        #endregion

        #region Public methods
        public void AddExpense(Expense expense)
        {
            _dbContext.Expenses.Add(expense);
            _dbContext.SaveChanges();
        }

        public void Delete(long expenseId)
        {
            var expense = _dbContext.Expenses.Find(expenseId);

            _dbContext.Expenses.Remove(expense);
            _dbContext.SaveChanges();
        }

        public void EditExpense(Expense expense)
        {
            _dbContext.Expenses.Update(expense);
            _dbContext.SaveChanges();
        }

        public IEnumerable<ExpenseDTO> ExpenseByGroupId(Member member)
        {
            var listOfExpense = _dbContext.Expenses.ToList().Where(x => x.GroupId == member.GroupId).OrderBy(x => x.TimeStamp);
            var listOfUser = _dbContext.ApplicationUsers.ToList();
            return (from ed in _dbContext.ExpenseDetails
                                  join e in listOfExpense
                                  on ed.ExpenseId equals e.Id
                                  where ed.UserId == member.UserId
                                  select new ExpenseDTO
                                  {
                                      Id = e.Id,
                                      AmountOwed = ed.AmountOwe - ed.AmountPaid,
                                      ExpenseName = e.ExpenseName,
                                      TimeStamp = e.TimeStamp.ToString()
                                  }
                                  );


        }

        public IEnumerable<ExpenseDTO> ExpenseByUserID(string userid)
        {
            throw new NotImplementedException();
        }

        public bool ExpenseExist(long expenseId)
        {
            return _dbContext.Expenses.Find(expenseId) != null ? true : false;
        }
        #endregion
    }
}
