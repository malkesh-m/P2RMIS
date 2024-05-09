using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace DLLTest.Partials
{
    /// <summary>
    /// Unit tests for ApplicationWorkflowStep
    /// </summary>
    [TestClass()]
    public class ApplicationWorkflowStepTest: DllBaseTest
    {

        #region Test Attributes
        private ApplicationWorkflowStep workflow;
        private static List<ApplicationWorkflowStepElement> elementList = new List<ApplicationWorkflowStepElement>();
        private int _stepId1 = 100;
        private int _stepId2 = 101;
        private int _stepId3 = 102;
        private int _templateStepId1 = 251;
        private int _templateStepId2 = 252;
        private int _templateStepId3 = 253;
        private int _templateStepId4 = 254;
        private int _userId = 11;
        private int _anotherUserId = 100;
        private const int _worklogId = 9992;
        private string contextText = "this is my context text";
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
        [TestInitialize()]
        public void MyTestInitialize()
        {

            workflow = new ApplicationWorkflowStep();
            //
            // initialize the list (set into a workflowStep)
            //
            elementList.Add(new ApplicationWorkflowStepElement { ApplicationTemplateElementId = _templateStepId1, ApplicationWorkflowStepId = _stepId1 });
            elementList.Add(new ApplicationWorkflowStepElement { ApplicationTemplateElementId = _templateStepId2, ApplicationWorkflowStepId = _stepId2 });
            elementList.Add(new ApplicationWorkflowStepElement { ApplicationTemplateElementId = _templateStepId3, ApplicationWorkflowStepId = _stepId3 });
            //
            // Initialize the necessary mocks
            //
            theMock = new MockRepository();            
            stepElementMock = theMock.PartialMock<ApplicationWorkflowStepElement>();
            stepMock = theMock.PartialMock<ApplicationWorkflowStep>();
            stepElementContentMock = theMock.PartialMock<ApplicationWorkflowStepElementContent>();
            //
            // Initialize any base attributes
            //
            targetSourceStepElement = new ApplicationWorkflowStepElement();
            sourceSourceStepElement = new ApplicationWorkflowStepElement();
       }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            workflow = null;
            elementList.Clear();
            //
            // Clear the mocks
            //
            theMock = null;
            stepElementMock = null;
            stepMock = null;
            stepElementContentMock = null;
            //
            // Clear the Attributes
            //
            targetSourceStepElement = null;
            sourceSourceStepElement = null;
        }
        //
        #endregion
        #endregion
        #region The MatchStepElement Tests
        /// <summary>
        /// Test MatchStepElement to find an ApplicationWorkflowStepElement
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStep.MatchStepElement")]
        public void MatchStepElement_Test()
        {
            //
            // Data set up
            //
            workflow.ApplicationWorkflowStepElements = elementList;
            ApplicationWorkflowStepElement elementToMatch = new ApplicationWorkflowStepElement { ApplicationTemplateElementId = _templateStepId3 };
            //
            // test
            //
            var x = workflow.MatchStepElement(elementToMatch);
            //
            // Make sure it works
            //
            Assert.IsNotNull(x, "Match did not find a match and should have");
            Assert.AreEqual(_templateStepId3, x.ApplicationTemplateElementId, "Object returned did not have same ApplicationTemplateElementId values");
            Assert.AreEqual(_stepId3, x.ApplicationWorkflowStepId, "Object returned did not have correct index value");
        }
        /// <summary>
        /// Test MatchStepElement to find an ApplicationWorkflowStepElement
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStep.MatchStepElement")]
        public void MatchStepElement_NoMatchTest()
        {
            //
            // Data set up
            //
            workflow.ApplicationWorkflowStepElements = elementList;
            ApplicationWorkflowStepElement elementToMatch = new ApplicationWorkflowStepElement { ApplicationTemplateElementId = _templateStepId4 };
            //
            // test
            //
            var x = workflow.MatchStepElement(elementToMatch);
            //
            // Make sure it works
            //
            Assert.IsNull(x, "Match found something and should not have");
        }
        #endregion
        #region The Promote Tests
        /// <summary>
        /// Test Promote to promote an element 
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStep.Promote")]
        public void Promote_OneTest()
        {
            List<ApplicationWorkflowStepElementContent> results = new List<ApplicationWorkflowStepElementContent>();
            ApplicationWorkflowStepElementContent return1 = new ApplicationWorkflowStepElementContent { };
            results.Add(return1);
            //
            // Set up mocks
            //
            MockRepository mocks = new MockRepository();
            ApplicationWorkflowStepElement mockStepElement = mocks.PartialMock<ApplicationWorkflowStepElement>();
            ApplicationWorkflowStep mockNextStep = mocks.PartialMock<ApplicationWorkflowStep>();
            ApplicationWorkflowStepElement mockStepElementInNextStep = mocks.PartialMock<ApplicationWorkflowStepElement>();
            mockStepElementInNextStep.ApplicationWorkflowStepElementId = 66;
            mockStepElement.ApplicationWorkflowStepId = 4004;
            //
            // Set up expectations
            //
            Expect.Call(mockNextStep.MatchStepElement(mockStepElement)).Return(mockStepElementInNextStep);
            Expect.Call(mockStepElement.Promote(mockStepElementInNextStep.ApplicationWorkflowStepElementId, _userId, _shouldNotRemoveMarkup)).Return(results);
            Expect.Call(mockNextStep.ShouldRemoveMarkup()).Return(_shouldNotRemoveMarkup);
            //
            // Data set up
            //
            workflow.ApplicationWorkflowStepElements.Clear();
            workflow.ApplicationWorkflowStepElements.Add(mockStepElement);
            mocks.ReplayAll();
            //
            // test
            //
            var x = workflow.Promote(mockNextStep, _userId);
            //
            // Make sure it works
            //
            Assert.IsNotNull(x, "Promote did not find a match and should have");
            Assert.AreEqual(x.Count(), results.Count(), "Promote did not return correct number of promoted content");
            mocks.VerifyAll();
        }
        /// <summary>
        /// Test Promote to promote multiple elements
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStep.Promote")]
        public void Promote_MultipleTest()
        {
            //
            // Set up the return
            //
            List<ApplicationWorkflowStepElementContent> results = new List<ApplicationWorkflowStepElementContent>();
            ApplicationWorkflowStepElementContent return1 = new ApplicationWorkflowStepElementContent { };
            ApplicationWorkflowStepElementContent return2 = new ApplicationWorkflowStepElementContent { };
            ApplicationWorkflowStepElementContent return3 = new ApplicationWorkflowStepElementContent { };
            results.Add(return1);
            results.Add(return2);
            results.Add(return3);
            //
            // Set up mocks
            //
            MockRepository mocks = new MockRepository();
            ApplicationWorkflowStepElement mockStepElement1 = mocks.PartialMock<ApplicationWorkflowStepElement>();
            ApplicationWorkflowStepElement mockStepElement2 = mocks.PartialMock<ApplicationWorkflowStepElement>();
            ApplicationWorkflowStepElement mockStepElement3 = mocks.PartialMock<ApplicationWorkflowStepElement>();
            ApplicationWorkflowStep mockNextStep = mocks.PartialMock<ApplicationWorkflowStep>();
            ApplicationWorkflowStepElement mockStepElementInNextStep1 = mocks.PartialMock<ApplicationWorkflowStepElement>();
            ApplicationWorkflowStepElement mockStepElementInNextStep2 = mocks.PartialMock<ApplicationWorkflowStepElement>();
            ApplicationWorkflowStepElement mockStepElementInNextStep3 = mocks.PartialMock<ApplicationWorkflowStepElement>();
            //
            // Set up expectations
            //
            Expect.Call(mockNextStep.MatchStepElement(mockStepElement1)).Return(mockStepElementInNextStep1);
            Expect.Call(mockNextStep.MatchStepElement(mockStepElement2)).Return(mockStepElementInNextStep2);
            Expect.Call(mockNextStep.MatchStepElement(mockStepElement3)).Return(mockStepElementInNextStep3);     
            Expect.Call(mockStepElement1.Promote(mockStepElementInNextStep1.ApplicationWorkflowStepElementId, _userId, _shouldNotRemoveMarkup)).Return(results);
            Expect.Call(mockStepElement2.Promote(mockStepElementInNextStep2.ApplicationWorkflowStepElementId, _userId, _shouldNotRemoveMarkup)).Return(results);
            Expect.Call(mockStepElement3.Promote(mockStepElementInNextStep3.ApplicationWorkflowStepElementId, _userId, _shouldNotRemoveMarkup)).Return(results);       
            Expect.Call(mockNextStep.ShouldRemoveMarkup()).Return(_shouldNotRemoveMarkup);
            //
            // Data set up
            //
            workflow.ApplicationWorkflowStepElements.Clear();
            workflow.ApplicationWorkflowStepElements.Add(mockStepElement1);
            workflow.ApplicationWorkflowStepElements.Add(mockStepElement2);
            workflow.ApplicationWorkflowStepElements.Add(mockStepElement3);
            mocks.ReplayAll();
            //
            // test
            //
            var x = workflow.Promote(mockNextStep, _userId);
            //
            // Make sure it works
            //
            Assert.IsNotNull(x, "Promote did not find a match and should have");
            Assert.AreEqual(x.Count(), results.Count()*3, "Promote did not return correct number of promoted content");
            mocks.VerifyAll();
        }
        #endregion
        #region The SetResolution Tests
        /// <summary>
        /// Test SetResolution assumptions
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStep.SetResolution")]
        public void SetResolution_AssumptionsTest()
        {
            //
            // data setup 
            //
            ApplicationWorkflowStep step = new ApplicationWorkflowStep();
            //
            // test the assumptions
            //
            Assert.AreEqual(0, step.ApplicationWorkflowStepId, "SetResolution - ApplicationWorkflowStepId not initialized as expected");
            Assert.AreEqual(0, step.ApplicationWorkflowId, "SetResolution - ApplicationWorkflowId not initialized as expected");
            Assert.AreEqual(0, step.StepTypeId, "SetResolution - StepTypeId not initialized as expected");
            Assert.IsNullOrEmpty(step.StepName, "SetResolution - StepName not initialized as expected");
            Assert.IsFalse(step.Active, "SetResolution - Active not initialized as expected");
            Assert.AreEqual(0, step.StepOrder, "SetResolution - StepOrder not initialized as expected");
            Assert.IsFalse(step.Resolution, "SetResolution - Resolution not initialized as expected");
            Assert.AreEqual(null, step.ResolutionDate, "SetResolution - ResolutionDate not initialized as expected");
            //Assert.AreEqual(0, step.CreatedBy, "SetResolution - CreatedBy not initialized as expected");
            //Assert.AreEqual(_noDate, step.CreatedDate, "SetResolution - CreatedDate not initialized as expected");
            //Assert.AreEqual(0, step.ModifiedBy, "SetResolution - ModifiedBy not initialized as expected");
            //Assert.AreEqual(_noDate, step.ModifiedDate, "SetResolution - ModifiedDate not initialized as expected");
            Assert.IsNull(step.ApplicationWorkflow, "SetResolution - ApplicationWorkflow not initialized as expected");
            Assert.IsNull(step.StepType, "SetResolution - ApplicationWorkflowStepElements not initialized as expected");
            Assert.IsNotNull(step.ApplicationWorkflowStepElements, "SetResolution - ApplicationWorkflowStepElements not initialized as expected");
            Assert.AreEqual(0, step.ApplicationWorkflowStepElements.Count(), "SetResolution - ApplicationWorkflowStepElements not initialized as expected");
            Assert.IsNotNull(step.ApplicationWorkflowStepAssignments, "SetResolution - ApplicationWorkflowStepAssignments not initialized as expected");
            Assert.AreEqual(0, step.ApplicationWorkflowStepAssignments.Count(), "SetResolution - ApplicationWorkflowStepAssignments not initialized as expected");
            Assert.IsNotNull(step.ApplicationWorkflowStepWorkLogs, "SetResolution - ApplicationWorkflowStepWorkLogs not initialized as expected");
            Assert.AreEqual(0, step.ApplicationWorkflowStepWorkLogs.Count(), "SetResolution - ApplicationWorkflowStepWorkLogs not initialized as expected");
        }
        /// <summary>
        /// Test SetResolution setting to true
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStep.SetResolution")]
        public void SetResolution_Test()
        {
            //
            // data setup 
            //
            ApplicationWorkflowStep step = new ApplicationWorkflowStep();
            DateTime beforeTime = DateTime.Now;
            //
            // Do the test
            //
            step.SetResolution(true, _userId);
            //
            // these should change
            //
            Assert.IsTrue(step.Resolution, "SetResolution - Resolution not as expected");
            Assert.That(step.ResolutionDate, Is.InRange(beforeTime, DateTime.Now), "SetResolution - ResolutionDate not as expected");

            Assert.AreEqual(_userId, step.ModifiedBy, "SetResolution - ModifiedBy not as expected");
            Assert.That(step.ModifiedDate, Is.InRange(beforeTime, DateTime.Now), "SetResolution - ModifiedDate not as expected");
            //
            // these should not
            //
            Assert.AreEqual(0, step.ApplicationWorkflowStepId, "SetResolution - ApplicationWorkflowStepId not initialized as expected");
            Assert.AreEqual(0, step.ApplicationWorkflowId, "SetResolution - ApplicationWorkflowId not initialized as expected");
            Assert.AreEqual(0, step.StepTypeId, "SetResolution - StepTypeId not initialized as expected");
            Assert.IsNullOrEmpty(step.StepName, "SetResolution - StepName not initialized as expected");
            Assert.IsFalse(step.Active, "SetResolution - Active not initialized as expected");
            Assert.AreEqual(0, step.StepOrder, "SetResolution - StepOrder not initialized as expected");
            //Assert.AreEqual(0, step.CreatedBy, "SetResolution - CreatedBy not initialized as expected");
            //Assert.AreEqual(_noDate, step.CreatedDate, "SetResolution - CreatedDate not initialized as expected");
            Assert.IsNull(step.ApplicationWorkflow, "SetResolution - ApplicationWorkflow not initialized as expected");
            Assert.IsNull(step.StepType, "SetResolution - ApplicationWorkflowStepElements not initialized as expected");
            Assert.IsNotNull(step.ApplicationWorkflowStepElements, "SetResolution - ApplicationWorkflowStepElements not initialized as expected");
            Assert.AreEqual(0, step.ApplicationWorkflowStepElements.Count(), "SetResolution - ApplicationWorkflowStepElements not initialized as expected");
            Assert.IsNotNull(step.ApplicationWorkflowStepAssignments, "SetResolution - ApplicationWorkflowStepAssignments not initialized as expected");
            Assert.AreEqual(0, step.ApplicationWorkflowStepAssignments.Count(), "SetResolution - ApplicationWorkflowStepAssignments not initialized as expected");
            Assert.IsNotNull(step.ApplicationWorkflowStepWorkLogs, "SetResolution - ApplicationWorkflowStepWorkLogs not initialized as expected");
            Assert.AreEqual(0, step.ApplicationWorkflowStepWorkLogs.Count(), "SetResolution - ApplicationWorkflowStepWorkLogs not initialized as expected");
        }
        /// <summary>
        /// Test SetResolution setting to true
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStep.SetResolution")]
        public void SetResolution_FalseTest()
        {
            //
            // data setup 
            //
            ApplicationWorkflowStep step = new ApplicationWorkflowStep();
            //
            // Do the test
            //
            step.SetResolution(false, _userId);
            //
            // these should not change
            //
            Assert.IsFalse(step.Resolution, "SetResolution - Resolution not as expected");
            Assert.AreEqual(null, step.ResolutionDate, "SetResolution - ResolutionDate not as expected");
            //Assert.AreEqual(0, step.ModifiedBy, "SetResolution - ModifiedBy not as expected");
            //Assert.AreEqual(_noDate, step.ModifiedDate, "SetResolution - ModifiedDate not as expected");
            //
            // these should not
            //
            Assert.AreEqual(0, step.ApplicationWorkflowStepId, "SetResolution - ApplicationWorkflowStepId not initialized as expected");
            Assert.AreEqual(0, step.ApplicationWorkflowId, "SetResolution - ApplicationWorkflowId not initialized as expected");
            Assert.AreEqual(0, step.StepTypeId, "SetResolution - StepTypeId not initialized as expected");
            Assert.IsNullOrEmpty(step.StepName, "SetResolution - StepName not initialized as expected");
            Assert.IsFalse(step.Active, "SetResolution - Active not initialized as expected");
            Assert.AreEqual(0, step.StepOrder, "SetResolution - StepOrder not initialized as expected");
            //Assert.AreEqual(0, step.CreatedBy, "SetResolution - CreatedBy not initialized as expected");
            //Assert.AreEqual(_noDate, step.CreatedDate, "SetResolution - CreatedDate not initialized as expected");
            Assert.IsNull(step.ApplicationWorkflow, "SetResolution - ApplicationWorkflow not initialized as expected");
            Assert.IsNull(step.StepType, "SetResolution - ApplicationWorkflowStepElements not initialized as expected");
            Assert.IsNotNull(step.ApplicationWorkflowStepElements, "SetResolution - ApplicationWorkflowStepElements not initialized as expected");
            Assert.AreEqual(0, step.ApplicationWorkflowStepElements.Count(), "SetResolution - ApplicationWorkflowStepElements not initialized as expected");
            Assert.IsNotNull(step.ApplicationWorkflowStepAssignments, "SetResolution - ApplicationWorkflowStepAssignments not initialized as expected");
            Assert.AreEqual(0, step.ApplicationWorkflowStepAssignments.Count(), "SetResolution - ApplicationWorkflowStepAssignments not initialized as expected");
            Assert.IsNotNull(step.ApplicationWorkflowStepWorkLogs, "SetResolution - ApplicationWorkflowStepWorkLogs not initialized as expected");
            Assert.AreEqual(0, step.ApplicationWorkflowStepWorkLogs.Count(), "SetResolution - ApplicationWorkflowStepWorkLogs not initialized as expected");
        }
        /// <summary>
        /// Test SetResolution setting to false after it is true
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStep.SetResolution")]
        public void SetResolution_UnresolveTest()
        {
            DateTime secondDate = new DateTime(2000, 1, 1);
            //
            // data setup 
            //
            ApplicationWorkflowStep step = new ApplicationWorkflowStep();
            step.SetResolution(true, _userId);
            step.ModifiedDate = secondDate;
            step.ResolutionDate = secondDate;
            //
            // Do the test
            //
            step.SetResolution(false, _anotherUserId);
            //
            // these should not change
            //
            Assert.IsTrue(step.Resolution, "SetResolution - Resolution not as expected");
            Assert.AreEqual(secondDate, step.ResolutionDate, "SetResolution - ResolutionDate not as expected");
            Assert.AreEqual(_userId, step.ModifiedBy, "SetResolution - ModifiedBy not as expected");
            Assert.AreEqual(secondDate, step.ModifiedDate, "SetResolution - ModifiedDate not as expected");
            //
            // these should not
            //
            Assert.AreEqual(0, step.ApplicationWorkflowStepId, "SetResolution - ApplicationWorkflowStepId not initialized as expected");
            Assert.AreEqual(0, step.ApplicationWorkflowId, "SetResolution - ApplicationWorkflowId not initialized as expected");
            Assert.AreEqual(0, step.StepTypeId, "SetResolution - StepTypeId not initialized as expected");
            Assert.IsNullOrEmpty(step.StepName, "SetResolution - StepName not initialized as expected");
            Assert.IsFalse(step.Active, "SetResolution - Active not initialized as expected");
            Assert.AreEqual(0, step.StepOrder, "SetResolution - StepOrder not initialized as expected");
            //Assert.AreEqual(0, step.CreatedBy, "SetResolution - CreatedBy not initialized as expected");
            //Assert.AreEqual(_noDate, step.CreatedDate, "SetResolution - CreatedDate not initialized as expected");
            Assert.IsNull(step.ApplicationWorkflow, "SetResolution - ApplicationWorkflow not initialized as expected");
            Assert.IsNull(step.StepType, "SetResolution - ApplicationWorkflowStepElements not initialized as expected");
            Assert.IsNotNull(step.ApplicationWorkflowStepElements, "SetResolution - ApplicationWorkflowStepElements not initialized as expected");
            Assert.AreEqual(0, step.ApplicationWorkflowStepElements.Count(), "SetResolution - ApplicationWorkflowStepElements not initialized as expected");
            Assert.IsNotNull(step.ApplicationWorkflowStepAssignments, "SetResolution - ApplicationWorkflowStepAssignments not initialized as expected");
            Assert.AreEqual(0, step.ApplicationWorkflowStepAssignments.Count(), "SetResolution - ApplicationWorkflowStepAssignments not initialized as expected");
            Assert.IsNotNull(step.ApplicationWorkflowStepWorkLogs, "SetResolution - ApplicationWorkflowStepWorkLogs not initialized as expected");
            Assert.AreEqual(0, step.ApplicationWorkflowStepWorkLogs.Count(), "SetResolution - ApplicationWorkflowStepWorkLogs not initialized as expected");
        }
        #endregion
        #region CreateHistory Tests
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStep.CreateHistory")] 
        public void CreateHistory_OneElementTest()
        {	
	        //
	        // Set up local data
	        //
            ApplicationWorkflowStepElement stepElement = new ApplicationWorkflowStepElement();
            workflow.ApplicationWorkflowStepElements.Add(stepElement);

            ApplicationWorkflowStepElementContentHistory historyElement = new ApplicationWorkflowStepElementContentHistory();
            ApplicationWorkflowStepElementContent contentElement = new ApplicationWorkflowStepElementContent();
            stepElement.ApplicationWorkflowStepElementContents.Add(contentElement);
	        //
	        // Test
	        //
            var results = workflow.CreateHistory(_userId, _worklogId);
	        //
	        // Verify
	        //
            Assert.IsNotNull(results, "CreateHistory did not return a result");
            Assert.AreEqual(1, results.Count(), "CreateHistory did not result the correct number of values");
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStep.CreateHistory")]
        public void CreateHistory_MultipleElementTest()
        {
            //
            // Set up local data
            //
            ApplicationWorkflowStepElement stepElement1 = new ApplicationWorkflowStepElement();
            ApplicationWorkflowStepElement stepElement2 = new ApplicationWorkflowStepElement();
            ApplicationWorkflowStepElement stepElement3 = new ApplicationWorkflowStepElement();
            workflow.ApplicationWorkflowStepElements.Add(stepElement1);
            workflow.ApplicationWorkflowStepElements.Add(stepElement2);
            workflow.ApplicationWorkflowStepElements.Add(stepElement3);

            ApplicationWorkflowStepElementContentHistory historyElement = new ApplicationWorkflowStepElementContentHistory();
            ApplicationWorkflowStepElementContent contentElement1 = new ApplicationWorkflowStepElementContent();
            ApplicationWorkflowStepElementContent contentElement2 = new ApplicationWorkflowStepElementContent();
            ApplicationWorkflowStepElementContent contentElement3 = new ApplicationWorkflowStepElementContent();
            stepElement1.ApplicationWorkflowStepElementContents.Add(contentElement1);
            stepElement2.ApplicationWorkflowStepElementContents.Add(contentElement2);
            stepElement3.ApplicationWorkflowStepElementContents.Add(contentElement3);
            //
            // Test
            //
            var results = workflow.CreateHistory(_userId, _worklogId);
            //
            // Verify
            //
            Assert.IsNotNull(results, "CreateHistory did not return a result");
            Assert.AreEqual(3, results.Count(), "CreateHistory did not result the correct number of values");
        }   
        #endregion
        #region LocateMatchingElementsByTemplateId Tests
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStep.LocateMatchingElementsByTemplateId")]
        public void LocateMatchingElementsByTemplateId_Test()
        {	
            int _templateId1 = 11;
            int _templateId2 = 22;
            int _templateId3 = 33;
	        //
	        // Set up local data
	        //
            ApplicationWorkflowStepElement stepElement1 = new ApplicationWorkflowStepElement();
            stepElement1.ApplicationWorkflowStepId = _stepId1;
            stepElement1.ApplicationTemplateElementId = _templateId1;
            ApplicationWorkflowStepElement stepElement2 = new ApplicationWorkflowStepElement();
            stepElement2.ApplicationWorkflowStepId = _stepId2;
            stepElement2.ApplicationTemplateElementId = _templateId2;
            ApplicationWorkflowStepElement stepElement3 = new ApplicationWorkflowStepElement();
            stepElement3.ApplicationWorkflowStepId = _stepId3;
            stepElement3.ApplicationTemplateElementId = _templateId3;

            workflow.ApplicationWorkflowStepElements.Add(stepElement1);
            workflow.ApplicationWorkflowStepElements.Add(stepElement2);
            workflow.ApplicationWorkflowStepElements.Add(stepElement3);
            //
	        // Test
	        //
            var result = workflow.LocateMatchingStepElementByTemplateId(_templateId2);
	        //
	        // Verify
	        //
            Assert.IsNotNull(result, "LocateMatchingStepElementByTemplateId did not return anything");
            Assert.AreEqual(_stepId2, result.ApplicationWorkflowStepId, "LocateMatchingStepElementByTemplateId did not return the expected step element");
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStep.LocateMatchingElementsByTemplateId")]
        public void LocateMatchingElementsByTemplateId_FirstTest()
        {
            int _templateId1 = 11;
            int _templateId2 = 22;
            int _templateId3 = 33;
            //
            // Set up local data
            //
            ApplicationWorkflowStepElement stepElement1 = new ApplicationWorkflowStepElement();
            stepElement1.ApplicationWorkflowStepId = _stepId1;
            stepElement1.ApplicationTemplateElementId = _templateId1;
            ApplicationWorkflowStepElement stepElement2 = new ApplicationWorkflowStepElement();
            stepElement2.ApplicationWorkflowStepId = _stepId2;
            stepElement2.ApplicationTemplateElementId = _templateId2;
            ApplicationWorkflowStepElement stepElement3 = new ApplicationWorkflowStepElement();
            stepElement3.ApplicationWorkflowStepId = _stepId3;
            stepElement3.ApplicationTemplateElementId = _templateId3;

            workflow.ApplicationWorkflowStepElements.Add(stepElement1);
            workflow.ApplicationWorkflowStepElements.Add(stepElement2);
            workflow.ApplicationWorkflowStepElements.Add(stepElement3);
            //
            // Test
            //
            var result = workflow.LocateMatchingStepElementByTemplateId(_templateId1);
            //
            // Verify
            //
            Assert.IsNotNull(result, "LocateMatchingStepElementByTemplateId did not return anything");
            Assert.AreEqual(_stepId1, result.ApplicationWorkflowStepId, "LocateMatchingStepElementByTemplateId did not return the expected step element");
        }
        #endregion
        #region CopyContentFromOtherWorkflowStep Tests
        /// <summary>
        /// Test comment is copied correctly
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStep.CopyContentFromOtherWorkflowStep")]
        public void CopyContentFromOtherWorkflowStep_Test()
        {
	        //
	        // Set up local data
	        //
            ApplicationWorkflowStep sourceStep = new ApplicationWorkflowStep();
            sourceSourceStepElement.ApplicationTemplateElementId = _templateStepId1;
            sourceStep.ApplicationWorkflowStepElements.Add(sourceSourceStepElement);
            ApplicationWorkflowStepElementContent sourceContent = new ApplicationWorkflowStepElementContent();
            sourceContent.ContentText = contextText;
            sourceSourceStepElement.ApplicationWorkflowStepElementContents.Add(sourceContent);

            targetSourceStepElement.ApplicationWorkflowStepElementContents.Add(stepElementContentMock);
            stepMock.ApplicationWorkflowStepElements.Add(targetSourceStepElement);
            //
            // Set Expectations
            //
            Expect.Call(stepMock.LocateMatchingStepElementByTemplateId(_templateStepId1)).Return(targetSourceStepElement);
            Expect.Call(delegate { stepElementContentMock.SaveModifiedContent(contextText, _userId); });
            theMock.ReplayAll();
            //
	        // Test
	        //
            stepMock.CopyContentFromOtherWorkflowStep(sourceStep, _userId);
	        //
	        // Verify - For some reason that I could not figure out, stepMock.VerifyAll() kept failing because it detected 
            //          a call to ApplicationWorkflowStepElements was not satisfied.  As the interested reader can see there
            //          is no expectation that it is called.  So Verifications are explicitly specified.
	        //
            stepElementContentMock.VerifyAllExpectations();
            stepMock.AssertWasCalled(x => x.LocateMatchingStepElementByTemplateId(_templateStepId1));
        }
        /// <summary>
        /// Test comment is copied correctly when target step does not have a matching ContentElement.
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStep.CopyContentFromOtherWorkflowStep")]
        public void CopyContentFromOtherWorkflowStep_NoElementTest()
        {
            //
            // Set up local data
            //
            ApplicationWorkflowStep sourceStep = new ApplicationWorkflowStep();
            sourceSourceStepElement.ApplicationTemplateElementId = _templateStepId1;
            sourceStep.ApplicationWorkflowStepElements.Add(sourceSourceStepElement);
            ApplicationWorkflowStepElementContent sourceContent = new ApplicationWorkflowStepElementContent();
            sourceContent.ContentText = contextText;
            sourceSourceStepElement.ApplicationWorkflowStepElementContents.Add(sourceContent);

            stepMock.ApplicationWorkflowStepElements.Add(targetSourceStepElement);
            //
            // Set Expectations
            //
            Expect.Call(stepMock.LocateMatchingStepElementByTemplateId(_templateStepId1)).Return(targetSourceStepElement);
            theMock.ReplayAll();
            //
            // Test
            //
            stepMock.CopyContentFromOtherWorkflowStep(sourceStep, _userId);
            //
            // Verify - For some reason that I could not figure out, stepMock.VerifyAll() kept failing because it detected 
            //          a call to ApplicationWorkflowStepElements was not satisfied.  As the interested reader can see there
            //          is no expectation that it is called.  So Verifications are explicitly specified.
            //
            stepMock.AssertWasCalled(x => x.LocateMatchingStepElementByTemplateId(_templateStepId1));

            Assert.AreEqual(1, targetSourceStepElement.ApplicationWorkflowStepElementContents.Count, "Context element was not added");
            ApplicationWorkflowStepElementContent targetContent = targetSourceStepElement.ApplicationWorkflowStepElementContents.ElementAtOrDefault(0);
            Assert.AreEqual(contextText, targetContent.ContentText, "Content text was not copied correctly");
        }

        #endregion
        #region IsCheckedOutByUser Tests
        //TODO-Need unit tests here IsCheckedOutByUser
        #endregion
        #region GetApplicationWorkflowStepElementById Tests
        //TODO- Need unit tests here for GetApplicationWorkflowStepElementById
        #endregion
        #region GetContentList Tests
        //
        // There are no tests for GetContentList.  It is a wrapper for Promote
        //
        #endregion
    }
}
