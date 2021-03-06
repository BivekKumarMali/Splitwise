﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        //GET : api/Expenses
        [HttpGet]
        public ActionResult<IEnumerable<ExpenseDTO>> GetExpenseByGroupID(int groupId)
        {
            if (_groupRepository.GroupExist(groupId))
            {
                return Ok(_expenseRepository.ExpenseByGroupId(groupId));
            }
            return NotFound();

        }
        [Route("'ById")]
        [HttpGet]
        public ActionResult<ExpenseDTO> GetExpenseByID(long expenseid)
        {
            if (_expenseRepository.ExpenseExist(expenseid))
            {
                return Ok(_expenseRepository.ExpenseById(expenseid));
            }
            return NotFound();

        }


        // GET : api/Expenses/userid
        [Route("ByUserID")]
        [HttpGet]
        public ActionResult<IEnumerable<ExpenseDTO>> GetExpenseByUserID(string userid)
        {
            if (_userRepository.UserExist(userid))
            {
                return Ok(_expenseRepository.ExpenseByUserID(userid));
            }
            return NotFound();

        }


        // GET : api/Expenses/userid
        [Route("ByFriend")]
        [HttpGet]
        public ActionResult<IEnumerable<ExpenseDTO>> GetExpenseByFriend(string ufid)
        {
            var ids = ufid.Split(' ');
            Friend friend = new Friend { Id = 0, UserId = ids[0], FriendId = ids[1] };
            if (_userRepository.UserExist(friend.UserId))
            {
                return Ok(_expenseRepository.ExpenseByFriend(friend));
            }
            return NotFound();

        }


        //POST : api/Expenses
        [HttpPost]
        public ActionResult<long> AddExpense(Expense expense)
        {
            if (_groupRepository.GroupExist(expense.GroupId))
            {
                return _expenseRepository.AddExpense(expense);
            }
            return NotFound();

        }
        //PUT : api/Expenses
        [HttpPut]
        public IActionResult EditExpense(Expense expense)
        {
            if (_expenseRepository.ExpenseExist(expense.Id))
            {
                _expenseRepository.EditExpense(expense);
                return Ok();
            }
            return NotFound();

        }

        //DELETE : api/Expenses
        [HttpDelete]
        public IActionResult DeleteExpense(long expenseId)
        {
            if (_expenseRepository.ExpenseExist(expenseId))
            {
                _expenseRepository.Delete(expenseId);
                return Ok();
            }
            return NotFound();

        }


        // GET : api/Expenses/ExpenseDetails/1
        [Route("ExpenseDetails")]
        [HttpGet]
        public ActionResult<IEnumerable<ExpenseDetailDTO>> GetExpenseDetail(long expenseid)
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
        public IActionResult AddExpenseDetails(ExpenseDetail[] expenseDetails)
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
        public IActionResult EditExpenseDetails(string jsonExpenseDetail)
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
