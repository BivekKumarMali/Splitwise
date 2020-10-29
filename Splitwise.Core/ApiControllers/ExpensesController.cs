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
            IExpenseDetailRepository<ExpenseDetailDTO> expenseDetailRepository,
            IUserRepository userRepository,
            IGroupRepository<GroupDTO> groupRepository
            )
        {
            _expenseRepository = expenseRepository;
            _expenseDetailRepository = expenseDetailRepository;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }
        #endregion

        #region Private variables

        private readonly IExpenseRepository<ExpenseDTO> _expenseRepository;
        private readonly IExpenseDetailRepository<ExpenseDetailDTO> _expenseDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository<GroupDTO> _groupRepository;

        #endregion

        #region Public methods

        //GET : api/Expenses/1
        [Route("ByGroupID/{groupId}")]
        [HttpGet("{groupId}")]
        public IActionResult GetByGroupID(int groupId)
        {
            if (_groupRepository.GroupExist(groupId))
            {
                return Ok(_expenseRepository.ExpenseByGroupId(groupId));
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

        /*GET : api/Expenses/userid
        [Route("ByUserID/{userid}")]
        [HttpGet("{userid}")]
        public IActionResult GetByUserID(string userid)
        {
            if (_userRepository.UserExist(userid))
            {
                return Ok(_expenseRepository.ExpenseByUserID(userid));
            }
            return NotFound();

        }*/

        // GET : api/Expenses/ExpenseDetails/1
        [Route("ExpenseDetails/{expenseid}")]
        [HttpGet("{expenseid}")]
        public IActionResult GetExpenseDetail(long expenseid)
        {
            if (_expenseRepository.ExpenseExist(expenseid))
            {
                return Ok(_expenseDetailRepository.ExpenseDetailByExpenseID(expenseid));
            }
            return NotFound();

        }

        //POST : api/Expenses/ExpenseDetails
        [Route("ExpenseDetails")]
        [HttpPost]
        public IActionResult AddDetails(ExpenseDetail[] expenseDetails)
        {
            //ExpenseDetail[] expenseDetails = _expenseDetailRepository.JsonToExpenseDetails(jsonExpenseDetail);
            if (_expenseRepository.ExpenseExist(expenseDetails[0].ExpenseId))
            {
                _expenseDetailRepository.AddExpenseDetail(expenseDetails);
                return Ok();
            }
            return NotFound();

        }
        //PUT : api/Expenses/ExpenseDetails
        [Route("ExpenseDetails")]
        [HttpPut]
        public IActionResult EditDetails(string jsonExpenseDetail)
        {
            ExpenseDetail[] expenseDetails = _expenseDetailRepository.JsonToExpenseDetails(jsonExpenseDetail);
            if (_expenseRepository.ExpenseExist(expenseDetails[0].ExpenseId))
            {
                _expenseDetailRepository.EditExpenseDetail(expenseDetails);
                return Ok();
            }
            return NotFound();

        }



        #endregion
    }
}
