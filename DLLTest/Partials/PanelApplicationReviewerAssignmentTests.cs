using NUnit.Framework;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
		
namespace DLLTest.Partials
{
    /// <summary>
    /// Unit tests for PanelApplicationReviewerAssignment extensions.
    /// </summary>
    [TestClass()]
    public class PanelApplicationReviewerAssignmentTests : DllBaseTest
    {
        private int sortOrder1 = 1;
        private int sortOrder2 = 2;
        private int panelUserAssignmentId1 = 432;
        private int panelUserAssignmentId2 = 5678;
        private int clientId1 = 890;
        private int clientId2 = 654;

        PanelApplicationReviewerAssignment entity;

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
            entity = new PanelApplicationReviewerAssignment();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            entity = null;
            CleanUpMocks();
        }
        #endregion
        #endregion
        #region IsPresentationOrderSetForOtherUser Tests
        /// <summary>
        /// Test Same user & same presentation order
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationReviewerAssignment")]
        public void SameOrderSameUserTest()
        {
            //
            // Set up local data
            //
            PanelApplicationReviewerAssignment entity = new PanelApplicationReviewerAssignment { SortOrder = sortOrder1, PanelUserAssignmentId = panelUserAssignmentId1 };
            //
            // Test
            //
            bool result = entity.IsPresentationOrderSetForOtherUser(sortOrder1, panelUserAssignmentId1);
            //
            // Verify
            //
            Assert.IsFalse(result, "Presentation Order test was not as expected");
        }
        /// <summary>
        /// Test Different user & same presentation order
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationReviewerAssignment")]
        public void SameOrderDifferentUserTest()
        {
            //
            // Set up local data
            //
            PanelApplicationReviewerAssignment entity = new PanelApplicationReviewerAssignment { SortOrder = sortOrder1, PanelUserAssignmentId = panelUserAssignmentId1 };
            //
            // Test
            //
            bool result = entity.IsPresentationOrderSetForOtherUser(sortOrder1, panelUserAssignmentId2);
            //
            // Verify
            //
            Assert.IsTrue(result, "Presentation Order test was not as expected");
        }
        /// <summary>
        /// Test Same user & Different presentation order
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationReviewerAssignment")]
        public void DifferentOrderSameUserTest()
        {
            //
            // Set up local data
            //
            PanelApplicationReviewerAssignment entity = new PanelApplicationReviewerAssignment { SortOrder = sortOrder1, PanelUserAssignmentId = panelUserAssignmentId1 };
            //
            // Test
            //
            bool result = entity.IsPresentationOrderSetForOtherUser(sortOrder2, panelUserAssignmentId1);
            //
            // Verify
            //
            Assert.IsFalse(result, "Presentation Order test was not as expected");
        }
        /// <summary>
        /// Test Different user & Different presentation order
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationReviewerAssignment")]
        public void DifferentOrderDifferentUserTest()
        {
            //
            // Set up local data
            //
            PanelApplicationReviewerAssignment entity = new PanelApplicationReviewerAssignment { SortOrder = sortOrder1, PanelUserAssignmentId = panelUserAssignmentId1 };
            //
            // Test
            //
            bool result = entity.IsPresentationOrderSetForOtherUser(sortOrder2, panelUserAssignmentId2);
            //
            // Verify
            //
            Assert.IsFalse(result, "Presentation Order test was not as expected");
        }
        #endregion
        #region IsSame Tests
        /// <summary>
        /// Test  same presentation order & same client assignment id
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationReviewerAssignment")]
        public void SameOrderSameClientTypeTest()
        {
            //
            // Set up local data
            //
            PanelApplicationReviewerAssignment entity = new PanelApplicationReviewerAssignment { SortOrder = sortOrder1, ClientAssignmentTypeId = clientId1 };
            //
            // Test
            //
            bool result = entity.IsSame(sortOrder1, this.clientId1);
            //
            // Verify
            //
            Assert.IsTrue(result, "Should have resulted in no change detected");
        }
        /// <summary>
        /// Test  same presentation order & different client assignment id
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationReviewerAssignment")]
        public void SameOrderDifferentClientTypeTest()
        {
            //
            // Set up local data
            //
            PanelApplicationReviewerAssignment entity = new PanelApplicationReviewerAssignment { SortOrder = sortOrder1, ClientAssignmentTypeId = clientId1 };
            //
            // Test
            //
            bool result = entity.IsSame(sortOrder1, this.clientId2);
            //
            // Verify
            //
            Assert.IsFalse(result, "Should have resulted in  change detected");
        }
        /// <summary>
        /// Test  different presentation order & same client assignment id
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationReviewerAssignment")]
        public void DifferenteOrderSameClientTypeTest()
        {
            //
            // Set up local data
            //
            PanelApplicationReviewerAssignment entity = new PanelApplicationReviewerAssignment { SortOrder = sortOrder1, ClientAssignmentTypeId = clientId1 };
            //
            // Test
            //
            bool result = entity.IsSame(sortOrder2, this.clientId1);
            //
            // Verify
            //
            Assert.IsFalse(result, "Should have resulted in  change detected");
        }
        /// <summary>
        /// Test  different presentation order & different client assignment id
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationReviewerAssignment")]
        public void DifferenteOrderDifferentClientTypeTest()
        {
            //
            // Set up local data
            //
            PanelApplicationReviewerAssignment entity = new PanelApplicationReviewerAssignment { SortOrder = sortOrder1, ClientAssignmentTypeId = clientId1 };
            //
            // Test
            //
            bool result = entity.IsSame(sortOrder2, this.clientId2);
            //
            // Verify
            //
            Assert.IsFalse(result, "Should have resulted in change detected");
        }
        #endregion
        #region Modify test
        /// <summary>
        /// Test Modify - expected fields modified
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationReviewerAssignment")]
        public void ModifyTest()
        {
            //
            // Set up local data
            //
            PanelApplicationReviewerAssignment entity = new PanelApplicationReviewerAssignment();
            Assert.IsNull(entity.SortOrder, "sort order did not initialize as expected");
            Assert.AreEqual(0, entity.ClientAssignmentTypeId, "clientTypeId did not initialize as expected");
            Assert.IsNull(entity.ModifiedBy, "ModifiedBy was not initialized as expected");
            Assert.IsNull(entity.ModifiedDate, "ModifiedDate was not initialized as expected");
            Assert.IsNull(entity.CreatedBy, "CreatedBy was  set");
            Assert.IsNull(entity.CreatedDate, "CreatedDate was  set");
            Assert.IsNull(entity.LegacyProposalAssignmentId, "LegacyProposalAssignmentId was  set");
            Assert.IsNull(entity.DeletedBy, "DeletedBy was  set");
            Assert.IsNull(entity.DeletedDate, "DeletedDate was set");
            //
            // Test
            //
            entity.Modify(sortOrder2, clientId2, _goodUserId);
            //
            // Verify
            //
            Assert.AreEqual(sortOrder2, entity.SortOrder, "Sort order was not as expected");
            Assert.AreEqual(clientId2, entity.ClientAssignmentTypeId, "client assignment type was not as expected");
            VerifyModifiedDate(entity, _goodUserId);
            //
            //
            Assert.AreEqual(0, entity.PanelApplicationReviewerAssignmentId, "PanelApplicationReviewerAssignmentId should not have changed");
            Assert.AreEqual(0, entity.PanelApplicationId, "PanelApplicationId should not have changed");
            Assert.AreEqual(0, entity.PanelUserAssignmentId, "PanelUserAssignmentId should not have changed");
            VerifyCreatedDateUnChanged(entity);
            Assert.IsNull(entity.LegacyProposalAssignmentId, "LegacyProposalAssignmentId was set");
            Assert.IsNull(entity.DeletedBy, "DeletedBy was set");
            Assert.IsNull(entity.DeletedDate, "DeletedDate was set");
        }
        #endregion
        #region Populate tests
        /// <summary>
        /// Test expected fields updated
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationReviewerAssignment")]
        public void PopulateTest()
        {
            int panelApplicationId = 432908;
            int panelUserAssignmentId = 65765;
            //
            // Set up local data
            //
            PanelApplicationReviewerAssignment entity = new PanelApplicationReviewerAssignment();
            Assert.IsNull(entity.SortOrder, "sort order did not initialize as expected");
            Assert.AreEqual(0, entity.ClientAssignmentTypeId, "clientTypeId did not initialize as expected");
            Assert.IsNull(entity.ModifiedBy, "ModifiedBy was not initialized as expected");
            Assert.IsNull(entity.ModifiedDate, "ModifiedDate was not initialized as expected");
            Assert.IsNull(entity.CreatedBy, "CreatedBy was  set");
            Assert.IsNull(entity.CreatedDate, "CreatedDate was  set");
            Assert.IsNull(entity.LegacyProposalAssignmentId, "LegacyProposalAssignmentId was  set");
            Assert.IsNull(entity.DeletedBy, "DeletedBy was  set");
            Assert.IsNull(entity.DeletedDate, "DeletedDate was set");
            //
            // Test
            //
            entity.Populate(sortOrder2, clientId2, panelApplicationId, panelUserAssignmentId, _goodUserId);
            //
            // Verify
            //
            Assert.AreEqual(sortOrder2, entity.SortOrder, "Sort order was not as expected");
            Assert.AreEqual(clientId2, entity.ClientAssignmentTypeId, "client assignment type was not as expected");
            VerifyModifiedDate(entity, _goodUserId);
            Assert.AreEqual(panelApplicationId, entity.PanelApplicationId, "PanelApplicationId was not as expected");
            Assert.AreEqual(panelUserAssignmentId, entity.PanelUserAssignmentId, "PanelUserAssignmentId was not as expected");
            Assert.AreEqual(_goodUserId, entity.CreatedBy, "CreatedBy was not set");
            Assert.IsNotNull(entity.CreatedDate, "CreatedDate was not set");
            //
            //
            Assert.AreEqual(0, entity.PanelApplicationReviewerAssignmentId, "PanelApplicationReviewerAssignmentId should not have changed");
            Assert.IsNull(entity.LegacyProposalAssignmentId, "LegacyProposalAssignmentId was set");
            Assert.IsNull(entity.DeletedBy, "DeletedBy was set");
            Assert.IsNull(entity.DeletedDate, "DeletedDate was set");
        }
        #endregion
        #region Default Tests
        /// <summary>
        /// Test  entity's default set up
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationReviewerAssignment")]
        public void DefaultTest()
        {
            //
            // Verify
            //
            Assert.NotNull(AssignmentType.Default, "PanelApplicationReviewerAssignment Default not initialized");
        }
        #endregion
        #region NullableSortOrder Tests
        /// <summary>
        /// Test SortOrder with null SortOrder
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationReviewerAssignment")]
        public void NullableSortOrderdNullSortOrderTest()
        {
            //
            // Verify
            //
            Assert.IsNull(entity.NullableSortOrder, "Null value not returned for NullableSortOrder");
        }
        /// <summary>
        /// Test NullableSortOrder with AssignmentTypeId
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationReviewerAssignment")]
        public void NullableSortOrderdSortOrderTest()
        {
            //
            // Set up local data
            //
            entity.SortOrder = 6;
            //
            // Verify
            //
            Assert.IsNotNull(entity.NullableSortOrder, "Null value returned for NullableSortOrder");
        }
        #endregion
    }
		
}
