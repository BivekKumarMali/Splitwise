using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Splitwise.Data;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public class UserRepository: IUserRepository
    {
        #region Contructor
        public UserRepository(
            AppDbContext dbContext,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            IMapper mapper
            )
        {
            _dbContext = dbContext;
           _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
        }


        #endregion
        #region Private Variable

        private readonly AppDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        #endregion
        #region Private method

        private string AddUser(IdentityUser identityUser, string password)
        {
            var addUserManagerStatus = _userManager.CreateAsync(identityUser, password);
            addUserManagerStatus.Wait();
            var check = addUserManagerStatus.Result.Succeeded;
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

        #endregion

        #region Public method
        public void AddApplicationUser(UserDTO newUser)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(newUser);
            IdentityUser identityUser = new IdentityUser { Email = user.Email, UserName = user.Email };
            user.UserId = AddUser(identityUser, "123456");
            _dbContext.ApplicationUsers.Add(user);
            _dbContext.SaveChanges();
        }

        public object LoginCredentials(ApplicationUser user)
        {
            user = GetUserByID(user.UserId);
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

        public void UpdateApplicationUser(UserDTO newUser)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(newUser);
            var oldUserValue = GetUserByID(user.UserId);
            if(oldUserValue.Email != user.Email)
                UpdateUser(user);
            _dbContext.ApplicationUsers.Update(user);
            _dbContext.SaveChanges();

        }

        public bool UserExist(string userid)
        {
            return _dbContext.ApplicationUsers.Find(userid) != null ? true : false;
        }

        public async Task<bool> UserValidation(ApplicationUser user, string password)
        {
            IdentityUser identity = await _userManager.FindByIdAsync(user.UserId);
            return await _userManager.CheckPasswordAsync(identity, password);

        }
        #endregion
    }
}
