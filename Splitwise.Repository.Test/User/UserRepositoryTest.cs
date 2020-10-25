using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Splitwise.Data;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.Test.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splitwise.Repository.Test.User
{
    [TestFixture]
    class UserRepositoryTest
    {
        private IUserRepository _userRepository;
        private IConfiguration _configuration;
        private FakeUserManager _userManager;

        private ApplicationUser CreateApplicationUser(string name, string email)
        {
            IdentityUser identityUser = new IdentityUser { Email = email, UserName = email };
            return new ApplicationUser { UserId = identityUser.Id, Name = name, Email = email }; ;
        }

        private void AddUserMock(ApplicationUser applicationUser, string databaseName)
        {
            using (var db = TestDbContextFactory.Create(databaseName))
            {
                _userRepository = SetUpUserRepository(db);
                _userRepository.AddApplicationUser(applicationUser);

            }
        }

        private FakeUserManager SetUserManager()
        {
            return new Mock<FakeUserManager>().Object;
        }

        private UserRepository SetUpUserRepository(AppDbContext db)
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
            return new UserRepository(db, SetUserManager(), _configuration);
        }

        [TestCase("test", "test123@gmail.com")]
        public void AddUserTest(string name, string email)
        {
            ApplicationUser applicationUser = CreateApplicationUser(name, email);
            AddUserMock(applicationUser, nameof(AddUserTest));
            using (var db = TestDbContextFactory.Create(nameof(AddUserTest)))
            {
                var storedUser = db.ApplicationUsers.Single();
                Assert.AreEqual(storedUser.UserId, applicationUser.UserId);
            }
        }
        
        [TestCase("test", "test123@gmail.com")]
        public void EditUserTest(string name, string email)
        {
            ApplicationUser applicationUser = CreateApplicationUser(name, email);
            AddUserMock(applicationUser, nameof(EditUserTest));
            using (var db = TestDbContextFactory.Create(nameof(EditUserTest)))
            {
                applicationUser = db.ApplicationUsers.Single();
                applicationUser.Name = "Test2";

                _userRepository = SetUpUserRepository(db);
                _userRepository.UpdateUser(applicationUser);
                

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
            ApplicationUser applicationUser = CreateApplicationUser(name, email);
            AddUserMock(applicationUser, nameof(UserExistTest));
            using (var db = TestDbContextFactory.Create(nameof(UserExistTest)))
            {
                applicationUser = db.ApplicationUsers.Single();

                _userRepository = SetUpUserRepository(db);
                bool result =_userRepository.UserExist(applicationUser.UserId);

                Assert.AreEqual(true, result);

            }
        }

        [TestCase("test", "test123@gmail.com")]
        public void LoginCredentialsTest(string name, string email)
        {
            ApplicationUser applicationUser = CreateApplicationUser(name, email);
            AddUserMock(applicationUser, nameof(LoginCredentialsTest));
            using (var db = TestDbContextFactory.Create(nameof(LoginCredentialsTest)))
            {
                applicationUser = db.ApplicationUsers.Single();

                _userRepository = SetUpUserRepository(db);
                object resultObject =_userRepository.LoginCredentials(applicationUser);
                Assert.AreNotEqual(null, resultObject);

            }
        }
        
        [TestCase("test", "test123@gmail.com")]
        public void UserValidationTest(string name, string email)
        {
            ApplicationUser applicationUser = CreateApplicationUser(name, email);
            IdentityUser user = new IdentityUser { Email = email, UserName = name };
            using (var db = TestDbContextFactory.Create(nameof(UserValidationTest)))
            {
                _userManager = SetUserManager();
                var Task =_userManager.CreateAsync(user, "13456");
                Task.Wait();
                var b = Task.Result;
                var Task2 = _userManager.CheckPasswordAsync(user, "13456");
                Task2.Wait();
                var result = Task.Result;
                Assert.AreEqual(true, result);

            }
        }

        

        

    }
}
