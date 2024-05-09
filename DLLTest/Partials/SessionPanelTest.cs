using System;
using NUnit.Framework;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using Entity = Sra.P2rmis.Dal;
		

namespace DLLTest.Partials
{
    /// <summary>
    /// Unit tests for SessionPanel Entity extensions
    /// </summary>
    [TestClass()]
    public class SessionPanelTest: DllBaseTest
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            InitializeMocks();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            CleanUpMocks();
        }
        //
        #endregion
        #endregion
        #region  GetPanelApplicationByApplicationLogNumber Tests
        /// <summary>
        /// Test for a single entry that matches
        /// </summary>
        [TestMethod()]
        [Category("SessionPanel")]
        public void GetPanelApplicationByApplicationLogNumberOneEntryTest()
        {
            //
            // Set up local data
            //
            int applicationId = 123;
            Entity.SessionPanel panel = new Entity.SessionPanel();
            string logNumber = "BC12345";
            panel.PanelApplications.Add(new Entity.PanelApplication { ApplicationId = applicationId, Application = new Entity.Application { LogNumber = logNumber } });

            RunGetPanelApplicationByApplicationLogNumberTest(panel, logNumber);
        }
        /// <summary>
        /// Test for a multiple entry that matches
        /// </summary>
        [TestMethod()]
        [Category("SessionPanel")]
        public void GetPanelApplicationByApplicationLogNumberMultipleEntryTest()
        {
            //
            // Set up local data
            //
            int applicationId = 123;
            Entity.SessionPanel panel = new Entity.SessionPanel();
            string logNumber1 = "BC12345";
            string logNumber2 = "BC234";
            string logNumber3 = "BC156745";
            string logNumber4 = "BC168732";
            string logNumber5 = "BC15";
            string logNumber6 = "BC10002345";
            string logNumber7 = "BC12357345";
            panel.PanelApplications.Add(new Entity.PanelApplication { ApplicationId = applicationId, Application = new Entity.Application { LogNumber = logNumber1 } });
            panel.PanelApplications.Add(new Entity.PanelApplication { ApplicationId = applicationId, Application = new Entity.Application { LogNumber = logNumber2 } });
            panel.PanelApplications.Add(new Entity.PanelApplication { ApplicationId = applicationId, Application = new Entity.Application { LogNumber = logNumber3 } });
            panel.PanelApplications.Add(new Entity.PanelApplication { ApplicationId = applicationId, Application = new Entity.Application { LogNumber = logNumber4 } });
            panel.PanelApplications.Add(new Entity.PanelApplication { ApplicationId = applicationId, Application = new Entity.Application { LogNumber = logNumber5 } });
            panel.PanelApplications.Add(new Entity.PanelApplication { ApplicationId = applicationId, Application = new Entity.Application { LogNumber = logNumber6 } });
            panel.PanelApplications.Add(new Entity.PanelApplication { ApplicationId = applicationId, Application = new Entity.Application { LogNumber = logNumber7 } });

            RunGetPanelApplicationByApplicationLogNumberTest(panel, logNumber1);
        }
        /// <summary>
        /// Test for an entry that is not in collection
        /// </summary>
        [TestMethod()]
        [Category("SessionPanel")]
        public void GetPanelApplicationByApplicationLogNumberNoMatchTest()
        {
            //
            // Set up local data
            //
            int applicationId = 123;
            Entity.SessionPanel panel = new Entity.SessionPanel();
            string logNumber1 = "BC12345";
            string logNumber2 = "BC234";
            string logNumber3 = "BC156745";
            string logNumber4 = "BC168732";
            string logNumber5 = "BC15";
            string logNumber6 = "BC10002345";
            string logNumber7 = "BC12357345";
            panel.PanelApplications.Add(new Entity.PanelApplication { ApplicationId = applicationId, Application = new Entity.Application { LogNumber = logNumber1 } });
            panel.PanelApplications.Add(new Entity.PanelApplication { ApplicationId = applicationId, Application = new Entity.Application { LogNumber = logNumber2 } });
            panel.PanelApplications.Add(new Entity.PanelApplication { ApplicationId = applicationId, Application = new Entity.Application { LogNumber = logNumber3 } });
            panel.PanelApplications.Add(new Entity.PanelApplication { ApplicationId = applicationId, Application = new Entity.Application { LogNumber = logNumber4 } });
            panel.PanelApplications.Add(new Entity.PanelApplication { ApplicationId = applicationId, Application = new Entity.Application { LogNumber = logNumber5 } });
            panel.PanelApplications.Add(new Entity.PanelApplication { ApplicationId = applicationId, Application = new Entity.Application { LogNumber = logNumber6 } });
            panel.PanelApplications.Add(new Entity.PanelApplication { ApplicationId = applicationId, Application = new Entity.Application { LogNumber = logNumber7 } });
            //
            // Test
            //
            var result = panel.GetPanelApplicationByApplicationLogNumber("ZZ45678");
            //
            // Verify
            //
            Assert.IsNull(result, "result was not null and should be");
        }
        /// <summary>
        /// Test collection is empty
        /// </summary>
        [TestMethod()]
        [Category("SessionPanel")]
        public void GetPanelApplicationByApplicationLogNumberEmptyCollectionTest()
        {
            //
            // Set up local data
            //
            Entity.SessionPanel panel = new Entity.SessionPanel();
            //
            // Test
            //
            var result = panel.GetPanelApplicationByApplicationLogNumber("ZZ45678");
            //
            // Verify
            //
            Assert.IsNull(result, "result was not null and should be");
        }
        #region IsActive tests
        /// <summary>
        /// Test - Now between dates
        /// </summary>
        [TestMethod()]
        [Category("SessionPanel")]
        public void IsActiveBetweenDatesTest()
        {
            Entity.SessionPanel sessionPanelEntity = new Entity.SessionPanel();
            sessionPanelEntity.StartDate = DateTime.Now.AddDays(-2);
            sessionPanelEntity.EndDate = DateTime.Now.AddDays(2);

            bool result = sessionPanelEntity.IsActive();

            Assert.IsTrue(result, "IsActive did not return correct value");
        }
        /// <summary>
        /// Test - Now equal StartDate
        /// </summary>
        [TestMethod()]
        [Category("SessionPanel")]
        public void EqualStartDateTest()
        {
            Entity.SessionPanel sessionPanelEntity = new Entity.SessionPanel();
            sessionPanelEntity.StartDate = DateTime.Now;
            sessionPanelEntity.EndDate = DateTime.Now.AddDays(2);

            bool result = sessionPanelEntity.IsActive();

            Assert.IsTrue(result, "IsActive did not return correct value");
        }
        /// <summary>
        /// Test - Now equal EndDate
        /// </summary>
        [TestMethod()]
        [Category("SessionPanel")]
        public void EqualEndDateTest()
        {
            Entity.SessionPanel sessionPanelEntity = new Entity.SessionPanel();
            sessionPanelEntity.StartDate = DateTime.Now.AddDays(-2);
            sessionPanelEntity.EndDate = DateTime.Now;

            bool result = sessionPanelEntity.IsActive();

            Assert.IsTrue(result, "IsActive did not return correct value");
        }
        /// <summary>
        /// Test - Now before StartDate
        /// </summary>
        [TestMethod()]
        [Category("SessionPanel")]
        public void BeforeStartDateTest()
        {
            Entity.SessionPanel sessionPanelEntity = new Entity.SessionPanel();
            sessionPanelEntity.StartDate = DateTime.Now.AddDays(2);
            sessionPanelEntity.EndDate = DateTime.Now.AddDays(4);

            bool result = sessionPanelEntity.IsActive();

            Assert.IsFalse(result, "IsActive did not return correct value");
        }
        /// <summary>
        /// Test - Now after EndDate
        /// </summary>
        [TestMethod()]
        [Category("SessionPanel")]
        public void AfterEndDateTest()
        {
            Entity.SessionPanel sessionPanelEntity = new Entity.SessionPanel();
            sessionPanelEntity.StartDate = DateTime.Now.AddDays(-4);
            sessionPanelEntity.EndDate = DateTime.Now.AddDays(-2);

            bool result = sessionPanelEntity.IsActive();

            Assert.IsFalse(result, "IsActive did not return correct value");
        }
        /// <summary>
        /// Test - Null for dates
        /// </summary>
        [TestMethod()]
        [Category("SessionPanel")]
        public void NullTest()
        {
            Entity.SessionPanel sessionPanelEntity = new Entity.SessionPanel();

            bool result = sessionPanelEntity.IsActive();

            Assert.IsFalse(result, "IsActive did not return correct value");
        }
        /// <summary>
        /// Test - Null EndDate
        /// </summary>
        [TestMethod()]
        [Category("SessionPanel")]
        public void NullEndDateTest()
        {
            Entity.SessionPanel sessionPanelEntity = new Entity.SessionPanel();
            sessionPanelEntity.StartDate = DateTime.Now.AddDays(-4);

            bool result = sessionPanelEntity.IsActive();

            Assert.IsFalse(result, "IsActive did not return correct value");
        }
        /// <summary>
        /// Test - Null StartDate
        /// </summary>
        [TestMethod()]
        [Category("SessionPanel")]
        public void NullStartDateTest()
        {
            Entity.SessionPanel sessionPanelEntity = new Entity.SessionPanel();
            sessionPanelEntity.EndDate = DateTime.Now.AddDays(4);

            bool result = sessionPanelEntity.IsActive();

            Assert.IsFalse(result, "IsActive did not return correct value");
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Runs test for GetPanelApplicationByApplicationLogNumber
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="logNumber"></param>
        private void RunGetPanelApplicationByApplicationLogNumberTest(Entity.SessionPanel panel, string logNumber)
        {
            //
            // Test
            //
            var result = panel.GetPanelApplicationByApplicationLogNumber(logNumber);
            //
            // Verify
            //
            Assert.IsNotNull(result, "result was null and should not be");
            Assert.AreEqual(logNumber, result.Application.LogNumber, "result applicationId was not as expected");
        }
        #endregion
        #endregion
    }
		
}
