using System.Linq;
using Integral.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Integral.Tests
{
    [TestClass]
    public class IdentityTest
    {
        private const string ConnectionString = @"Data Source=B:\Data\Test.db";

        [TestMethod]
        public void SelectUsers()
        {
            DbContextOptionsBuilder<IdentityContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
            dbContextOptionsBuilder.UseSqlite(ConnectionString);

            IdentityContext identityContext = new IdentityContext(dbContextOptionsBuilder.Options);
            int count = identityContext.Users.Count();

            Assert.IsTrue(count > 0);
        }
    }
}