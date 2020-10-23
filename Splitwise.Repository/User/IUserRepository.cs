using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public interface IUserRepository
    {
        bool UserExist(string userid);
        bool AddApplicationUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
        object LoginCredentials();
        bool LogIn(ApplicationUser user);
    }
}
