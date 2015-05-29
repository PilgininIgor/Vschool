using System;
using System.Data.Common;
using System.Web.Mvc;
using ILS.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    public abstract class BaseTest
    {
        protected ILSContext context;

        [TestInitialize]
        public void SetupTest()
        {
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient();
            using (var c = new ILSContext(connection))
            {
                c.Database.CreateIfNotExists();
                AddMockData(c);
                c.SaveChanges();
            }
            context = new ILSContext(connection);
        }

        [TestCleanup]
        public void CleanupTest()
        {
            if (context != null)
                context.Dispose();
            context = null;
        }

        protected abstract void AddMockData(ILSContext context);

        protected T CreateController<T>() where T : Controller
        {
            return (T)Activator.CreateInstance(typeof(T), context);
        }
    }
}
