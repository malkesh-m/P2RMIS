using System;
using System.Activities;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace BLLTest.Workflow
{
    /// <summary>
    /// Unit tests for WorkflowService.Execute
    /// </summary>
    [TestClass()]
    public partial class WorkflowServiceTest
    {
        #region Attributes
        private const int _goodWorkflowId = 1234;
        private const int _badWorkflowId = -66614;
        private const int _zeroUserId = 0;
        private readonly TestCheckoutActivity _testCheckoutActivity = new TestCheckoutActivity();
        private const int _contentId = 9;
        private const int _badContentId = -876;
        private const string _content = "this be the content";
        private const int _elementId = 654987;

        private WorkflowService _workflowServiceMock;
        private ApplicationWorkflowStep _step;
        #endregion
        #region Execute Tests
        /// <summary>
        /// Test completing a workflow
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.Execute")]
        public void Execute_CompleteWorkflowTest()
        {
            //
            // Create the objects & set up 
            //
            this._testCheckoutActivity.TheStateToReturn = WorkflowState.Complete;
            //
            // Set up Expectations
            //
            Expect.Call(theWorkMock.ApplicationWorkflowRepository).Return(theWorkflowRepositoryMock).Repeat.Times(2);
            Expect.Call(theWorkMock.ApplicationWorkflowStepRepository).Return(theWorkflowStepRepositoryMock);
            Expect.Call(theWorkflowRepositoryMock.GetByID(_goodWorkflowId)).Return(thePartialWorkflowMock);
            Expect.Call(thePartialWorkflowMock.CurrentStep()).Return(_step);
            Expect.Call(thePartialWorkflowMock.IsComplete()).Return(true);
            Expect.Call(delegate { thePartialWorkflowMock.Complete(GOOD_USER_ID); });
            Expect.Call(_workflowServiceMock.CreateActivity(P2rmisActions.Checkin)).Return(this._testCheckoutActivity);
            Expect.Call(delegate { theWorkflowStepRepositoryMock.Update(_step); });
            Expect.Call(delegate { theWorkflowRepositoryMock.Update(thePartialWorkflowMock); });
            Expect.Call(delegate { theWorkMock.Save(); });
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.Execute(_goodWorkflowId, P2rmisActions.Checkin, _goodUserId, null);
            //
            // verify
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test completing a step
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.Execute")]
        public void Execute_CompleteTest()
        {
            //TODO:  need to do this step when activity written
        }
        /// <summary>
        /// Test successfully executing a workflow activity
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.Execute")] 
        public void Execute_CheckinTest()
        {
            //
            // Create the objects & set up 
            //
            this._testCheckoutActivity.TheStateToReturn = WorkflowState.Started;
            //
            // Set up Expectations
            //
            Expect.Call(theWorkMock.ApplicationWorkflowRepository).Return(theWorkflowRepositoryMock);
            Expect.Call(theWorkMock.ApplicationWorkflowStepRepository).Return(theWorkflowStepRepositoryMock);
            Expect.Call(theWorkflowRepositoryMock.GetByID(_goodWorkflowId)).Return(thePartialWorkflowMock);
            Expect.Call(thePartialWorkflowMock.CurrentStep()).Return(_step);
            Expect.Call(_workflowServiceMock.CreateActivity(P2rmisActions.Checkin)).Return(this._testCheckoutActivity);
            Expect.Call(delegate { theWorkflowStepRepositoryMock.Update(_step); });
            Expect.Call(delegate { theWorkMock.Save(); });
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.Execute(_goodWorkflowId, P2rmisActions.Checkin, _goodUserId, null);
            //
            // verify
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test Execute with a bad ApplicationId
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.Execute")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "WorkflowService.Execute received invalid parameters: workflowId [-66614]; userId [1111]")]
        public void Execute_BadApplicationIdTest()
        {
            //
            // Create the objects
            //
            this._testCheckoutActivity.TheStateToReturn = WorkflowState.Started;
            //
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.Execute(_badWorkflowId, P2rmisActions.Checkin, _goodUserId, null);
            Assert.Fail("An exception should have been thrown");
        }
        /// <summary>
        /// Test Execute with a bad UserId
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.Execute")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "WorkflowService.Execute received invalid parameters: workflowId [1234]; userId [-4]")]
        public void Execute_BadUserIdTest()
        {
            //
            // Create the objects
            //
            this._testCheckoutActivity.TheStateToReturn = WorkflowState.Started;
            //
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.Execute(_goodWorkflowId, P2rmisActions.Checkin, _badUserId, null);
            Assert.Fail("An exception should have been thrown");
        }
        /// <summary>
        /// Test Execute with a zero UserId
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.Execute")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "WorkflowService.Execute received invalid parameters: workflowId [1234]; userId [0]")]
        public void Execute_ZeroUserIdTest()
        {
            //
            // Create the objects
            //
            this._testCheckoutActivity.TheStateToReturn = WorkflowState.Started;
            //
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.Execute(_goodWorkflowId, P2rmisActions.Checkin, _zeroUserId, null);
            Assert.Fail("An exception should have been thrown");
        }
        /// <summary>
        /// Method called before each test.
        /// </summary>
        private void ExecuteInitialize()
        {

            this._testCheckoutActivity.TheStateToReturn = WorkflowState.Default;

            _workflowServiceMock = theMock.PartialMock<TestWorkflowService>(theWorkMock);
            _step = new ApplicationWorkflowStep();
        }
        /// <summary>
        /// Method called after each test.
        /// </summary>
        private void ExecuteCleanUp()
        {
            _workflowServiceMock = null;
            _step = null;
        }
        #endregion
        #region ExecuteCheckoutWorkflow Tests
        /// <summary>
        /// Test ExecuteCheckoutWorkflow
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.ExecuteCheckoutWorkflow")]
        public void ExecuteCheckoutWorkflow_Test()
        {
            //
            // Set up Expectations
            //
            Expect.Call(delegate { _workflowServiceMock.Execute(_goodWorkflowId, P2rmisActions.Checkout, _goodUserId, null, null); }).IgnoreArguments();
            theMock.ReplayAll();
            //
            //  Now test - (Result will always return as true even though some times it could return false.  Not sure how to set the return up).
            //
            bool result = _workflowServiceMock.ExecuteCheckoutWorkflow(_goodWorkflowId,_goodUserId);
            //
            // verify
            //
            _workflowServiceMock.AssertWasCalled(x => x.Execute(Arg<int>.Is.Equal(_goodWorkflowId),
                                                    Arg<P2rmisActions>.Is.Equal(P2rmisActions.Checkout),
                                                    Arg<int>.Is.Equal(_goodUserId),
                                                    Arg<IDictionary>.Is.Anything,
                                                    Arg<IDictionary>.Is.Anything));
            Assert.IsTrue(result, "Return value not as expected.");
        }
        /// <summary>
        /// Test ExecuteCheckoutWorkflow for a bad application id.  Testing reliance on Execute for parameter testing.
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.ExecuteCheckoutWorkflow")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "WorkflowService.Execute received invalid parameters: workflowId [-66614]; userId [1111]")]
        public void ExecuteCheckoutWorkflow_BadApplicationIdTest()
        {
            
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.ExecuteCheckoutWorkflow(_badWorkflowId, _goodUserId);
            //
            // verify
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test ExecuteCheckoutWorkflow for a bad application id.  Testing reliance on Execute for parameter testing.
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.ExecuteCheckoutWorkflow")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "WorkflowService.Execute received invalid parameters: workflowId [1234]; userId [-4]")]
        public void ExecuteCheckoutWorkflow_BadUserIdTest()
        {
            //
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.ExecuteCheckoutWorkflow(_goodWorkflowId, _badUserId);
            //
            // verify
            //
            theMock.VerifyAll();
        }
        #endregion
        #region ExecuteCheckinWorkflow Tests
        /// <summary>
        /// Test ExecuteCheckinWorkflow
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.ExecuteCheckinWorkflow")]
        public void ExecuteCheckinWorkflow_Test()
        {
            //
            // Set up Expectations
            //
            Expect.Call(delegate { _workflowServiceMock.Execute(_goodWorkflowId, P2rmisActions.Checkin, _goodUserId, null); });
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.ExecuteCheckinWorkflow(_goodWorkflowId, _goodUserId);
            //
            // verify
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test ExecuteCheckinWorkflow for a bad application id.  Testing reliance on Execute for parameter testing.
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.ExecuteCheckinWorkflow")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "WorkflowService.Execute received invalid parameters: workflowId [-66614]; userId [1111]")]
        public void ExecuteCheckinWorkflow_BadApplicationIdTest()
        {
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.ExecuteCheckinWorkflow(_badWorkflowId, _goodUserId);
            //
            // verify
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test ExecuteCheckinWorkflow for a bad application id.  Testing reliance on Execute for parameter testing.
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.ExecuteCheckinWorkflow")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "WorkflowService.Execute received invalid parameters: workflowId [1234]; userId [-4]")]
        public void ExecuteCheckinWorkflow_BadUserIdTest()
        {
            //
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.ExecuteCheckinWorkflow(_goodWorkflowId, _badUserId);
            //
            // verify
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test completing a workflow - no specific parameters & no results expected back
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.Execute")]
        public void ExecuteOnly_CompleteWorkflowNoSpecificParametersNoResultsTest()
        {
            ASecondTestActivity _testActivity = new ASecondTestActivity();

            string key1 = "key1";
            string key2 = "key2";
            string key3 = "key3";

            string value1 = "value1";
            string value2 = "value2";
            string value3 = "value3";

            IDictionary inSpecificParameters = new Hashtable(3);
            inSpecificParameters.Add(key1, value1);
            inSpecificParameters.Add(key2, value2);
            inSpecificParameters.Add(key3, value3);

            IDictionary resultsList = new Hashtable(3);

            //
            // Create the objects & set up 
            //
            _testActivity.TheStateToReturn = WorkflowState.Complete;
            //
            // Set up Expectations
            //
            Expect.Call(theWorkMock.ApplicationWorkflowRepository).Return(theWorkflowRepositoryMock).Repeat.Times(2);
            Expect.Call(theWorkMock.ApplicationWorkflowStepRepository).Return(theWorkflowStepRepositoryMock);
            Expect.Call(theWorkflowRepositoryMock.GetByID(_goodWorkflowId)).Return(thePartialWorkflowMock);
            Expect.Call(thePartialWorkflowMock.CurrentStep()).Return(_step);
            Expect.Call(thePartialWorkflowMock.IsComplete()).Return(true);
            Expect.Call(delegate { thePartialWorkflowMock.Complete(GOOD_USER_ID); });
            Expect.Call(_workflowServiceMock.CreateActivity(P2rmisActions.Checkin)).Return(_testActivity);
            Expect.Call(delegate { theWorkflowStepRepositoryMock.Update(_step); });
            Expect.Call(delegate { theWorkflowRepositoryMock.Update(thePartialWorkflowMock); });
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.ExecuteOnly(_goodWorkflowId, P2rmisActions.Checkin, _goodUserId, inSpecificParameters, resultsList);
            //
            // verify
            //
            theMock.VerifyAll();
            //
            // Could not seem to get at the input arguments to ensure that they are set.  But can at least ensure the id is ok.
            //
            Assert.AreEqual(_goodUserId.ToString(), this._testCheckoutActivity.UserId.Expression.ToString(), "Activity does not have the userId set");

            Assert.AreEqual(inSpecificParameters.Count, _testActivity.SetParametersList.Count, "In specific parameter list  not the size expected");
            Assert.AreEqual(value1, _testActivity.SetParametersList[key1], "parameter not as expected");
            Assert.AreEqual(value2, _testActivity.SetParametersList[key2], "parameter not as expected");
            Assert.AreEqual(value3, _testActivity.SetParametersList[key3], "parameter not as expected");
            //
            // Now test the out parameters
            //
            Assert.AreEqual(ASecondTestActivity.ResultsCount, resultsList.Count, "Number of parameters return not as expected");
            Assert.AreEqual(ASecondTestActivity.Value1, resultsList[ASecondTestActivity.Result1], "Value of parameters return not as expected for result 1");
            Assert.AreEqual(ASecondTestActivity.Value2, resultsList[ASecondTestActivity.Result2], "Value of parameters return not as expected for result 2");
        }
        #endregion
        #region ExecuteSaveWorkflow Tests
        /// <summary>
        /// Test ExecuteSaveWorkflow
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.ExecuteSaveWorkflow")]
        public void ExecuteSaveWorkflow_Test()
        {
            this._testCheckoutActivity.TheStateToReturn = WorkflowState.Default;
            IDictionary d = new Hashtable(2);
            d[SaveActivity.SaveParameters.ContentId] = _contentId;
            d[SaveActivity.SaveParameters.Content] = _content;
            d[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // Set up Expectations
            //
            Expect.Call(delegate { _workflowServiceMock.Execute(_goodWorkflowId, P2rmisActions.Save, _goodUserId, null); }).IgnoreArguments();
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.ExecuteSaveWorkflow(_goodWorkflowId, _goodUserId, _contentId, _content, _elementId); 
            //
            // verify
            //
            _workflowServiceMock.AssertWasCalled(x => x.Execute(Arg<int>.Is.Equal(_goodWorkflowId),
                                                                Arg<P2rmisActions>.Is.Equal(P2rmisActions.Save),
                                                                Arg<int>.Is.Equal(_goodUserId),
                                                                Arg<IDictionary>.Is.Equal(d)));
        }
        /// <summary>
        /// Test ExecuteSaveWorkflow for a bad application id.  Testing reliance on Execute for parameter testing.
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.ExecuteSaveWorkflow")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "WorkflowService.Execute received invalid parameters: workflowId [-66614]; userId [1111]")]
        public void ExecuteSaveWorkflow_BadApplicationIdTest()
        {
            this._testCheckoutActivity.TheStateToReturn = WorkflowState.Default;
            IDictionary d = new Hashtable(2);
            d[SaveActivity.SaveParameters.ContentId] = _contentId;
            d[SaveActivity.SaveParameters.Content] = _content;
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.ExecuteSaveWorkflow(_badWorkflowId, _goodUserId, _contentId, _content, 99);  //TODO: unit test repair
            //
            // verify
            //
            _workflowServiceMock.AssertWasCalled(x => x.Execute(Arg<int>.Is.Equal(_badWorkflowId),
                                                                Arg<P2rmisActions>.Is.Equal(P2rmisActions.Save),
                                                                Arg<int>.Is.Equal(_goodUserId),
                                                                Arg<IDictionary>.Is.Equal(d)));
        }
        /// <summary>
        /// Test ExecuteSaveWorkflow for a bad application id.  Testing reliance on Execute for parameter testing.
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.ExecuteSaveWorkflow")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "WorkflowService.Execute received invalid parameters: workflowId [1234]; userId [-4]")]
        public void ExecuteSaveWorkflow_BadUserIdTest()
        {
            this._testCheckoutActivity.TheStateToReturn = WorkflowState.Default;
            IDictionary d = new Hashtable(2);
            d[SaveActivity.SaveParameters.ContentId] = _contentId;
            d[SaveActivity.SaveParameters.Content] = _content;
            //
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.ExecuteSaveWorkflow(_goodWorkflowId, _badUserId, _contentId, _content, 99);  //TODO: unit test repair
            //
            // verify
            //
            _workflowServiceMock.AssertWasCalled(x => x.Execute(Arg<int>.Is.Equal(_goodWorkflowId),
                                                                Arg<P2rmisActions>.Is.Equal(P2rmisActions.Save),
                                                                Arg<int>.Is.Equal(_badUserId),
                                                                Arg<IDictionary>.Is.Equal(d)));
        }
        /// <summary>
        /// Test ExecuteSaveWorkflow with bad content id
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.ExecuteSaveWorkflow")]
        public void ExecuteSaveWorkflow_BadContentIdTest()
        {
            this._testCheckoutActivity.TheStateToReturn = WorkflowState.Default;
            IDictionary d = new Hashtable(2);
            d[SaveActivity.SaveParameters.ContentId] = _badContentId;
            d[SaveActivity.SaveParameters.Content] = _content;
            d[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // Set up Expectations
            //
            Expect.Call(delegate { _workflowServiceMock.Execute(_goodWorkflowId, P2rmisActions.Save, _goodUserId, null); }).IgnoreArguments();
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.ExecuteSaveWorkflow(_goodWorkflowId, _goodUserId, _badContentId, _content, _elementId); 
            //
            // verify
            //
            _workflowServiceMock.AssertWasCalled(x => x.Execute(Arg<int>.Is.Equal(_goodWorkflowId),
                                                               Arg<P2rmisActions>.Is.Equal(P2rmisActions.Save),
                                                               Arg<int>.Is.Equal(_goodUserId),
                                                               Arg<IDictionary>.Is.Equal(d)));
        }
        #endregion
        #region ExecuteAssignUser Tests - multiple assigns at a time
        /// <summary>
        /// Test for a single entry in the collections
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.ExecuteAssiguUser")]
        public void ExecuteAssiguUser_MultipleTest()
        {
            int targetStepIdOne = 9;
            int targetStepIdTwo = 99;
            int aGoodId = 456;
            //
            // Set up test data
            // 
            ICollection<AssignWorkflowStep> collection = new List<AssignWorkflowStep>();
            AssignWorkflowStep param = new AssignWorkflowStep(_goodWorkflowId, targetStepIdOne);
            collection.Add(param);
            AssignWorkflowStep param2 = new AssignWorkflowStep(aGoodId, targetStepIdTwo);
            collection.Add(param2);

            //
            // Set up Expectations
            //
            Expect.Call(delegate { _workflowServiceMock.ExecuteOnly(_goodWorkflowId, P2rmisActions.AssignWorkflowStep, _goodUserId, null, null); }).IgnoreArguments().Repeat.Times(2);
            Expect.Call(delegate { theWorkMock.Save(); });
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.ExecuteAssignWorkflow(collection, _goodUserId);
            //
            // verify
            //
            _workflowServiceMock.AssertWasCalled(x => x.ExecuteOnly(Arg<int>.Is.Equal(_goodWorkflowId),
                                                               Arg<P2rmisActions>.Is.Equal(P2rmisActions.AssignWorkflowStep),
                                                               Arg<int>.Is.Equal(_goodUserId),
                                                               Arg<IDictionary>.Is.Anything,
                                                               Arg<IDictionary>.Is.Equal(null)));
            _workflowServiceMock.AssertWasCalled(x => x.ExecuteOnly(Arg<int>.Is.Equal(aGoodId),
                                                   Arg<P2rmisActions>.Is.Equal(P2rmisActions.AssignWorkflowStep),
                                                   Arg<int>.Is.Equal(_goodUserId),
                                                   Arg<IDictionary>.Is.Anything,
                                                   Arg<IDictionary>.Is.Equal(null)));
            //
            // Now check that the methods were called the correct number of times
            //
            _workflowServiceMock.AssertWasCalled(x => x.ExecuteOnly(Arg<int>.Is.Anything,
                                                               Arg<P2rmisActions>.Is.Anything,
                                                               Arg<int>.Is.Anything,
                                                               Arg<IDictionary>.Is.Anything,
                                                               Arg<IDictionary>.Is.Anything), s => s.Repeat.Times(2));
            theWorkMock.AssertWasCalled(x => x.Save(), t => t.Repeat.Times(1));

            //
            // Because the method dynamically allocates one of the parameters we need to check that the arguments are correct.
            //
            // Note:
            //  In general retrieving the arguments to a call works pretty well.  However in this case a specific coding change needed
            //  to be made to support the tests.  What I believe happens is that Rhino mocks saves a reference to each call's parameters.
            //  In this particular case the method allocated a IDictionary object before a loop was entered.  It then cleared the dictionary
            //  added the appropriate values to the dictionary and then called ExecuteOnly().  Simple enough but the assertion on dictionary
            //  value kept failing.  I believe the reason was as stated above.  The object is saved.  Since the object is the same in each case
            //  but with a different value, the test for the first value would always fail.  Once the service method structure was changed
            //  and a dictionary allocated with each iteration of the loop the test started to pass.
            //
            IList<object[]> argsPerCall = _workflowServiceMock.GetArgumentsForCallsMadeOn(x => x.ExecuteOnly(_goodWorkflowId, P2rmisActions.AssignWorkflowStep, _goodUserId, null, null));

            IDictionary callArgs = argsPerCall[0][3] as IDictionary;
            Assert.AreEqual(1, callArgs.Count, "First optional argument list count is not as expected");
            Assert.AreEqual(targetStepIdOne, callArgs[AssignWorkflowStepActivity.SaveParameters.TargetStepId], "Target step id not as expected");

            callArgs = argsPerCall[1][3] as IDictionary;
            Assert.AreEqual(1, callArgs.Count, "Second optional argument list count is not as expected");
            Assert.AreEqual(targetStepIdTwo, callArgs[AssignWorkflowStepActivity.SaveParameters.TargetStepId], "Target step id not as expected");
        }
        /// <summary>
        /// Test for a single entry in the collections
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.ExecuteAssiguUser")]
        public void ExecuteAssiguUser_SingleTest()
        {
            int targetStepId = 9;
            //
            // Set up test data
            // 
            ICollection<AssignWorkflowStep> collection = new List<AssignWorkflowStep>();
            AssignWorkflowStep param = new AssignWorkflowStep(_goodWorkflowId, targetStepId);
            collection.Add(param);

            //
            // Set up Expectations
            //
            Expect.Call(delegate { _workflowServiceMock.ExecuteOnly(_goodWorkflowId, P2rmisActions.AssignWorkflowStep, _goodUserId, null, null); }).IgnoreArguments();
            Expect.Call(delegate { theWorkMock.Save(); });
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.ExecuteAssignWorkflow(collection, _goodUserId);
            //
            // verify
            //
            _workflowServiceMock.AssertWasCalled(x => x.ExecuteOnly(Arg<int>.Is.Equal(_goodWorkflowId),
                                                               Arg<P2rmisActions>.Is.Equal(P2rmisActions.AssignWorkflowStep),
                                                               Arg<int>.Is.Equal(_goodUserId),
                                                               Arg<IDictionary>.Is.Anything,
                                                               Arg<IDictionary>.Is.Equal(null)));
            //
            // Now check that the value is pulled correctly
            //
            IList<object[]> argsPerCall = _workflowServiceMock.GetArgumentsForCallsMadeOn(x => x.ExecuteOnly(_goodWorkflowId, P2rmisActions.AssignWorkflowStep, _goodUserId, null, null));
            IDictionary callArgs = argsPerCall[0][3] as IDictionary;
            Assert.AreEqual(targetStepId, callArgs[AssignWorkflowStepActivity.SaveParameters.TargetStepId], "Target step id not as expected");
            //
            // Now check that the methods were called the correct number of times
            //
            _workflowServiceMock.AssertWasCalled(x => x.ExecuteOnly(Arg<int>.Is.Anything,
                                                             Arg<P2rmisActions>.Is.Anything,
                                                             Arg<int>.Is.Anything,
                                                             Arg<IDictionary>.Is.Anything,
                                                             Arg<IDictionary>.Is.Anything), s => s.Repeat.Times(1));
            theWorkMock.AssertWasCalled(x => x.Save(), t => t.Repeat.Times(1));
        }
        /// <summary>
        /// Test null collection passed.
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.ExecuteAssiguUser")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "WorkflowService.ExecuteAssignWorkflow received invalid parameters: collection is null")]
        public void ExecuteAssiguUser_NullTest()
        {
            //
            // Set up Expectations
            //
            theMock.ReplayAll();
            //
            //  Now test
            //
            _workflowServiceMock.ExecuteAssignWorkflow(null, _goodUserId);
            //
            // If we get here then we be toast
            //
            Assert.Fail();
        }
        #endregion
    }
    /// <summary>
    /// Testing activity
    /// </summary>
    public class TestCheckoutActivity : P2rmisActivity
    {
        /// <summary>
        /// State to return
        /// </summary>
        public WorkflowState TheStateToReturn { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            this.OutState.Set(context, TheStateToReturn);
        }

    }

    /// <summary>
    /// Testing activity
    /// </summary>
    public class ASecondTestActivity : P2rmisActivity
    {
        public static string Result1 = "result1";
        public static string Result2 = "result2";

        public static string Value1 = "value1";
        public static string Value2 = "value2";

        public static int ResultsCount = 2;
        /// <summary>
        /// State to return
        /// </summary>
        public WorkflowState TheStateToReturn { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            this.OutState.Set(context, TheStateToReturn);
        }
        /// <summary>
        /// So we can see what was provided to the activity.
        /// </summary>
        public IDictionary SetParametersList { get; set; }
        public override void SetParameters(IDictionary list)
        {
            base.SetParameters(list);
            SetParametersList = list;
        }

        public override IDictionary GetResults(IDictionary<string, object> results, IDictionary resultsList)
        {
            resultsList[Result1] = Value1;
            resultsList[Result2] = Value2;

            return resultsList;
        }

    }
}
