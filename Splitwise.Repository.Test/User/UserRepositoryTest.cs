using NUnit.Framework;
using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Splitwise.Repository;

namespace Splitwise.Repository.Test
{
    [TestFixture]
    public class UserRepositoryTest
    {
        [OneTimeSetUp]
        public void Init()
        {
        }

        [Test]
        public void TestAddUser()
        {
            UserRepository userRepository = new UserRepository();
            ApplicationUser sampleuser = new ApplicationUser();
            Assert.AreEqual(true, userRepository.AddApplicationUser(sampleuser));

        }

        public bool LogIn(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public object LoginCredentials()
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public bool UserExist(long userid)
        {
            throw new NotImplementedException();
        }
    }
}
