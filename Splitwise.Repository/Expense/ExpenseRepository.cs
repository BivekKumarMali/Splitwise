using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository
{
    public class ExpenseRepository : IExpenseRepository<ExpenseDTO>
    {
        public void AddExpense(Expense expense)
        {
            throw new NotImplementedException();
        }

        public void Delete(long expenseId)
        {
            throw new NotImplementedException();
        }

        public void EditExpense(Expense expense)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExpenseDTO> ExpenseByGroupId(int groupid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExpenseDTO> ExpenseByUserID(string userid)
        {
            throw new NotImplementedException();
        }

        public bool ExpenseExist(long expenseId)
        {
            throw new NotImplementedException();
        }
    }
}
