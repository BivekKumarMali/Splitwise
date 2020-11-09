using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository
{
    public interface IExpenseRepository<T> where T: ExpenseDTO
    {
        IEnumerable<T> ExpenseByGroupId(int groupId);
        long AddExpense(Expense expense);
        void EditExpense(Expense expense);
        void Delete(long expenseId);
        IEnumerable<T> ExpenseByUserID(string userid);
        bool ExpenseExist(long expenseId);
        IEnumerable<ExpenseDTO> ExpenseByFriend(Friend friend);
    }
}
