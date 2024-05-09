using System.Linq;
using NUnit.Framework;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
		
namespace DLLTest.Partials
{
    /// <summary>
    /// Unit tests for PanelApplication
    /// </summary>
    [TestClass()]
    public class PanelApplicationTest: DllBaseTest
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
        #region Reorder Tests
        /// <summary>
        /// Test setting the review order
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void ReorderValueTest()
        {
            int currentReviewOrder = 3;
            int newReviewOrder = 15;
            PanelApplication panel = new PanelApplication { ReviewOrder = currentReviewOrder };

            panel.Reorder(newReviewOrder, _goodUserId);

            Assert.AreEqual(newReviewOrder, panel.ReviewOrder, "panel reviewOrder was not as expected");
            VerifyCreatedDateUnChanged(panel);
            VerifyModifiedDate(panel, _goodUserId);
        }

        /// <summary>
        /// Test setting the review order to null
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void ReorderNullTest()
        {
            int currentReviewOrder = 3;
            PanelApplication panel = new PanelApplication { ReviewOrder = currentReviewOrder };

            panel.Reorder(null, _goodUserId);

            Assert.IsNull(panel.ReviewOrder, "panel reviewOrder was not as expected");
            VerifyCreatedDateUnChanged(panel);
            VerifyModifiedDate(panel, _goodUserId);
        }
        #endregion
        #region SetReviewStatus Tests
        /// <summary>
        /// Test setting an application with a Triaged review status to Triaged.
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void SetReviewStatusIsTriageTrueTriagedTest()
        {
            //
            // Set up local data
            //
            PanelApplication panel = new PanelApplication();
            ApplicationReviewStatu reviewStatus = new ApplicationReviewStatu { ReviewStatusId = ReviewStatu.Triaged };
            panel.ApplicationReviewStatus.Add(reviewStatus);
            //
            // Test
            //
            panel.SetReviewStatus(true, _goodUserId);
            //
            // Verify
            //
            Assert.AreEqual(1, panel.ApplicationReviewStatus.Count(), "number of review status is not as expected");
            Assert.AreEqual(ReviewStatu.Triaged, panel.ApplicationReviewStatus.ElementAt(0).ReviewStatusId, "status id is not as expected");
            VerifyCreatedDateUnChanged(reviewStatus);
            VerifyModifiedDateUnChanged(reviewStatus);
        }
        /// <summary>
        /// Test setting an application with a non Triaged review status to Triaged. (single entry)
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void SetReviewStatusIsTriageTrueNotTriagedTest()
        {
            //
            // Set up local data
            //
            PanelApplication panel = new PanelApplication();
            ApplicationReviewStatu reviewStatus = new ApplicationReviewStatu { ReviewStatusId = ReviewStatu.FullReview };
            panel.ApplicationReviewStatus.Add(reviewStatus);
            //
            // Test
            //
            panel.SetReviewStatus(true, _goodUserId);
            //
            // Verify
            //
            Assert.AreEqual(1, panel.ApplicationReviewStatus.Count(), "number of review status is not as expected");
            Assert.AreEqual(ReviewStatu.Triaged, panel.ApplicationReviewStatus.ElementAt(0).ReviewStatusId, "status id is not as expected");
            VerifyCreatedDateUnChanged(reviewStatus);
            VerifyModifiedDate(reviewStatus, _goodUserId);
        }
        /// <summary>
        /// Test setting an application with a non Triaged review status to Triaged. (single entry)
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void SetReviewStatusIsTriageTrueMultipleNotTriagedTest()
        {
            //
            // Set up local data
            //
            PanelApplication panel = new PanelApplication();
            ApplicationReviewStatu reviewStatus = new ApplicationReviewStatu { ReviewStatusId = ReviewStatu.FullReview };
            panel.ApplicationReviewStatus.Add(reviewStatus);
            panel.ApplicationReviewStatus.Add(new ApplicationReviewStatu { ReviewStatusId = ReviewStatu.PriorityOne });
            //
            // Test
            //
            panel.SetReviewStatus(true, _goodUserId);
            //
            // Verify
            //
            Assert.AreEqual(1, panel.ApplicationReviewStatus.Count(), "number of review status is not as expected");
            Assert.AreEqual(ReviewStatu.Triaged, panel.ApplicationReviewStatus.ElementAt(0).ReviewStatusId, "status id is not as expected");
            VerifyCreatedDateUnChanged(reviewStatus);
            VerifyModifiedDate(reviewStatus, _goodUserId);
        }
        /// <summary>
        /// Test setting an application with a non Triaged review status to Triaged. (single entry)
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void SetReviewStatusIsTriageFalseMultipleNotTriagedTest()
        {
            //
            // Set up local data
            //
            PanelApplication panel = new PanelApplication();
            ApplicationReviewStatu reviewStatus1 = new ApplicationReviewStatu { ReviewStatusId = ReviewStatu.FullReview };
            panel.ApplicationReviewStatus.Add(reviewStatus1);
            ApplicationReviewStatu reviewStatus2 = new ApplicationReviewStatu { ReviewStatusId = ReviewStatu.PriorityOne };
            panel.ApplicationReviewStatus.Add(reviewStatus2);
            //
            // Test
            //
            panel.SetReviewStatus(false, _goodUserId);
            //
            // Verify
            //
            Assert.AreEqual(2, panel.ApplicationReviewStatus.Count(), "number of review status is not as expected");
            Assert.AreEqual(ReviewStatu.FullReview, panel.ApplicationReviewStatus.ElementAt(0).ReviewStatusId, "status id is not as expected");
            Assert.AreEqual(ReviewStatu.PriorityOne, panel.ApplicationReviewStatus.ElementAt(1).ReviewStatusId, "status id is not as expected");
            VerifyCreatedDateUnChanged(reviewStatus1);
            VerifyModifiedDateUnChanged(reviewStatus1);
            VerifyCreatedDateUnChanged(reviewStatus2);
            VerifyModifiedDateUnChanged(reviewStatus2);
        }
        /// <summary>
        /// Test setting an application with a Triaged review status to not Triaged. (single entry)
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void SetReviewStatusIsTriageFalsesingleTriagedTest()
        {
            //
            // Set up local data
            //
            PanelApplication panel = new PanelApplication();
            ApplicationReviewStatu reviewStatus1 = new ApplicationReviewStatu { ReviewStatusId = ReviewStatu.Triaged };
            panel.ApplicationReviewStatus.Add(reviewStatus1);
            //
            // Test
            //
            panel.SetReviewStatus(false, _goodUserId);
            //
            // Verify
            //
            Assert.AreEqual(1, panel.ApplicationReviewStatus.Count(), "number of review status is not as expected");
            Assert.AreEqual(ReviewStatu.FullReview, panel.ApplicationReviewStatus.ElementAt(0).ReviewStatusId, "status id is not as expected");
            VerifyCreatedDateUnChanged(reviewStatus1);
            VerifyModifiedDate(reviewStatus1, _goodUserId);
        }
        #endregion
        #region CurrentPresentationOrders Tests
        /// <summary>
        /// Test - values for all sort orders
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void AllValuesForPresentationOrdersTest()
        {
            //
            // Set up local data
            //
            PanelApplication aPanelApplication = new PanelApplication();
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = 2 });
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = 1 });
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = 4 });
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = 3 });

            int count = aPanelApplication.PanelApplicationReviewerAssignments.Count();
            //
            // Test
            //
            var result = aPanelApplication.CurrentPresentationOrders();
            //
            // Verify
            //
            Assert.IsNotNull(result, "Result was null & it should not be");
            Assert.AreEqual(count, result.Count(), "Unexpected number of reviewer assignments returned.");

            var list2 = result;
            list2.Sort();

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(list2[i], result[i], "array was not sorted");
            }
        }
        /// <summary>
        /// Test - values for all sort orders
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void ANullValueForPresentationOrdersTest()
        {
            //
            // Set up local data
            //
            PanelApplication aPanelApplication = new PanelApplication();
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = 2 });
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = 1 });
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = null });
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = 3 });

            int count = aPanelApplication.PanelApplicationReviewerAssignments.Count();
            count--; // adjust for the null
            //
            // Test
            //
            var result = aPanelApplication.CurrentPresentationOrders();
            //
            // Verify
            //
            Assert.IsNotNull(result, "Result was null & it should not be");
            Assert.AreEqual(count, result.Count(), "Unexpected number of reviewer assignments returned.");

            var list2 = result;
            list2.Sort();

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(list2[i], result[i], "array was not sorted");
            }
        }
        /// <summary>
        /// Test - no reviewer assignments
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void NoValuesForPresentationOrdersTest()
        {
            //
            // Set up local data
            //
            PanelApplication aPanelApplication = new PanelApplication();

            int count = aPanelApplication.PanelApplicationReviewerAssignments.Count();
            //
            // Test
            //
            var result = aPanelApplication.CurrentPresentationOrders();
            //
            // Verify
            //
            Assert.IsNotNull(result, "Result was null & it should not be");
            Assert.AreEqual(count, result.Count(), "Unexpected number of reviewer assignments returned.");
        }
		
        #endregion
        #region IsDisapprovedStatus
        /// <summary>
        /// Test ReviewStatus = Triaged 
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void IsDisapprovedTriagedTest()
        {
            //
            // Set up local data
            //
            PanelApplication aPanelApplication = MakeIsDisapprovedTestPanelApplication(ReviewStatu.Triaged);
            //
            // Test
            //
            bool result = aPanelApplication.IsDisapprovedStatus();
            //
            // Verify
            //
            Assert.IsFalse(result, "panel application should not be Disapproved");
        }
        /// <summary>
        /// Test ReviewStatus =  FullReview
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void IsDisapprovedFullReviewTest()
        {
            //
            // Set up local data
            //
            PanelApplication aPanelApplication = MakeIsDisapprovedTestPanelApplication(ReviewStatu.FullReview);
            //
            // Test
            //
            bool result = aPanelApplication.IsDisapprovedStatus();
            //
            // Verify
            //
            Assert.IsFalse(result, "panel application should not be Disapproved");
        }
        /// <summary>
        /// Test ReviewStatus = PriorityOne 
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void IsDisapprovedPriorityOneTest()
        {
            //
            // Set up local data
            //
            PanelApplication aPanelApplication = MakeIsDisapprovedTestPanelApplication(ReviewStatu.PriorityOne);
            //
            // Test
            //
            bool result = aPanelApplication.IsDisapprovedStatus();
            //
            // Verify
            //
            Assert.IsFalse(result, "panel application should not be Disapproved");
        }
        /// <summary>
        /// Test ReviewStatus = PriorityTwo 
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void IsDisapprovedPriorityTwoTest()
        {
            //
            // Set up local data
            //
            PanelApplication aPanelApplication = MakeIsDisapprovedTestPanelApplication(ReviewStatu.PriorityTwo);
            //
            // Test
            //
            bool result = aPanelApplication.IsDisapprovedStatus();
            //
            // Verify
            //
            Assert.IsFalse(result, "panel application should not be Disapproved");
        }
        /// <summary>
        /// Test ReviewStatus = Disapproved 
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void IsDisapprovedDisapprovedTest()
        {
            //
            // Set up local data
            //
            PanelApplication aPanelApplication = MakeIsDisapprovedTestPanelApplication(ReviewStatu.Disapproved);
            //
            // Test
            //
            bool result = aPanelApplication.IsDisapprovedStatus();
            //
            // Verify
            //
            Assert.IsTrue(result, "panel application should be Disapproved");
        }
        /// <summary>
        /// Test ReviewStatus = null 
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void IsDisapprovedNullTest()
        {
            //
            // Set up local data
            //
            PanelApplication aPanelApplication = new PanelApplication();
            ApplicationReviewStatu status = new ApplicationReviewStatu ();
            aPanelApplication.ApplicationReviewStatus.Add(status);
            //
            // Test
            //
            bool result = aPanelApplication.IsDisapprovedStatus();
            //
            // Verify
            //
            Assert.IsFalse(result, "panel application should not be Disapproved");
        }
        #region Helpers
        private PanelApplication MakeIsDisapprovedTestPanelApplication(int reviewStatusId)
        {
            PanelApplication aPanelApplication = new PanelApplication();
            ApplicationReviewStatu status = new ApplicationReviewStatu { ReviewStatusId = reviewStatusId };
            aPanelApplication.ApplicationReviewStatus.Add(status);
            return aPanelApplication;
        }
        #endregion
        #endregion
        #region SynchornousReviewStage Tests
        /// <summary>
        /// Test Review stage
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void IsReviewTest()
        {
            //
            // Set up local data
            //
            PanelApplication aPanelApplication = new PanelApplication();
            ApplicationStage stage0 = new ApplicationStage { ReviewStageId = ReviewStage.Synchronous };
            ApplicationStage stage1 = new ApplicationStage { ReviewStageId = ReviewStage.Summary };
            ApplicationStage stage2 = new ApplicationStage { ReviewStageId = ReviewStage.Asynchronous };
            aPanelApplication.ApplicationStages.Add(stage1);
            aPanelApplication.ApplicationStages.Add(stage0);
            aPanelApplication.ApplicationStages.Add(stage2);
            //
            // Test
            //
            var result = aPanelApplication.SynchronousReviewStage();
            //
            // Verify
            //
            Assert.AreEqual(result, stage0, "panel application should be Review stage");
        }
        /// <summary>
        /// Test no Review stage
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void IsNoReviewTest()
        {
            //
            // Set up local data
            //
            PanelApplication aPanelApplication = new PanelApplication();
            ApplicationStage stage1 = new ApplicationStage { ReviewStageId = ReviewStage.Summary };
            ApplicationStage stage2 = new ApplicationStage { ReviewStageId = ReviewStage.Asynchronous };
            aPanelApplication.ApplicationStages.Add(stage1);
            aPanelApplication.ApplicationStages.Add(stage2);
            //
            // Test
            //
            var result = aPanelApplication.SynchronousReviewStage();
            //
            // Verify
            //
            Assert.IsNull(result, "panel application should not be Review stage");
        }
        #endregion
        #region IsStageScored Tests
        /// <summary>
        /// Test IsStageScored
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void SimpleNotScoredTest()
        {
            //
            // Set up local data
            //
            PanelApplication pa = new PanelApplication();
            var stage = MakeIsStageScoredTestPanelApplication(false);
            //
            // Test
            //
            bool result = pa.IsStageScored(stage);
            //
            // Verify
            //
            Assert.IsFalse(result, "result should not be scored");
        }
        /// <summary>
        /// Test IsStageScored
        /// </summary>
        [TestMethod()]
        [Category("PanelApplication")]
        public void SimpleScoredTest()
        {
            //
            // Set up local data
            //
            PanelApplication pa = new PanelApplication();
            var stage = MakeIsStageScoredTestPanelApplication(true);
            //
            // Test
            //
            bool result = pa.IsStageScored(stage);
            //
            // Verify
            //
            Assert.IsTrue(result, "result should be scored");
        }		
        #region Helpers
        private ApplicationStage MakeIsStageScoredTestPanelApplication(bool scored)
        {
            ApplicationStage stage = new ApplicationStage();
            //
            // the workflow
            //
            var workflow0 = new ApplicationWorkflow();
            stage.ApplicationWorkflows.Add(workflow0);
            var workflow1 = new ApplicationWorkflow();
            stage.ApplicationWorkflows.Add(workflow1);
            var workflow2 = new ApplicationWorkflow();
            stage.ApplicationWorkflows.Add(workflow2);
            //
            // workflow step
            //
            ApplicationWorkflowStep s0 = new ApplicationWorkflowStep();
            workflow0.ApplicationWorkflowSteps.Add(s0);

            ApplicationWorkflowStep s1 = new ApplicationWorkflowStep();
            workflow1.ApplicationWorkflowSteps.Add(s1);

            ApplicationWorkflowStep s2 = new ApplicationWorkflowStep();
            workflow2.ApplicationWorkflowSteps.Add(s2);
            //
            // step Element
            //
            ApplicationWorkflowStepElement se0 = new ApplicationWorkflowStepElement();
            s0.ApplicationWorkflowStepElements.Add(se0);

            ApplicationWorkflowStepElement se1 = new ApplicationWorkflowStepElement();
            s1.ApplicationWorkflowStepElements.Add(se1);

            ApplicationWorkflowStepElement se2 = new ApplicationWorkflowStepElement();
            s2.ApplicationWorkflowStepElements.Add(se2);
            //
            // content
            //
            ApplicationWorkflowStepElementContent c0 = new ApplicationWorkflowStepElementContent();
            c0.Score = (scored) ? 3 : (decimal?)null;
            se0.ApplicationWorkflowStepElementContents.Add(c0);
            ApplicationWorkflowStepElementContent c1 = new ApplicationWorkflowStepElementContent();
            c1.Score = (scored) ? 4 : (decimal?)null;
            se1.ApplicationWorkflowStepElementContents.Add(c0); 
            ApplicationWorkflowStepElementContent c2 = new ApplicationWorkflowStepElementContent();
            c2.Score = (scored) ? 1 : (decimal?)null;
            se2.ApplicationWorkflowStepElementContents.Add(c2);
            return stage;
        }
        #endregion
        #endregion
        #region GetUsersExpertiseOnApplication Tests
        /// <summary>
        /// Test GetUsersExpertiseOnApplication
        /// </summary>
        [TestMethod()]
        [Category("GetUsersExpertiseOnApplication")]
        public void GetUsersExpertiseOnApplicationEqualOneTest()
        {
            //
            // Set up local data
            //
            int panelUserAssignmentId = 4;
            PanelApplication pa = new PanelApplication();
            PanelApplicationReviewerExpertise pare = new PanelApplicationReviewerExpertise { PanelUserAssignmentId = panelUserAssignmentId };
            pa.PanelApplicationReviewerExpertises.Add(pare);
            //
            // Test
            //
            var result = pa.GetUsersExpertiseOnApplication(panelUserAssignmentId);
            //
            // Verify
            //
            Assert.IsNotNull(result, "result was null and it should not have been");
            Assert.AreEqual(panelUserAssignmentId, result.PanelUserAssignmentId, "Did not match id's");
        }
        /// <summary>
        /// Test GetUsersExpertiseOnApplication
        /// </summary>
        [TestMethod()]
        [Category("GetUsersExpertiseOnApplication")]
        public void GetUsersExpertiseOnApplicationNotEqualOneTest()
        {
            //
            // Set up local data
            //
            int panelUserAssignmentId = 4;
            PanelApplication pa = new PanelApplication();
            PanelApplicationReviewerExpertise pare = new PanelApplicationReviewerExpertise { PanelUserAssignmentId = panelUserAssignmentId };
            pa.PanelApplicationReviewerExpertises.Add(pare);
            //
            // Test
            //
            var result = pa.GetUsersExpertiseOnApplication(6);
            //
            // Verify
            //
            Assert.IsNull(result, "result was not null and it should have been");
        }		
        /// <summary>
        /// Test GetUsersExpertiseOnApplication
        /// </summary>
        [TestMethod()]
        [Category("GetUsersExpertiseOnApplication")]
        public void GetUsersExpertiseOnApplicationEqualMultipleTest()
        {
            //
            // Set up local data
            //
            int panelUserAssignmentId = 4;
            PanelApplication pa = new PanelApplication();
            PanelApplicationReviewerExpertise pare1 = new PanelApplicationReviewerExpertise { PanelUserAssignmentId = 5 };
            PanelApplicationReviewerExpertise pare2 = new PanelApplicationReviewerExpertise { PanelUserAssignmentId = panelUserAssignmentId };
            PanelApplicationReviewerExpertise pare3 = new PanelApplicationReviewerExpertise { PanelUserAssignmentId = 9 };
            pa.PanelApplicationReviewerExpertises.Add(pare1);
            pa.PanelApplicationReviewerExpertises.Add(pare2);
            pa.PanelApplicationReviewerExpertises.Add(pare3);
            //
            // Test
            //
            var result = pa.GetUsersExpertiseOnApplication(panelUserAssignmentId);
            //
            // Verify
            //
            Assert.IsNotNull(result, "result was null and it should not have been");
            Assert.AreEqual(panelUserAssignmentId, result.PanelUserAssignmentId, "Did not match id's");
        }
        /// <summary>
        /// Test GetUsersExpertiseOnApplication
        /// </summary>
        [TestMethod()]
        [Category("GetUsersExpertiseOnApplication")]
        public void GetUsersExpertiseOnApplicationNotEqualMultipleTest()
        {
            //
            // Set up local data
            //
            int panelUserAssignmentId = 4;
            PanelApplication pa = new PanelApplication();
            PanelApplicationReviewerExpertise pare1 = new PanelApplicationReviewerExpertise { PanelUserAssignmentId = 5 };
            PanelApplicationReviewerExpertise pare2 = new PanelApplicationReviewerExpertise { PanelUserAssignmentId = 12 };
            PanelApplicationReviewerExpertise pare3 = new PanelApplicationReviewerExpertise { PanelUserAssignmentId = 9 };
            pa.PanelApplicationReviewerExpertises.Add(pare1);
            pa.PanelApplicationReviewerExpertises.Add(pare2);
            pa.PanelApplicationReviewerExpertises.Add(pare3);
            //
            // Test
            //
            var result = pa.GetUsersExpertiseOnApplication(panelUserAssignmentId);
            //
            // Verify
            //
            Assert.IsNull(result, "result was not null and it should have been");
        }
        #endregion
        #region ReviewWorkflowStepForThisReviewer Tests
        /// <summary>
        /// Test ReviewWorkflowStepForThisReviewerTest
        /// </summary>
        [TestMethod()]
        [Category("ReviewWorkflowStepForThisReviewer")]
        public void ReviewWorkflowStepForThisReviewerTest()
        {
            //
            // Set up local data
            //
            int panelUserAssignmentId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationStage s = new ApplicationStage { ReviewStageId = ReviewStage.Synchronous };
            pa.ApplicationStages.Add(s);
            //
            // The expectation is that there is only a single workflow
            //
            ApplicationWorkflow aw = new ApplicationWorkflow { PanelUserAssignmentId = panelUserAssignmentId};
            s.ApplicationWorkflows.Add(aw);
            int stepId = 987867;
            ApplicationWorkflowStep aws = new ApplicationWorkflowStep { ApplicationWorkflowStepId = stepId};
            aw.ApplicationWorkflowSteps.Add(aws);
            //
            // Test
            //
            var result = pa.ReviewWorkflowStepForThisReviewer(panelUserAssignmentId);
            //
            // Verify
            //
            Assert.IsNotNull(result, "result was null and it should have been not null");
            Assert.IsInstanceOf<ApplicationWorkflowStep>(result);
            Assert.AreEqual(stepId, result.ApplicationWorkflowStepId, "ApplicationWorkflowStepIds not as expected");
        }
        #endregion
        #region ClientId Tests
        /// <summary>
        /// Test ClientId
        /// </summary>
        [TestMethod()]
        [Category("ClientId")]
        public void ClientIdTest()
        {
            //
            // Set up local data
            //
            int clientId = 4;
            ClientMeeting cm = new ClientMeeting {ClientId = clientId };
            MeetingSession ms = new MeetingSession {ClientMeeting = cm };
            SessionPanel sp = new SessionPanel {MeetingSession = ms };
            PanelApplication pa = new PanelApplication { SessionPanel = sp };    
            //
            // Test
            //
            var result = pa.ClientId();
            //
            // Verify
            //
            Assert.AreEqual(clientId, result, "ClientId() did not return expected value");
        }
        #endregion
        #region IsCurrentDiscussion Tests
        /// <summary>
        /// Test IsCurrentDiscussion
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentDiscussion")]
        public void IsCurrentDiscussionTest()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu {ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.Active };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentDiscussion();
            //
            // Verify
            //
            Assert.IsTrue(result, "result was not true and it should have been");
       }
        /// <summary>
        /// Test IsCurrentDiscussion
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentDiscussion")]
        public void IsCurrentDiscussionNotTest()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.Disapproved };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentDiscussion();
            //
            // Verify
            //
            Assert.IsFalse(result, "result was true and it should have been false");
       }
        /// <summary>
        /// Test IsCurrentDiscussion
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentDiscussion")]
        public void IsCurrentDiscussionNotTestFullReview()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.FullReview };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentDiscussion();
            //
            // Verify
            //
            Assert.IsFalse(result, "result was true and it should have been false");
       }
        /// <summary>
        /// Test IsCurrentDiscussion
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentDiscussion")]
        public void IsCurrentDiscussionNotTestPriorityOne()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.PriorityOne };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentDiscussion();
            //
            // Verify
            //
            Assert.IsFalse(result, "result was true and it should have been false");
       }
        /// <summary>
        /// Test IsCurrentDiscussion
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentDiscussion")]
        public void IsCurrentDiscussionNotTestPriorityTwo()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.PriorityTwo };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentDiscussion();
            //
            // Verify
            //
            Assert.IsFalse(result, "result was true and it should have been false");
       }
        /// <summary>
        /// Test IsCurrentDiscussion
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentDiscussion")]
        public void IsCurrentDiscussionNotTestReadyToScore()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.ReadyToScore };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentDiscussion();
            //
            // Verify
            //
            Assert.IsFalse(result, "result was true and it should have been false");
       }
        /// <summary>
        /// Test IsCurrentDiscussion
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentDiscussion")]
        public void IsCurrentDiscussionNotTestScored()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.Scored };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentDiscussion();
            //
            // Verify
            //
            Assert.IsFalse(result, "result was true and it should have been false");
       }
        /// <summary>
        /// Test IsCurrentDiscussion
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentDiscussion")]
        public void IsCurrentDiscussionNotTestScoring()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.Scoring };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentDiscussion();
            //
            // Verify
            //
            Assert.IsFalse(result, "result was true and it should have been false");
        }
        /// <summary>
        /// Test IsCurrentDiscussion
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentDiscussion")]
        public void IsCurrentDiscussionNotTestTriaged()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.Triaged };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentDiscussion();
            //
            // Verify
            //
            Assert.IsFalse(result, "result was true and it should have been false");
       }
       #endregion
        #region IsCurrentlyScoring Tests
        /// <summary>
        /// Test IsCurrentlyScoring
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentlyScoring")]
        public void IsCurrentlyScoringNotTestActive()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.Active };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentlyScoring();
            //
            // Verify
            //
            Assert.IsFalse(result, "result was true and it should have been false");
        }
        /// <summary>
        /// Test IsCurrentlyScoring
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentlyScoring")]
        public void IsCurrentlyScoringNotTest()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.Disapproved };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentlyScoring();
            //
            // Verify
            //
            Assert.IsFalse(result, "result was true and it should have been false");
        }
        /// <summary>
        /// Test IsCurrentlyScoring
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentlyScoring")]
        public void IsCurrentlyScoringNotTestFullReview()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.FullReview };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentlyScoring();
            //
            // Verify
            //
            Assert.IsFalse(result, "result was true and it should have been false");
        }
        /// <summary>
        /// Test IsCurrentlyScoring
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentlyScoring")]
        public void IsCurrentlyScoringNotTestPriorityOne()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.PriorityOne };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentlyScoring();
            //
            // Verify
            //
            Assert.IsFalse(result, "result was true and it should have been false");
        }
        /// <summary>
        /// Test IsCurrentlyScoring
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentlyScoring")]
        public void IsCurrentlyScoringNotTestPriorityTwo()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.PriorityTwo };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentlyScoring();
            //
            // Verify
            //
            Assert.IsFalse(result, "result was true and it should have been false");
        }
        /// <summary>
        /// Test IsCurrentlyScoring
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentlyScoring")]
        public void IsCurrentlyScoringNotTestReadyToScore()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.ReadyToScore };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentlyScoring();
            //
            // Verify
            //
            Assert.IsFalse(result, "result was true and it should have been false");
        }
        /// <summary>
        /// Test IsCurrentlyScoring
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentlyScoring")]
        public void IsCurrentlyScoringNotTestScored()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.Scored };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentlyScoring();
            //
            // Verify
            //
            Assert.IsFalse(result, "result was true and it should have been false");
        }
        /// <summary>
        /// Test IsCurrentlyScoring
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentlyScoring")]
        public void IsCurrentlyScoringNotTestScoring()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.Scoring };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentlyScoring();
            //
            // Verify
            //
            Assert.IsTrue(result, "result was false and it should have been true");
        }
        /// <summary>
        /// Test IsCurrentlyScoring
        /// </summary>
        [TestMethod()]
        [Category("IsCurrentlyScoring")]
        public void IsCurrentlyScoringNotTestTriaged()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.Triaged };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;

            //
            // Test
            //
            var result = pa.IsCurrentlyScoring();
            //
            // Verify
            //
            Assert.IsFalse(result, "result was true and it should have been false");
        }
        #endregion
        #region GetCurrentReviewStage Test
        /// <summary>
        /// Test GetCurrentReviewStage
        /// </summary>
        [TestMethod()]
        [Category("GetCurrentReviewStage")]
        public void GetCurrentReviewStageScoredTest()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.Scored };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;
            //
            // Test
            //
            var result = pa.GetCurrentReviewStage();
            //
            // Verify
            //
            Assert.AreEqual(ReviewStage.Synchronous, result, "result was not as not the correct ReviewStage");
        }
        /// <summary>
        /// Test GetCurrentReviewStage
        /// </summary>
        [TestMethod()]
        [Category("GetCurrentReviewStage")]
        public void GetCurrentReviewStageScoringTest()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.Scoring };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;
            //
            // Test
            //
            var result = pa.GetCurrentReviewStage();
            //
            // Verify
            //
            Assert.AreEqual(ReviewStage.Synchronous, result, "result was not as not the correct ReviewStage");
        }
        /// <summary>
        /// Test GetCurrentReviewStage
        /// </summary>
        [TestMethod()]
        [Category("GetCurrentReviewStage")]
        public void GetCurrentReviewStageActiveTest()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.Active };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;
            //
            // Test
            //
            var result = pa.GetCurrentReviewStage();
            //
            // Verify
            //
            Assert.AreEqual(ReviewStage.Asynchronous, result, "result was not as not the correct ReviewStage");
        }
        /// <summary>
        /// Test GetCurrentReviewStage
        /// </summary>
        [TestMethod()]
        [Category("GetCurrentReviewStage")]
        public void GetCurrentReviewStageDisapprovedTest()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.Disapproved };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;
            //
            // Test
            //
            var result = pa.GetCurrentReviewStage();
            //
            // Verify
            //
            Assert.AreEqual(ReviewStage.Asynchronous, result, "result was not as not the correct ReviewStage");
        }
        /// <summary>
        /// Test GetCurrentReviewStage
        /// </summary>
        [TestMethod()]
        [Category("GetCurrentReviewStage")]
        public void GetCurrentReviewStagePriorityOneTest()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.PriorityOne };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;
            //
            // Test
            //
            var result = pa.GetCurrentReviewStage();
            //
            // Verify
            //
            Assert.AreEqual(ReviewStage.Asynchronous, result, "result was not as not the correct ReviewStage");
        }
        /// <summary>
        /// Test GetCurrentReviewStage
        /// </summary>
        [TestMethod()]
        [Category("GetCurrentReviewStage")]
        public void GetCurrentReviewStagePriorityTwoTest()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.PriorityTwo };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;
            //
            // Test
            //
            var result = pa.GetCurrentReviewStage();
            //
            // Verify
            //
            Assert.AreEqual(ReviewStage.Asynchronous, result, "result was not as not the correct ReviewStage");
        }
        /// <summary>
        /// Test GetCurrentReviewStage
        /// </summary>
        [TestMethod()]
        [Category("GetCurrentReviewStage")]
        public void GetCurrentReviewStageReadyToScoreTest()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.ReadyToScore };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;
            //
            // Test
            //
            var result = pa.GetCurrentReviewStage();
            //
            // Verify
            //
            Assert.AreEqual(ReviewStage.Asynchronous, result, "result was not as not the correct ReviewStage");
        }
        /// <summary>
        /// Test GetCurrentReviewStage
        /// </summary>
        [TestMethod()]
        [Category("GetCurrentReviewStage")]
        public void GetCurrentReviewStageTriagedTest()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.Triaged };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Review };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;
            //
            // Test
            //
            var result = pa.GetCurrentReviewStage();
            //
            // Verify
            //
            Assert.AreEqual(ReviewStage.Asynchronous, result, "result was not as not the correct ReviewStage");
        }
        /// <summary>
        /// Test GetCurrentReviewStage
        /// </summary>
        [TestMethod()]
        [Category("GetCurrentReviewStage")]
        public void GetCurrentReviewStageNonReviewStageTest()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ApplicationReviewStatusId = applicationReviewStatusId, ReviewStatusId = ReviewStatu.Triaged };
            ReviewStatu rs = new ReviewStatu { ReviewStatusTypeId = ReviewStatusType.Summary };

            pa.ApplicationReviewStatus.Add(ars);
            ars.ReviewStatu = rs;
            //
            // Test
            //
            var result = pa.GetCurrentReviewStage();
            //
            // Verify
            //
            Assert.AreEqual(ReviewStage.Asynchronous, result, "result was not as not the correct ReviewStage");
        }
        #endregion
        #region ListReviewers Test
        /// <summary>
        /// Test ListReviewers
        /// </summary>
        [TestMethod()]
        [Category("ListReviewers")]
        public void ListReviewersTest()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId = 4;
            PanelApplication pa = new PanelApplication();
            PanelApplicationReviewerAssignment pua = new PanelApplicationReviewerAssignment { PanelUserAssignmentId = applicationReviewStatusId };
            pa.PanelApplicationReviewerAssignments.Add(pua);
            //
            // Test
            //
            var result = pa.ListReviewers();
            //
            // Verify
            //
            Assert.IsNotNull(result, "result was null and it should not have been");
            Assert.AreEqual(1, result.Count(), "Incorrect number of entries was returned");
            Assert.AreEqual(applicationReviewStatusId, result[0], "unexpected value returned");
        }        
        /// <summary>
        /// Test ListReviewers
        /// </summary>
        [TestMethod()]
        [Category("ListReviewers")]
        public void ListReviewersNoTest()
        {
            //
            // Set up local data
            //
            PanelApplication pa = new PanelApplication();
            //
            // Test
            //
            var result = pa.ListReviewers();
            //
            // Verify
            //
            Assert.IsNotNull(result, "result was null and it should not have been");
            Assert.AreEqual(0, result.Count(), "Incorrect number of entries was returned");
        }
        /// <summary>
        /// Test ListReviewers
        /// </summary>
        [TestMethod()]
        [Category("ListReviewers")]
        public void ListReviewersMultipleTest()
        {
            //
            // Set up local data
            //
            int applicationReviewStatusId1 =  41;
            int applicationReviewStatusId2 = 42;
            int applicationReviewStatusId3 = 43;
            int applicationReviewStatusId4 = 44;
            int applicationReviewStatusId5 = 45;
            int applicationReviewStatusId6 = 46;
            PanelApplication pa = new PanelApplication();
            PanelApplicationReviewerAssignment pua1 = new PanelApplicationReviewerAssignment { PanelUserAssignmentId = applicationReviewStatusId1 };
            PanelApplicationReviewerAssignment pua2 = new PanelApplicationReviewerAssignment { PanelUserAssignmentId = applicationReviewStatusId2 };
            PanelApplicationReviewerAssignment pua3 = new PanelApplicationReviewerAssignment { PanelUserAssignmentId = applicationReviewStatusId3 };
            PanelApplicationReviewerAssignment pua4 = new PanelApplicationReviewerAssignment { PanelUserAssignmentId = applicationReviewStatusId4 };
            PanelApplicationReviewerAssignment pua5 = new PanelApplicationReviewerAssignment { PanelUserAssignmentId = applicationReviewStatusId5 };
            PanelApplicationReviewerAssignment pua6 = new PanelApplicationReviewerAssignment { PanelUserAssignmentId = applicationReviewStatusId6 };
            pa.PanelApplicationReviewerAssignments.Add(pua1);
            pa.PanelApplicationReviewerAssignments.Add(pua2);
            pa.PanelApplicationReviewerAssignments.Add(pua3);
            pa.PanelApplicationReviewerAssignments.Add(pua4);
            pa.PanelApplicationReviewerAssignments.Add(pua5);
            pa.PanelApplicationReviewerAssignments.Add(pua6);
            //
            // Test
            //
            var result = pa.ListReviewers();
            //
            // Verify
            //
            Assert.IsNotNull(result, "result was null and it should not have been");
            Assert.AreEqual(6, result.Count(), "Incorrect number of entries was returned");
            Assert.AreEqual(applicationReviewStatusId1, result[0], "unexpected value returned");
            Assert.AreEqual(applicationReviewStatusId2, result[1], "unexpected value returned");
            Assert.AreEqual(applicationReviewStatusId3, result[2], "unexpected value returned");
            Assert.AreEqual(applicationReviewStatusId4, result[3], "unexpected value returned");
            Assert.AreEqual(applicationReviewStatusId5, result[4], "unexpected value returned");
            Assert.AreEqual(applicationReviewStatusId6, result[5], "unexpected value returned");
        }
        #endregion
        #region IsActiveStatus Tests
        /// <summary>
        /// Test IsActiveStatus
        /// </summary>
        [TestMethod()]
        [Category("IsActiveStatus")]
        public void IsActiveStatusTriagedTest()
        {
            //
            // Set up local data
            //
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ReviewStatusId = ReviewStatu.Triaged };
            pa.ApplicationReviewStatus.Add(ars);
            //
            // Test
            //
            var result = pa.IsActiveStatus();
            //
            // Verify
            //
            Assert.IsFalse(result, "Active status should be false");
        }       
        /// <summary>
        /// Test IsActiveStatus
        /// </summary>
        [TestMethod()]
        [Category("IsActiveStatus")]
        public void IsActiveStatusScoringTest()
        {
            //
            // Set up local data
            //
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ReviewStatusId = ReviewStatu.Scoring };
            pa.ApplicationReviewStatus.Add(ars);
            //
            // Test
            //
            var result = pa.IsActiveStatus();
            //
            // Verify
            //
            Assert.IsTrue(result, "Active status should be true");
        }
        /// <summary>
        /// Test IsActiveStatus
        /// </summary>
        [TestMethod()]
        [Category("IsActiveStatus")]
        public void IsActiveStatusScoredTest()
        {
            //
            // Set up local data
            //
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ReviewStatusId = ReviewStatu.Scored };
            pa.ApplicationReviewStatus.Add(ars);
            //
            // Test
            //
            var result = pa.IsActiveStatus();
            //
            // Verify
            //
            Assert.IsFalse(result, "Active status should be false");
        }
        /// <summary>
        /// Test IsActiveStatus
        /// </summary>
        [TestMethod()]
        [Category("IsActiveStatus")]
        public void IsActiveStatusReadyToScoreTest()
        {
            //
            // Set up local data
            //
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ReviewStatusId = ReviewStatu.ReadyToScore };
            pa.ApplicationReviewStatus.Add(ars);
            //
            // Test
            //
            var result = pa.IsActiveStatus();
            //
            // Verify
            //
            Assert.IsFalse(result, "Active status should be false");
        }
        /// <summary>
        /// Test IsActiveStatus
        /// </summary>
        [TestMethod()]
        [Category("IsActiveStatus")]
        public void IsActiveStatusPriorityTwoTest()
        {
            //
            // Set up local data
            //
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ReviewStatusId = ReviewStatu.PriorityTwo };
            pa.ApplicationReviewStatus.Add(ars);
            //
            // Test
            //
            var result = pa.IsActiveStatus();
            //
            // Verify
            //
            Assert.IsFalse(result, "Active status should be false");
        }
        /// <summary>
        /// Test IsActiveStatus
        /// </summary>
        [TestMethod()]
        [Category("IsActiveStatus")]
        public void IsActiveStatusPriorityOneTest()
        {
            //
            // Set up local data
            //
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ReviewStatusId = ReviewStatu.PriorityOne };
            pa.ApplicationReviewStatus.Add(ars);
            //
            // Test
            //
            var result = pa.IsActiveStatus();
            //
            // Verify
            //
            Assert.IsFalse(result, "Active status should be false");
        }
        /// <summary>
        /// Test IsActiveStatus
        /// </summary>
        [TestMethod()]
        [Category("IsActiveStatus")]
        public void IsActiveStatusFullReviewTest()
        {
            //
            // Set up local data
            //
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ReviewStatusId = ReviewStatu.FullReview };
            pa.ApplicationReviewStatus.Add(ars);
            //
            // Test
            //
            var result = pa.IsActiveStatus();
            //
            // Verify
            //
            Assert.IsFalse(result, "Active status should be false");
        }
        /// <summary>
        /// Test IsActiveStatus
        /// </summary>
        [TestMethod()]
        [Category("IsActiveStatus")]
        public void IsActiveStatusDisapprovedTest()
        {
            //
            // Set up local data
            //
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ReviewStatusId = ReviewStatu.Disapproved };
            pa.ApplicationReviewStatus.Add(ars);
            //
            // Test
            //
            var result = pa.IsActiveStatus();
            //
            // Verify
            //
            Assert.IsFalse(result, "Active status should be false");
        }
        /// <summary>
        /// Test IsActiveStatus
        /// </summary>
        [TestMethod()]
        [Category("IsActiveStatus")]
        public void IsActiveStatusActiveTest()
        {
            //
            // Set up local data
            //
            PanelApplication pa = new PanelApplication();
            ApplicationReviewStatu ars = new ApplicationReviewStatu { ReviewStatusId = ReviewStatu.Active };
            pa.ApplicationReviewStatus.Add(ars);
            //
            // Test
            //
            var result = pa.IsActiveStatus();
            //
            // Verify
            //
            Assert.IsTrue(result, "Active status should be true");
        }
        #endregion
        #region UsersAssignmentType Tests
        /// <summary>
        /// Test UsersAssignmentType
        /// </summary>
        [TestMethod()]
        [Category("UsersAssignmentType")]
        public void UsersAssignmentTypeTest()
        {
            //
            // Set up local data
            //
            int userId = 4;
            int panelApplicationReviewerAssignmentId = 15;

            PanelApplication pa = new PanelApplication();
            PanelUserAssignment pua = new PanelUserAssignment { UserId = userId };
            PanelApplicationReviewerAssignment pra = new PanelApplicationReviewerAssignment { PanelUserAssignment = pua, PanelApplicationReviewerAssignmentId = panelApplicationReviewerAssignmentId};
            pa.PanelApplicationReviewerAssignments.Add(pra);
            //
            // Test
            //
            var result = pa.UsersAssignmentType(userId);
            //
            // Verify
            //
            Assert.IsNotNull(result, "result should not be null");
            Assert.AreEqual(panelApplicationReviewerAssignmentId, result.PanelApplicationReviewerAssignmentId, "Unexpected PanelApplicationReviewerAssignment returned");
        }
        /// <summary>
        /// Test UsersAssignmentType
        /// </summary>
        [TestMethod()]
        [Category("UsersAssignmentType")]
        public void UsersAssignmentTypeNotThereTest()
        {
            //
            // Set up local data
            //
            int userId = 4;
            int panelApplicationReviewerAssignmentId = 15;

            PanelApplication pa = new PanelApplication();
            PanelUserAssignment pua = new PanelUserAssignment { UserId = userId };
            PanelApplicationReviewerAssignment pra = new PanelApplicationReviewerAssignment { PanelUserAssignment = pua, PanelApplicationReviewerAssignmentId = panelApplicationReviewerAssignmentId };
            pa.PanelApplicationReviewerAssignments.Add(pra);
            //
            // Test
            //
            var result = pa.UsersAssignmentType(userId + 1);
            //
            // Verify
            //
            Assert.IsNull(result, "result should be null");
        }
        /// <summary>
        /// Test UsersAssignmentType
        /// </summary>
        [TestMethod()]
        [Category("UsersAssignmentType")]
        public void UsersAssignmentTypeMultipeTest()
        {
            //
            // Set up local data
            //
            int userId1 = 41;
            int userId2 = 42;
            int userId3 = 43;
            int userId4 = 44;
            int panelApplicationReviewerAssignmentId1 = 151;
            int panelApplicationReviewerAssignmentId2 = 152;
            int panelApplicationReviewerAssignmentId3 = 153;
            int panelApplicationReviewerAssignmentId4 = 154;

            PanelApplication pa = new PanelApplication();

            PanelUserAssignment pua1 = new PanelUserAssignment { UserId = userId1 };
            PanelApplicationReviewerAssignment pra1 = new PanelApplicationReviewerAssignment { PanelUserAssignment = pua1, PanelApplicationReviewerAssignmentId = panelApplicationReviewerAssignmentId1 };
            pa.PanelApplicationReviewerAssignments.Add(pra1);

            PanelUserAssignment pua2 = new PanelUserAssignment { UserId = userId2 };
            PanelApplicationReviewerAssignment pra2 = new PanelApplicationReviewerAssignment { PanelUserAssignment = pua2, PanelApplicationReviewerAssignmentId = panelApplicationReviewerAssignmentId2 };
            pa.PanelApplicationReviewerAssignments.Add(pra2);

            PanelUserAssignment pua3 = new PanelUserAssignment { UserId = userId3 };
            PanelApplicationReviewerAssignment pra3 = new PanelApplicationReviewerAssignment { PanelUserAssignment = pua3, PanelApplicationReviewerAssignmentId = panelApplicationReviewerAssignmentId3 };
            pa.PanelApplicationReviewerAssignments.Add(pra3);

            PanelUserAssignment pua4 = new PanelUserAssignment { UserId = userId4 };
            PanelApplicationReviewerAssignment pra4 = new PanelApplicationReviewerAssignment { PanelUserAssignment = pua4, PanelApplicationReviewerAssignmentId = panelApplicationReviewerAssignmentId4 };
            pa.PanelApplicationReviewerAssignments.Add(pra4);

            //
            // Test
            //
            var result = pa.UsersAssignmentType(userId3);
            //
            // Verify
            //
            Assert.IsNotNull(result, "result should not be null");
            Assert.AreEqual(panelApplicationReviewerAssignmentId3, result.PanelApplicationReviewerAssignmentId, "Unexpected PanelApplicationReviewerAssignment returned");
        }
        #endregion
    }	
}
