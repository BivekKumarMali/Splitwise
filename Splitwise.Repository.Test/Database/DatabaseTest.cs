using NUnit.Framework;
using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Splitwise.Repository;
using Microsoft.AspNetCore.Identity;
using Splitwise.Repository.Test.Database;
using System.Linq;

namespace Splitwise.Repository.Test.Database
{
    [TestFixture]
    public class DatabaseTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [TestCase("test", "testgmailcom")]
        public void TestDatabase(string name, string email)
        {
            ApplicationUser sampleuser = new ApplicationUser { UserId = "", Name = name, Email = email };
            using (var db = TestDbContextFactory.Create(nameof(TestDatabase)))
            {
                db.ApplicationUsers.Add(sampleuser);
                db.SaveChanges();
            }
            using (var db = TestDbContextFactory.Create(nameof(TestDatabase)))
            {
                var savedUser = db.ApplicationUsers.Single();
                Assert.AreEqual(sampleuser.Name, savedUser.Name);
            }

        }
        [Test]
        public void TestUserManager()
        {
            IdentityUser user = new IdentityUser { Email = "test", UserName = "test" };
            using (var db = TestDbContextFactory.Create(nameof(TestUserManager)))
            {
                db.Add(user);
                db.SaveChanges();
            }
            using (var db = TestDbContextFactory.Create(nameof(TestUserManager)))
            {
                var newUser = db.Users.Single();
                Assert.AreEqual(user.Id, newUser.Id);
            }
        }
    }
}

