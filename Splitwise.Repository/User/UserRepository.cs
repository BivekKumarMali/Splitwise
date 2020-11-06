using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Splitwise.Data;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public class UserRepository : IUserRepository
    {
        #region Contructor
        public UserRepository(
            AppDbContext dbContext,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration
            )
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _configuration = configuration;
        }


        #endregion
        #region Private Variable

        private readonly AppDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        #endregion
        #region Private method

        private string AddUser(IdentityUser identityUser, string password)
        {
            var addUserManagerStatus = _userManager.CreateAsync(identityUser, password);
            addUserManagerStatus.Wait();
            _ = addUserManagerStatus.Result;
            var TaskUser = _userManager.FindByEmailAsync(identityUser.Email);
            TaskUser.Wait();
            return TaskUser.Result.Id;
        }

        private bool UpdateUser(ApplicationUser user)
        {
            IdentityUser identityUser = new IdentityUser { Id = user.UserId, UserName = user.Email, Email = user.Email };
            var updateUserManagerStatus = _userManager.UpdateAsync(identityUser);
            updateUserManagerStatus.Wait();
            return updateUserManagerStatus.Result.Succeeded;
        }

        private ApplicationUser GetUserByID(string userid)
        {
            return _dbContext.ApplicationUsers.Find(userid);
        }


        private ApplicationUser GetUserByMali(string email)
        {
            return _dbContext.ApplicationUsers.First(x => x.Email == email);
        }
        #endregion

        #region Public method
        public void AddApplicationUser(ApplicationUser user)
        {
            IdentityUser identityUser = new IdentityUser { Email = user.Email, UserName = user.Email };
            user.UserId = AddUser(identityUser, "123456");
            _dbContext.ApplicationUsers.Add(user);
            _dbContext.SaveChanges();
        }

        public object LoginCredentials(string email)
        {
            ApplicationUser user = GetUserByMali(email);
            var authClaims = new List<Claim>
                        {
                            new Claim("name", user.Name),
                            new Claim("UserID", user.UserId.ToString()),
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };


            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return (new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }


        public void UpdateApplicationUser(ApplicationUser user)
        {
            UpdateUser(user);
            _dbContext.ApplicationUsers.Update(user);
            _dbContext.SaveChanges();

        }

        public bool UserExist(string userid)
        {
            return _dbContext.ApplicationUsers.Find(userid) != null;
        }
        public bool UserExistByMail(string email)
        {
            return _dbContext.ApplicationUsers.FirstOrDefault(x => x.Email == email) != null;
        }



        public async Task<bool> UserValidation(string email, string password)
        {
            IdentityUser identity = await _userManager.FindByEmailAsync(email);
            return await _userManager.CheckPasswordAsync(identity, password);

        }

        public IEnumerable<UserDTO> FindByMail(string mail)
        {
            var user = from u in _dbContext.ApplicationUsers
                       where u.Email == mail
                       select new UserDTO
                       {
                           Id = u.UserId,
                           Name = u.Name,
                           Email = u.Email
                       };
            if (user.ToList().Count == 0)
            {
                return from u in _dbContext.ApplicationUsers.ToList()
                       select new UserDTO
                       {
                           Id = u.UserId,
                           Name = u.Name,
                           Email = u.Email
                       };
            }
            return user.ToList();
        }
        #endregion
    }
}
