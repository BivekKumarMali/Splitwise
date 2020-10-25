using Splitwise.DomainModel.Models;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public interface IUserRepository
    {
        bool UserExist(string userid);
        void AddApplicationUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
        object LoginCredentials(ApplicationUser user);
        Task<bool> UserValidation(ApplicationUser user, string password);
    }
}
