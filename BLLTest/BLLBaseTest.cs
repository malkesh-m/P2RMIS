using System.Collections.Generic;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.Repository.UserProfileManagement;
using Sra.P2rmis.Dal.ResultModels;
using File = Sra.P2rmis.Bll.FileService;

namespace BLLTest
{
    /// <summary>
    /// Base for unit tests.  Collection of "stuff" to simplify unit tests.
    /// </summary>
    public class BLLBaseTest
    {
        /// <summary>
        /// The rhino mocks repository
        /// </summary>
        protected MockRepository theMock;                                                                   //theMock = new MockRepository();                                                                   
        #region Mock objects
        protected ApplicationWorkflow thePartialWorkflowMock;                                               //thePartialWorkflowMock = theMock.PartialMock<ApplicationWorkflow>();
        protected ApplicationWorkflowStep thePartialStepMock;                                               //thePartialStepMock = theMock.PartialMock<ApplicationWorkflowStep>();
        protected ApplicationWorkflowStepElementContent thePartialStepElementContentMock;                   //thePartialStepElementContentMock = theMock.PartialMock<ApplicationWorkflowStepElementContent>();
        protected ApplicationWorkflowStepElement thePartialStepElementMock;                                 //thePartialStepElementMock = theMock.PartialMock<ApplicationWorkflowStepElement>();
        protected ApplicationWorkflowStepWorkLog thePartialStepWorkLogMock;                                 //thePartialStepWorkLogMock = theMock.PartialMock<ApplicationWorkflowStepWorkLog>();
        #endregion
        #region Repository mocks
        protected IUnitOfWork theWorkMock;                                                                  //theWorkMock = theMock.DynamicMock<IUnitOfWork>();
        protected IApplicationWorkflowStepElementContentRepository theContentElementRepositoryMock;         //theContentElementRepositoryMock = theMock.DynamicMock<IApplicationWorkflowStepElementContentRepository>();
        protected IApplicationWorkflowRepository theWorkflowRepositoryMock;                                 //workflowRepositoryMock = theMock.DynamicMock<IApplicationWorkflowRepository>();
        protected IApplicationWorkflowStepRepository theWorkflowStepRepositoryMock;                         //workflowStepRepositoryMock = theMock.DynamicMock<IApplicationWorkflowStepRepository>();
        protected IApplicationWorkflowStepElementRepository theWorkflowStepElementRepositoryMock;           //theWorkflowStepElementRepositoryMock = theMock.DynamicMock<IApplicationWorkflowStepElementRepository>();
        protected IApplicationWorkflowStepWorkLogRepository theWorkflowStepWorkLogRepositoryMock;           //theWorkflowStepWorkLogRepositoryMock = theMock.DynamicMock<IApplicationWorkflowStepWorkLogRepository>();
        protected IApplicationWorkflowStepElementContentHistoryRepository theStepElementContentHistoryRepositoryMock;   //theStepElementContentHistoryRepositoryMock = theMock.DynamicMock<IApplicationWorkflowStepElementContentHistoryRepository>();
        protected ISummaryManagementRepository theSummaryManagementRepositoryMock;                          // theSummaryManagementRepositoryMock = theMock.DynamicMock<ISummaryManagementRepository>();
        protected IApplicationRepository theApplicationRepositoryMock;                                      // theApplicationRepositoryMock = theMock.DynamicMock<IApplicationRepository>();
        protected IApplicationReviewStatusRepository theApplicationReviewStatusRepositoryMock;              // theApplicationReviewStatusRepositoryMock = theMock.DynamicMock<IApplicationReviewStatusRepository>();
        protected IUserApplicationCommentRepository theUserApplicationCommentRepositoryMock;                // theUserApplicationCommentRepositoryMock = theMock.DynamicMock<UserApplicationCommentRepository>();
        protected IWorkflowMechanismRepository theWorkflowMechanismRepositoryMock;                          // theWorkflowMechanismRepositoryMock = theMock.DynamicMock<IWorkflowMechanismRepository>();
        protected IApplicationWorkflowStepAssignmentRepository theApplicationWorkflowStepAssignmentRepositoryMock;     // theApplicationWorkflowStepAssignmentRepository = theMock.DynamicMock<IApplicationWorkflowStepAssignmentRepository>(); 
        protected IPanelManagementRepository thePanelManagementRepositoryMock;                              // thePanelManagementRepositoryMock = theMock.DynamicMock<IPanelManagementRepository>();
        protected ISessionPanelRepository theSessionPanelRepositoryMock;                                    // heSessionPanelRepositoryMock = theMock.DynamicMock<ISessionPanelRepository>();
        protected IActionLogRepository theActionLogRepositoryMock;                                          // theActionLogRepositoryMock = theMock.DynamicMock<IActionLogRepository>();
        protected IPanelApplicationReviewerAssignmentRepository thePanelApplicationReviewerAssignmentRepositoryMock;    // thePanelApplicationReviewerAssignmentRepositoryMock = theMock.DynamicMock<IPanelApplicationReviewerAssignmentRepository>();
        protected IPanelApplicationRepository thePanelApplicationRepositoryMock;                            // thePanelApplicationRepositoryMock = theMock.DynamicMock<IPanelApplicationRepository>();
        protected IPanelUserAssignmentRepository thePanelUserAssignmentRepositoryMock;                      // thePanelUserAssignmentRepositoryMock = theMock.DynamicMock<IPanelUserAssignmentRepository>();
        protected IGenericRepository<Gender> theGenderRepositoryMock;
        protected IGenericRepository<Ethnicity> theEthnicityRepositoryMock;
        protected IGenericRepository<Prefix> thePrefixRepositoryMock;
        protected IGenericRepository<PhoneType> thePhoneTypeRepositoryMock;
        protected IGenericRepository<State> theStateRepositoryMock;
        protected IGenericRepository<Country> theCountryRepositoryMock;
        protected IGenericRepository<Degree> theDegreeRepositoryMock;
        protected IGenericRepository<UserDegree> theUserDegreeRepositoryMock;
        protected IGenericRepository<ProfileType> theProfileTypeRepositoryMock;
        protected IGenericRepository<MilitaryRank> theMilitaryRankRepositoryMock;
        protected IGenericRepository<MilitaryStatusType> theMilitaryStatusTypeRepository;
        protected IGenericRepository<EmailAddressType> theEmailAddressTypeRepository;
        protected IGenericRepository<AlternateContactType> theAlternateContactTypeRepository;
        protected IGenericRepository<UserResume> theUserResumeRepositoryMock;
        protected IUserRepository theUserRepositoryMock;
        protected IGenericRepository<UserProfile> theUserProfileRepositoryMock;
        protected IApplicationSummaryLogRepository theApplicationSummaryLogRepositoryMock;
        #endregion
        #region Constants
        protected const int _goodUserId = 1111;
        protected const int _badUserId = -4;
        protected const int _UserIdZero = 0;
        protected const int _aProgram = 44;
        protected const int _emptyProgram = 0;
        protected const int _fy = 2254;
        protected const int _aCycleId = 4;
        protected int? _nullIdValue = null;
        protected const int _aPanelId = 22;
        protected const int _anAwardId = 235;
        #endregion
        #region Attributes
        protected ClientSummaryServiceTestService theTestClientSummaryService; 
        protected SummaryManagementServiceTestService testService;
        protected SummaryProcessingServiceTestService thetestSummaryProcessingService;
        protected CriteriaTestService theTestCriteriaService;
        protected WorkflowTestService theTestWorkflowService;                                              //  theTestWorkflowService = theMock.PartialMock<WorkflowTestService>(theWorkMock);
        protected PanelManagementService theTestPanelManagementService;                                    // theTestPanelManagementService = theMock.PartialMock<PanelManagementService>(theWorkMock);
        protected MailService theTestMailService;                                                          // theTestMailService = theMock.PartialMock<theTestMailService>(theWorkMock);
        protected LookupService theTestLookupService;                                                      // theTestLookupService = theMock.PartialMock<TestLookupService>(theWorkMock);
        protected UserProfileManagementService theTestUserProfileManagementService;
        protected File.FileService theTestFileService;    
        protected ReportViewerService theTestReportViewerService;   
        #endregion
        #region Rhino Mock Wrappers
        /// <summary>
        /// Initialize all mocks object in the base.
        /// </summary>
        protected void InitializeMocks()
        {
            //
            // Create mock object
            //
            //
            // Now create the mocks
            //
            theMock = new MockRepository();
            theWorkMock = theMock.DynamicMock<IUnitOfWork>();
            theSessionPanelRepositoryMock = theMock.DynamicMock<ISessionPanelRepository>();
            theTestPanelManagementService = theMock.PartialMock<TestPanelManagementService>(theWorkMock);
            thePanelManagementRepositoryMock = theMock.DynamicMock<IPanelManagementRepository>();
            theActionLogRepositoryMock = theMock.DynamicMock<IActionLogRepository>();
            theWorkflowRepositoryMock = theMock.DynamicMock<IApplicationWorkflowRepository>();
            theWorkflowStepRepositoryMock = theMock.DynamicMock<IApplicationWorkflowStepRepository>();
            theWorkflowStepElementRepositoryMock = theMock.DynamicMock<IApplicationWorkflowStepElementRepository>();
            theWorkflowStepWorkLogRepositoryMock = theMock.DynamicMock<IApplicationWorkflowStepWorkLogRepository>();
            theApplicationWorkflowStepAssignmentRepositoryMock = theMock.DynamicMock<IApplicationWorkflowStepAssignmentRepository>();
            thePanelApplicationReviewerAssignmentRepositoryMock = theMock.DynamicMock<IPanelApplicationReviewerAssignmentRepository>();
            thePanelApplicationRepositoryMock = theMock.DynamicMock<IPanelApplicationRepository>();
            thePanelUserAssignmentRepositoryMock = theMock.DynamicMock<IPanelUserAssignmentRepository>();
            theGenderRepositoryMock = theMock.DynamicMock<IGenericRepository<Gender>>();
            theEthnicityRepositoryMock = theMock.DynamicMock<IGenericRepository<Ethnicity>>();
            thePrefixRepositoryMock = theMock.DynamicMock<IGenericRepository<Prefix>>();
            thePhoneTypeRepositoryMock = theMock.DynamicMock<IGenericRepository<PhoneType>>();
            theStateRepositoryMock = theMock.DynamicMock<IGenericRepository<State>>();
            theCountryRepositoryMock = theMock.DynamicMock<IGenericRepository<Country>>();
            theDegreeRepositoryMock = theMock.DynamicMock<IGenericRepository<Degree>>();
            theUserDegreeRepositoryMock = theMock.DynamicMock<IGenericRepository<UserDegree>>();
            theProfileTypeRepositoryMock = theMock.DynamicMock<IGenericRepository<ProfileType>>();
            theMilitaryRankRepositoryMock = theMock.DynamicMock<IGenericRepository<MilitaryRank>>();
            theMilitaryStatusTypeRepository = theMock.DynamicMock<IGenericRepository<MilitaryStatusType>>();
            theEmailAddressTypeRepository = theMock.DynamicMock<IGenericRepository<EmailAddressType>>();
            theUserRepositoryMock = theMock.DynamicMock<IUserRepository>();
            theAlternateContactTypeRepository = theMock.DynamicMock<IGenericRepository<AlternateContactType>>();
            theUserProfileRepositoryMock = theMock.DynamicMock<IGenericRepository<UserProfile>>();
            theApplicationSummaryLogRepositoryMock = theMock.DynamicMock<IApplicationSummaryLogRepository>();
            //
            // Now create the test services
            //
            this.theTestMailService = theMock.PartialMock<TestMailService>(theWorkMock);
            theTestLookupService = theMock.PartialMock<TestLookupService>(theWorkMock);
            theTestUserProfileManagementService = theMock.PartialMock<TestUserProfileManagmnentService>(theWorkMock);
            theTestFileService = theMock.PartialMock<TestFileervice>(theWorkMock);
            this.theTestReportViewerService = theMock.PartialMock<TestReportViewerService>(theWorkMock);
        }
        /// <summary>
        /// Clean up mocks after the test.
        /// </summary>
        protected void CleanUpMocks()
        {
            theUserProfileRepositoryMock = null;
            theUserRepositoryMock = null;
            theAlternateContactTypeRepository = null;
            theEmailAddressTypeRepository = null;
            theMilitaryStatusTypeRepository = null;
            theMilitaryRankRepositoryMock = null;
            theProfileTypeRepositoryMock = null;
            theDegreeRepositoryMock = null;
            theUserDegreeRepositoryMock = null;
            theCountryRepositoryMock = null;
            theStateRepositoryMock = null;
            thePhoneTypeRepositoryMock = null;
            thePrefixRepositoryMock = null;
            theEthnicityRepositoryMock = null;
            theGenderRepositoryMock = null;
            theApplicationSummaryLogRepositoryMock = null;
            thePanelUserAssignmentRepositoryMock = null;
            thePanelApplicationRepositoryMock = null;
            thePanelApplicationReviewerAssignmentRepositoryMock = null;
            theApplicationWorkflowStepAssignmentRepositoryMock = null;
            theWorkflowStepWorkLogRepositoryMock = null;
            theWorkflowStepElementRepositoryMock = null;
            theWorkflowStepRepositoryMock = null;
            theWorkflowRepositoryMock = null;
            theActionLogRepositoryMock = null;
            thePanelManagementRepositoryMock = null;
            theTestPanelManagementService = null;
            theSessionPanelRepositoryMock = null;
            theWorkMock = null;
            theMock = null;
            //
            // Clean up the test services
            //
            theTestMailService = null;
            theTestLookupService = null;
            theTestUserProfileManagementService = null;
            theTestFileService = null;
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.ApplicationSummaryLogRepository
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<IApplicationSummaryLogRepository> MockUnitOfWorkApplicationSummaryLogRepository()
        {
            return Expect.Call(theWorkMock.ApplicationSummaryLogRepository).Return(theApplicationSummaryLogRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.ApplicationWorkflowRepository
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<IApplicationWorkflowRepository> MockUnitOfWorkApplicationWorkflowRepository()
        {
            return Expect.Call(theWorkMock.ApplicationWorkflowRepository).Return(theWorkflowRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.Save
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<Rhino.Mocks.Expect.Action> MockUnitOfWorkSave()
        {
            return Expect.Call(delegate { theWorkMock.Save(); });
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.SessionPanelRepository
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<ISessionPanelRepository> MockUnitOfWorkSessionPanelRepository()
        {
            return Expect.Call(theWorkMock.SessionPanelRepository).Return(theSessionPanelRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for HasSelectProgramPanelPermission
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<IPanelManagementRepository> MockPanelManagementRepository()
        {
            return Expect.Call(theWorkMock.PanelManagementRepository);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for HasSelectProgramPanelPermission
        /// </summary>
        /// <param name="value">Value to return</param>
        public void MockPanelManagementRepositoryReturnOnce()
        {
            MockPanelManagementRepository().Return(thePanelManagementRepositoryMock);
        }

        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for ActionLogRepository
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<IActionLogRepository> MockActionLogRepository()
        {
            return Expect.Call(theWorkMock.ActionLogRepository);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for the ActionLogRepository
        /// </summary>
        public void MockActionLogRepositoryReturnOnce()
        {
            MockActionLogRepository().Return(theActionLogRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.PanelApplicationReviewerAssignmentRepository
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<IPanelApplicationReviewerAssignmentRepository> MockUnitOfWorkPanelApplicationReviewerAssignmentRepository()
        {
            return Expect.Call(theWorkMock.PanelApplicationReviewerAssignmentRepository).Return(thePanelApplicationReviewerAssignmentRepositoryMock);
        }
        public IMethodOptions<IPanelApplicationRepository> MockPanelApplicationRepository()
        {
            return Expect.Call(theWorkMock.PanelApplicationRepository).Return(thePanelApplicationRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.GenderRepository
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<IGenericRepository<Gender>> MockUnitOfWorkGenderRepository()
        {
            return Expect.Call(theWorkMock.GenderRepository).Return(theGenderRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.Ethnicity
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<IGenericRepository<Ethnicity>> MockUnitOfWorkEthnicityRepository()
        {
            return Expect.Call(theWorkMock.EthnicityRepository).Return(theEthnicityRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.Prefix
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<IGenericRepository<Prefix>> MockUnitOfWorkPrefixRepository()
        {
            return Expect.Call(theWorkMock.PrefixRepository).Return(thePrefixRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.PhoneType
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<IGenericRepository<PhoneType>> MockUnitOfWorkPhoneTypeRepository()
        {
            return Expect.Call(theWorkMock.PhoneTypeRepository).Return(thePhoneTypeRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.State
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<IGenericRepository<State>> MockUnitOfWorkStateRepository()
        {
            return Expect.Call(theWorkMock.StateRepository).Return(theStateRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.Country
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<IGenericRepository<Country>> MockUnitOfWorkCountryRepository()
        {
            return Expect.Call(theWorkMock.CountryRepository).Return(theCountryRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.Country
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<IGenericRepository<Degree>> MockUnitOfWorkDegreeRepository()
        {
            return Expect.Call(theWorkMock.DegreeRepository).Return(theDegreeRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.ProfileType
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<IGenericRepository<ProfileType>> MockUnitOfWorkProfileTypeRepository()
        {
            return Expect.Call(theWorkMock.ProfileTypeRepository).Return(theProfileTypeRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.MilitaryRank
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<IGenericRepository<MilitaryRank>> MockUnitOfWorkMilitaryRankRepository()
        {
            return Expect.Call(theWorkMock.MilitaryRankRepository).Return(theMilitaryRankRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.MilitaryStatusTypeRepository
        /// </summary>
        /// <param name="value">Mock MilitaryStatusType Repository</param>
        public IMethodOptions<IGenericRepository<MilitaryStatusType>> MockUnitOfWorkMilitaryStatusTypeRepository()
        {
            return Expect.Call(theWorkMock.MilitaryStatusTypeRepository).Return(theMilitaryStatusTypeRepository);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.EmailAddressTypeRepository
        /// </summary>
        /// <param name="value">Mock EmailAddressType Repository</param>
        public IMethodOptions<IGenericRepository<EmailAddressType>> MockUnitOfWorkEmailAddressTypeRepository()
        {
            return Expect.Call(theWorkMock.EmailAddressTypeRepository).Return(theEmailAddressTypeRepository);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.AlternateContactType Repository
        /// </summary>
        /// <param name="value">Mock EmailAddressType Repository</param>
        public IMethodOptions<IGenericRepository<AlternateContactType>> MockUnitOfWorkAlternateContactTypeRepository()
        {
            return Expect.Call(theWorkMock.AlternateContactTypeRepository).Return(theAlternateContactTypeRepository);
        }

        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.AlternateContactType Repository
        /// </summary>
        /// <param name="value">Mock EmailAddressType Repository</param>
        public IMethodOptions<IGenericRepository<UserDegree>> MockUnitOfWorkUserDegreeRepository()
        {
            return Expect.Call(theWorkMock.UserDegreeRepository).Return(theUserDegreeRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.AlternateContactType Repository
        /// </summary>
        /// <param name="value">Mock EmailAddressType Repository</param>
        public IMethodOptions<IGenericRepository<UserResume>> MockUnitOfWorkUserResumeRepository()
        {
            return Expect.Call(theWorkMock.UserResumeRepository).Return(theUserResumeRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.User Repository
        /// </summary>
        /// <param name="value">Mock EmailAddressType Repository</param>
        public IMethodOptions<IUserRepository> MockUnitOfWorkUserRepository()
        {
            return Expect.Call(theWorkMock.UserRepository).Return(theUserRepositoryMock);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for UnitOfWork.User Repository
        /// </summary>
        /// <param name="value">Mock EmailAddressType Repository</param>
        public IMethodOptions<IGenericRepository<UserProfile>> MockUnitOfWorkUserProfileRepository()
        {
            return Expect.Call(theWorkMock.UserProfileRepository).Return(theUserProfileRepositoryMock);
        }
        #endregion
        #region Helper Methods
        /// <summary>
        /// Generic helper method that constructs a ResultModel populated with a list
        /// of specific web models.
        /// </summary>
        /// <typeparam name="I">Interface type of WebModel</typeparam>
        /// <typeparam name="T">Concrete type of WebModel</typeparam>
        /// <param name="howMany">Number of WebModels to create</param>
        /// <returns>Populated WebModel</returns>
        public ResultModel<I> BuildResult<I, T>(int howMany) where T : I, new()
        {
            var result = new ResultModel<I>();
            List<I> list = new List<I>();

            for (int i = 0; i < howMany; i++)
            {
                I x = new T();
                list.Add(x);
            }
            result.ModelList = list;

            return result;
        }
        /// <summary>
        /// Generic helper method that constructs a list populated with a list
        /// of entity models
        /// </summary>
        /// <typeparam name="T">Concrete type of entity object</typeparam>
        /// <param name="howMany">Number of entity object to create</param>
        /// <returns>List of entity object</returns>
        protected IEnumerable<T> BuildEntityList<T>(int howMany) where T:  new()
        {
            List<T> list = new List<T>();

            for (int i = 0; i < howMany; i++)
            {
                list.Add(new T());
            }

            return list;
        }
        #endregion
    }
    #region Test Server Wrapper Classes
    /// <summary>
    /// Wrapper for summary management service to enable mocking.
    /// </summary>
    public class SummaryManagementServiceTestService : SummaryManagementService
    {
        public SummaryManagementServiceTestService(IUnitOfWork unit)
        {
            UnitOfWork = unit;
        }
    }
    /// <summary>
    /// Wrapper for summary processing service to enable mocking.
    /// </summary>
    public class SummaryProcessingServiceTestService: SummaryProcessingService
    {
        public SummaryProcessingServiceTestService(IUnitOfWork unit)
        {
            UnitOfWork = unit;
        }
    }
    /// <summary>
    /// Server test class.  Necessary to overwrite UnitOfWork so database is not instantiated.
    /// </summary>
    public class CriteriaTestService : CriteriaService
    {
        public CriteriaTestService(IUnitOfWork unit)
        {
            UnitOfWork = unit;
        }
    }
    /// <summary>
    /// Server test class.  Necessary to overwrite UnitOfWork so database is not instantiated.
    /// </summary>
    public class WorkflowTestService : WorkflowService
    {
        public WorkflowTestService(IUnitOfWork unit)
        {
            UnitOfWork = unit;
        }
    }
    /// <summary>
    /// Server test class.  Necessary to overwrite UnitOfWork so database is not instantiated.
    /// </summary>
    public class ClientSummaryServiceTestService : ClientSummaryService
    {
        public ClientSummaryServiceTestService(IUnitOfWork unit)
        {
            UnitOfWork = unit;
        }
    }
    /// <summary>
    /// Server test class.  Necessary to overwrite UnitOfWork so database is not instantiated.
    /// </summary>
    public class TestPanelManagementService : PanelManagementService
    {
        public TestPanelManagementService(IUnitOfWork unit)
        {
            UnitOfWork = unit;
        }
    }
    /// <summary>
    /// Server test class.  Necessary to overwrite UnitOfWork so database is not instantiated.
    /// </summary>
    public class TestMailService : MailService
    {
        public TestMailService(IUnitOfWork unit)
        {
            UnitOfWork = unit;
        }
    }
    /// <summary>
    /// Server test class.  Necessary to overwrite UnitOfWork so database is not instantiated.
    /// </summary>
    public class TestLookupService : LookupService
    {
        public TestLookupService(IUnitOfWork unit)
        {
            UnitOfWork = unit;
        }
    }
    /// <summary>
    /// Server test class.  Necessary to overwrite UnitOfWork so database is not instantiated.
    /// </summary>
    public class TestUserProfileManagmnentService : UserProfileManagementService
    {
        public TestUserProfileManagmnentService(IUnitOfWork unit)
        {
            UnitOfWork = unit;
        }
    }
    /// <summary>
    /// Server test class.  Necessary to overwrite UnitOfWork so database is not instantiated.
    /// </summary>
    public class TestFileervice : File.FileService
    {
        public TestFileervice(IUnitOfWork unit)
        {
            UnitOfWork = unit;
        }
    }
    /// <summary>
    /// Server test class.  Necessary to overwrite UnitOfWork so database is not instantiated.
    /// </summary>
    public class TestReportViewerService : ReportViewerService
    {
        public TestReportViewerService(IUnitOfWork unit)
        {
            UnitOfWork = unit;
        }
    }
    #endregion

}
