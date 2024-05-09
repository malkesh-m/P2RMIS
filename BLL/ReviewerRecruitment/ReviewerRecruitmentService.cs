using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.ModelBuilders.ReviewerRecruitment;
using Sra.P2rmis.Bll.OptionService;
using Sra.P2rmis.Bll.OptionService.ResetContract;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.ReviewerRecruitment;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.ReviewerRecruitment
{
    /// <summary>
    /// Provides services specific to ReviewerRecruitment
    /// </summary>
    public interface IReviewerRecruitmentService
    {
        /// <summary>
        /// Construct & retrieve a container of work list items for a specific client.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of work IRecruitmentWorkList</returns>
        Container<IWorkList> GetWorkList(int clientId);
        /// <summary>
        /// Construct & retrieve a container of profile update list items to be reviewed for a specific user
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns></returns>
        Container<IProfileUpdateList> GetProfileUpdateListForReview(int userInfoId);
        /// <summary>
        /// Marks the identified UserInfoChangeLog entities as Reviewed.
        /// </summary>
        /// <param name="listOfChanges">Collection of UserInfoChangeLog entity identifiers</param>
        /// <param name="userId">User entity identifier</param>
        void SaveProfileReviewerAcknowledgement(ICollection<int> listOfChanges, int userId);
        /// <summary>
        /// Get the recruit's preferred contact information and construct and retrieve the recruit's communications log
        /// </summary>
        /// <param name="userInfoId">The recruits user info identifier</param>
        /// <param name="requestorUserId">User entity identifier of user requesting the log</param>
        /// <returns>The communication log model for this user</returns>
        IUserCommunicationLogModel GetRecruitCommunicationLog(int userInfoId, int requestorUserId);
        /// <summary>
        /// Save any changes from the UserCommunicationLog Modal.  (Adds, Modifications & Deletes are processed)
        /// </summary>
        /// <param name="model">IUserCommunicationLogModel</param>
        /// <param name="userId">User entity identifier</param>
        void SaveRecruitCommunicationLog(IUserCommunicationLogModel model, int userId);
        /// <summary>
        /// Deletes the indicated panel user potential assignment
        /// </summary>
        /// <param name="potentialAssignmentId">The panel user potential assignment identifier </param>
        /// <param name="userId">The user identifier for the user making the change</param>
        void DeletePanelUserPotentialAssignment(int potentialAssignmentId, int userId);
        /// <summary>
        /// Builds and retrieves the required information for the Panel Assignment Model
        /// </summary>
        /// <param name="sessionId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>IPanelAssignmentModalModel containing a reviewers Participation history and assignment status</returns>
        IPanelAssignmentModalModel RetrieveReviewerParticipation(int sessionId, int userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="panelUserPotentialAssignmentId">PanelUSerPotentialAssignment entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="reviewerUserId">User entity identifier for the reviewer</param>
        /// <param name="clientParticipantTypeId">ClientParticipationType entity identifier</param>
        /// <param name="clientRoleId">ClientRole entity identifier</param>
        /// <param name="participantionMethodId">ParticipantionMethod entity identifier</param>
        /// <param name="clientApprovalFlag">Client approval flag</param>
        /// <param name="restrictedAccessFlag">Restricted access flag</param>
        /// <param name="recruitedFlag"></param>
        /// <param name="userId">User entity identifier for the user adding the potoential assignment</param>
        void SavePanelUserAssignPotentialReviewer(int? panelUserPotentialAssignmentId, int sessionPanelId, int reviewerUserId, int? clientParticipantTypeId,
                                     int? clientRoleId, int? participantionMethodId, bool? clientApprovalFlag, bool restrictedAccessFlag, bool recruitedFlag,
                                    int userId);
        /// Saves updates to a PanelUserPotentialAssignment entity or PanelUserAssignment entity
        /// displayed on the PanelAssignmentModel
        /// </summary>
        /// <param name="panelUserPotentialAssignmentId">PanelUserPotentialAssignment entity identifier</param>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="reviewertUserId">Reviewer entity identifier</param>
        /// <param name="clientParticipantTypeId">ClientParticipantType entity identifier</param>
        /// <param name="clinetRoleId">ClientRole entity identifier</param>
        /// <param name="participantMethodId">ParticipantMethod entity identifier</param>
        /// <param name="clientApprovalFlag">Client approval state</param>
        /// <param name="restrictedAccessFlag">Restricted access state</param>
        /// <param name="assignmentStatus">This is the value from the checkbox</param>
        /// <param name="isAssigned">This is the original value form the web model</param>
        /// <param name="userId">User entity identifier of the user submitting the form</param>
        bool AssignReviewerToPanel(int panelUserPotentialAssignmentId, int panelUserAssignmentId, int sessionPanelId, int reviewerUserId, int? clientParticipantTypeId,
                                      int? clinetRoleId, int? participantMethodId, bool? clientApprovalFlag, bool restrictedAccessFlag,
                                      bool assignmentStatus, bool isAssigned, bool noStatus, int userId);
        /// <summary>
        /// Sends an email to a user informing them that they have been assigned to this panel as a reviewer
        /// </summary>
        /// <param name="theMailService">The mail service</param>
        /// <param name="reviewerId">The user identifier for the reviewer</param>
        /// <param name="sessionPanelId">The session panel identifier</param>
        /// <param name="userId">The user identifier for the user making the assignment</param>
        bool SendPanelAssignmentEmail(IMailService theMailService, int reviewerId, int sessionPanelId, int userId);

        /// <summary>
        /// Determines whether the specified participant user identifier is a new profile (prospect).
        /// </summary>
        /// <param name="participantUserId">The participant user identifier.</param>
        /// <returns>True if user is a new prospect; otherwise false.</returns>
        bool IsNewProfile(int participantUserId);

        /// <summary>
        /// Toggles the reviewer profile from prospect to reviewer.
        /// </summary>
        /// <param name="participantUserId">The participant user identifier.</param>
        /// <param name="getUserId">The get user identifier.</param>
        void ToggleReviewerProfile(int participantUserId, int getUserId);
        /// <summary>
        /// Indicates if the contract has been signed for a user on a SessionPanel.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <param name="sessionId">SessionPanel entity identifier</param>
        /// <returns>True if the contract has been signed; false otherwise</returns>
        bool IsContractSigned(int userId, int sessionId);
        /// <summary>
        /// Transfers the reviewer to panel.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="newSessionPanelId">The new session panel identifier.</param>
        /// <param name="reviewerUserId">The reviewer user identifier.</param>
        /// <param name="clientParticipantTypeId">The client participant type identifier.</param>
        /// <param name="clientRoleId">The client role identifier.</param>
        /// <param name="participantMethodId">The participant method identifier.</param>
        /// <param name="clientApprovalFlag">The client approval flag.</param>
        /// <param name="restrictedAccessFlag">if set to <c>true</c> [restricted access flag].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        bool TransferReviewerToPanel(int panelUserAssignmentId, int newSessionPanelId, int reviewerUserId, int? clientParticipantTypeId,
                                      int? clientRoleId, int? participantMethodId, bool? clientApprovalFlag, bool restrictedAccessFlag,
                                      int userId);
    }
    /// <summary>
    /// Provides services specific to ReviewerRecruitment
    /// </summary>
    public class ReviewerRecruitmentService : ServerBase, IReviewerRecruitmentService
    {
        #region Constants
        public class Constants
        {
            /// <summary>
            /// Text of reviewer invitation message.
            /// </summary>
            public const string Invitation = "Invitation sent.";
        }
        #endregion
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public ReviewerRecruitmentService()
        {
            UnitOfWork = new UnitOfWork();
        }
        #endregion
        #region Services Provided
        /// <summary>
        /// Construct & retrieve a container of work list items for a specific client.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of work IWorkList objects</returns>
        public Container<IWorkList> GetWorkList(int clientId)
        {
            ValidateInt(clientId, "ReviewerRecruitmentService.GetWorkList", "clientId");

            WorklistModelBuilder builder = new WorklistModelBuilder(UnitOfWork, clientId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Construct & retrieve a container of profile update list items to be reviewed for a specific user
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns></returns>
        public Container<IProfileUpdateList> GetProfileUpdateListForReview(int userInfoId)
        {
            ValidateInt(userInfoId, "ReviewerRecruitmentService.GetProfileUpdateList", "userInfoId");

            ProfileUpdateListModelBuilder builder = new ProfileUpdateListModelBuilder(UnitOfWork, userInfoId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Marks the identified UserInfoChangeLog entities as Reviewed.
        /// </summary>
        /// <param name="listOfChanges">Collection of UserInfoChangeLog entity identifiers</param>
        /// <param name="userId">User entity identifier</param>
        public void SaveProfileReviewerAcknowledgement(ICollection<int> listOfChanges, int userId)
        {
            this.ValidateCollection(listOfChanges, "ReviewerRecruitmentService.SaveProfileReviewcAcknowledgement", "listOfChanges");
            this.ValidateInteger(userId, "ReviewerRecruitmentService.SaveProfileReviewcAcknowledgement", "userId");
            //
            // Create the ServiceAction to perform the Crud operations & initialize it
            //
            UserInfoChangeLogServiceAction editAction = new UserInfoChangeLogServiceAction();
            editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.UserInfoChangeLogRepository, ServiceAction<UserInfoChangeLog>.DoNotUpdate, 0, userId);
            //
            // Now we just iterate over the enumeration of UserInfoChangeLog entity identifiers and change them.
            //
            foreach (int userInfoChangeLogId in listOfChanges)
            {
                editAction.Populate(userInfoChangeLogId);
                editAction.Execute();
            }
            //
            // Now we save all of our changes.
            //
            this.UnitOfWork.Save();
        }
        /// <summary>
        /// Get the recruit's preferred contact information and construct and retrieve the recruit's communications log
        /// </summary>
        /// <param name="userInfoId">The recruits user info identifier</param>
        /// <param name="requestorUserId">User entity identifier of user requesting the log</param>
        /// <returns>The communication log model for this user</returns>
        public IUserCommunicationLogModel GetRecruitCommunicationLog(int userInfoId, int requestorUserId)
        {
            ValidateInt(userInfoId, "ReviewerRecruitmentService.GetRecruitCommunicationLog", "userInfoId");
            IUserCommunicationLogModel result = new UserCommunicationLogModel();

            UserInfo userInfo = this.UnitOfWork.UserInfoRepository.GetByID(userInfoId);

            result.RecruitContactInformation = GetRecruitPreferredContactInfo(userInfo);

            RecruitCommunicationInfoModelBuilder builder = new RecruitCommunicationInfoModelBuilder(UnitOfWork, requestorUserId, userInfo.User.UserID);
            builder.BuildContainer();

            result.RecruitCommunicationLog = builder.Results.ModelList.ToList();
            return result;
        }
        /// <summary>
        /// Save any changes from the UserCommunicationLog Modal.  (Adds, Modifications & Deletes are processed)
        /// </summary>
        /// <param name="collection">Collection of IUserCommunicationLogModel entities</param>
        /// <param name="userId">User entity identifier</param>
        public void SaveRecruitCommunicationLog(IUserCommunicationLogModel model, int userId)
        {
            ValidateInt(userId, FullName(nameof(ReviewerRecruitmentService), nameof(SaveRecruitCommunicationLog)), nameof(userId));
            ValidateModelExists<IUserCommunicationLogModel>(model, FullName(nameof(ReviewerRecruitmentService), nameof(SaveRecruitCommunicationLog)), nameof(model));
            ValidateCollectionExists<IRecruitCommunicationInfo>(model.RecruitCommunicationLog, FullName(nameof(ReviewerRecruitmentService), nameof(SaveRecruitCommunicationLog)), nameof(model.RecruitCommunicationLog));
            //
            // Create the ServiceAction to perform the Crud operations & initialize it
            //
            UserCommunicationLogServiceAction editAction = new UserCommunicationLogServiceAction();
            editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.UserCommunicationLogRepository, ServiceAction<UserCommunicationLog>.DoNotUpdate, userId);
            //
            // Now we just iterate over the enumeration of the models and perform the CRUD operation that they indicate
            //
            foreach (var entry in model.RecruitCommunicationLog)
            {
                editAction.Populate(entry.UserCommunicationLogId, model.RecruitContactInformation.UserId, entry.CommunicationMethodId, entry.Comment);
                editAction.Execute();
            }
            //
            // Now we save all of our changes.
            //
            this.UnitOfWork.Save();
        }
        /// <summary>
        /// Creates a new communication log record
        /// </summary>
        /// <param name="recipientId">The user identifier of the communication recipient</param>
        /// <param name="communicationMethodId">The communication method identifier</param>
        /// <param name="comment">Comment</param>
        /// <param name="userId">The user identifier of the communications initiator</param>
        internal void CreateUserCommunicationLog(int recipientId, int communicationMethodId, string comment, int userId)
        {
            IUserCommunicationLogModel model = new UserCommunicationLogModel();

            model.RecruitContactInformation = new RecruitPreferredContactInfo(recipientId);

            IRecruitCommunicationInfo info = new RecruitCommunicationInfo(communicationMethodId, comment);
            model.RecruitCommunicationLog.Add(info);

            SaveRecruitCommunicationLog(model, userId);
        }
        /// <summary>
        /// Builds and returns a RecruitPreferredContactInfo object
        /// </summary>
        /// <param name="userInfo">User Info entity object</param>
        /// <returns>IRecruitPreferredContactInfo object</returns>
        internal IRecruitPreferredContactInfo GetRecruitPreferredContactInfo(UserInfo userInfo)
        {
            UserPhone phone = userInfo.UserPhones.Where(x => x.PrimaryFlag == true).DefaultIfEmpty(new UserPhone()).FirstOrDefault();
            UserPhone fax = userInfo.UserPhones.Where(x => x.PhoneTypeId == PhoneType.WorkFax || x.PhoneTypeId == PhoneType.HomeFax).OrderBy(x => x.PhoneTypeId).DefaultIfEmpty(new UserPhone()).FirstOrDefault();
            UserEmail email = userInfo.UserEmails.Where(x => x.PrimaryFlag == true).DefaultIfEmpty(new UserEmail()).FirstOrDefault();

            return new RecruitPreferredContactInfo(userInfo.UserID, userInfo.LastName, userInfo.FirstName, phone.Phone, phone.Extension, fax.Phone, email.Email);
        }
        /// <summary>
        /// Deletes the indicated panel user potential assignment
        /// </summary>
        /// <param name="potentialAssignmentId">The panel user potential assignment identifier </param>
        /// <param name="userId">The user identifier for the user making the change</param>
        public void DeletePanelUserPotentialAssignment(int potentialAssignmentId, int userId)
        {
            ValidateInt(potentialAssignmentId, FullName(nameof(ReviewerRecruitmentService), nameof(DeletePanelUserPotentialAssignment)), nameof(potentialAssignmentId));
            ValidateInt(userId, FullName(nameof(ReviewerRecruitmentService), nameof(DeletePanelUserPotentialAssignment)), nameof(userId));

            PanelUserPotentialAssignmentServiceAction serviceAction = new PanelUserPotentialAssignmentServiceAction();
            serviceAction.InitializeAction(this.UnitOfWork, UnitOfWork.PanelUserPotentialAssignmentRepository, ServiceAction<PanelUserPotentialAssignment>.DoUpdate, potentialAssignmentId, userId);
            // sets ToDelete in the action's attributes
            serviceAction.Populate();
            serviceAction.Execute();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="panelUserPotentialAssignmentId">PanelUSerPotentialAssignment entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="reviewerUserId">User entity identifier for the reviewer</param>
        /// <param name="clientParticipantTypeId">ClientParticipationType entity identifier</param>
        /// <param name="clientRoleId">ClientRole entity identifier</param>
        /// <param name="participantionMethodId">ParticipantionMethod entity identifier</param>
        /// <param name="clientApprovalFlag">Client approval flag</param>
        /// <param name="restrictedAccessFlag">Restricted access flag</param>
        /// <param name="recruitedFlag"></param>
        /// <param name="userId">User entity identifier for the user adding the potoential assignment</param>
        public void SavePanelUserAssignPotentialReviewer(int? panelUserPotentialAssignmentId, int sessionPanelId, int reviewerUserId, int? clientParticipantTypeId,
                                     int? clientRoleId, int? participantionMethodId, bool? clientApprovalFlag, bool restrictedAccessFlag, bool recruitedFlag,
                                    int userId)
        {
            ValidateSavePanelUserAssignPotentialReviewer(panelUserPotentialAssignmentId, sessionPanelId, reviewerUserId,
                                                     clientParticipantTypeId, clientRoleId, participantionMethodId, userId);

            var pupaEntity = UnitOfWork.PanelUserPotentialAssignmentRepository.Get(x => x.SessionPanelId == sessionPanelId && x.UserId == reviewerUserId);
            var puaEntity = UnitOfWork.PanelUserAssignmentRepository.Get(x => x.SessionPanelId == sessionPanelId && x.UserId == reviewerUserId);

            // ensure that an user already assigned as a potential reviewer or reviewer is not added as potential reviewer
            if (pupaEntity.Count() == 0 && puaEntity.Count() == 0)
            {
                int id = panelUserPotentialAssignmentId ?? 0;

                PanelUserPotentialAssignmentServiceAction serviceAction = new PanelUserPotentialAssignmentServiceAction();

                serviceAction.InitializeAction(this.UnitOfWork, UnitOfWork.PanelUserPotentialAssignmentRepository, ServiceAction<PanelUserPotentialAssignment>.DoUpdate, id, userId);
                serviceAction.Populate(sessionPanelId, reviewerUserId, clientParticipantTypeId, clientRoleId, participantionMethodId, clientApprovalFlag, restrictedAccessFlag, recruitedFlag);
                serviceAction.Execute();
            }
        }
        /// <summary>
        /// Builds and retrieves the required information for the Panel Assignment Model
        /// </summary>
        /// <param name="sessionId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>IPanelAssignmentModalModel containing a reviewers Participation history and assignment status</returns>
        public IPanelAssignmentModalModel RetrieveReviewerParticipation(int sessionId, int userId)
        {
            ValidateInt(sessionId, FullName(nameof(ReviewerRecruitmentService), nameof(RetrieveReviewerParticipation)), nameof(sessionId));
            ValidateInt(userId, FullName(nameof(ReviewerRecruitmentService), nameof(RetrieveReviewerParticipation)), nameof(userId));
            //
            // Create a builder & build
            //
            PanelAssignmentModalModelBuilder builder = new PanelAssignmentModalModelBuilder(this.UnitOfWork, sessionId, userId);
            return builder.Build() as PanelAssignmentModalModel;
        }
        /// <summary>
        /// Saves updates to a PanelUserPotentialAssignment entity or PanelUserAssignment entity
        /// displayed on the PanelAssignmentModel
        /// </summary>
        /// <param name="panelUserPotentialAssignmentId">PanelUserPotentialAssignment entity identifier</param>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="reviewertUserId">Reviewer entity identifier</param>
        /// <param name="clientParticipantTypeId">ClientParticipantType entity identifier</param>
        /// <param name="clinetRoleId">ClientRole entity identifier</param>
        /// <param name="participantMethodId">ParticipantMethod entity identifier</param>
        /// <param name="clientApprovalFlag">Client approval state</param>
        /// <param name="restrictedAccessFlag">Restricted access state</param>
        /// <param name="assignmentStatus">This is the value from the checkbox</param>
        /// <param name="isAssigned">This is the original value form the web model</param>
        /// <param name="userId">User entity identifier of the user submitting the form</param>
        public bool AssignReviewerToPanel(int panelUserPotentialAssignmentId, int panelUserAssignmentId, int sessionPanelId, int reviewerUserId, int? clientParticipantTypeId,
                                      int? clinetRoleId, int? participantMethodId, bool? clientApprovalFlag, bool restrictedAccessFlag,
                                      bool assignmentStatus, bool isAssigned, bool noStatus, int userId)
        {
            //
            // Validate the parameters that are common to all paths
            //
            ValidateAssignReviewerToPanelPanel(sessionPanelId, reviewerUserId, userId);

            bool newAssignment = false;
            PanelUserAssignment newAssignmentEntity = null;
            //
            // The displayed entity is a PanelUserPotentialAssignment entity.  So make any changes to it.
            //
            if (!isAssigned)
            {
                PanelUserPotentialAssignmentServiceAction potentialServiceAction = new PanelUserPotentialAssignmentServiceAction();
                potentialServiceAction.InitializeAction(this.UnitOfWork, UnitOfWork.PanelUserPotentialAssignmentRepository, ServiceAction<PanelUserPotentialAssignment>.DoNotUpdate, panelUserPotentialAssignmentId, userId);
                potentialServiceAction.Populate(sessionPanelId, reviewerUserId, clientParticipantTypeId, clinetRoleId,
                                       participantMethodId, clientApprovalFlag, restrictedAccessFlag, assignmentStatus);
                potentialServiceAction.Execute();
                //
                // And now do we need to create a PanelUserAssignment entity?  If so we create it.  The
                // original record displayed was a PanelUserPotentialAssignment and the checkbox was checked.
                //
                    if ((!isAssigned) && (assignmentStatus))
                    {
                        ValidateAssignReviewerToPanelPanel(clientParticipantTypeId, participantMethodId);
                        //
                        // Create the ServiceAction to Add a PanelUserAssignment entity & execute it.
                        //
                        PanelUserAssignmentServiceAction serviceAction = new PanelUserAssignmentServiceAction();
                        serviceAction.InitializeAction(this.UnitOfWork, UnitOfWork.PanelUserAssignmentRepository, ServiceAction<PanelUserAssignment>.DoNotUpdate, panelUserAssignmentId, userId);
                        serviceAction.Populate(sessionPanelId, reviewerUserId, clientParticipantTypeId.Value, clinetRoleId,
                                               participantMethodId.Value, clientApprovalFlag, restrictedAccessFlag);
                        serviceAction.Execute();
                        newAssignmentEntity = serviceAction.CreatedPanelUserAssignment;

                        newAssignment = true;
                    }
            }
            else
            //
            // Well it is not a PanelUserPotentialAssignment so it is a PanelUserAssignment and we update it.
            {
                ValidateAssignReviewerToPanelPanel(clientParticipantTypeId, participantMethodId);
                //
                // Create the ServiceAction to modify a PanelUserAssignment entity & execute it.
                //
                UpdateContract(sessionPanelId, panelUserAssignmentId, userId, clientParticipantTypeId, participantMethodId, restrictedAccessFlag);
                ModifyPanelUserAssignment(panelUserAssignmentId, sessionPanelId, reviewerUserId, clientParticipantTypeId, clinetRoleId, participantMethodId, clientApprovalFlag, restrictedAccessFlag, userId);
                
            }
            //
            // Now finally save the results
            //
            UnitOfWork.Save();
            int panelAssignmentId = newAssignmentEntity?.PanelUserAssignmentId ?? 0;
            //
            // If new assignment, we need to set up registration for the user
            //
            if (newAssignment && panelAssignmentId > 0)
            {
                UnitOfWork.PanelManagementRepository.SetupNewRegistration(panelAssignmentId, userId);
                UnitOfWork.Save();
            }
            //
            // and we need to return something.
            //
            return newAssignment;
        }
        /// <summary>
        /// Transfers the reviewer to panel.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="newSessionPanelId">The new session panel identifier.</param>
        /// <param name="reviewerUserId">The reviewer user identifier.</param>
        /// <param name="clientParticipantTypeId">The client participant type identifier.</param>
        /// <param name="clientRoleId">The client role identifier.</param>
        /// <param name="participantMethodId">The participant method identifier.</param>
        /// <param name="clientApprovalFlag">The client approval flag.</param>
        /// <param name="restrictedAccessFlag">if set to <c>true</c> [restricted access flag].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public bool TransferReviewerToPanel(int panelUserAssignmentId, int newSessionPanelId, int reviewerUserId, int? clientParticipantTypeId,
                                      int? clientRoleId, int? participantMethodId, bool? clientApprovalFlag, bool restrictedAccessFlag,
                                      int userId)
        {
            //
            // Validate the parameters that are common to all paths
            //
            ValidateTransferReviewerToPanel(newSessionPanelId, reviewerUserId, clientParticipantTypeId, participantMethodId, userId);
            
            var sessionPanel = UnitOfWork.SessionPanelRepository.GetByID(newSessionPanelId);
            if (!sessionPanel.IsReviewerAssigned(reviewerUserId))
            {
                PanelUserAssignment newAssignmentEntity = null;
                // Remove old panel user assignment
                UnitOfWork.PanelManagementRepository.RemoveUserFromPanel(panelUserAssignmentId, userId);
                // Create a PanelUserAssignment entity
                PanelUserAssignmentServiceAction serviceAction = new PanelUserAssignmentServiceAction();
                serviceAction.InitializeAction(this.UnitOfWork, UnitOfWork.PanelUserAssignmentRepository, ServiceAction<PanelUserAssignment>.DoNotUpdate, userId);
                serviceAction.Populate(newSessionPanelId, reviewerUserId, clientParticipantTypeId.Value, clientRoleId,
                                        participantMethodId.Value, clientApprovalFlag, restrictedAccessFlag);
                serviceAction.Execute();
                newAssignmentEntity = serviceAction.CreatedPanelUserAssignment;
                //
                // Now finally save the results
                //
                UnitOfWork.Save();
                //
                // Set up registration for the user
                UnitOfWork.PanelManagementRepository.SetupNewRegistration(newAssignmentEntity.PanelUserAssignmentId, userId);

                return true;
            } 
            else
            {
                return false;
            }
        }
        #region MyRegion
        /// <summary>
        /// Wrapper for the PanelUserAssignmentServiceAction.
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="reviewertUserId">Reviewer entity identifier</param>
        /// <param name="clientParticipantTypeId">ClientParticipantType entity identifier</param>
        /// <param name="clinetRoleId">ClientRole entity identifier</param>
        /// <param name="participantMethodId">ParticipantMethod entity identifier</param>
        /// <param name="clientApprovalFlag">Client approval state</param>
        /// <param name="restrictedAccessFlag">Restricted access state</param>
        /// <param name="userId">User entity identifier of the user submitting the form</param>
        internal virtual void ModifyPanelUserAssignment(int panelUserAssignmentId, int sessionPanelId, int reviewerUserId, int? clientParticipantTypeId, int? clinetRoleId, int? participantMethodId, bool? clientApprovalFlag, bool restrictedAccessFlag, int userId)
        {
            PanelUserAssignmentServiceAction serviceAction = new PanelUserAssignmentServiceAction();
            serviceAction.InitializeAction(this.UnitOfWork, UnitOfWork.PanelUserAssignmentRepository, ServiceAction<PanelUserAssignment>.DoNotUpdate, panelUserAssignmentId, userId);
            serviceAction.Populate(sessionPanelId, reviewerUserId, clientParticipantTypeId.Value, clinetRoleId,
                                   participantMethodId.Value, clientApprovalFlag, restrictedAccessFlag);
            serviceAction.Execute();
        }
        /// <summary>
        /// Implements the business rules surrounding the updating of a PanelUserAssignment after
        /// the contract has been signed.
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <param name="userId">User entity identifier of user making the change</param>
        internal virtual void UpdateContract(int sessionPanelId, int panelUserAssignmentId, int userId, int? clientParticipantTypeId, int? participantMethodId, bool restrictedAccessFlag)
        {
            SessionPanel sessionPanelEntity = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
            int clientId = sessionPanelEntity.ClientId();
            //
            // First create the option' parameter block & initialize it
            //
            OptionInitializeBlockContractToaster block = new OptionInitializeBlockContractToaster();
            block.Initialize(this.UnitOfWork, panelUserAssignmentId, userId, clientParticipantTypeId, participantMethodId, restrictedAccessFlag);
            //
            // Now create the option
            //
            IOptionAction option = OptionFactory.Create(UnitOfWork, clientId, SystemConfiguration.Indexes.ResetContractOnUpdate);
            option.Initialize(block);
            //
            // Now we just run it, easy peasy
            //
            option.Execute();
        }
        #endregion
        /// <summary>
        /// Sends an email to a user informing them that they have been assigned to this panel as a reviewer
        /// </summary>
        /// <param name="theMailService">The mail service</param>
        /// <param name="reviewerId">The user identifier for the reviewer</param>
        /// <param name="sessionPanelId">The session panel identifier</param>
        /// <param name="userId">The user identifier for the user making the assignment</param>
        /// <returns>True if assignment email was sent & logged; false otherwise</returns>
        public bool SendPanelAssignmentEmail(IMailService theMailService, int reviewerId, int sessionPanelId, int userId)
        {
            bool result = false;
            ValidateSendPanelAssignmentEmailParameters(reviewerId, sessionPanelId, userId);

            MailService.MailStatus sent = theMailService.SendPanelAssignmentEmail(reviewerId, sessionPanelId, userId);

            if (sent == MailService.MailStatus.Success)
            {
                CreateUserCommunicationLog(reviewerId, CommunicationMethod.Indexes.Email, Constants.Invitation, userId);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Determines whether the specified participant user identifier is a new profile (prospect).
        /// </summary>
        /// <param name="participantUserId">The participant user identifier.</param>
        /// <returns>
        /// True if user is a new prospect; otherwise false.
        /// </returns>
        public bool IsNewProfile(int participantUserId)
        {
            var user = UnitOfWork.UserRepository.GetByID(participantUserId);
            return user.UserProfileTypeId() == ProfileType.Indexes.Prospect;
        }

        /// <summary>
        /// Toggles the reviewer profile.
        /// </summary>
        /// <param name="participantUserId">The participant user identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void ToggleReviewerProfile(int participantUserId, int userId)
        {
            ValidateInt(participantUserId, FullName(nameof(ReviewerRecruitmentService), nameof(ToggleReviewerProfile)), nameof(participantUserId));
            ValidateInt(userId, FullName(nameof(ReviewerRecruitmentService), nameof(ToggleReviewerProfile)), nameof(userId));
            //User should always have a User Profile associated
            var userProfileId = UnitOfWork.UserRepository.GetByID(participantUserId).UserProfileId();
            UserProfileServiceAction serviceAction = new UserProfileServiceAction();
            serviceAction.InitializeAction(this.UnitOfWork, UnitOfWork.UserProfileRepository, ServiceAction<UserProfile>.DoUpdate, userProfileId, userId);
            // sets ToDelete in the action's attributes
            serviceAction.Populate(ProfileType.Indexes.Reviewer);
            serviceAction.Execute();
            UnitOfWork.Save();
        }
        #endregion
        /// <summary>
        /// Indicates if the contract has been signed for this user on this SessionPanel.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <param name="sessionId">SessionPanel entity identifier</param>
        /// <returns>True if the contract has been signed; false otherwise</returns>
        public bool IsContractSigned(int userId, int sessionId)
        {
            string fullName = FullName(nameof(ReviewerRecruitmentService), nameof(IsContractSigned));
            ValidateInt(userId, fullName, nameof(userId));
            ValidateInt(sessionId, fullName, nameof(sessionId));
            //
            // First we get the entities we need.
            //
            var sessionPanelEntity = UnitOfWork.SessionPanelRepository.GetByID(sessionId);
            PanelUserAssignment panelUserAssignmentEntity = sessionPanelEntity.PanelUserAssignment(userId);
            //
            //  If they do not have a PanelUserAssignment then by default they have not signed a contract
            //  Otherwise
            //
            return (panelUserAssignmentEntity != null) ?
                //
                // We retrieve all of their registration documents
                panelUserAssignmentEntity.PanelUserRegistrations.SelectMany(x => x.PanelUserRegistrationDocuments).
                //
                // Then we locate the one that is a contract and determine if it is signed.
                //
                Where(x => x.ClientRegistrationDocument.IsContract() && x.IsSigned()).Any() :
                false;

        }
        #region Helpers
        /// <summary>
        /// Validate the parameters for AssignReviewerToPanel
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="reviewerUserId">Reviewer entity identifier</param>
        /// <param name="userId">User entity identifier of the user submitting the form</param>
        private void ValidateAssignReviewerToPanelPanel(int sessionPanelId, int reviewerUserId, int userId)
        {
            string methodName = FullName(nameof(ReviewerRecruitmentService), nameof(AssignReviewerToPanel));
            ValidateInt(sessionPanelId, methodName, nameof(sessionPanelId));
            ValidateInt(reviewerUserId, methodName, nameof(reviewerUserId));
            ValidateInt(userId, methodName, nameof(userId));
        }
        /// <summary>
        /// Validate the parameters for AssignReviewerToPanel
        /// </summary>
        /// <param name="clientParticipantTypeId">ClientParticipantType entity identifier</param>
        /// <param name="participantMethodId">ParticipantMethod entity identifier</param>
        private void ValidateAssignReviewerToPanelPanel(int? clientParticipantTypeId, int? participantMethodId)
        {
            string methodName = FullName(nameof(ReviewerRecruitmentService), nameof(AssignReviewerToPanel));

            ValidateNullableIntegerMustHaveValue(clientParticipantTypeId, methodName, nameof(clientParticipantTypeId));
            ValidateNullableIntegerMustHaveValue(participantMethodId, methodName, nameof(participantMethodId));
        }
        /// <summary>
        /// Validates the transfer reviewer to panel.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="reviewerUserId">The reviewer user identifier.</param>
        /// <param name="clientParticipantTypeId">The client participant type identifier.</param>
        /// <param name="participantMethodId">The participant method identifier.</param>
        /// <param name="userId">The user identifier.</param>
        private void ValidateTransferReviewerToPanel(int sessionPanelId, int reviewerUserId, int? clientParticipantTypeId, int? participantMethodId, int userId)
        {
            string methodName = FullName(nameof(ReviewerRecruitmentService), nameof(TransferReviewerToPanel));

            ValidateInt(sessionPanelId, methodName, nameof(sessionPanelId));
            ValidateInt(reviewerUserId, methodName, nameof(reviewerUserId));
            ValidateInt(userId, methodName, nameof(userId));
            ValidateNullableIntegerMustHaveValue(clientParticipantTypeId, methodName, nameof(clientParticipantTypeId));
            ValidateNullableIntegerMustHaveValue(participantMethodId, methodName, nameof(participantMethodId));
        }
        /// <summary>
        /// Validate the parameters for SendPanelAssignmentEmail
        /// </summary>
        /// <param name="reviewerId">The user identifier for the reviewer</param>
        /// <param name="sessionPanelId">The session panel identifier</param>
        /// <param name="userId">The user identifier for the user making the assignment</param>
        private void ValidateSendPanelAssignmentEmailParameters(int reviewerId, int sessionPanelId, int userId)
        {
            string methodName = FullName(nameof(ReviewerRecruitmentService), nameof(SendPanelAssignmentEmail));

            ValidateInt(reviewerId, methodName, nameof(reviewerId));
            ValidateInt(reviewerId, methodName, nameof(sessionPanelId));
            ValidateInt(userId, methodName, nameof(userId));
        }


        /// <summary>
        /// alidate the parameters for SavePanelUserPotentialAssignment
        /// </summary>
        /// <param name="panelUserPotentialAssignmentId">PanelUserPotentialAssignment entiry identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="reviewerUserId">Reviewer entity identifier</param>
        /// <param name="clientParticipantTypeId">ClientParticipantType entity identifier</param>
        /// <param name="clientRoleId">ClientRole entity identifier</param>
        /// <param name="participantMethodId">ParticipantMethod entity identifier</param>
        /// <param name="userId">User entity identifier of the user submitting the form</param>
        private void ValidateSavePanelUserAssignPotentialReviewer(int? panelUserPotentialAssignmentId, int sessionPanelId, int reviewerUserId,
                                                     int? clientParticipantTypeId, int? clientRoleId, int? participantionMethodId, int userId)
        {
            string methodName = FullName(nameof(ReviewerRecruitmentService), nameof(SavePanelUserAssignPotentialReviewer));

            ValidateNullableInteger(panelUserPotentialAssignmentId, methodName, nameof(panelUserPotentialAssignmentId));
            ValidateInt(sessionPanelId, methodName, nameof(sessionPanelId));
            ValidateInt(reviewerUserId, methodName, nameof(reviewerUserId));
            ValidateNullableInteger(clientParticipantTypeId, methodName, nameof(clientParticipantTypeId));
            ValidateNullableInteger(clientRoleId, methodName, nameof(clientRoleId));
            ValidateNullableInteger(participantionMethodId, methodName, nameof(participantionMethodId));
            ValidateInt(userId, methodName, nameof(userId));
        }
        #endregion
    }
}
