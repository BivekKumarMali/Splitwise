using Microsoft.AspNetCore.Identity;
using Splitwise.Data;
using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public class UserRepository: IUserRepository
    {
        #region Contructor
        public UserRepository(
            AppDbContext dbContext,
            UserManager<IdentityUser> userManager
            )
        {
            _dbContext = dbContext;
           _userManager = userManager;
        }


        #endregion
        #region Private Variable

        private readonly AppDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        #endregion
        #region Private method

        private async Task<string> AddUser(string email, string password)
        {
            IdentityUser user = new IdentityUser { Email = email, UserName = email };
            var addUserManagerStatus = await _userManager.CreateAsync(user, password);
            user = await _userManager.FindByEmailAsync(email);
            return user.Id;
        }

        #endregion

        #region Public method
        public bool AddApplicationUser(ApplicationUser user)
        {
            var Task = AddUser(user.Email, user.Email);
            Task.Wait();
            user.UserId = Task.Result;
            _dbContext.ApplicationUsers.Add(user);
            return true;
        }

        public bool LogIn(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public object LoginCredentials()
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public bool UserExist(string userid)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
