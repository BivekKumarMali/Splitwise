﻿using Microsoft.EntityFrameworkCore.Internal;
using Splitwise.Data;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        public long AddExpense(Expense expense)
        {
            _dbContext.Expenses.Add(expense);
            _dbContext.SaveChanges();
            return expense.Id;
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
                       TimeStamp = e.TimeStamp.ToString(),
                   };

        }

        public IEnumerable<ExpenseDTO> ExpenseByUserID(string userid)
        {
            return from e in _dbContext.Expenses.ToList()
                   join ed in _dbContext.ExpenseDetails.ToList()
                   on e.Id equals ed.ExpenseId
                   where ed.UserId == userid
                   select new ExpenseDTO
                   {
                       Id = e.Id,
                       Amount = ed.AmountPaid - ed.AmountOwe,
                       ExpenseName = e.ExpenseName,
                       TimeStamp = e.TimeStamp.ToString(),
                       UserName = "You"
                   };
        }

        public bool ExpenseExist(long expenseId)
        {
            return _dbContext.Expenses.Find(expenseId) != null ? true : false;
        }

        public IEnumerable<ExpenseDTO> ExpenseByFriend(Friend friend)
        {
            return from userEd in _dbContext.ExpenseDetails.ToList()
                   join friendEd in _dbContext.ExpenseDetails.ToList()
                   on userEd.ExpenseId equals friendEd.ExpenseId
                   join u in _dbContext.ApplicationUsers.ToList()
                   on userEd.UserId equals u.UserId
                   join e in _dbContext.Expenses.ToList()
                   on userEd.ExpenseId equals e.Id
                   where userEd.UserId == friend.UserId & friendEd.UserId == friend.FriendId
                   select new ExpenseDTO
                   {
                       Id = e.Id,
                       Amount = friendEd.AmountPaid - friendEd.AmountOwe,
                       ExpenseName = e.ExpenseName,
                       TimeStamp = e.TimeStamp.ToString(),
                       UserName = u.Name
                   };
        }

        public ExpenseDTO ExpenseById(long expenseid)
        {
            var expense = _dbContext.Expenses.Find(expenseid);
            var user = _dbContext.ApplicationUsers.Find(expense.UserId);
            var expenseDetails = _dbContext.ExpenseDetails.Where(x => x.ExpenseId == expenseid).ToList();
            var amount = expenseDetails.Sum(x => x.AmountPaid);
            return new ExpenseDTO
            {
                Amount = amount,
                ExpenseName = expense.ExpenseName,
                Id = expense.Id,
                GroupId = expense.GroupId,
                UserName = user.Name,
                TimeStamp = expense.TimeStamp.ToString()
            };
        }
        #endregion
    }
}
