using System;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.HelperClasses;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
		
namespace BLLTest.Mail
{
    /// <summary>
    /// Unit tests for Mail Service
    /// </summary>
    [TestClass()]
    public class MailServiceTest: BLLBaseTest
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
        /// Test successful path
        /// </summary>
        [TestMethod()]
        [Category("MailService.ListPanelUserEmailAddresses")]
        public void GoodTest()
        {
            int sessionPanelId = 44;
            //
            // Set up local data
            //
            ResultModel<IEmailAddress> resultModel = BuildResult<IEmailAddress, EmailAddress>(6);
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.SessionPanelRepository).Return(theSessionPanelRepositoryMock);
            Expect.Call(theSessionPanelRepositoryMock.ListPanelUserEmailAddresses(sessionPanelId)).Return(resultModel);

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestMailService.ListPanelUserEmailAddresses(sessionPanelId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test 0 session panel id
        /// </summary>
        [TestMethod()]
        [Category("MailService.ListPanelUserEmailAddresses")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "MailService.ListPanelUserEmailAddresses detected an invalid parameter: sessionPanelId was 0")]
        public void ZeroSessionPanelIdTest()
        {
            this.ListPanelUserEmailAddressesFailTest(0);
            //
            // Should not be here so...
            //
            Assert.Fail("Should have thrown an exception for an invalid parameter");
        }
        /// <summary>
        /// Test negative session panel id
        /// </summary>
        [TestMethod()]
        [Category("MailService.ListPanelUserEmailAddresses")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "MailService.ListPanelUserEmailAddresses detected an invalid parameter: sessionPanelId was -56789")]
        public void NegativeSessionPanelIdTest()
        {
            this.ListPanelUserEmailAddressesFailTest(-56789);
            //
            // Should not be here so...
            //
            Assert.Fail("Should have thrown an exception for an invalid parameter");
        }		
        #region Helpers
        /// <summary>
        /// Test steps for a failure test for ListPanelUserEmailAddresses
        /// </summary>
        /// <param name="sessionPanelId">SessionUPanelId to test</param>
        private void ListPanelUserEmailAddressesFailTest(int sessionPanelId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = theTestMailService.ListPanelUserEmailAddresses(sessionPanelId);
        }        
        #endregion
        #endregion
    }
		
}
