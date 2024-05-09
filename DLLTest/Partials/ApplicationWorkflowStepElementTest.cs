using System;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;

namespace DLLTest.Partials
{
    /// <summary>
    /// Unit tests for ApplicationWorkflowStepElement extensions
    /// </summary>
    [TestClass()]
    public class ApplicationWorkflowStepElementTest
    {
        #region Attributes
        private const int _userId = 11;
        private const int _stepId = 333;
        private const int _badUserId = -11;
        private const int _badStepId = -333;
        private const int _worklogId = 6785;
        private const bool _shouldNotRemoveMarkup = false;
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
        /// <summary>
        /// Test promoting one comment
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepElement.Promote")]
        public void Promote_OneTest()
        {
            //
            // create the needed data
            //
            ApplicationWorkflowStepElement stepElement = new ApplicationWorkflowStepElement();
            ApplicationWorkflowStepElementContent contentToReturn = new ApplicationWorkflowStepElementContent();
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            ApplicationWorkflowStepElementContent mockContent = mocks.PartialMock<ApplicationWorkflowStepElementContent>(); 
            //
            // configure the data
            //
            stepElement.ApplicationWorkflowStepElementContents.Add(mockContent);
            //
            // Set up the expectations
            //
            Expect.Call(mockContent.Promote(_stepId, _userId, _shouldNotRemoveMarkup)).Return(contentToReturn);
            mocks.ReplayAll();
            //
            // Do the test
            //
            var x = stepElement.Promote(_stepId, _userId, _shouldNotRemoveMarkup);
            //
            // Verify that the test worked
            //
            Assert.IsNotNull(x, "Promote did not find a match and should have");
            Assert.AreEqual(stepElement.ApplicationWorkflowStepElementContents.Count(), x.Count(), "Count did not return a prompted content");
            mocks.VerifyAll();
        }
        /// <summary>
        /// Test promoting one comment
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepElement.Promote")]
        public void Promote_MultipleTest()
        {
            //
            // create the needed data
            //
            ApplicationWorkflowStepElement stepElement = new ApplicationWorkflowStepElement();
            ApplicationWorkflowStepElementContent contentToReturn = new ApplicationWorkflowStepElementContent();
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            ApplicationWorkflowStepElementContent mockContent1 = mocks.PartialMock<ApplicationWorkflowStepElementContent>();
            ApplicationWorkflowStepElementContent mockContent2 = mocks.PartialMock<ApplicationWorkflowStepElementContent>();
            ApplicationWorkflowStepElementContent mockContent3 = mocks.PartialMock<ApplicationWorkflowStepElementContent>();
            //
            // configure the data
            //
            stepElement.ApplicationWorkflowStepElementContents.Add(mockContent1);
            stepElement.ApplicationWorkflowStepElementContents.Add(mockContent2);
            stepElement.ApplicationWorkflowStepElementContents.Add(mockContent3);
            //
            // Set up the expectations
            //
            Expect.Call(mockContent1.Promote(_stepId, _userId, _shouldNotRemoveMarkup)).Return(contentToReturn);
            Expect.Call(mockContent2.Promote(_stepId, _userId, _shouldNotRemoveMarkup)).Return(contentToReturn);
            Expect.Call(mockContent3.Promote(_stepId, _userId, _shouldNotRemoveMarkup)).Return(contentToReturn);
            mocks.ReplayAll();
            //
            // Do the test
            //
            var x = stepElement.Promote(_stepId, _userId, _shouldNotRemoveMarkup);
            //
            // Verify that the test worked
            //
            Assert.IsNotNull(x, "Promote did not find a match and should have");
            Assert.AreEqual(stepElement.ApplicationWorkflowStepElementContents.Count(), x.Count(), "Count did not return a prompted content");
            mocks.VerifyAll();
        }
        /// <summary>
        /// Test bad step id
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepElement.Promote")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "ApplicationWorkflowStepElementContent.Promote received an invalid parameter nextWorkflowStepElementId is [-333] and userId is [11]")]
        public void Promote_BadParam1Test()
        {
            //
            // create the needed data
            //
            ApplicationWorkflowStepElement stepElement = new ApplicationWorkflowStepElement();
            ApplicationWorkflowStepElementContent contentToReturn = new ApplicationWorkflowStepElementContent();
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            ApplicationWorkflowStepElementContent mockContent1 = mocks.PartialMock<ApplicationWorkflowStepElementContent>();
            ApplicationWorkflowStepElementContent mockContent2 = mocks.PartialMock<ApplicationWorkflowStepElementContent>();
            ApplicationWorkflowStepElementContent mockContent3 = mocks.PartialMock<ApplicationWorkflowStepElementContent>();
            //
            // configure the data
            //
            stepElement.ApplicationWorkflowStepElementContents.Add(mockContent1);
            stepElement.ApplicationWorkflowStepElementContents.Add(mockContent2);
            stepElement.ApplicationWorkflowStepElementContents.Add(mockContent3);
            //
            // Set up the expectations
            //
            mocks.ReplayAll();
            //
            // Do the test
            //
            var x = stepElement.Promote(_badStepId, _userId, _shouldNotRemoveMarkup);
            //
            // Verify that the test worked
            //
            Assert.Fail("Promote_BadParam1Test should have thrown an exception");
        }
        /// <summary>
        /// Test bad user id
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepElement.Promote")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "ApplicationWorkflowStepElementContent.Promote received an invalid parameter nextWorkflowStepElementId is [333] and userId is [-11]")]
        public void Promote_BadParam2Test()
        {
            //
            // create the needed data
            //
            ApplicationWorkflowStepElement stepElement = new ApplicationWorkflowStepElement();
            ApplicationWorkflowStepElementContent contentToReturn = new ApplicationWorkflowStepElementContent();
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            ApplicationWorkflowStepElementContent mockContent1 = mocks.PartialMock<ApplicationWorkflowStepElementContent>();
            ApplicationWorkflowStepElementContent mockContent2 = mocks.PartialMock<ApplicationWorkflowStepElementContent>();
            ApplicationWorkflowStepElementContent mockContent3 = mocks.PartialMock<ApplicationWorkflowStepElementContent>();
            //
            // configure the data
            //
            stepElement.ApplicationWorkflowStepElementContents.Add(mockContent1);
            stepElement.ApplicationWorkflowStepElementContents.Add(mockContent2);
            stepElement.ApplicationWorkflowStepElementContents.Add(mockContent3);
            //
            // Set up the expectations
            //
            mocks.ReplayAll();
            //
            // Do the test
            //
            var x = stepElement.Promote(_stepId, _badUserId, _shouldNotRemoveMarkup);
            //
            // Verify that the test worked
            //
            Assert.Fail("Promote_BadParam1Test should have thrown an exception");
        }
        /// <summary>
        /// Test bad parameters
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepElement.Promote")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "ApplicationWorkflowStepElementContent.Promote received an invalid parameter nextWorkflowStepElementId is [-333] and userId is [-11]")]
        public void Promote_BadParamTest()
        {
            //
            // create the needed data
            //
            ApplicationWorkflowStepElement stepElement = new ApplicationWorkflowStepElement();
            ApplicationWorkflowStepElementContent contentToReturn = new ApplicationWorkflowStepElementContent();
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            ApplicationWorkflowStepElementContent mockContent1 = mocks.PartialMock<ApplicationWorkflowStepElementContent>();
            ApplicationWorkflowStepElementContent mockContent2 = mocks.PartialMock<ApplicationWorkflowStepElementContent>();
            ApplicationWorkflowStepElementContent mockContent3 = mocks.PartialMock<ApplicationWorkflowStepElementContent>();
            //
            // configure the data
            //
            stepElement.ApplicationWorkflowStepElementContents.Add(mockContent1);
            stepElement.ApplicationWorkflowStepElementContents.Add(mockContent2);
            stepElement.ApplicationWorkflowStepElementContents.Add(mockContent3);
            //
            // Set up the expectations
            //
            mocks.ReplayAll();
            //
            // Do the test
            //
            var x = stepElement.Promote(_badStepId, _badUserId, _shouldNotRemoveMarkup);
            //
            // Verify that the test worked
            //
            Assert.Fail("Promote_BadParam1Test should have thrown an exception");
        }
        #endregion
        #region CreateHistory Tests
        /// <summary>
        /// Test CreateHistor
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepElement.CreateHistory")]
        public void CreateHistory_Test()
        {
            //
            // Set up local data
            //
            ApplicationWorkflowStepElement entity = new ApplicationWorkflowStepElement();
            entity.ApplicationWorkflowStepElementContents.Add(new ApplicationWorkflowStepElementContent());
            //
            // Test
            //
            var x = entity.CreateHistory(_userId, _worklogId);
            //
            // Verify
            //
            Assert.AreEqual(1, x.Count(), "number of history records returned was not as expected");
        }
        #endregion
    }
}
