using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public interface IUserRepository
    {
        bool UserExist(string userid);
        void AddApplicationUser(ApplicationUser user);
        void UpdateApplicationUser(ApplicationUser user);
        object LoginCredentials(ApplicationUser user);
        Task<bool> UserValidation(ApplicationUser user, string password);
    }
}
