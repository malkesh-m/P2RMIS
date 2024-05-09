using System;
using NUnit.Framework;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace DLLTest.Partials
{
    /// <summary>
    /// Unit tests for ApplicationWorkflowStepElementContent extensions
    /// </summary>
    [TestClass()]
    public class ApplicationWorkflowStepElementContentTest
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
        #region Promote Tests
        //TODO:: unit tests for Promote method for this class
        #endregion
        #region SaveModifiedContent Tests
        /// <summary>
        /// Test SaveModifiedContent updates the expected values and does not change
        /// any others.
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepElementContent.SaveModifiedContent")]
        public void SaveModifiedContentTests()
        {
            //
            // Set up local data
            //
            int userId = 22;
            string contentText = "this is the new content";
            DateTime beforeTime = DateTime.Now;
            ApplicationWorkflowStepElementContent entity = new ApplicationWorkflowStepElementContent();
            //
            // Run the test
            //
            entity.SaveModifiedContent(contentText, userId);
            //
            // Now test
            //
            Assert.AreEqual(contentText, entity.ContentText, "Context text was not as it should be");
            Assert.AreEqual(userId, entity.ModifiedBy, "Modified by was not as it should be");
            Assert.That(entity.ModifiedDate, Is.InRange(beforeTime, DateTime.Now), "Modified Date was not as it should be");

            Assert.AreEqual(false, entity.Abstain, "Abstain was not as it should be");
            Assert.IsNull(entity.ApplicationWorkflowStepElement, "ApplicationWorkflowStepElement was not as it should be");
            Assert.AreEqual(0, entity.ApplicationWorkflowStepElementContentId, "ApplicationWorkflowStepElementContentId was not as it should be");
            Assert.AreEqual(0, entity.ApplicationWorkflowStepElementId, "ApplicationWorkflowStepElementId was not as it should be");
            //Assert.AreEqual(0, entity.CreatedBy, "CreatedBy was not as it should be");
            //Assert.AreEqual(_noDate, entity.CreatedDate, "CreatedDate was not as it should be");
            Assert.IsNull(entity.Score, "Score was not as it should be");
        }
        #endregion
        #region SaveModifiedContent Tests
        /// <summary>
        /// Test getting the template id
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepElementContent.GetTemplateId")]
        public void GetTemplateIdTest()
        {
            //
            // Set up local data
            //
            int theIdValue = 3;
            ApplicationWorkflowStepElementContent entity = new ApplicationWorkflowStepElementContent();
            entity.ApplicationWorkflowStepElement = new ApplicationWorkflowStepElement { ApplicationTemplateElementId = theIdValue };
            //
            // Run the test
            //
            int result = entity.GetTemplateId();
            //
            // Now test
            //
            Assert.AreEqual(theIdValue, result, "GetTemplateId did not return expected value");
        }
        #endregion
    }
}
