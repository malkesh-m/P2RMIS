using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.SummaryStatement;
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
    public class ClientSummaryService: BLLBaseTest
    {
        private const int Program1 = 425;
        private const int ProgramBlank = 0;
        private const int FY1 = 242;
        private const int FYBlank = 0;
        private const int PanelId1 = 2;
        private const int PanelIdNoResults = 5;
        private const int NegativePanelId = -1;
        private const int ZeroPanelId = 0;

        private readonly ICollection<int> collectionOfSingleEntry = new List<int>(new int[] {14});
        private readonly ICollection<int> collectionOfMultipleEntries = new List<int>(new int[] { 14, 32, 13, 1, 4 });
        private readonly ICollection<int> collectionWithZeroEntry = new List<int>(new int[] { 14444, 0, 183744023 });
        private readonly ICollection<int> collectionWithNegativeEntry = new List<int>(new int[] { 14444, 44450, -183744023 });
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
            //
            // Create the mocks
            //
            theMock = new MockRepository();
            theWorkMock = theMock.DynamicMock<IUnitOfWork>();
            theSummaryManagementRepositoryMock = theMock.DynamicMock<ISummaryManagementRepository>();
            theTestClientSummaryService = theMock.PartialMock<ClientSummaryServiceTestService>(theWorkMock);
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            theTestClientSummaryService = null;
            theSummaryManagementRepositoryMock = null;
            theWorkMock = null;
            theMock = null;
        }
        //
        #endregion
        #endregion
    }
}
