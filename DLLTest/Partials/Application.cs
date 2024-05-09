using NUnit.Framework;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using EntityApplication = Sra.P2rmis.Dal.Application;

namespace DLLTest.Partials
{
    /// <summary>
    /// Unit tests for 
    /// </summary>
    [TestClass()]
    public class Application
    {
        private const int STATUS_ID_1 = 1;
        private const int STATUS_ID_2 = 2;
        private const int STATUS_ID_3 = 3;
        private const int STATUS_ID_4 = 4;
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
        #region The Tests
        /// <summary>
        /// Test a duplicate.
        /// </summary>
        [TestMethod()]
        [Category("Application")] 
        public void DuplicateTest()
        {
            EntityApplication a = new EntityApplication();
            ApplicationReviewStatu rs = new ApplicationReviewStatu { ReviewStatusId = STATUS_ID_1 };
            rs.ReviewStatu = new ReviewStatu { ReviewStatusId = STATUS_ID_1 };
            a.ApplicationReviewStatus.Add(rs);

            bool result = a.IsDuplicateStatus(STATUS_ID_1);
            Assert.IsTrue(result, "Should have located duplicate status");
        }
        /// <summary>
        /// Test inserting into a application with no status
        /// </summary>
        [TestMethod()]
        [Category("Application")]
        public void EmptyTest()
        {
            EntityApplication a = new EntityApplication();
            bool result = a.IsDuplicateStatus(STATUS_ID_1);
            Assert.IsFalse(result, "Should have located duplicate status");
        }
        /// <summary>
        /// Test multiple entries for a status that does not exist
        /// </summary>
        [TestMethod()]
        [Category("Application")]
        public void ApplicationWithStatussNotExistTest()
        {
            EntityApplication a = new EntityApplication();
            ApplicationReviewStatu rs = new ApplicationReviewStatu { ReviewStatusId = STATUS_ID_1 };
            rs.ReviewStatu = new ReviewStatu { ReviewStatusId = STATUS_ID_1 };
            a.ApplicationReviewStatus.Add(rs);

            rs = new ApplicationReviewStatu { ReviewStatusId = STATUS_ID_2 };
            rs.ReviewStatu = new ReviewStatu { ReviewStatusId = STATUS_ID_2 };
            a.ApplicationReviewStatus.Add(rs);

            rs = new ApplicationReviewStatu { ReviewStatusId = STATUS_ID_3 };
            rs.ReviewStatu = new ReviewStatu { ReviewStatusId = STATUS_ID_3 };
            a.ApplicationReviewStatus.Add(rs);

            bool result = a.IsDuplicateStatus(STATUS_ID_4);
            Assert.IsFalse(result, "Should have located duplicate status");
        }
        /// <summary>
        /// Test multiple entries for a status that does  exist
        /// </summary>
        [TestMethod()]
        [Category("Application")]
        public void ApplicationWithStatussExistTest()
        {
            EntityApplication a = new EntityApplication();
            ApplicationReviewStatu rs = new ApplicationReviewStatu { ReviewStatusId = STATUS_ID_1 };
            rs.ReviewStatu = new ReviewStatu { ReviewStatusId = STATUS_ID_1 };
            a.ApplicationReviewStatus.Add(rs);

            rs = new ApplicationReviewStatu { ReviewStatusId = STATUS_ID_2 };
            rs.ReviewStatu = new ReviewStatu { ReviewStatusId = STATUS_ID_2 };
            a.ApplicationReviewStatus.Add(rs);

            rs = new ApplicationReviewStatu { ReviewStatusId = STATUS_ID_3 };
            rs.ReviewStatu = new ReviewStatu { ReviewStatusId = STATUS_ID_3 };
            a.ApplicationReviewStatus.Add(rs);

            bool result = a.IsDuplicateStatus(STATUS_ID_3);
            Assert.IsTrue(result, "Should have located duplicate status");
        }
        /// <summary>
        /// Test multiple entries for a status that does  exist
        /// </summary>
        [TestMethod()]
        [Category("Application")]
        public void ZeroTest()
        {
            EntityApplication a = new EntityApplication();
            ApplicationReviewStatu rs = new ApplicationReviewStatu { ReviewStatusId = STATUS_ID_1 };
            rs.ReviewStatu = new ReviewStatu { ReviewStatusId = STATUS_ID_1 };
            a.ApplicationReviewStatus.Add(rs);

            rs = new ApplicationReviewStatu { ReviewStatusId = STATUS_ID_2 };
            rs.ReviewStatu = new ReviewStatu { ReviewStatusId = STATUS_ID_2 };
            a.ApplicationReviewStatus.Add(rs);

            rs = new ApplicationReviewStatu { ReviewStatusId = STATUS_ID_3 };
            rs.ReviewStatu = new ReviewStatu { ReviewStatusId = STATUS_ID_3 };
            a.ApplicationReviewStatus.Add(rs);

            bool result = a.IsDuplicateStatus(0);
            Assert.IsFalse(result, "Should have located duplicate status");
        }
        #endregion
        #region PrimaryInvestigator Tests
        /// <summary>
        /// Test - retrieving primary investigator
        /// </summary>
        [TestMethod()]
        [Category("Application")]
        public void PrimaryInvestigatorHappyPathTest()
        {
            //
            // Set up local data
            //
            EntityApplication a = new EntityApplication();
            ApplicationPersonnel p = new ApplicationPersonnel { PrimaryFlag = true };
            a.ApplicationPersonnels.Add(p);
            //
            // Test
            //
            var result = a.PrimaryInvestigator();
            //
            // Verify
            //
            Assert.AreEqual(p, result, "expected ApplicationPersonnel object not returned");
        }
        /// <summary>
        /// Test - no primary investigator
        /// </summary>
        [TestMethod()]
        [Category("Application")]
        public void PrimaryInvestigatorNoInvestigatorTest()
        {
            //
            // Set up local data
            //
            EntityApplication a = new EntityApplication();
            ApplicationPersonnel p = new ApplicationPersonnel();
            a.ApplicationPersonnels.Add(p);
            //
            // Test
            //
            var result = a.PrimaryInvestigator();
            //
            // Verify
            //
            Assert.AreEqual(ApplicationPersonnel.Default, result, "expected ApplicationPersonnel object not returned");
        }
        /// <summary>
        /// Test - retrieving primary investigator
        /// </summary>
        [TestMethod()]
        [Category("Application")]
        public void PrimaryInvestigatorHappyPathMultipleEntriesTest()
        {
            //
            // Set up local data
            //
            EntityApplication a = new EntityApplication();
            ApplicationPersonnel p = new ApplicationPersonnel { PrimaryFlag = true };
            a.ApplicationPersonnels.Add(p);
            ApplicationPersonnel p1 = new ApplicationPersonnel { PrimaryFlag = true };
            a.ApplicationPersonnels.Add(p1);
            ApplicationPersonnel p2 = new ApplicationPersonnel { PrimaryFlag = true };
            a.ApplicationPersonnels.Add(p2);
            ApplicationPersonnel p3 = new ApplicationPersonnel { PrimaryFlag = true };
            a.ApplicationPersonnels.Add(p3);            //
            // Test
            //
            var result = a.PrimaryInvestigator();
            //
            // Verify
            //
            Assert.AreEqual(p, result, "expected ApplicationPersonnel object not returned");
        }
        #endregion
    }
}
