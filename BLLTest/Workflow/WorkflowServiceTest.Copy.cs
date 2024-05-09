using System;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using EntityWorkflow = Sra.P2rmis.Dal.Workflow;

namespace BLLTest.Workflow
{
    /// <summary>
    /// Unit tests for WorkflowService Copy function
    /// </summary>
    [TestClass]
    public partial class WorkflowServiceTest
    {
        private int _AWorkflowId = 456789;
        private int _negativeWorkflowId = -6;
        private int _zero = 0;
        #region The Tests
        /// <summary>
        /// Test a successful copy
        /// </summary>
        [TestMethod]
        [Category("WorkflowService.Copy")] 
        public void GoodCopyTest()
        {
            //
            // create standard stuff
            //
            Tuple<MockRepository, IUnitOfWork, IWorkflowTemplateRepository> t = Create();
            MockRepository mocks = t.Item1;
            IUnitOfWork workMock = t.Item2;
            IWorkflowTemplateRepository repositoryMock = t.Item3;
            //
            // Now we need to set this to be returned when it is called from the UnitOfWork
            //
            SetupResult.For(workMock.WorkflowTemplateRepository).Return(repositoryMock);
            //
            // Set the repository to return the ReportClientProgramListResultModel when the DL is called
            //
            EntityWorkflow w = new EntityWorkflow();
            Expect.Call(repositoryMock.GetByID(_AWorkflowId)).Return(w);          // getById call
            Expect.Call(delegate { repositoryMock.Add(w); }).IgnoreArguments();   // add call
            Expect.Call(workMock.Save);                                           // save call
            //
            // Finally create the service.  Had to create a "test" service class & change the visibility
            // of the UnitOfWork to make this work with mocking
            //
            IWorkflowService serviceMock = mocks.StrictMock<TestWorkflowService>(workMock);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            bool b = serviceMock.Copy(_AWorkflowId, GOOD_USER_ID);
            //
            // Now what do we get?
            //
            Assert.IsTrue(b, "Failed to do the copy?");
        }
        /// <summary>
        /// Test not able to locate workflow
        /// </summary>
        [TestMethod]
        [Category("WorkflowService.Copy")]
        public void CantFindWorkflowTest()
        {
            //
            // create standard stuff
            //
            Tuple<MockRepository, IUnitOfWork, IWorkflowTemplateRepository> t = Create();
            MockRepository mocks = t.Item1;
            IUnitOfWork workMock = t.Item2;
            IWorkflowTemplateRepository repositoryMock = t.Item3;
            //
            // Now we need to set this to be returned when it is called from the UnitOfWork
            //
            SetupResult.For(workMock.WorkflowTemplateRepository).Return(repositoryMock);
            //
            // Set the repository to return the ReportClientProgramListResultModel when the DL is called
            //
            Expect.Call(repositoryMock.GetByID(_AWorkflowId)).Return(null);          // getById call
            //
            // Finally create the service.  Had to create a "test" service class & change the visibility
            // of the UnitOfWork to make this work with mocking
            //
            IWorkflowService serviceMock = mocks.StrictMock<TestWorkflowService>(workMock);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            bool b = serviceMock.Copy(_AWorkflowId, GOOD_USER_ID);
            //
            // Now what do we get?
            //
            Assert.IsFalse(b, "Did the copy but it should not have found it?");
        }
        /// <summary>
        /// Test for a negative workflow ID
        /// </summary>
        [TestMethod]
        [Category("WorkflowService.Copy")]
        public void NegativeWorkflowIdTest()
        {
            //
            // create standard stuff
            //
            Tuple<MockRepository, IUnitOfWork, IWorkflowTemplateRepository> t = Create();
            MockRepository mocks = t.Item1;
            IUnitOfWork workMock = t.Item2;
            IWorkflowTemplateRepository repositoryMock = t.Item3;
            //
            // Finally create the service.  Had to create a "test" service class & change the visibility
            // of the UnitOfWork to make this work with mocking
            //
            IWorkflowService serviceMock = mocks.StrictMock<TestWorkflowService>(workMock);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            bool b = serviceMock.Copy(_negativeWorkflowId, GOOD_USER_ID);
            //
            // Now what do we get?
            //
            Assert.IsFalse(b, "Should not have done copy!");
        }
        /// <summary>
        /// Test for a zero workflow ID
        /// </summary>
        [TestMethod]
        [Category("WorkflowService.Copy")]
        public void ZeroWorkflowIdTest()
        {
            //
            // create standard stuff
            //
            Tuple<MockRepository, IUnitOfWork, IWorkflowTemplateRepository> t = Create();
            MockRepository mocks = t.Item1;
            IUnitOfWork workMock = t.Item2;
            IWorkflowTemplateRepository repositoryMock = t.Item3;
            //
            // Finally create the service.  Had to create a "test" service class & change the visibility
            // of the UnitOfWork to make this work with mocking
            //
            IWorkflowService serviceMock = mocks.StrictMock<TestWorkflowService>(workMock);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            bool b = serviceMock.Copy(_zero, GOOD_USER_ID);
            //
            // Now what do we get?
            //
            Assert.IsFalse(b, "Should not have done copy!");
        }
        /// <summary>
        /// Test for a negative user ID
        /// </summary>
        [TestMethod]
        [Category("WorkflowService.Copy")]
        public void NegativeUserIdTest()
        {
            //
            // create standard stuff
            //
            Tuple<MockRepository, IUnitOfWork, IWorkflowTemplateRepository> t = Create();
            MockRepository mocks = t.Item1;
            IUnitOfWork workMock = t.Item2;
            IWorkflowTemplateRepository repositoryMock = t.Item3;
            //
            // Finally create the service.  Had to create a "test" service class & change the visibility
            // of the UnitOfWork to make this work with mocking
            //
            IWorkflowService serviceMock = mocks.StrictMock<TestWorkflowService>(workMock);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            bool b = serviceMock.Copy(_AWorkflowId, BAD_USER_ID);
            //
            // Now what do we get?
            //
            Assert.IsFalse(b, "Should not have done copy!");
        }
        /// <summary>
        /// Test for a zero user ID
        /// </summary>
        [TestMethod]
        [Category("WorkflowService.Copy")]
        public void ZeroUserIdTest()
        {
            //
            // create standard stuff
            //
            Tuple<MockRepository, IUnitOfWork, IWorkflowTemplateRepository> t = Create();
            MockRepository mocks = t.Item1;
            IUnitOfWork workMock = t.Item2;
            IWorkflowTemplateRepository repositoryMock = t.Item3;
            //
            // Finally create the service.  Had to create a "test" service class & change the visibility
            // of the UnitOfWork to make this work with mocking
            //
            IWorkflowService serviceMock = mocks.StrictMock<TestWorkflowService>(workMock);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            bool b = serviceMock.Copy(_AWorkflowId, ZERO_USER_ID);
            //
            // Now what do we get?
            //
            Assert.IsFalse(b, "Should not have done copy!");
        }
        /// <summary>
        /// Test for a negative user ID
        /// </summary>
        [TestMethod]
        [Category("WorkflowService.Copy")]
        public void NegativeIdTest()
        {
            //
            // create standard stuff
            //
            Tuple<MockRepository, IUnitOfWork, IWorkflowTemplateRepository> t = Create();
            MockRepository mocks = t.Item1;
            IUnitOfWork workMock = t.Item2;
            IWorkflowTemplateRepository repositoryMock = t.Item3;
            //
            // Finally create the service.  Had to create a "test" service class & change the visibility
            // of the UnitOfWork to make this work with mocking
            //
            IWorkflowService serviceMock = mocks.StrictMock<TestWorkflowService>(workMock);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            bool b = serviceMock.Copy(_negativeWorkflowId, BAD_USER_ID);
            //
            // Now what do we get?
            //
            Assert.IsFalse(b, "Should not have done copy!");
        }
        /// <summary>
        /// Test for a zero user ID
        /// </summary>
        [TestMethod]
        [Category("WorkflowService.Copy")]
        public void ZeroIdTest()
        {
            //
            // create standard stuff
            //
            Tuple<MockRepository, IUnitOfWork, IWorkflowTemplateRepository> t = Create();
            MockRepository mocks = t.Item1;
            IUnitOfWork workMock = t.Item2;
            IWorkflowTemplateRepository repositoryMock = t.Item3;
            //
            // Finally create the service.  Had to create a "test" service class & change the visibility
            // of the UnitOfWork to make this work with mocking
            //
            IWorkflowService serviceMock = mocks.StrictMock<TestWorkflowService>(workMock);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            bool b = serviceMock.Copy(_zero, ZERO_USER_ID);
            //
            // Now what do we get?
            //
            Assert.IsFalse(b, "Should not have done copy!");
        }
        #endregion

   }
}
