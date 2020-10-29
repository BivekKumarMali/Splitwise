using Microsoft.EntityFrameworkCore.Internal;
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

        public IEnumerable<ExpenseDTO> ExpenseByGroupId(int groupId)
        {
            var listOfExpense = _dbContext.Expenses.Where(x => x.GroupId == groupId).OrderBy(x => x.TimeStamp).ToList();
            var listOfUser = _dbContext.ApplicationUsers.ToList();
            return from e in listOfExpense
                   join u in listOfUser
                   on e.UserId equals u.UserId
                   select new ExpenseDTO
                   {
                       Id = e.Id,
                       ExpenseName = e.ExpenseName,
                       UserName = u.Name,
                       TimeStamp = e.TimeStamp.ToString()
                   };

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
