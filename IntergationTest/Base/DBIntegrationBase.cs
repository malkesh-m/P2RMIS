using NUnit.Framework;
using Sra.P2rmis.Dal;
using System.Data.Entity;

namespace DBIntegrationTest.Base
{
    [SetUpFixture]
    public class DBIntegrationBase
    {

        protected P2RMISNETEntities _context;
        protected DbContextTransaction _transaction;
        
        [OneTimeSetUp]
        public void Setup()
        {
            _context = new P2RMISNETEntities();           
            _transaction = _context.Database.BeginTransaction();
            
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _transaction.Rollback();
            _transaction.Dispose();            
            _context.Dispose();
        }
    }
}
