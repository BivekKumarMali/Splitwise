using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Splitwise.DomainModel.Models;
using Splitwise.Repository;
using Splitwise.Repository.DTOs;

namespace Splitwise.Web.Splitwise.Core.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        #region Constructors
        public ExpensesController(
            IExpenseRepository<ExpenseDTO> expenseRepository,
            IUserRepository userRepository,
            IGroupRepository<GroupDTO> groupRepository
            )
        {
            _expenseRepository = expenseRepository;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }
        #endregion

        #region Private variables

        private readonly IExpenseRepository<ExpenseDTO> _expenseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository<GroupDTO> _groupRepository;

        #endregion

        #region Public methods

        //GET : api/Expenses
        [HttpGet]
        public IActionResult GetByGroupID(int groupid)
        {
            if (_groupRepository.GroupExist(groupid))
            {
                return Ok(_expenseRepository.ExpenseByGroupId(groupid));
            }
            return NotFound();

        }
        //POST : api/Expenses
        [HttpPost]
        public IActionResult Add(Expense expense)
        {
            if (_groupRepository.GroupExist(expense.GroupId))
            {
                _expenseRepository.AddExpense(expense);
                return Ok();
            }
            return NotFound();

        }
        //PUT : api/Expenses
        [HttpPut]
        public IActionResult Edit(Expense expense)
        {
            if (_expenseRepository.ExpenseExist(expense.Id))
            {
                _expenseRepository.EditExpense(expense);
                return Ok();
            }
            return NotFound();

        }
        //DELETE : api/Expenses
        [HttpGet]
        public IActionResult Delete(long expenseId)
        {
            if (_expenseRepository.ExpenseExist(expenseId))
            {
                _expenseRepository.Delete(expenseId);
                return Ok();
            }
            return NotFound();

        }

        //GET : api/Expenses/userid
        [Route("{userid}")]
        [HttpGet("{userid}")]
        public IActionResult GetByUserID(long userid)
        {
            if (_userRepository.UserExist(userid))
            {
                return Ok(_expenseRepository.ExpenseByUserID(userid));
            }
            return NotFound();

        }
        #endregion
    }
}
