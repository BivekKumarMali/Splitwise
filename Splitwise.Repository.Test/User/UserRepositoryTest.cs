using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Splitwise.Data;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.Test.Database;
using System.Collections.Generic;
using System.Linq;

namespace Splitwise.Repository.Test.User
{
    [TestFixture]
    class UserRepositoryTest
    {
        private IUserRepository _userRepository;
        private Mock<IUserRepository> testuser;
        private IConfiguration _configuration;
        private Mock<FakeUserManager> _userManager = new Mock<FakeUserManager>();
        private IdentityResult identityResult = new IdentityResult();

        private IdentityUser CreateIdentityUser(string email)
        {
            return new IdentityUser { Email = email, UserName = email };
        }
        
        private ApplicationUser CreateApplicationUser(IdentityUser identityUser, string name)
        {
            return new ApplicationUser {UserId = identityUser.Id, Email = identityUser.Email, Name = name };
        }

        private void AddUserMock(ApplicationUser applicationUser, string databaseName, FakeUserManager userManager)
        {
            using (var db = TestDbContextFactory.Create(databaseName))
            {
                _userRepository = SetUserRepository(db, userManager);
                _userRepository.AddApplicationUser(applicationUser);
            }
        }

        private void SetUserManager(IdentityUser identityUser)
        {
            IdentityResult result = IdentityResult.Success;

            _userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(result);

            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(identityUser);

            _userManager.Setup(x => x.UpdateAsync(It.IsAny<IdentityUser>()))
                .ReturnsAsync(result);
            
            _userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);


        }

        private UserRepository SetUserRepository(AppDbContext db, FakeUserManager userManager)
        {
            var myConfiguration = new Dictionary<string, string>
            {
                { "JWT:ValidAudience", "http://localhost:4200" },
                {    "JWT:ValidIssuer", "http://localhost:44339" },
                {    "JWT:Secret", "ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"}
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();
            return new UserRepository(db, userManager, _configuration);
        }

        [TestCase("test", "test123@gmail.com")]
        public void AddUserTest(string name, string email)
        {
            IdentityUser identityUser = CreateIdentityUser(email);
            ApplicationUser applicationUser = CreateApplicationUser(identityUser, email);

            SetUserManager(identityUser);

            AddUserMock(applicationUser, nameof(AddUserTest), _userManager.Object);
            using (var db = TestDbContextFactory.Create(nameof(AddUserTest)))
            {
                var storedUser = db.ApplicationUsers.Single();
                Assert.AreEqual(storedUser.UserId, applicationUser.UserId);
            }
        }
        
        [TestCase("test", "test123@gmail.com")]
        public void EditUserTest(string name, string email)
        {
            IdentityUser identityUser = CreateIdentityUser(email);
            ApplicationUser applicationUser = CreateApplicationUser(identityUser, email);

            SetUserManager(identityUser);

            AddUserMock(applicationUser, nameof(EditUserTest), _userManager.Object);
            using (var db = TestDbContextFactory.Create(nameof(EditUserTest)))
            {
                applicationUser = db.ApplicationUsers.Single();
                applicationUser.Name = "Test2";

                _userRepository = SetUserRepository(db, _userManager.Object);
                _userRepository.UpdateApplicationUser(applicationUser);
                

            }
            using (var db = TestDbContextFactory.Create(nameof(EditUserTest)))
            {
                var storedUser = db.ApplicationUsers.Single();
                Assert.AreEqual(storedUser.Name, applicationUser.Name);
            }
        }
        
        [TestCase("test", "test123@gmail.com")]
        public void UserExistTest(string name, string email)
        {
            IdentityUser identityUser = CreateIdentityUser(email);
            ApplicationUser applicationUser = CreateApplicationUser(identityUser, email);

            SetUserManager(identityUser);

            AddUserMock(applicationUser, nameof(EditUserTest), _userManager.Object);
            using (var db = TestDbContextFactory.Create(nameof(UserExistTest)))
            {
                applicationUser = db.ApplicationUsers.Single();

                _userRepository = SetUserRepository(db, _userManager.Object);
                bool result =_userRepository.UserExist(applicationUser.UserId);

                Assert.AreEqual(true, result);

            }
        }

        [TestCase("test", "test123@gmail.com")]
        public void LoginCredentialsTest(string name, string email)
        {
            IdentityUser identityUser = CreateIdentityUser(email);
            ApplicationUser applicationUser = CreateApplicationUser(identityUser, email);

            SetUserManager(identityUser);

            AddUserMock(applicationUser, nameof(EditUserTest), _userManager.Object);
            using (var db = TestDbContextFactory.Create(nameof(LoginCredentialsTest)))
            {
                applicationUser = db.ApplicationUsers.Single();

                _userRepository = SetUserRepository(db, _userManager.Object);
                object resultObject =_userRepository.LoginCredentials(applicationUser);
                Assert.AreNotEqual(null, resultObject);

            }
        }
        
        [TestCase("test", "test123@gmail.com", "123456")]
        public void UserValidationTest(string name, string email, string password)
        {
            IdentityUser identityUser = CreateIdentityUser(email);
            ApplicationUser applicationUser = CreateApplicationUser(identityUser, email);

            SetUserManager(identityUser);

            AddUserMock(applicationUser, nameof(EditUserTest), _userManager.Object);
            IdentityUser user = new IdentityUser { Email = email, UserName = name };
            using (var db = TestDbContextFactory.Create(nameof(UserValidationTest)))
            {
                var result = _userRepository.UserValidation(applicationUser, password);
                result.Wait();
                Assert.AreEqual(true, result.Result);
            }
        }
        
        

        

    }
}
