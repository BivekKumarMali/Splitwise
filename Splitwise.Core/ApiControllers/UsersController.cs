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
                _userRepository.UpdateApplicationUser(user);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        // POST: api/Users
        [HttpPost]
        public IActionResult Register(ApplicationUser user)
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
        public IActionResult Login(ApplicationUser user, string password)
        {
            var task = _userRepository.UserValidation(user, password);
            task.Wait();
            if (_userRepository.UserExist(user.UserId) && task.Result)
            {
                return Ok(_userRepository.LoginCredentials(user));
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET: api/Users/ByMail
        [HttpGet("{mail}")]
        [Route("ByMail/{mail}")]
        public IActionResult ByMail(string mail)
        {
            var user = _userRepository.FindByMail(mail);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Users/Friends
        [HttpPost]
        [Route("Friends")]
        public virtual IActionResult AddFriend(Friend friend)
        {
            if (_userRepository.UserExist(friend.UserId))
            {
                _friendRepository.AddFriend(friend.UserId, friend.FriendId);
                return Ok();
            }
            return NotFound();
        }

        // DELETE: api/Users/Friends
        [HttpDelete]
        [Route("Friends")]
        public IActionResult RemoveFriend(Friend friend)
        {
            if (_userRepository.UserExist(friend.UserId))
            {
                _friendRepository.RemoveFriend(friend.UserId, friend.FriendId);
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
                return Ok( _friendRepository.AllFriends(userid));
            }
            return NotFound();

        }
        #endregion
    }
}
