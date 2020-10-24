using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
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

        // UserManager
        [TestCase("test", "testgmailcom")]
        public void AddUserTest(string name, string email)
        {
            ApplicationUser applicationUser = new ApplicationUser { UserId = "", Name = name, Email = email };
            using (var db = TestDbContextFactory.Create(nameof(AddUserTest))) 
            {
                //_userRepository =  new UserRepository(db);
                var result = _userRepository.AddApplicationUser(applicationUser);
                Assert.AreEqual(true, true);

            }
            using (var db = TestDbContextFactory.Create(nameof(AddUserTest))) 
            {
                var storedUser = db.ApplicationUsers.Single();
                Assert.AreEqual(storedUser, applicationUser);
            }
        }

    }
}
