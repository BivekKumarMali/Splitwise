using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Splitwise.Repository.Test.Database
{
    public class FakeUserManager : UserManager<IdentityUser>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<IdentityUser>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<IdentityUser>>().Object,
                  new[] { new Mock<IUserValidator<IdentityUser>>().Object },
                    new[] { new Mock<IPasswordValidator<IdentityUser>>().Object },
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<IdentityUser>>>().Object)
        { }
    }
}
