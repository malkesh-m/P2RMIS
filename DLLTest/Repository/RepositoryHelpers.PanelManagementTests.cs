using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.HelperClasses;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
		
namespace DLLTest.Repository
{
    /// <summary>
    /// Unit tests for RepositoryHelpers for panel management
    /// </summary>
    [TestClass()]
    public partial class RepositoryHelpers: DllBaseTest
    {
        #region Attributes
        private string firstName = "first";
        private string lastName = "last";
        private string eMailAddress = "foo@foo.com";
        private int panelUserAssignmentId = 2345;

        private string firstName2 = "first";
        private string lastName2 = "last";
        private string eMailAddress2 = "foo@foo.com";
        private int userId2 = 4567;
        private int panelUserAssignmentId2 = 54321;

        #endregion
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
            this.InitializeMocks();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            this.CleanUpMocks();
        }
        //
        #endregion
        #endregion
        #region ListPanelUserEmailAddresses Tests

        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("RepositoryHelpers.ListPanelUserEmailAddresses")]
        public void Test2()
        {
            //
            // Set up local data
            //
            int sessionPanelId = 5;
            List<SessionPanel> context = new List<SessionPanel>();
            SessionPanel aSessionPanel = new SessionPanel { SessionPanelId = sessionPanelId };
            context.Add(aSessionPanel);
            //
            // Set up a single user assignment
            //
            PanelUserAssignment pua = SetUpPanelUserAssignment(firstName, lastName, eMailAddress, true, _goodUserId, panelUserAssignmentId); 
            aSessionPanel.PanelUserAssignments.Add(pua);
            pua = SetUpPanelUserAssignment(firstName2, lastName2, eMailAddress2, true, userId2, panelUserAssignmentId2);
            aSessionPanel.PanelUserAssignments.Add(pua);

            //
            // Test
            //
            var result = Sra.P2rmis.Dal.RepositoryHelpers.ListPanelUserEmailAddresses(context.AsQueryable<SessionPanel>(), sessionPanelId);
            //
            // Verify
            //
            Assert.IsNotNull(result, "No results returned");
            Assert.AreEqual(2, result.Count(), "Counts are not correct");
            AssertPanelUserAssignment(firstName, lastName, eMailAddress, panelUserAssignmentId, (EmailAddress)result.ElementAt(0));
            AssertPanelUserAssignment(firstName2, lastName2, eMailAddress2, panelUserAssignmentId2, (EmailAddress)result.ElementAt(1));
        }

        #region Helpers
		private void AssertPanelUserAssignment(string firstName, string lastName, string eMailAddress, int panelUserAssignmentId, EmailAddress result)
        {
            Assert.AreEqual(lastName, result.LastName, "Last name is not correct");
            Assert.AreEqual(firstName, result.FirstName, "First name is not correct");
            Assert.AreEqual(eMailAddress, result.UserEmailAddress, "Email address in not correct");
            Assert.AreEqual(panelUserAssignmentId, result.PanelUserAssignmentId, "PanelUserAssignmentId in not correct");

        }
        private PanelUserAssignment SetUpPanelUserAssignment(string firstName, string lastName, string eMailAddress, bool primaryFlag, int userId, int panelUserAssignmentId)
        {
            //
            // set the assignments
            //
            PanelUserAssignment pua = new PanelUserAssignment { UserId = userId, PanelUserAssignmentId = panelUserAssignmentId };
            //
            // Set the ClientParticipantType
            //
            ClientParticipantType cpt = new ClientParticipantType { ParticipantTypeAbbreviation = "SR" };
            pua.ClientParticipantType = cpt;
            //
            // set the user
            //
            User u = new User { UserID = userId };
            pua.User = u;
            //
            // Now a UserInfo
            //
            UserInfo ui = new UserInfo { LastName = lastName, FirstName = firstName };
            u.UserInfoes.Add(ui);
            //
            // Now add an email
            //
            UserEmail ue = new UserEmail { PrimaryFlag = primaryFlag, Email = eMailAddress };
            ui.UserEmails.Add(ue);

            return pua;
        }
        #endregion
        #endregion
    }		
}
