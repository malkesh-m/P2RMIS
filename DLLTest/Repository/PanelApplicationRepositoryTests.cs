using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
		
namespace DLLTest.Repository
{
    /// <summary>
    /// Unit tests for PanelApplicationRepository services
    /// </summary>
    [TestClass()]
    public class PanelApplicationRepositoryTests: DllBaseTest
    {
        #region Constants 
        private int panelApplicationId = 444;

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
        #region GetPanelApplicationReviewerAssignment Tests
        /// <summary>
        /// Test finding a specific sort order
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationRepository.GetPanelApplicationReviewerAssignment")]
        public void BasicTest()
        {
            //
            // Set up local data
            //
            int findThisSortOrder = 30;
            PanelApplication aPanelApplication = BuildPanelApplication(panelApplicationId, 5);
            //
            // Set expectations
            //
            Expect.Call(thePanelApplicationRepositoryMock.GetByID(panelApplicationId)).Return(aPanelApplication);
            thePanelApplicationRepositoryMock.Replay();
            //
            // Test
            //
            var result = thePanelApplicationRepositoryMock.GetPanelApplicationReviewerAssignment(findThisSortOrder, panelApplicationId);
            //
            // Verify
            //
            Assert.IsNotNull(result, "result was null and it should not have been");
            Assert.AreEqual(findThisSortOrder, result.SortOrder, "Sort order not as expected");
        }
         //<summary>
         //Test finding a specific sort order that is not there
         //</summary>
        [TestMethod()]
        [Category("PanelApplicationRepository.GetPanelApplicationReviewerAssignment")]
        public void FindSomethingThatIsNotThereTest()
        {
            //
            // Set up local data
            //
            int findThisSortOrder = 300;
            PanelApplication aPanelApplication = BuildPanelApplication(panelApplicationId, 5);
            //
            // Set expectations
            //
            Expect.Call(thePanelApplicationRepositoryMock.GetByID(panelApplicationId)).Return(aPanelApplication);
            thePanelApplicationRepositoryMock.Replay();
            //
            // Test
            //
            var result = thePanelApplicationRepositoryMock.GetPanelApplicationReviewerAssignment(findThisSortOrder, panelApplicationId);
            //
            // Verify
            //
            Assert.IsNull(result, "result was not null and it should have been");
        }
        /// <summary>
        /// Test finding a specific sort order that is not there
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationRepository.GetPanelApplicationReviewerAssignment")]
        public void FindSomethingZeroTest()
        {
            //
            // Set up local data
            //
            int findThisSortOrder = 300;
            PanelApplication aPanelApplication = new PanelApplication { PanelApplicationId = panelApplicationId };
            //
            // Set expectations
            //
            Expect.Call(thePanelApplicationRepositoryMock.GetByID(panelApplicationId)).Return(aPanelApplication);
            thePanelApplicationRepositoryMock.Replay();
            //
            // Test
            //
            var result = thePanelApplicationRepositoryMock.GetPanelApplicationReviewerAssignment(findThisSortOrder, panelApplicationId);
            //
            // Verify
            //
            Assert.IsNull(result, "result was not null and it should have been");
        }
        #endregion
        #region GetPanelApplicationReviewerAssignmentForSpecificReviewer Tests
        /// <summary>
        /// Test finding a specific sort order
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationRepository.GetPanelApplicationReviewerAssignmentForSpecificReviewer")]
        public void BasicSpecificReviewerTest()
        {
            //
            // Set up local data
            //
            int findThisPanelUserAssignmentId = 222;

            PanelApplication aPanelApplication = BuildPanelApplication(panelApplicationId, 5);
            //
            // Set expectations
            //
            Expect.Call(thePanelApplicationRepositoryMock.GetByID(panelApplicationId)).Return(aPanelApplication);
            thePanelApplicationRepositoryMock.Replay();
            //
            // Test
            //
            var result = thePanelApplicationRepositoryMock.GetPanelApplicationReviewerAssignmentForSpecificReviewer(panelApplicationId, findThisPanelUserAssignmentId);
            //
            // Verify
            //
            Assert.IsNotNull(result, "result was null and it should not have been");
            Assert.AreEqual(findThisPanelUserAssignmentId, result.PanelUserAssignmentId, "panelUserAssignmentId not as expected");
        }
        /// <summary>
        /// Test finding a specific sort order
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationRepository.GetPanelApplicationReviewerAssignmentForSpecificReviewer")]
        public void BasicSpecificReviewerThatIsNotThereTest()
        {
            //
            // Set up local data
            //
            int findThisPanelUserAssignmentId = 505;

            PanelApplication aPanelApplication = BuildPanelApplication(panelApplicationId, 5);
            //
            // Set expectations
            //
            Expect.Call(thePanelApplicationRepositoryMock.GetByID(panelApplicationId)).Return(aPanelApplication);
            thePanelApplicationRepositoryMock.Replay();
            //
            // Test
            //
            var result = thePanelApplicationRepositoryMock.GetPanelApplicationReviewerAssignmentForSpecificReviewer(panelApplicationId, findThisPanelUserAssignmentId);
            //
            // Verify
            //
            Assert.IsNull(result, "result was not null and it should have been");
        }
        /// <summary>
        /// Test finding a specific sort order
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationRepository.GetPanelApplicationReviewerAssignmentForSpecificReviewer")]
        public void BasicSpecificReviewerZeroTest()
        {
            //
            // Set up local data
            //
            int findThisPanelUserAssignmentId = 505;

            PanelApplication aPanelApplication = new PanelApplication { PanelApplicationId = panelApplicationId };
            //
            // Set expectations
            //
            Expect.Call(thePanelApplicationRepositoryMock.GetByID(panelApplicationId)).Return(aPanelApplication);
            thePanelApplicationRepositoryMock.Replay();
            //
            // Test
            //
            var result = thePanelApplicationRepositoryMock.GetPanelApplicationReviewerAssignmentForSpecificReviewer(panelApplicationId, findThisPanelUserAssignmentId);
            //
            // Verify
            //
            Assert.IsNull(result, "result was not null and it should have been");
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Helper builds a PanelApplication for testing
        /// </summary>
        /// <param name="howMany"></param>
        /// <returns></returns>
        private PanelApplication BuildPanelApplication(int panelApplicationId, int howMany)
        {
            //
            // Set up local data
            //
            PanelApplication aPanelApplication = new PanelApplication { PanelApplicationId = panelApplicationId };
            List<PanelApplicationReviewerAssignment> aList = BuildEntityCollection<PanelApplicationReviewerAssignment>(howMany);

            int i = 0;
            int[] sortOrder = { 10, 20, 30, 40, 50 };
            int[] panelUserAssignmentId = {111, 444, 333, 222, 555 };
            aList.ForEach(x => x.SortOrder = sortOrder[i++]);
            i = 0;
            aList.ForEach(x => x.PanelUserAssignmentId = panelUserAssignmentId[i++]);
            aPanelApplication.PanelApplicationReviewerAssignments = aList;

            return aPanelApplication;
        }
        #endregion
    }
		
}
