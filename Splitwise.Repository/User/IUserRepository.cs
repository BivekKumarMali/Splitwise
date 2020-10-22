using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public interface IUserRepository
    {
        bool UserExist(long userid);
        void AddUser(User user);
        void UpdateUser(User user);
        object LoginCredentials();
        bool LogIn(User user);
    }
}
