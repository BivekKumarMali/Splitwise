using Splitwise.Data;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Splitwise.Repository
{
    public class ExpenseDetailRepository : IExpenseDetailRepository<ExpenseDetailDTO>
    {
        #region Contructor

        public ExpenseDetailRepository(AppDbContext dbContext)
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
        public void AddExpenseDetail(ExpenseDetail[] expenseDetails)
        {
            _dbContext.AddRange(expenseDetails);
            _dbContext.SaveChanges();
        }

        public void EditExpenseDetail(ExpenseDetail[] expenseDetails)
        {
            var listofExpenseDetails = _dbContext.ExpenseDetails.Where(x => x.ExpenseId == expenseDetails[0].ExpenseId).ToList();

            _dbContext.RemoveRange(listofExpenseDetails);

            AddExpenseDetail(expenseDetails);
        }
        // left
        public IEnumerable<ExpenseDetailDTO> ExpenseDetailByExpenseID(long expenseid)
        {
            var expense = _dbContext.Expenses.Find(expenseid);

            var listOfMembers = from u in _dbContext.ApplicationUsers
                                join m in _dbContext.Members
                                on u.UserId equals m.UserId
                                where m.GroupId == expense.GroupId
                                select new UserDTO
                                {
                                    Id = u.UserId,
                                    Name = u.Name
                                };

            var listOfExpenseDetails = _dbContext.ExpenseDetails.Where(x => x.ExpenseId == expenseid).ToList();


            return from ed in listOfExpenseDetails
                   join lm in listOfMembers
                   on ed.UserId equals lm.Id
                   select new ExpenseDetailDTO
                   {
                       Id = ed.ExpenseId,
                       AmountOwed = ed.AmountOwe,
                       AmountPaid = ed.AmountPaid,
                       UserName = lm.Name
                   };
        }

        public ExpenseDetail[] JsonToExpenseDetails(string jsonExpenseDetail)
        {
            return JsonSerializer.Deserialize<ExpenseDetail[]>(jsonExpenseDetail); ;
        }
        #endregion
    }
}
