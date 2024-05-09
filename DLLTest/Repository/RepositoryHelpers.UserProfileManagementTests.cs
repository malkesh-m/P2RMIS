/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.WebModels.UserProfileManagement;
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
    public partial class ProfileRepositoryHelpers : DllBaseTest
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
        private string fiscalYear = "2008";
        private string clientAbrv = "USAMRMC";
        private string participantType = "Reviewer";
        private string participantRole = "Scientist";
        private string programAbrv = "BCRP";
        private string panelAbrv = "CBY-1";
        private DateTime? panelEnd = DateTime.Parse("03/15/2015");
        private string scope = "Panel";
        private int participantId = 15248;
        private DateTime? notificationSent = null;
        private string fiscalYear2 = "2010";
        private string clientAbrv2 = "USAMRMC";
        private string participantType2 = "Ad-Hoc Member";
        private string participantRole2 = null;
        private string programAbrv2 = "BCRP";
        private string panelAbrv2 = null;
        private DateTime? panelEnd2 = null;
        private string scope2 = "Program";
        private int participantId2 = 17215;
        private DateTime? notificationSent2 = null;
        private string fiscalYear3 = "2010";
        private string clientAbrv3 = "CPRIT COM";
        private string participantType3 = "Reviewer";
        private string participantRole3 = "Advocate";
        private string programAbrv3 = "PCRP";
        private string panelAbrv3 = "PBY-1";
        private DateTime? panelEnd3 = DateTime.Parse("05/12/2015");
        private string scope3 = "Panel";
        private int participantId3 = 110294;
        private DateTime? notificationSent3 = DateTime.Parse("05/02/2015");


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
        #region GetParticipationHistory Tests

        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("RepositoryHelpers.GetParticipationHistory")]
        public void Test2()
        {
            //
            // Set up local data
            //
            int userInfoId = 10;
            int userId = 10;
            List<User> context = new List<User>();
            User aUser = new User { UserID = userId };
            UserInfo aUserInfo = new UserInfo {UserID = userId, UserInfoID = userInfoId};
            //Call method to set up at the most granular level
            PanelUserAssignment pua = SetUpPanelUserAssignment(userId, fiscalYear, clientAbrv, participantType, participantRole, programAbrv, panelAbrv, panelEnd, scope, participantId, notificationSent);
            ProgramUserAssignment prua = SetUpProgramUserAssignment(userId, fiscalYear2, clientAbrv2, participantType2,
                participantRole2, programAbrv2, panelAbrv2, panelEnd2, scope2, participantId2, notificationSent2);
            //
            // Set up a single user assignment
            //
            context.Add(aUser);
            aUser.UserInfoes.Add(aUserInfo);
            aUser.PanelUserAssignments.Add(pua);
            aUser.ProgramUserAssignments.Add(prua);
            pua = SetUpPanelUserAssignment(userId, fiscalYear3, clientAbrv3, participantType3, participantRole3, programAbrv3,
                panelAbrv3, panelEnd3, scope3, participantId3, notificationSent3);
            aUser.PanelUserAssignments.Add(pua);
            //
            // Test
            //
            var result = Sra.P2rmis.Dal.Repository.UserProfileManagement.RepositoryHelpers.GetParticipationHistory(context.AsQueryable<User>(), userInfoId);
            //
            // Verify
            //
            Assert.IsNotNull(result, "No results returned");
            Assert.AreEqual(3, result.Count(), "Counts are not correct");
            
            AssertParticipationHistory(fiscalYear, clientAbrv, participantType, participantRole, programAbrv, panelAbrv, panelEnd, scope, participantId, notificationSent, (UserParticipationHistoryModel)result.ElementAt(0));
            AssertParticipationHistory(fiscalYear2, clientAbrv2, participantType2, participantRole2, programAbrv2, panelAbrv2, panelEnd2, scope2, participantId2, notificationSent2, (UserParticipationHistoryModel)result.ElementAt(1));
            AssertParticipationHistory(fiscalYear3, clientAbrv3, participantType3, participantRole3, programAbrv3, panelAbrv3, panelEnd3, scope3, participantId3, notificationSent3, (UserParticipationHistoryModel)result.ElementAt(2));
        }


        private void AssertParticipationHistory(string p0, string p1, string p2, string p3, string p4, string p5, DateTime? p6, string p7, int p8, DateTime? p9, UserParticipationHistoryModel result)
        {
            Assert.AreEqual(p0, result.FiscalYear, "FiscalYear is not correct");
            Assert.AreEqual(p1, result.ClientAbrv, "ClientAbrv is not correct");
            Assert.AreEqual(p2, result.ParticipantType, "ParticipantType is not correct");
            Assert.AreEqual(p3, result.ParticipantRole, "ParticipantRole is not correct");
            Assert.AreEqual(p4, result.ProgramAbrv, "ProgramAbrv is not correct");
            Assert.AreEqual(p5, result.PanelAbrv, "PanelAbrv is not correct");
            Assert.AreEqual(p6, result.PanelEndDate, "PanelEndDate is not correct");
            Assert.AreEqual(p7, result.Scope, "Scope is not correct");
            Assert.AreEqual(p8, result.ParticipationId, "ParticipationId is not correct");
            Assert.AreEqual(p9, result.NotificationSent, "NotificationSent is not correct");
        }

        #region Helpers
        // keyGen is a generic integer for setting up primary keys
        // This allows us to use the navigation properties to uniquely navigate from one object to the next
        private PanelUserAssignment SetUpPanelUserAssignment(int keyGen, int userId, string fiscalYear, string clientAbrv, string participantType, string participantRole, string programAbrv, string panelAbrv, DateTime? panelEnd, string scope, int participantId, DateTime? notificationSent)
        {
            //
            // set the assignments
            //
            PanelUserAssignment pua = new PanelUserAssignment { UserId = userId, PanelUserAssignmentId = participantId };
            //
            // Set the ClientParticipantType
            //
            ClientParticipantType cpt = new ClientParticipantType { ParticipantTypeAbbreviation = participantType };
            pua.ClientParticipantType = cpt;
            //
            // set the user
            //
            User u = new User { UserID = userId };
            pua.User = u;
            //
            // Now a SessionPanel
            //
            SessionPanel sp = new SessionPanel { SessionPanelId = };
            //
            // Now a ProgramYear
            //
            ProgramYear py = new ProgramYear {};
            //
            // Now a ClientProgram
            //
            ClientProgram cp = new ClientProgram {};
            //
            // Now a Client
            //
            Client cl = new Client {};
            //
            // Now a participant role
            //
            ClientRole cr = new ClientRole {};

            return pua;
        }

        private ProgramUserAssignment SetUpProgramUserAssignment(int keyGen, int userId, string fiscalYear, string clientAbrv, string participantType, string participantRole, string programAbrv, string panelAbrv, DateTime? panelEnd, string scope, int participantId, DateTime? notificationSent)
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
*/