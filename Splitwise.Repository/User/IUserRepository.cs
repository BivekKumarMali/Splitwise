using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public interface IUserRepository
    {
        bool UserExist(string userid);
        void AddApplicationUser(ApplicationUser user);
        void UpdateApplicationUser(ApplicationUser user);
        object LoginCredentials(string email);
        Task<bool> UserValidation(string email, string password);
        IEnumerable<UserDTO> FindByMail(string mail);
        bool UserExistByMail(string email);
    }
}
