using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Splitwise.DomainModel.Models;

namespace Splitwise.Core.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Constructors
        public UsersController()
        {

        }
        #endregion

        #region Private variables
        #endregion

        #region Public methods

        [HttpGet]
        [Route("Login")]
        public virtual IActionResult Login(User user)
        {
            //TODO: Uncomment the next line to return response 0 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(0);


            throw new NotImplementedException();
        }

        [HttpPut]
        public virtual IActionResult Edit(User user)
        {
            //TODO: Uncomment the next line to return response 0 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(0);


            throw new NotImplementedException();
        }

        [HttpPost]
        public virtual IActionResult Register(User user)
        {
            //TODO: Uncomment the next line to return response 0 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(0);


            throw new NotImplementedException();
        }



        [HttpPost]
        [Route("Friends")]
        public virtual IActionResult AddFriend(Friend friends)
        {
            //TODO: Uncomment the next line to return response 0 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(0);

            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("Friends")]
        public virtual IActionResult RemoveFriend(Friend friends)
        {
            //TODO: Uncomment the next line to return response 0 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(0);


            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Friends")]
        public virtual IActionResult GetFriends(string userid)
        {
            //TODO: Uncomment the next line to return response 0 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(0);


            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("RecentActivity/{userid}")]
        public virtual IActionResult GetRecentActivity(string userid)
        {
            //TODO: Uncomment the next line to return response 0 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(0);


            throw new NotImplementedException();
        }
        #endregion
    }
}
