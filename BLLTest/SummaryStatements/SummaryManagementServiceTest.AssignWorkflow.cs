using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;

using TestMethod = NUnit.Framework.TestAttribute;
using Sra.P2rmis.CrossCuttingServices;

namespace BLLTest.SummaryStatements
{
    /// <summary>
    /// Unit tests for 
    /// </summary>
    [TestClass()] 
    public partial class SummaryManagementServiceTest
    {
        #region Constants
        private int UserId = 444;
        #endregion
        #region The Tests
        /// <summary>
        /// Test for updating a single entry
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowSinglePriorityOneTestWithEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.PriorityOne, true);
            collection.Add(t1);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating multiple entries of the same priority
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowTwoPriorityOneTestWithEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.PriorityOne, true);
            collection.Add(t1);
            TestArguments t2 = new TestArguments(120, 230, ReviewStatu.PriorityOne, true);
            collection.Add(t2);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating multiple entries of the same priority
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowThreePriorityOneTestWithEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.PriorityOne, true);
            collection.Add(t1);
            TestArguments t2 = new TestArguments(120, 230, ReviewStatu.PriorityOne, true);
            collection.Add(t2);
            TestArguments t3 = new TestArguments(1200, 20, ReviewStatu.PriorityOne, true);
            collection.Add(t3);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating a single entry
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowSinglePriorityTwoTestWithEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.PriorityTwo, true);
            collection.Add(t1);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating multiple entries of the same priority
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowTwoPriorityTwoTestWithEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.PriorityTwo, true);
            collection.Add(t1);
            TestArguments t2 = new TestArguments(120, 230, ReviewStatu.PriorityTwo, true);
            collection.Add(t2);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating multiple entries of the same priority
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowThreePriorityTwoTestWithEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.PriorityTwo, true);
            collection.Add(t1);
            TestArguments t2 = new TestArguments(120, 230, ReviewStatu.PriorityTwo, true);
            collection.Add(t2);
            TestArguments t3 = new TestArguments(1200, 20, ReviewStatu.PriorityTwo, true);
            collection.Add(t3);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating a single entry
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowSingleNoPriorityTestWithEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.NoPriority, true);
            collection.Add(t1);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating multiple entries of the same priority
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowTwoNoPriorityTestWithEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.NoPriority, true);
            collection.Add(t1);
            TestArguments t2 = new TestArguments(120, 230, ReviewStatu.NoPriority, true);
            collection.Add(t2);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating multiple entries of the same priority
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowThreeNoPriorityTestWithEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.NoPriority, true);
            collection.Add(t1);
            TestArguments t2 = new TestArguments(120, 230, ReviewStatu.NoPriority, true);
            collection.Add(t2);
            TestArguments t3 = new TestArguments(1200, 20, ReviewStatu.NoPriority, true);
            collection.Add(t3);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating a single entry
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowSinglePriorityOneTestCreateEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.PriorityOne, false);
            collection.Add(t1);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating multiple entries of the same priority
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowTwoPriorityOneTestCreateEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.PriorityOne, false);
            collection.Add(t1);
            TestArguments t2 = new TestArguments(120, 230, ReviewStatu.PriorityOne, false);
            collection.Add(t2);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating multiple entries of the same priority
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowThreePriorityOneTestCreateEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.PriorityOne, false);
            collection.Add(t1);
            TestArguments t2 = new TestArguments(120, 230, ReviewStatu.PriorityOne, false);
            collection.Add(t2);
            TestArguments t3 = new TestArguments(1200, 20, ReviewStatu.PriorityOne, false);
            collection.Add(t3);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating a single entry
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowSinglePriorityTwoTestCreateEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.PriorityTwo, false);
            collection.Add(t1);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating multiple entries of the same priority
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowTwoPriorityTwoTestCreateEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.PriorityTwo, false);
            collection.Add(t1);
            TestArguments t2 = new TestArguments(120, 230, ReviewStatu.PriorityTwo, false);
            collection.Add(t2);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating multiple entries of the same priority
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowThreePriorityTwoTestCreateEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.PriorityTwo, false);
            collection.Add(t1);
            TestArguments t2 = new TestArguments(120, 230, ReviewStatu.PriorityTwo, false);
            collection.Add(t2);
            TestArguments t3 = new TestArguments(1200, 20, ReviewStatu.PriorityTwo, false);
            collection.Add(t3);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating a single entry
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowSingleNoPriorityTestCreateEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.NoPriority, false);
            collection.Add(t1);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating multiple entries of the same priority
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowTwoNoPriorityTestCreateEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.NoPriority, false);
            collection.Add(t1);
            TestArguments t2 = new TestArguments(120, 230, ReviewStatu.NoPriority, false);
            collection.Add(t2);

            AssignWorkflowSuccessTest(collection);
        }
        /// <summary>
        /// Test for updating multiple entries of the same priority
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowThreeNoPriorityTestCreateEntity()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, 20, ReviewStatu.NoPriority, false);
            collection.Add(t1);
            TestArguments t2 = new TestArguments(120, 230, ReviewStatu.NoPriority, false);
            collection.Add(t2);
            TestArguments t3 = new TestArguments(1200, 20, ReviewStatu.NoPriority, false);
            collection.Add(t3);

            AssignWorkflowSuccessTest(collection);
        }






        /// <summary>
        /// Test for invalid parameter - null collection of parameters
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SummaryManagementService.AssignWorkflow detected an invalid parameter.  collection is null? = True; userId = 444")]
        public void AssignWorkflowNullParametertTest()
        {
            testService.AssignWorkflow(null, UserId);
        }
        /// <summary>
        /// Test for empty parameter list.
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        public void AssignWorkflowZeroParametertTest()
        {
            List<TestArguments> collection = new List<TestArguments>();

            AssignWorkflowFailTest(collection);
        }
        /// <summary>
        /// Test for invalid parameters - mechanismId of zero
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SummaryManagementService.AssignWorkflow detected an invalid parameter.  mechanismId = 0; workflowId = 20")]
        public void AssignWorkflowBadMechanismIdParametertTest()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(0, 20, ReviewStatu.NoPriority, false);
            collection.Add(t1);

            AssignWorkflowFailTest(collection);
        }
        /// <summary>
        /// Test for invalid parameters - negative workflowId
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SummaryManagementService.AssignWorkflow detected an invalid parameter.  mechanismId = 10; workflowId = -20")]
        public void AssignWorkflowBadWorkflowIdParametertTest()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(10, -20, ReviewStatu.NoPriority, false);
            collection.Add(t1);

            AssignWorkflowFailTest(collection);
        }
        /// <summary>
        /// Test for both bad parameters
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.AssignWorkflow")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SummaryManagementService.AssignWorkflow detected an invalid parameter.  mechanismId = 0; workflowId = -20")]
        public void AssignWorkflowBadParametertTest()
        {
            List<TestArguments> collection = new List<TestArguments>();

            TestArguments t1 = new TestArguments(0, -20, ReviewStatu.NoPriority, false);
            collection.Add(t1);

            AssignWorkflowFailTest(collection);
        }
        #endregion
        #region Helpers
        public class TestArguments
        {
            public int MechanismId { get; set; }
            public int WorkflowId { get; set; }
            public int? ReviewStatusId { get; set; }
            public WorkflowMechanism Entity { get; set; }

            public TestArguments(int mechanismId, int workflowId, int? reviewStatusId, bool create)
            {
                this.MechanismId = mechanismId;
                this.WorkflowId = workflowId;
                this.ReviewStatusId = reviewStatusId;
                Entity = (create)? new WorkflowMechanism(): null;
            }
        }
        /// <summary>
        /// Test pattern for a failed test.
        /// </summary>
        /// <param name="theArgumentList">Test argument list</param>
        private void AssignWorkflowFailTest(List<TestArguments> theArgumentList)
        {
            List<AssignWorkflowToSave> list = BuildArgumentList(theArgumentList);
            //
            // Finally turn off recording
            //
            theMock.ReplayAll();
            //
            //  Now test
            //
            testService.AssignWorkflow(list, UserId);
        }
        /// <summary>
        /// Builds the argument list from the test argument lists
        /// </summary>
        /// <param name="theArgumentList">Test argument list</param>
        /// <returns></returns>
        private List<AssignWorkflowToSave> BuildArgumentList(List<TestArguments> theArgumentList)
        {
            //
            // Set up local data
            //
            List<AssignWorkflowToSave> list = new List<AssignWorkflowToSave>();
            foreach (var arg in theArgumentList)
            {
                AssignWorkflowToSave a = new AssignWorkflowToSave();
                if (!arg.ReviewStatusId.HasValue)
                {
                    a.SetNoPriorityWorkflow(arg.MechanismId.ToString(), arg.WorkflowId.ToString());
                }
                else if (arg.ReviewStatusId == ReviewStatu.PriorityOne)
                {
                    a.SetPriorityOneWorkflow(arg.MechanismId.ToString(), arg.WorkflowId.ToString());
                }
                else if (arg.ReviewStatusId == ReviewStatu.PriorityTwo)
                {
                    a.SetPriorityTwoWorkflow(arg.MechanismId.ToString(), arg.WorkflowId.ToString());
                }
                else
                {
                    Assert.Fail("Test set up incorrectly");
                }
                list.Add(a);
            }
            return list;
        }
        /// <summary>
        /// Test pattern for a successful test.  There is a bit of a limitation with the test because
        /// one cannot mix Updates & Adds in the same argument list.
        /// </summary>
        /// <param name="theArgumentList">Test argument list</param>
        private void AssignWorkflowSuccessTest(List<TestArguments> theArgumentList)
        {
            //
            // Set up local data
            //
            List<AssignWorkflowToSave> list = BuildArgumentList(theArgumentList);
            //
            // Set up expectations
            //
            Expect.Call(theWorkMock.WorkflowMechanismRepository).Repeat.Any().Return(theWorkflowMechanismRepositoryMock);
            bool shouldSave = false;

            foreach (var arg in theArgumentList)
            {
                Expect.Call(theWorkflowMechanismRepositoryMock.GetByMechanismIdAndReviewStatusId(arg.MechanismId, arg.ReviewStatusId)).Return(arg.Entity);
                if (arg.Entity != null)
                {
                    Expect.Call(delegate { theWorkflowMechanismRepositoryMock.Update(arg.Entity); });
                    shouldSave = true;
                }
                else
                {
                    Expect.Call(delegate { theWorkflowMechanismRepositoryMock.Add(null); }).IgnoreArguments();
                    shouldSave = true;
                }
            }
            if (shouldSave)
            {
                Expect.Call(delegate { theWorkMock.Save(); });
            }
            //
            // Finally turn off recording
            //
            theMock.ReplayAll();
            //
            //  Now test
            //
            testService.AssignWorkflow(list, UserId);
            //
            // verify
            //
            if (theArgumentList[0].Entity == null)
            {
                AddAssertions(theArgumentList);
            }
            else
            {
                UpdateAssertions(theArgumentList);
            }
            //
            // this will verify that Save was called
            //
            theWorkMock.VerifyAllExpectations();
        }
        /// <summary>
        /// Assertions for existing entity tests
        /// </summary>
        /// <param name="theArgumentList">Test argument list</param>
        private void UpdateAssertions(List<TestArguments> theArgumentList)
        {
            //
            // Note: to use GetArgumentsForCallsMadeOn() when using multiple calls one must have distinct argument value.
            // There should not be same values across the calls. For example foo(2, 4) is one and foo(2, 7) would fail.
            //
            IList<object[]> argsPerCall = theWorkflowMechanismRepositoryMock.GetArgumentsForCallsMadeOn(x => x.Update(null));

            for (int i = 0; i < theArgumentList.Count; i++)
            {
                WorkflowMechanism argumentPassed = argsPerCall[i][0] as WorkflowMechanism;

                Assert.IsNotNull(argumentPassed, "Oh no! argumentPassed was null");

                Assert.AreEqual(theArgumentList[i].MechanismId, argumentPassed.MechanismId, "Mechanism id is not correct");
                Assert.AreEqual(theArgumentList[i].WorkflowId, argumentPassed.WorkflowId, "Mechanism id is not correct");
                Assert.AreEqual(UserId, argumentPassed.ModifiedBy, "Modified by is not correct");
                Assert.GreaterOrEqual(GlobalProperties.P2rmisDateTimeNow, argumentPassed.ModifiedDate, "Modified date is not correct");
            }
                theWorkflowMechanismRepositoryMock.VerifyAllExpectations();
        }
        /// <summary>
        /// Assertions for non-existing entity tests
        /// </summary>
        /// <param name="theArgumentList">Argument list</param>
        private void AddAssertions(List<TestArguments> theArgumentList)
        {
            //
            // verify
            //
            // Note: to use GetArgumentsForCallsMadeOn() when using multiple calls one must have distinct argument value.
            // There should not be same values across the calls. For example foo(2, 4) is one and foo(2, 7) would fail.
            //
            IList<object[]> argsPerCall = theWorkflowMechanismRepositoryMock.GetArgumentsForCallsMadeOn(x => x.Add(null));

            for (int i = 0; i < theArgumentList.Count; i++)
            {
                WorkflowMechanism argumentPassed = argsPerCall[i][0] as WorkflowMechanism;

                Assert.IsNotNull(argumentPassed, "Oh no! argumentPassed was null");

                Assert.AreEqual(theArgumentList[i].MechanismId, argumentPassed.MechanismId, "Mechanism id is not correct");
                Assert.AreEqual(theArgumentList[i].WorkflowId, argumentPassed.WorkflowId, "Mechanism id is not correct");
                Assert.AreEqual(UserId, argumentPassed.ModifiedBy, "Modified by is not correct");
                Assert.AreEqual(UserId, argumentPassed.CreatedBy, "Created by is not correct");
                Assert.GreaterOrEqual(GlobalProperties.P2rmisDateTimeNow, argumentPassed.ModifiedDate, "Modified date is not correct");
                Assert.GreaterOrEqual(GlobalProperties.P2rmisDateTimeNow, argumentPassed.CreatedDate, "Created date is not correct");
            }
            theWorkflowMechanismRepositoryMock.AssertWasCalled(s => s.Add(null), yy => yy.Repeat.Times(theArgumentList.Count).IgnoreArguments());
        }
        #endregion

    }
}
