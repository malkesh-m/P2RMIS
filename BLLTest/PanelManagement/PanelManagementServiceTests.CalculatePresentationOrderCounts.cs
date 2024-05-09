using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sra.P2rmis.WebModels.PanelManagement;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace BLLTest.PanelManagement
{
    /// <summary>
    /// Unit tests for PanelManagementService 
    /// </summary>

    public partial class PanelManagementServiceTests
    {
        /// <summary>
        /// Unit tests for PanelManagementService.CalculatePresentationOrderCounts()
        /// </summary>
        #region CalculatePresentationOrderCounts Tests

        /// <summary>
        /// Test basic calculations
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.CalculatePresentationOrderCounts")]
        public void BasicCalculationFirstTests()
        {
            int key = _goodUserId;
            int reviewOrder = 1;
            //
            // Set up local data
            //
            List<IReviewerExpertise> list = new List<IReviewerExpertise>();
            list.Add(new ReviewerExpertise { UserId = key, ReviewOrder = reviewOrder });
            //
            // Test
            //
            var result = this.theTestPanelManagementService.CalculatePresentationOrderCounts(list);
            //
            // Verify
            //
            Assert.IsNotNull(result, "result was null & it should not be");
            Assert.AreEqual(1, result.Keys.Count(), "number of entries not as expected");
            Assert.Contains(_goodUserId, result.Keys, "User id key could not be found");
            //
            // Verify the Calculation
            //
            var re = result[key];
            Assert.IsNotNull(re, "ReviewerExpertise was null & it should not be");
            Assert.AreEqual(1, re.FirstReviewerCount, "First position count was not correct");
            Assert.AreEqual(0, re.SecondReviewerCount, "Second position count was not correct");
            Assert.AreEqual(1, re.AllPositionsCount, "all position count was not correct");
            Assert.AreEqual(_goodUserId, re.UserId, "user id was not expected value");
        }
        /// <summary>
        /// Test basic calculations
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.CalculatePresentationOrderCounts")]
        public void BasicCalculationSecondTests()
        {
            int key = _goodUserId;
            int reviewOrder = 2;
            //
            // Set up local data
            //
            List<IReviewerExpertise> list = new List<IReviewerExpertise>();
            list.Add(new ReviewerExpertise { UserId = key, ReviewOrder = reviewOrder, AssignmentTypeId = 5 });
            //
            // Test
            //
            var result = this.theTestPanelManagementService.CalculatePresentationOrderCounts(list);
            //
            // Verify
            //
            Assert.IsNotNull(result, "result was null & it should not be");
            Assert.AreEqual(1, result.Keys.Count(), "number of entries not as expected");
            Assert.Contains(_goodUserId, result.Keys, "User id key could not be found");
            //
            // Verify the Calculation
            //
            var re = result[key];
            Assert.IsNotNull(re, "ReviewerExpertise was null & it should not be");
            Assert.AreEqual(0, re.FirstReviewerCount, "First position count was not correct");
            Assert.AreEqual(1, re.SecondReviewerCount, "Second position count was not correct");
            Assert.AreEqual(1, re.AllPositionsCount, "all position count was not correct");
            Assert.AreEqual(_goodUserId, re.UserId, "user id was not expected value");
        }
        /// <summary>
        /// Test basic calculations
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.CalculatePresentationOrderCounts")]
        public void BasicCalculationAnyTests()
        {
            int key = _goodUserId;
            int reviewOrder = 10;
            //
            // Set up local data
            //
            List<IReviewerExpertise> list = new List<IReviewerExpertise>();
            list.Add(new ReviewerExpertise { UserId = key, ReviewOrder = reviewOrder, AssignmentTypeId = 5 });
            //
            // Test
            //
            var result = this.theTestPanelManagementService.CalculatePresentationOrderCounts(list);
            //
            // Verify
            //
            Assert.IsNotNull(result, "result was null & it should not be");
            Assert.AreEqual(1, result.Keys.Count(), "number of entries not as expected");
            Assert.Contains(_goodUserId, result.Keys, "User id key could not be found");
            //
            // Verify the Calculation
            //
            var re = result[key];
            Assert.IsNotNull(re, "ReviewerExpertise was null & it should not be");
            Assert.AreEqual(0, re.FirstReviewerCount, "First position count was not correct");
            Assert.AreEqual(1, re.SecondReviewerCount, "Second position count was not correct");
            Assert.AreEqual(1, re.AllPositionsCount, "all position count was not correct");
            Assert.AreEqual(_goodUserId, re.UserId, "user id was not expected value");
        }
        /// <summary>
        /// Test basic calculations
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.CalculatePresentationOrderCounts")]
        public void CalculationTwoUsersTests()
        {
            int key1 = _goodUserId;
            int key2 = _goodUserId + 2;
            int reviewOrder = 1;
            string name11 = "first";
            string name12 = "last";
            string name21 = "the other first name";
            string name22 = "the other last name";
            //
            // Set up local data
            //
            List<IReviewerExpertise> list = new List<IReviewerExpertise>();
            list.Add(new ReviewerExpertise { UserId = key1, ReviewOrder = reviewOrder, ReviewerFirstName = name11, ReviewerLastName = name12 });
            list.Add(new ReviewerExpertise { UserId = key2, ReviewOrder = reviewOrder, ReviewerFirstName = name21, ReviewerLastName = name22 });
            //
            // Test
            //
            var result = this.theTestPanelManagementService.CalculatePresentationOrderCounts(list);
            //
            // Verify
            //
            Assert.IsNotNull(result, "result was null & it should not be");
            Assert.AreEqual(2, result.Keys.Count(), "number of entries not as expected");
            Assert.Contains(_goodUserId, result.Keys, "User id key could not be found");
            //
            // Verify the Calculation
            //
            var re = result[key1];
            Assert.IsNotNull(re, "ReviewerExpertise was null & it should not be");
            Assert.AreEqual(1, re.FirstReviewerCount, "First position count was not correct");
            Assert.AreEqual(0, re.SecondReviewerCount, "Second position count was not correct");
            Assert.AreEqual(1, re.AllPositionsCount, "all position count was not correct");
            Assert.AreEqual(_goodUserId, re.UserId, "user id was not expected value");
            Assert.AreEqual(name11, re.ReviewerFirstName, "First name not as expected");
            Assert.AreEqual(name12, re.ReviewerLastName, "Last name not as expected");
            //
            // Verify the Calculation
            //
            var re2 = result[key2];
            Assert.IsNotNull(re2, "ReviewerExpertise was null & it should not be");
            Assert.AreEqual(1, re2.FirstReviewerCount, "First position count was not correct");
            Assert.AreEqual(0, re2.SecondReviewerCount, "Second position count was not correct");
            Assert.AreEqual(1, re2.AllPositionsCount, "all position count was not correct");
            Assert.AreEqual(key2, re2.UserId, "user id was not expected value");
            Assert.AreEqual(name21, re2.ReviewerFirstName, "First name not as expected");
            Assert.AreEqual(name22, re2.ReviewerLastName, "Last name not as expected");
        }
        /// <summary>
        /// Test basic calculations
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.CalculatePresentationOrderCounts")]
        public void ScientestFlagTrueTest()
        {
            int key = _goodUserId;
            int reviewOrder = 10;
            //
            // Set up local data
            //
            List<IReviewerExpertise> list = new List<IReviewerExpertise>();
            list.Add(new ReviewerExpertise { UserId = key, ReviewOrder = reviewOrder, ScientistFlag = true });
            //
            // Test
            //
            var result = this.theTestPanelManagementService.CalculatePresentationOrderCounts(list);
            //
            // Verify
            //
            var re = result[key];
            Assert.IsTrue(re.ScientistFlag, "ScientistFlag was not correct");
        }
        /// <summary>
        /// Test basic calculations
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.CalculatePresentationOrderCounts")]
        public void ScientestFlagFalseTest()
        {
            int key = _goodUserId;
            int reviewOrder = 10;
            //
            // Set up local data
            //
            List<IReviewerExpertise> list = new List<IReviewerExpertise>();
            list.Add(new ReviewerExpertise { UserId = key, ReviewOrder = reviewOrder, ScientistFlag = false });
            //
            // Test
            //
            var result = this.theTestPanelManagementService.CalculatePresentationOrderCounts(list);
            //
            // Verify
            //
            var re = result[key];
            Assert.IsFalse(re.ScientistFlag, "ScientistFlag was not correct");
        }
        /// <summary>
        /// Test basic calculations
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.CalculatePresentationOrderCounts")]
        public void SpecialestFlagTrueTest()
        {
            int key = _goodUserId;
            int reviewOrder = 10;
            //
            // Set up local data
            //
            List<IReviewerExpertise> list = new List<IReviewerExpertise>();
            list.Add(new ReviewerExpertise { UserId = key, ReviewOrder = reviewOrder, SpecialistFlag = true });
            //
            // Test
            //
            var result = this.theTestPanelManagementService.CalculatePresentationOrderCounts(list);
            //
            // Verify
            //
            var re = result[key];
            Assert.IsTrue(re.SpecialistFlag, "SpecialistFlag was not correct");
        }
        /// <summary>
        /// Test basic calculations
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.CalculatePresentationOrderCounts")]
        public void SpecialestFlagFalseTest()
        {
            int key = _goodUserId;
            int reviewOrder = 10;
            //
            // Set up local data
            //
            List<IReviewerExpertise> list = new List<IReviewerExpertise>();
            list.Add(new ReviewerExpertise { UserId = key, ReviewOrder = reviewOrder, SpecialistFlag = false });
            //
            // Test
            //
            var result = this.theTestPanelManagementService.CalculatePresentationOrderCounts(list);
            //
            // Verify
            //
            var re = result[key];
            Assert.IsFalse(re.SpecialistFlag, "SpecialistFlag was not correct");
        }
        /// <summary>
        /// Test basic calculations
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.CalculatePresentationOrderCounts")]
        public void ConsumerFlagTrueTest()
        {
            int key = _goodUserId;
            int reviewOrder = 10;
            //
            // Set up local data
            //
            List<IReviewerExpertise> list = new List<IReviewerExpertise>();
            list.Add(new ReviewerExpertise { UserId = key, ReviewOrder = reviewOrder, ConsumerFlag = true });
            //
            // Test
            //
            var result = this.theTestPanelManagementService.CalculatePresentationOrderCounts(list);
            //
            // Verify
            //
            var re = result[key];
            Assert.IsTrue(re.ConsumerFlag, "ConsumerFlag was not correct");
        }
        /// <summary>
        /// Test basic calculations
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.CalculatePresentationOrderCounts")]
        public void ConsumerFlagFalseTest()
        {
            int key = _goodUserId;
            int reviewOrder = 10;
            //
            // Set up local data
            //
            List<IReviewerExpertise> list = new List<IReviewerExpertise>();
            list.Add(new ReviewerExpertise { UserId = key, ReviewOrder = reviewOrder, ConsumerFlag = false });
            //
            // Test
            //
            var result = this.theTestPanelManagementService.CalculatePresentationOrderCounts(list);
            //
            // Verify
            //
            var re = result[key];
            Assert.IsFalse(re.ConsumerFlag, "ConsumerFlag was not correct");
        }
        /// <summary>
        /// Test basic calculations
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.CalculatePresentationOrderCounts")]
        public void MultipleTest()
        {
            int key1 = _goodUserId;
            int key2 = _goodUserId + 1;
            int key3 = _goodUserId + 2;
            //
            // Set up local data
            //
            List<IReviewerExpertise> list = CreateMultipleTestData(key1, key2, key3);
            //
            // Test
            //
            var result = this.theTestPanelManagementService.CalculatePresentationOrderCounts(list);
            //
            // Verify
            //
            Assert.IsNotNull(result, "result was null & it should not be");
            Assert.AreEqual(3, result.Keys.Count(), "number of entries not as expected");
            Assert.Contains(key1, result.Keys, "User id key could not be found");
            Assert.Contains(key2, result.Keys, "User id key could not be found");
            Assert.Contains(key3, result.Keys, "User id key could not be found");

            Assert.AreEqual(3, result.Keys.Count(), "Number of entries not correct for keys");

            var re = result[key1];
            Assert.AreEqual(4, re.AllPositionsCount, "All position count not equal");
            Assert.AreEqual(1, re.FirstReviewerCount, "first position count not equal");
            Assert.AreEqual(3, re.SecondReviewerCount, "second position count not equal");
            Assert.IsFalse(re.SpecialistFlag, "SpecialistFlag not expected value");
            Assert.IsFalse(re.ConsumerFlag, "ConsumerFlag not expected value");
            Assert.IsTrue(re.ScientistFlag, "ScientistFlag not expected value");

            re = result[key2];
            Assert.AreEqual(3, re.AllPositionsCount, "All position count not equal");
            Assert.AreEqual(1, re.FirstReviewerCount, "first position count not equal");
            Assert.AreEqual(2, re.SecondReviewerCount, "second position count not equal");
            Assert.IsFalse(re.SpecialistFlag, "SpecialistFlag not expected value");
            Assert.IsFalse(re.ConsumerFlag, "ConsumerFlag not expected value");
            Assert.IsTrue(re.ScientistFlag, "ScientistFlag not expected value");

            re = result[key3];
            Assert.AreEqual(3, re.AllPositionsCount, "All position count not equal");
            Assert.AreEqual(0, re.FirstReviewerCount, "first position count not equal");
            Assert.AreEqual(3, re.SecondReviewerCount, "second position count not equal");
            Assert.IsFalse(re.SpecialistFlag, "SpecialistFlag not expected value");
            Assert.IsTrue(re.ConsumerFlag, "ConsumerFlag not expected value");
            Assert.IsFalse(re.ScientistFlag, "ScientistFlag not expected value");

        }
        /// <summary>
        /// create the local data for a test
        /// </summary>
        private List<IReviewerExpertise> CreateMultipleTestData(int key1, int key2, int key3)
        {
            //
            // Set up local data
            //
            List<IReviewerExpertise> list = new List<IReviewerExpertise>();
            list.Add(new ReviewerExpertise { UserId = key1, ReviewOrder = 2, ScientistFlag = true, AssignmentTypeId = 5 });
            list.Add(new ReviewerExpertise { UserId = key3, ReviewOrder = null, ConsumerFlag = true, AssignmentTypeId = 6 });
            list.Add(new ReviewerExpertise { UserId = key1, ReviewOrder = 4, ScientistFlag = true, AssignmentTypeId = 5 });
            list.Add(new ReviewerExpertise { UserId = key2, ReviewOrder = 1, ScientistFlag = true, AssignmentTypeId = 5 });

            list.Add(new ReviewerExpertise { UserId = key2, ReviewOrder = 2, ScientistFlag = true, AssignmentTypeId = 5 });
            list.Add(new ReviewerExpertise { UserId = key2, ReviewOrder = 2, ScientistFlag = true, AssignmentTypeId = 5 });
            list.Add(new ReviewerExpertise { UserId = key2, ReviewOrder = null, ScientistFlag = true, AssignmentTypeId = 5 });
            list.Add(new ReviewerExpertise { UserId = key1, ReviewOrder = null, ScientistFlag = true, AssignmentTypeId = 5 });
            list.Add(new ReviewerExpertise { UserId = key3, ReviewOrder = 10, ConsumerFlag = true, AssignmentTypeId = 6 });

            list.Add(new ReviewerExpertise { UserId = key3, ReviewOrder = 6, ConsumerFlag = true, AssignmentTypeId = 6 });
            list.Add(new ReviewerExpertise { UserId = key1, ReviewOrder = 7, ScientistFlag = true, AssignmentTypeId = 5 });
            list.Add(new ReviewerExpertise { UserId = key3, ReviewOrder = 5, ConsumerFlag = true, AssignmentTypeId = 6 });
            list.Add(new ReviewerExpertise { UserId = key1, ReviewOrder = 1, ScientistFlag = true, AssignmentTypeId = 5 });
            list.Add(new ReviewerExpertise { UserId = key3, ReviewOrder = null, ConsumerFlag = true, AssignmentTypeId = 6 });
            list.Add(new ReviewerExpertise { UserId = key2, ReviewOrder = null, ScientistFlag = true, AssignmentTypeId = 5 });

            return list;
        }
        #endregion
    }
}
