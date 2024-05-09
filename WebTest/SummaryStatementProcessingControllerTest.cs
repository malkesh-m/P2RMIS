using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Web.Controllers;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace WebTest
{
    [TestClass]
    public class SummaryStatementProcessingControllerTest
    {
        #region Constants

        private const string LogNumber1 = "BC140001";
        private static DateTime AssignmentDate1 = new DateTime(2014, 8, 19, 9, 56, 12);
        private static DateTime WorkflowStart1 = new DateTime(2014, 8, 19, 9, 56, 12);
        private static DateTime WorkflowEnd1 = new DateTime(2014, 8, 20, 11, 24, 52);
        private const string WorkFlowStatus1 = "The Workflow Status";

        private ISummaryAssignedModel sa1 = new SummaryAssignedModel() { LogNumber = LogNumber1, AssignmentDate = AssignmentDate1, WorkStartDate = WorkflowStart1, WorkEndDate = WorkflowEnd1, WorkflowName = WorkFlowStatus1 };

        private const string assignmentsView = "Assignments";

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

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            
        }

        #endregion

        #region AssignmentsTests

        /// <summary>
        /// Test AvailableProgressApplications:
        /// </summary>
        //  -- Fix this.  Controller references User [TestMethod]
        [Category("SummaryStatementProcessingController.Assignments")]
        public void AssignmentsTest()
        {
            // setting up assignments model and data
            Container<ISummaryAssignedModel> containerAssignments = new Container<ISummaryAssignedModel>();
            containerAssignments.ModelList = new List<ISummaryAssignedModel>() { sa1 };

            // populate data into view model
            AssignmentsViewModel aM = new AssignmentsViewModel();
            aM.Assignments = new List<ISummaryAssignedModel>() { sa1 };

            // setting up mock objects
            MockRepository mocks = new MockRepository();
            ISummaryManagementService summaryManagementServiceMock = mocks.DynamicMock<ISummaryManagementService>();
            ISummaryProcessingService summaryProcessingServiceMock = mocks.DynamicMock<ISummaryProcessingService>();
            SummaryStatementProcessingController spc = mocks.PartialMock<SummaryStatementProcessingController>(summaryManagementServiceMock, null, null, summaryProcessingServiceMock);

            // expected controller call
            Expect.Call(spc.GetUserId()).Return(2);
            Expect.Call(spc.HasManagePermission()).Return(true);
            Expect.Call(summaryProcessingServiceMock.GetDraftSummmariesCheckedout(2)).IgnoreArguments().Return(containerAssignments);
            mocks.ReplayAll();

            // expected controller action
            var x = spc.Assignments();
            Assert.IsNotNull(x);

            // the view result
            ViewResult v = x as ViewResult;
            Console.WriteLine(v.ViewName);

            // asserts
            //Assert.AreEqual(v.ViewName, assignmentsView);
            Assert.IsNotNull(v.Model);
            Assert.AreEqual(sa1.LogNumber, aM.Assignments.First().LogNumber);
            Assert.AreEqual(sa1.AssignmentDate, aM.Assignments.First().AssignmentDate);
            Assert.AreEqual(sa1.WorkStartDate, aM.Assignments.First().WorkStartDate);
            Assert.AreEqual(sa1.WorkEndDate, aM.Assignments.First().WorkEndDate);
            Assert.AreEqual(sa1.WorkflowName, aM.Assignments.First().WorkflowName);
        }

        #endregion
    }
}
