using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using NUnit.Framework;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.PanelManagement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;


namespace DLLTest
{
    [TestClass()]
    public class DLLTest
    {
        #region Overhead
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
        #endregion
        [Category("TEST")]
        [TestMethod()]
        public void test()
        {
            var c = new FakeP2rmisContext
            {
                LookupReportGroups = 
                {
                    new LookupReportGroup {}
                }
            };

            var x = GetMenu(c);
            Assert.IsNotNull(x);
            Assert.AreEqual(1, x.Count());
        }

        #region TEST ME
        /// <summary>
        /// JUST TO TEST RETURN ALL
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ReportListModel> GetMenu(IP2rmisDbContext context)
        {
            var result = from reportGroup in context.LookupReportGroups
                         select new ReportListModel
                         {
                             ReportGroupId = reportGroup.ReportGroupId,
                             ReportGroupName = reportGroup.ReportGroupName,
                             ReportGroupDescription = reportGroup.ReportDescription,
                             ReportGroupSortOrder = reportGroup.SortOrder
                         };
            return result;
        }


        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("CATEGORY.Testing")]
        public void Test()
        {
            string _thisEmail = "bingo  bingo";
            int _sessionPanelId = 32;
            //
            // Set up local data
            //
            List<PanelUserAssignment> list = new List<PanelUserAssignment>();

            User u = new User();
            UserInfo ui = new UserInfo();
            UserEmail ue = new UserEmail {PrimaryFlag = true, Email = _thisEmail};
            ClientParticipantType cpt = new ClientParticipantType { ReviewerFlag = false };

            ui.UserEmails.Add(ue);
            u.UserInfoes.Add(ui);

            PanelUserAssignment p = new PanelUserAssignment { SessionPanelId = _sessionPanelId};
            list.Add(p);

            p.User = u;
            p.ClientParticipantType = cpt;
            //
            // Test
            //
            IEnumerable<IPanelAdministrators> result = RepositoryHelpers.GetPanelAdministrators(list.AsQueryable<PanelUserAssignment>(), _sessionPanelId);
            //
            // Verify
            //

            Assert.IsNotNull(result);
            if (result == null) Console.WriteLine("1");
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(_thisEmail, result.ElementAt(0).EmailAddress);
        }
        /// <summary>
        /// Test 
        /// </summary>
        public void Test2()
        {
            string _thisEmail = "bingo  bingo";
            int _sessionPanelId = 32;
            //
            // Set up local data
            //
            List<PanelUserAssignment> list = new List<PanelUserAssignment>();

            User u = new User();

            UserInfo ui = new UserInfo();
            UserInfo ui2 = new UserInfo();

            UserEmail ue = new UserEmail { PrimaryFlag = true, Email = _thisEmail };
            UserEmail ue2 = new UserEmail { PrimaryFlag = false, Email = "absdeedd" };

            ui.UserEmails.Add(ue);
            ui2.UserEmails.Add(ue2);

            u.UserInfoes.Add(ui);
            u.UserInfoes.Add(ui2);

            PanelUserAssignment p = new PanelUserAssignment { SessionPanelId = _sessionPanelId };
            list.Add(p);

            p.User = u;
            p.ClientParticipantType = new ClientParticipantType { ReviewerFlag = false };
            //
            // Test
            //
            IEnumerable<IPanelAdministrators> result = RepositoryHelpers.GetPanelAdministrators(list.AsQueryable<PanelUserAssignment>(), _sessionPanelId);
            //
            // Verify
            //

            Assert.IsNotNull(result);
            if (result == null) Console.WriteLine("1");
            var x = result.ElementAtOrDefault(0);
            if (x == null) Console.WriteLine("2");
            // did not work var y = result.AsEnumerable<IPanelAdministrators>().Count<IPanelAdministrators>();
            //var pp = result.Where(a => a != null);
            // did not work var y = result.ToArray();
            // did not work var y = result.ToList<IPanelAdministrators>();
            //Console.WriteLine("count " + pp.Count<IPanelAdministrators>());
            var q0 = result.ElementAtOrDefault(0);
            Console.WriteLine(" q0 " + q0.EmailAddress);

            var q1 = result.ElementAtOrDefault(1);
            Console.WriteLine("q10 " + q1.EmailAddress);

            //Assert.AreEqual(1, result.ElementAtOrDefault(0));
            //foreach (var b in pp)
            //{
            //    Console.WriteLine("3");
            //}
            Assert.AreEqual(_thisEmail, x.EmailAddress);
        }		
        #endregion
    }

    #region FAKE DB
    public interface IP2rmisDbContext
    {
        IDbSet<LookupReportGroup> LookupReportGroups { get; }
        int SaveChanges();
    }

    #endregion
    #region FAKE DBSET
    public class FakeDbSet<T> : IDbSet<T>  where T : class
    {
        ObservableCollection<T> _data;
        IQueryable _query;

        public FakeDbSet()
        {
            _data = new ObservableCollection<T>();
            _query = _data.AsQueryable();
        }

        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public T Add(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Detach(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<T> Local
        {
            get { return _data; }
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
    #endregion 
    #region FAKE DBOBJECTS
    public class FakeLookupReportGroupSet : FakeDbSet<LookupReportGroup>
    {
        //
        // IF find is called define what you want find to look like
        //
        public override LookupReportGroup Find(params object[] keyValues)
        {
            return base.Find(keyValues);
        }
    }
    #endregion
    #region FAKE Context Implementation 

    public class FakeP2rmisContext : IP2rmisDbContext
    {
        public FakeP2rmisContext()
        {
            this.LookupReportGroups = new FakeLookupReportGroupSet();
        }

        public IDbSet<LookupReportGroup> LookupReportGroups { get; private set; }


        public int SaveChanges()
        {
            return 0;
        }
    }

    #endregion

}
