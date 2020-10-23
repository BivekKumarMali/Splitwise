using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Splitwise.DomainModel.Models;
using Splitwise.Repository;
using Splitwise.Repository.DTOs;

namespace Splitwise.Core.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Constructors
        public UsersController(
            IUserRepository userRepository, 
            IFriendRepository<FriendDTO> friendRepository
            )
        {
            _userRepository = userRepository;
            _friendRepository = friendRepository;
        }
        #endregion

        #region Private variables

        private readonly IUserRepository _userRepository;
        private readonly IFriendRepository<FriendDTO> _friendRepository;

        #endregion

        #region Public methods

        // PUT: api/Users
        [HttpPut]
        public virtual IActionResult Edit(ApplicationUser user)
        {
            if (user != null)
            {
                _userRepository.UpdateUser(user);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        // POST: api/Users
        [HttpPost]
        public virtual IActionResult Register(ApplicationUser user)
        {
            if (user != null)
            {
                _userRepository.AddApplicationUser(user);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        // GET: api/Users/Login
        [HttpGet]
        [Route("Login")]
        public IActionResult Login(ApplicationUser user)
        {
            if (_userRepository.LogIn(user))
            {
                return Ok(_userRepository.LoginCredentials());
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Users/Friends
        [HttpPost]
        [Route("Friends")]
        public virtual IActionResult AddFriend(string userid)
        {
            if (_userRepository.UserExist(userid))
            {
                _friendRepository.AddFriend();
                return Ok();
            }
            return NotFound();
        }

        // DELETE: api/Users/Friends
        [HttpDelete]
        [Route("Friends")]
        public virtual IActionResult RemoveFriend(string userid)
        {
            if (_userRepository.UserExist(userid))
            {
                _friendRepository.RemoveFriend();
                return Ok();
            }
            return NotFound();
        }

        // GET: api/Users/Friends
        [HttpGet]
        [Route("Friends")]
        public IActionResult GetFriends(string userid)
        {
            if (_userRepository.UserExist(userid))
            {
                return Ok( _friendRepository.AllFriends());
            }
            return NotFound();


            throw new NotImplementedException();
        }
        #endregion
    }
}
