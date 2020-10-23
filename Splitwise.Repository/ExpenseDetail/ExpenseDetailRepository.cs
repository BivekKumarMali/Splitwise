using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository
{
    public class ExpenseDetailRepository : IExpenseDetailRepository<ExpenseDetailDTO>
    {
        public void AddExpenseDetail(ExpenseDetail[] expenseDetails)
        {
            throw new NotImplementedException();
        }

        public void EditExpenseDetail(ExpenseDetail[] expenseDetails)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExpenseDetailDTO> ExpenseDetailByExpenseID(long expenseid)
        {
            throw new NotImplementedException();
        }

        public ExpenseDetail[] JsonToExpenseDetails(string jsonExpenseDetail)
        {
            throw new NotImplementedException();
        }
    }
}
