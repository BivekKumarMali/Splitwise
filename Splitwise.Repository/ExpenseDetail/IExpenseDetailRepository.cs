using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository
{
    public interface IExpenseDetailRepository<T> where T: ExpenseDetailDTO
    {
        IEnumerable<T> ExpenseDetailByExpenseID(long expenseid);
        ExpenseDetail[] JsonToExpenseDetails(string jsonExpenseDetail);
        void AddExpenseDetail(ExpenseDetail[] expenseDetails);
        void EditExpenseDetail(ExpenseDetail[] expenseDetails);
    }
}
