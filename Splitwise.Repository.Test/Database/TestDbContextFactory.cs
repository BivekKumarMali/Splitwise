using Microsoft.EntityFrameworkCore;
using Splitwise.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository.Test.Database
{
    public class TestDbContextFactory
    {
        public static AppDbContext Create(string databaseName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(nameof(databaseName))
                .Options;
            return new AppDbContext(options);
        }

    }
}
