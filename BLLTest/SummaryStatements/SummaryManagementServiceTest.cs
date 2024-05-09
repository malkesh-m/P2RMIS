using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;


namespace BLLTest.SummaryStatements
{
    /// <summary>
    /// Unit test for SummaryManagementServer class.
    /// </summary>
    [TestClass()]
    public partial class SummaryManagementServiceTest : BLLBaseTest
    {
        /* 
         * This portion of the SummaryManagementServiceTest partial class contains items that
         * are common to all the SummaryManagementServiceTest partial classes.
         * 
         * Potentially common items should be added to this file when identified
         * 
         * */

        #region Overhead -  Common to all class SummaryManagementServiceTest  tests
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
        #endregion

        #region Additional test attributes common to all class SummaryManagementServiceTest tests
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
            theMock = new MockRepository();
            theWorkMock = theMock.DynamicMock<IUnitOfWork>();
            theSummaryManagementRepositoryMock = theMock.DynamicMock<ISummaryManagementRepository>();
            testService = theMock.PartialMock<SummaryManagementServiceTestService>(theWorkMock);
            theWorkflowMechanismRepositoryMock = theMock.DynamicMock<IWorkflowMechanismRepository>();
       }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            theWorkflowMechanismRepositoryMock = null;
            testService = null;
            theSummaryManagementRepositoryMock = null;
            theWorkMock = null;
            theMock = null;
        }
        #endregion


    }
}
