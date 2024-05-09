using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;

namespace BLLTest.PanelManagement
{
    /// <summary>
    /// Unit tests for PanelManagementService 
    /// </summary>
    [TestClass()]
    public partial class PanelManagementServiceTests : BLLBaseTest
    {
        #region ListApplicationInformation Tests
        /// <summary>
        /// Test successful return from PanelManagement repository call.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListApplicationInformation")]
        public void ListApplicationInformationSuccessfulTest()
        {
            //
            // Set up local data
            //
            ResultModel<IApplicationInformationModel> resultModel = new ResultModel<IApplicationInformationModel>();
            List<IApplicationInformationModel> list = new List<IApplicationInformationModel>();
            list.Add(new ApplicationInformationModel { LogNumber = "AB12345" });
            list.Add(new ApplicationInformationModel { LogNumber = "AB67899" });
            list.Add(new ApplicationInformationModel { LogNumber = "AB09876" });
            resultModel.ModelList = list;
	        //
	        // Test
	        //
            ListApplicationInformationSuccessTest(resultModel, 34);
        }
        /// <summary>
        /// Test zero sessionPanelId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListApplicationInformation")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListApplicationInformation received an invalid parameter: sessionPanelId: [0].")]
        public void ListApplicationInformationZeroSessionPanelIdTest()
        {
            //
            // Test
            //
            ListApplicationInformationFailTest(0);
        }
        /// <summary>
        /// Test negative sessionPanelId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListApplicationInformation")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListApplicationInformation received an invalid parameter: sessionPanelId: [-5].")]
        public void ListApplicationInformationNegativeessionPanelIdTest()
        {
            //
            // Test
            //
            ListApplicationInformationFailTest(-5);
        }
        #region Helpers
        /// <summary>
        /// Test steps for a successful test for ListApplicationInformation
        /// </summary>
        /// <param name="resultModel">ResultModel to return</param>
        /// <param name="sessionPanelId">SessionUPanelId to test</param>
        private void ListApplicationInformationSuccessTest(ResultModel<IApplicationInformationModel> resultModel, int sessionPanelId)
        {
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.PanelManagementRepository).Return(thePanelManagementRepositoryMock);
            Expect.Call(thePanelManagementRepositoryMock.ListApplicationInformation(sessionPanelId)).Return(resultModel);

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestPanelManagementService.ListApplicationInformation(sessionPanelId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test steps for a failure test for ListApplicationInformation
        /// </summary>
        /// <param name="sessionPanelId">SessionUPanelId to test</param>
        private void ListApplicationInformationFailTest(int sessionPanelId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = theTestPanelManagementService.ListApplicationInformation(sessionPanelId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theWorkMock.AssertWasNotCalled(s => s.PanelManagementRepository);
        }
        #endregion
        #endregion

        #region ListProgramYears Tests
        /// <summary>
        /// Test successful return from PanelManagement repository call.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListProgramYears")]
        public void ListProgramYearsSuccessfulTest()
        {
            //
            // Set up local data
            //
            ResultModel<IProgramYearModel> resultModel = new ResultModel<IProgramYearModel>();
            List<IProgramYearModel> list = new List<IProgramYearModel>();
            list.Add(new ProgramYearModel { FY = "2011" });
            list.Add(new ProgramYearModel { FY = "2012" });
            list.Add(new ProgramYearModel { FY = "2013" });
            resultModel.ModelList = list;
            //
            // Test
            //
            ListProgramYearsSuccessTest(resultModel, 2);
        }
        /// <summary>
        /// Test negative userId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListProgramYears")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListProgramYears detected an invalid parameter: userId was -5")]
        public void ListProgramYearsNegativeUserIdTest()
        {
            //
            // Test
            //
            ListProgramYearsFailTest(-5);
        }
        #region Helpers
        /// <summary>
        /// Test steps for a successful test for ListProgramYears
        /// </summary>
        /// <param name="resultModel">ResultModel to return</param>
        /// <param name="userId">userId to test</param>
        private void ListProgramYearsSuccessTest(ResultModel<IProgramYearModel> resultModel, int userId)
        {
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.PanelManagementRepository).Return(thePanelManagementRepositoryMock);
            Expect.Call(thePanelManagementRepositoryMock.ListProgramYears(userId)).Return(resultModel);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = new Container<IProgramYearModel>();
            container = this.theTestPanelManagementService.ListProgramYears(userId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test steps for a failure test for ListProgramYears
        /// </summary>
        /// <param name="userId">userId to test</param>
        private void ListProgramYearsFailTest(int userId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = new Container<IProgramYearModel>();
            container = theTestPanelManagementService.ListProgramYears(userId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theWorkMock.AssertWasNotCalled(s => s.PanelManagementRepository);
        }
        #endregion
        #endregion

        #region ListPanelSignification Tests
        /// <summary>
        /// Test successful return from PanelManagement repository call.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPanelSignification")]
        public void ListPanelSignificationForSrmSuccessfulTest()
        {
            //
            // Set up local data
            //
            ResultModel<IPanelSignificationsModel> resultModel = new ResultModel<IPanelSignificationsModel>();
            List<IPanelSignificationsModel> list = new List<IPanelSignificationsModel>();
            list.Add(new PanelSignificationsModel { PanelName = "Panel1" });
            list.Add(new PanelSignificationsModel { PanelName = "Panel2" });
            list.Add(new PanelSignificationsModel { PanelName = "Panel3" });
            resultModel.ModelList = list;
            //
            // Test
            //
            ListPanelSignificationSuccessTest(resultModel, 2, 3);
        }
        /// <summary>
        /// Test successful return from PanelManagement repository call.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPanelSignification")]
        public void ListPanelSignificationForSroSuccessfulTest()
        {
            //
            // Set up local data
            //
            ResultModel<IPanelSignificationsModel> resultModel = new ResultModel<IPanelSignificationsModel>();
            List<IPanelSignificationsModel> list = new List<IPanelSignificationsModel>();
            list.Add(new PanelSignificationsModel { PanelName = "Panel1" });
            list.Add(new PanelSignificationsModel { PanelName = "Panel2" });
            list.Add(new PanelSignificationsModel { PanelName = "Panel3" });
            resultModel.ModelList = list;
            //
            // Test
            //
            ListPanelSignificationSuccessTest(resultModel, 2, null);
        }
        /// <summary>
        /// Test negative userId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPanelSignification")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListPanelSignifications detected an invalid parameter: userId was -5")]
        public void ListPanelSignificationNegativeUserIdTest()
        {
            //
            // Test
            //
            ListPanelSignificationFailTest(-5, 3);
        }
        /// <summary>
        /// Test negative programYearId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPanelSignification")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListPanelSignifications detected an invalid parameter: programYearId was -5")]
        public void ListPanelSignificationNegativeProgramYearIdTest()
        {
            //
            // Test
            //
            ListPanelSignificationFailTest(2, -5);
        }
        #region Helpers
        /// <summary>
        /// Test steps for a successful test for ListPanelSignification
        /// </summary>
        /// <param name="resultModel">ResultModel to return</param>
        /// <param name="userId">userId to test</param>
        /// <param name="programYearId">ProgramYearId to test</param>
        private void ListPanelSignificationSuccessTest(ResultModel<IPanelSignificationsModel> resultModel, int userId, int? programYearId)
        {
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.PanelManagementRepository).Return(thePanelManagementRepositoryMock);
            if (programYearId == null)
                Expect.Call(thePanelManagementRepositoryMock.ListPanelSignifications(userId)).Return(resultModel);
            else
                Expect.Call(thePanelManagementRepositoryMock.ListPanelSignifications(userId, (int)programYearId)).Return(resultModel);
            
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = new Container<IPanelSignificationsModel>();
            if (programYearId == null)
                container = this.theTestPanelManagementService.ListPanelSignifications(userId);
            else
                container = this.theTestPanelManagementService.ListPanelSignifications(userId, (int)programYearId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test steps for a failure test for ListPanelSignification
        /// </summary>
        /// <param name="userId">userId to test</param>
        /// <param name="programYearId">ProgramYearId to test</param>
        private void ListPanelSignificationFailTest(int userId, int? programYearId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
             var container = new Container<IPanelSignificationsModel>();
            if (programYearId == null)
                container = theTestPanelManagementService.ListPanelSignifications(userId);
            else
                container = theTestPanelManagementService.ListPanelSignifications(userId, (int)programYearId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theWorkMock.AssertWasNotCalled(s => s.PanelManagementRepository);
        }

        #endregion
        #endregion

        #region ListPersonnelWithCoi Tests
        /// <summary>
        /// Test successful return from PanelManagement repository call.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPersonnelWithCoi")]
        public void ListPersonnelWithCoiSuccessfulTest()
        {
            //
            // Set up local data
            //
            ResultModel<IPersonnelWithCoi> resultModel = new ResultModel<IPersonnelWithCoi>();
            List<IPersonnelWithCoi> list = new List<IPersonnelWithCoi>();
            list.Add(new PersonnelWithCoi { FirstName = "Mary", LastName = "Smith" });
            list.Add(new PersonnelWithCoi { FirstName = "Tom", LastName = "Jackson" });
            list.Add(new PersonnelWithCoi { FirstName = "Jennifer", LastName = "Lawrence" });
            resultModel.ModelList = list;
            //
            // Test
            //
            ListPersonnelWithCoiSuccessTest(resultModel, 34);
        }
        /// <summary>
        /// Test zero userId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPersonnelWithCoi")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListPersonnelWithCoi detected an invalid parameter: applicationId was 0")]
        public void ListPersonnelWithCoiZeroApplicationIdTest()
        {
            //
            // Test
            //
            ListPersonnelWithCoiFailTest(0);
        }
        /// <summary>
        /// Test negative userId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPersonnelWithCoi")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListPersonnelWithCoi detected an invalid parameter: applicationId was -5")]
        public void ListPersonnelWithCoiNegativeApplicationIdTest()
        {
            //
            // Test
            //
            ListPersonnelWithCoiFailTest(-5);
        }
        #region Helpers
        /// <summary>
        /// Test steps for a successful test for ListPersonnelWithCoi
        /// </summary>
        /// <param name="resultModel">ResultModel to return</param>
        /// <param name="applicationId">the application identifier to test</param>
        private void ListPersonnelWithCoiSuccessTest(ResultModel<IPersonnelWithCoi> resultModel, int applicationId)
        {
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.PanelManagementRepository).Return(thePanelManagementRepositoryMock);
            Expect.Call(thePanelManagementRepositoryMock.ListPersonnelWithCoi(applicationId)).Return(resultModel);

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestPanelManagementService.ListPersonnelWithCoi(applicationId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test steps for a failure test for ListPersonnelWithCoi
        /// </summary>
        /// <param name="applicationId">the application identifier to test</param>
        private void ListPersonnelWithCoiFailTest(int applicationId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = theTestPanelManagementService.ListPersonnelWithCoi(applicationId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theWorkMock.AssertWasNotCalled(s => s.PanelManagementRepository);
        }
        #endregion
        #endregion

        #region ListReviewerExpertise Tests
        /// <summary>
        /// Test successful return from PanelManagement repository call.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListReviewerExpertise")]
        public void ListReviewerExpertiseSuccessfulTest()
        {
            //
            // Set up local data
            //
            ResultModel<IReviewerExpertise> resultModel = new ResultModel<IReviewerExpertise>();
            List<IReviewerExpertise> list = new List<IReviewerExpertise>();
            list.Add(new ReviewerExpertise { LogNumber = "AB12345" });
            list.Add(new ReviewerExpertise { LogNumber = "AB67899" });
            list.Add(new ReviewerExpertise { LogNumber = "AB09876" });
            resultModel.ModelList = list;
            //
            // Test
            //
            ListReviewerExpertiseSuccessTest(resultModel, 34);
        }
        /// <summary>
        /// Test zero sessionPanelId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListReviewerExpertise")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListReviewerExpertise detected an invalid parameter: sessionPanelId was 0")]
        public void ListReviewerExpertiseZeroSessionPanelIdTest()
        {
            //
            // Test
            //
            ListReviewerExpertiseFailTest(0);
        }
        /// <summary>
        /// Test negative sessionPanelId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListReviewerExpertise")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListReviewerExpertise detected an invalid parameter: sessionPanelId was -5")]
        public void ListReviewerExpertiseNegativeSessionPanelIdTest()
        {
            //
            // Test
            //
            ListReviewerExpertiseFailTest(-5);
        }
        #region Helpers
        /// <summary>
        /// Test steps for a successful test for ListReviewerExpertise
        /// </summary>
        /// <param name="resultModel">ResultModel to return</param>
        /// <param name="sessionPanelId">the sessionPanelId to test</param>
        private void ListReviewerExpertiseSuccessTest(ResultModel<IReviewerExpertise> resultModel, int sessionPanelId)
        {
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.PanelManagementRepository).Return(thePanelManagementRepositoryMock);
            Expect.Call(thePanelManagementRepositoryMock.ListReviewerExpertise(sessionPanelId, _goodUserId)).Return(resultModel);

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestPanelManagementService.ListReviewerExpertise(sessionPanelId, _goodUserId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test steps for a failure test for ListReviewerExpertise
        /// </summary>
        /// <param name="sessionPanelId">the sessionPanelId to test</param>
        private void ListReviewerExpertiseFailTest(int sessionPanelId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = theTestPanelManagementService.ListReviewerExpertise(sessionPanelId, _goodUserId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theWorkMock.AssertWasNotCalled(s => s.PanelManagementRepository);
        }
        #endregion
        #endregion
        #region ListPanelNames Tests
        /// <summary>
        /// Test successful return from ListPanelNames repository call.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPanelNames")]
        public void ListPanelNamesSuccessfulTest()
        {
            //
            // Set up local data
            //
            ResultModel<ITransferPanelModel> resultModel = new ResultModel<ITransferPanelModel>();
            List<ITransferPanelModel> list = new List<ITransferPanelModel>();
            list.Add(new TransferPanelModel { Name = "panel name 1" });
            list.Add(new TransferPanelModel { Name = "panel name 2" });
            list.Add(new TransferPanelModel { Name = "panel name 3" });
            resultModel.ModelList = list;
            //
            // Test
            //
            ListPanelNamesSuccessfulTest(resultModel, 34, 25);
        }
        /// <summary>
        /// Test zero applicationId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPanelNames")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListPanelNames detected an invalid parameter: applicationId was 0")]
        public void ListPanelNamesZeroApplicationIdTest()
        {
            //
            // Test
            //
            ListPanelNamesFailTest(0, 25);
        }
        /// <summary>
        /// Test negative applicationId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPanelNames")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListPanelNames detected an invalid parameter: applicationId was -10")]
        public void ListPanelNamesNegativeApplicationIdTest()
        {
            //
            // Test
            //
            ListPanelNamesFailTest(-10, 25);
        }
        /// <summary>
        /// Test zero current panel identifier
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPanelNames")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListPanelNames detected an invalid parameter: currentPanelId was 0")]
        public void ListPanelNamesZeroCurrentPaneldTest()
        {
            //
            // Test
            //
            ListPanelNamesFailTest(10, 0);
        }
        /// <summary>
        /// Test negative current panel identifier
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPanelNames")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListPanelNames detected an invalid parameter: currentPanelId was -25")]
        public void ListPanelNamesNegativeCurrentPanelIdTest()
        {
            //
            // Test
            //
            ListPanelNamesFailTest(10, -25);
        }
        /// <summary>
        /// Test zero parameter value test
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPanelNames")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListPanelNames detected an invalid parameter: applicationId was 0")]
        public void ListPanelNamesZeroValuesTest()
        {
            //
            // Test
            //
            ListPanelNamesFailTest(0, 0);
        }
        /// <summary>
        /// Test negative parameter value test
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPanelNames")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListPanelNames detected an invalid parameter: applicationId was -20")]
        public void ListPanelNamesNegativeValuesTest()
        {
            //
            // Test
            //
            ListPanelNamesFailTest(-20, -20);
        }
         /// <summary>
        /// Test zero parameter value test
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPanelNames")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListPanelNames detected an invalid parameter: applicationId was 0")]
        public void ListPanelNamesZeroApplicationIdNegativeCurrentPanelIdTest()
        {
            //
            // Test
            //
            ListPanelNamesFailTest(0, -1);
        }
         /// <summary>
        /// Test zero parameter value test
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPanelNames")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListPanelNames detected an invalid parameter: applicationId was -1")]
        public void ListPanelNamesZeroNegativeApplicationidZeroCurrentPanelIdTest()
        {
            //
            // Test
            //
            ListPanelNamesFailTest(-1, 0);
        }
                      
        #region Helpers
        /// <summary>
        /// Test steps for a successful test for ListReviewerExpertise
        /// </summary>
        /// <param name="resultModel">ResultModel to return</param>
        /// <param name="sessionPanelId">the sessionPanelId to test</param>
        private void ListPanelNamesSuccessfulTest(ResultModel<ITransferPanelModel> resultModel, int applicationId, int currentPanelId)
        {
            //
            // set the expectations
            //
            MockPanelManagementRepositoryReturnOnce();
            Expect.Call(thePanelManagementRepositoryMock.ListPanelNames(applicationId, currentPanelId)).Return(resultModel);

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestPanelManagementService.ListPanelNames(applicationId, currentPanelId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test steps for a failure test for ListReviewerExpertise
        /// </summary>
        /// <param name="sessionPanelId">the sessionPanelId to test</param>
        private void ListPanelNamesFailTest(int applicationId, int currentPanelId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestPanelManagementService.ListPanelNames(applicationId, currentPanelId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theWorkMock.AssertWasNotCalled(s => s.PanelManagementRepository);
        }
        #endregion
        #endregion
        #region ListSiblingPanelNames Tests
        /// <summary>
        /// Test successful return from ListSiblingPanelNames repository call.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListSiblingPanelNames")]
        public void ListSiblingPanelNamesSuccessfulTest()
        {
            //
            // Set up local data
            //
            ResultModel<ITransferPanelModel> resultModel = BuildResult<ITransferPanelModel, TransferPanelModel>(4);
            var currentPanelId = 34;
            //
            // set the expectations
            //
            MockPanelManagementRepositoryReturnOnce();
            Expect.Call(thePanelManagementRepositoryMock.ListSiblingPanelNames(currentPanelId)).Return(resultModel);

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestPanelManagementService.ListSiblingPanelNames(currentPanelId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test zero current panel identifier
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListSiblingPanelNames")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListSiblingPanelNames detected an invalid parameter: currentPanelId was 0")]
        public void ListSiblingPanelNamesZeroCurrentPaneldTest()
        {
            //
            // Test
            //
            ListSiblingPanelNamesFailTest(0);
        }
        /// <summary>
        /// Test negative current panel identifier
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListPanelNames")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListSiblingPanelNames detected an invalid parameter: currentPanelId was -25")]
        public void ListSiblingPanelNamesNegativeCurrentPanelIdTest()
        {
            //
            // Test
            //
            ListSiblingPanelNamesFailTest(-25);
        }                 
        #region Helpers
        /// <summary>
        /// Test steps for a failure test for ListSiblingPanelNames
        /// </summary>
        /// <param name="currentPanelId">the currentPanelId to test</param>
        private void ListSiblingPanelNamesFailTest(int currentPanelId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestPanelManagementService.ListSiblingPanelNames(currentPanelId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theWorkMock.AssertWasNotCalled(s => s.PanelManagementRepository);
        }
        #endregion
        #endregion
        #region ListReviewerNames Tests
        /// <summary>
        /// Test successful return from ListReviewerNames repository call.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListReviewerNames")]
        public void ListReviewerNamesSuccessfulTest()
        {
            //
            // Set up local data
            //
            ResultModel<IUserModel> resultModel = BuildResult<IUserModel, UserModel>(4);
            int sessionPanelId = 25;
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.PanelUserAssignmentRepository).Return(thePanelUserAssignmentRepositoryMock);
            Expect.Call(thePanelUserAssignmentRepositoryMock.ListReviewerNames(sessionPanelId)).Return(resultModel);

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestPanelManagementService.ListReviewerNames(sessionPanelId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test zero session panel identifier
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListReviewerNames")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListReviewerNames detected an invalid parameter: sessionPanelId was 0")]
        public void ListReviewerNamesZeroSessionPaneldTest()
        {
            //
            // Test
            //
            ListReviewerNamesFailTest(0);
        }
        /// <summary>
        /// Test negative session panel identifier
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ListReviewerNames")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ListReviewerNames detected an invalid parameter: sessionPanelId was -25")]
        public void ListReviewerNamesNegativeSessionPanelIdTest()
        {
            //
            // Test
            //
            ListReviewerNamesFailTest(-25);
        }                     
        #region Helpers
        /// <summary>
        /// Test steps for a failure test for ListReviewerNames
        /// </summary>
        /// <param name="sessionPanelId">thesessionPanelId to test</param>
        private void ListReviewerNamesFailTest(int sessionPanelId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestPanelManagementService.ListReviewerNames(sessionPanelId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theWorkMock.AssertWasNotCalled(s => s.PanelManagementRepository);
        }
        #endregion
        #endregion
        #region ListTransferReasons Tests
        /// <summary>
        /// Test steps for a successful test for ListTransferReasons
        /// </summary>
        /// <param name="resultModel">ResultModel to return</param>
        /// <param name="sessionPanelId">the sessionPanelId to test</param>
        [Category("PanelManagementService.ListApplicationTransferReasons")]
        public void ListTransferReasonsSuccessfulTest()
        {
            ResultModel<IReasonModel> resultModel = BuildResult<IReasonModel, ReasonModel>(4);

            //
            // set the expectations
            //
            MockPanelManagementRepositoryReturnOnce();
            Expect.Call(thePanelManagementRepositoryMock.ListApplicationTransferReasons()).Return(resultModel);

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestPanelManagementService.ListTransferReasons();
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        #endregion
        #region LogApplicationTranferRequest Tests
        /// <summary>
        /// Test successful return from LogApplicationTranferRequest repository call.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.LogApplicationTranferRequest")]
        public void LogApplicationTranferRequestMessageTest()
        {
            string message = "this is my message";
            LogApplicationTranferRequestSuccessfulTest(message, _goodUserId);
        }
        /// <summary>
        /// Test empty message - this should be successful.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.LogApplicationTranferRequest")]
        public void LogApplicationTranferRequestEmptyMessageTest()
        {
            LogApplicationTranferRequestSuccessfulTest(string.Empty, _goodUserId);
        }
        /// <summary>
        /// Test null message
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.LogApplicationTranferRequest")]
        public void LogApplicationTranferRequestNullMessageTest()
        {
            ////
            //// Test
            ////
            LogApplicationTranferRequestSuccessfulTest(null, _goodUserId);
        }
        /// <summary>
        /// Test empty string
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.LogApplicationTranferRequest")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.LogApplicationTranferRequest detected an invalid parameter: userId was -3")]
        public void LogApplicationTranferRequestNegativeUserIdTest()
        {
            ////
            //// Test
            ////
            string message = "this is my message";
            LogApplicationTranferRequestFailTest(message, -3);
        }
        /// <summary>
        /// Test zero current panel identifier
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.LogApplicationTranferRequest")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.LogApplicationTranferRequest detected an invalid parameter: userId was 0")]
        public void LogApplicationTranferRequestZeroUserIdTest()
        {
            ////
            //// Test
            ////
            string message = "this is my message";
            LogApplicationTranferRequestFailTest(message, 0);
        }
        #region Helpers
        /// <summary>
        /// Test steps for a successful test for LogApplicationTranferRequest
        /// </summary>
        /// <param name="resultModel">ResultModel to return</param>
        /// <param name="sessionPanelId">the sessionPanelId to test</param>
        private void LogApplicationTranferRequestSuccessfulTest(string message, int userId)
        {
            //
            //// set the expectations
            ////
            MockActionLogRepositoryReturnOnce();
            Expect.Call(delegate { theActionLogRepositoryMock.Add(null); }).IgnoreArguments();
            MockUnitOfWorkSave();

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            this.theTestPanelManagementService.LogApplicationTranferRequest(message, userId);
            //
            // Test the assertions
            //
            IList<object[]> argsPerCall = theActionLogRepositoryMock.GetArgumentsForCallsMadeOn(x => x.Add(null));
            ActionLog actionLogEntityCreated = argsPerCall[0][0] as ActionLog;
            Assert.AreEqual(message, actionLogEntityCreated.Message);
            theActionLogRepositoryMock.AssertWasCalled(x => x.Add(actionLogEntityCreated));
            theWorkMock.AssertWasCalled(x => x.Save());
        }
        /// <summary>
        /// Test steps for a failure test for LogApplicationTranferRequest
        /// </summary>
        /// <param name="sessionPanelId">the sessionPanelId to test</param>
        private void LogApplicationTranferRequestFailTest(string message, int userId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            this.theTestPanelManagementService.LogApplicationTranferRequest(message, userId);
        }
        #endregion
        #endregion
        #region LogReviewerTranferRequest Tests
        /// <summary>
        /// Test successful return from LogReviewerTranferRequest repository call.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.LogReviewerTranferRequest")]
        public void LogReviewerTranferRequestMessageTest()
        {
            string message = "this is my message";
            LogReviewerTranferRequestSuccessfulTest(message, _goodUserId);
        }
        /// <summary>
        /// Test empty message - this should be successful.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.LogReviewerTranferRequest")]
        public void LogReviewerTranferRequestEmptyMessageTest()
        {
            LogReviewerTranferRequestSuccessfulTest(string.Empty, _goodUserId);
        }
        /// <summary>
        /// Test null message
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.LogReviewerTranferRequest")]
        public void LogReviewerTranferRequestNullMessageTest()
        {
            ////
            //// Test
            ////
            LogReviewerTranferRequestSuccessfulTest(null, _goodUserId);
        }
        /// <summary>
        /// Test empty string
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.LogReviewerTranferRequest")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.LogReviewerTranferRequest detected an invalid parameter: userId was -3")]
        public void LogReviewerTranferRequestNegativeUserIdTest()
        {
            ////
            //// Test
            ////
            string message = "this is my message";
            LogReviewerTranferRequestFailTest(message, -3);
        }
        /// <summary>
        /// Test zero current panel identifier
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.LogReviewerTranferRequest")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.LogReviewerTranferRequest detected an invalid parameter: userId was 0")]
        public void LogReviewerTranferRequestZeroUserIdTest()
        {
            ////
            //// Test
            ////
            string message = "this is my message";
            LogReviewerTranferRequestFailTest(message, 0);
        }
        #region Helpers
        /// <summary>
        /// Test steps for a successful test for LogReviewerTranferRequest
        /// </summary>
        /// <param name="message">the message to test</param>
        /// <param name="userId">the userId to test</param>
        private void LogReviewerTranferRequestSuccessfulTest(string message, int userId)
        {
            //
            //// set the expectations
            ////
            MockActionLogRepositoryReturnOnce();
            Expect.Call(delegate { theActionLogRepositoryMock.Add(null); }).IgnoreArguments();
            MockUnitOfWorkSave();

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            this.theTestPanelManagementService.LogReviewerTranferRequest(message, userId);
            //
            // Test the assertions
            //
            IList<object[]> argsPerCall = theActionLogRepositoryMock.GetArgumentsForCallsMadeOn(x => x.Add(null));
            ActionLog actionLogEntityCreated = argsPerCall[0][0] as ActionLog;
            Assert.AreEqual(message, actionLogEntityCreated.Message);
            theActionLogRepositoryMock.AssertWasCalled(x => x.Add(actionLogEntityCreated));
            theWorkMock.AssertWasCalled(x => x.Save());
        }
        /// <summary>
        /// Test steps for a failure test for LogReviewerTranferRequest
        /// </summary>
        /// <param name="message">the message to test</param>
        /// <param name="userId">the userId to test</param>
        private void LogReviewerTranferRequestFailTest(string message, int userId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            this.theTestPanelManagementService.LogReviewerTranferRequest(message, userId);
        }
        #endregion
        #endregion
        #region GetPanelAdministrators Tests
        /// <summary>
        /// Test successful return from PanelManagement repository call.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.GetPanelAdministrators")]
        public void GetPanelAdministratorsSuccessfulTest()
        {
            //
            // Set up local data
            //
            ResultModel<IPanelAdministrators> resultModel = BuildResult<IPanelAdministrators, PanelAdministrators>(3);
            //
            // Test
            //
            GetPanelAdministratorsSuccessTest(resultModel, 34);
        }
        /// <summary>
        /// Test zero sessionPanelId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.GetPanelAdministrators")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.GetPanelAdministrators detected an invalid parameter: sessionPanelId was 0")]
        public void GetPanelAdministratorsZeroSessionPanelIdTest()
        {
            //
            // Test
            //
            GetPanelAdministratorsFailTest(0);
        }
        /// <summary>
        /// Test negative sessionPanelId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.GetPanelAdministrators")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.GetPanelAdministrators detected an invalid parameter: sessionPanelId was -5")]
        public void GetPanelAdministratorsNegativeSessionPanelIdTest()
        {
            //
            // Test
            //
            GetPanelAdministratorsFailTest(-5);
        }
        #region Helpers
        /// <summary>
        /// Test steps for a successful test for GetPanelAdministrators
        /// </summary>
        /// <param name="resultModel">ResultModel to return</param>
        /// <param name="sessionPanelId">SessionPanelId to test</param>
        private void GetPanelAdministratorsSuccessTest(ResultModel<IPanelAdministrators> resultModel, int sessionPanelId)
        {
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.PanelManagementRepository).Return(thePanelManagementRepositoryMock);
            Expect.Call(thePanelManagementRepositoryMock.GetPanelAdministrators(sessionPanelId)).Return(resultModel);

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestPanelManagementService.GetPanelAdministrators(sessionPanelId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test steps for a failure test for GetPanelAdministrators
        /// </summary>
        /// <param name="sessionPanelId">SessionUPanelId to test</param>
        private void GetPanelAdministratorsFailTest(int sessionPanelId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = theTestPanelManagementService.GetPanelAdministrators(sessionPanelId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theWorkMock.AssertWasNotCalled(s => s.PanelManagementRepository);
        }
        #endregion
        #endregion

        #region GetApplicationCritiques Tests
        /// <summary>
        /// Test successful return from PanelManagement repository call.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.GetApplicationCritiques")]
        public void GetApplicationCritiquesSuccessfulTest()
        {
            //
            // Set up local data
            //
            IApplicationCritiqueModel resultModel = new ApplicationCritiqueModel();

            int panelApplicationId = 25;
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.PanelManagementRepository).Return(thePanelManagementRepositoryMock);
            Expect.Call(thePanelManagementRepositoryMock.GetApplicationCritiques(panelApplicationId)).Return(resultModel);

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestPanelManagementService.GetApplicationCritiques(panelApplicationId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test zero panelApplicationId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.GetApplicationCritiques")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.GetApplicationCritiques detected an invalid parameter: panelApplicationId was 0")]
        public void GetApplicationCritiquesZeroPanelApplicationIdTest()
        {
            //
            // Test
            //
            GetApplicationCritiquesFailTest(0);
        }
        /// <summary>
        /// Test negative panelApplicationId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.GetApplicationCritiques")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.GetApplicationCritiques detected an invalid parameter: panelApplicationId was -5")]
        public void GetApplicationCritiquesNegativePanelApplicationIdTest()
        {
            //
            // Test
            //
            GetApplicationCritiquesFailTest(-5);
        }
        #region Helpers
        /// <summary>
        /// Test steps for a failure test for GetApplicationCritiques
        /// </summary>
        /// <param name="panelApplicationId">panelApplicationId to test</param>
        private void GetApplicationCritiquesFailTest(int panelApplicationId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = theTestPanelManagementService.GetApplicationCritiques(panelApplicationId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            theWorkMock.AssertWasNotCalled(s => s.PanelManagementRepository);
        }
        #endregion
        #endregion
    }
}
