using Microsoft.AspNetCore.Identity;
using Splitwise.Data;
using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public class UserRepository : IUserRepository
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

        public UserRepository()
        {
        }

        #endregion
        #region Private Variable

        private readonly AppDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        #endregion
        #region Private method

        private async Task<bool> AddUser(string email, string password)
        {
            IdentityUser user = new IdentityUser { Email = email, UserName = email };
            var addUserManagerStatus = await _userManager.CreateAsync(user, password);
            return addUserManagerStatus.Succeeded;
        }

        #endregion

        #region Public method
        public bool AddApplicationUser(ApplicationUser user)
        {
            return true;
            //var Task = AddUser(user.Email, user.Email);
            //Task.Wait();
            //return Task.Result;
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
