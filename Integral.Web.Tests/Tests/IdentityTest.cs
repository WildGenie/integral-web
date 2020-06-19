using System;
using System.IO;
using System.Linq;
using Integral.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Integral.Tests
{
    [TestClass]
    public class IdentityTest
    {
        private static readonly string ProjectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        private static readonly string ConnectionString = $"DataSource={ProjectDirectory}\\Test.db";

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