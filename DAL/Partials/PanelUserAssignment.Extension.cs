using Sra.P2rmis.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's PanelUserAssignment object. 
    /// </summary>
    public partial class PanelUserAssignment: IStandardDateFields, IPanelReviewer
    {
        #region Services Provided
        /// <summary>
        /// Populate the PanelUserAssignment's properties
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">Reviewer User entity identifier</param>
        /// <param name="clientParticipantTypeId">ClientParticipantType entity identifier</param>
        /// <param name="clientRoleId">ClientRole entity identifier</param>
        /// <param name="participantMethodId">ParticipantMethod entity identifier</param>
        /// <param name="clientApprovalFlag">Client approval state</param>
        /// <param name="restrictedAssignedFlag">Restricted access state</param>
        /// <param name="clientApprovedBy">User entity identifier who approved the reviewer for the client</param>
        /// <param name="clientApprovalDate">Date/Time reviewer was approved by the client</param>
        public void Populate(int sessionPanelId, int userId, int clientParticipantTypeId, int? clientRoleId,
                     int participantMethodId, bool? clientApprovalFlag, bool restrictedAssignedFlag, int? clientApprovedBy,
                     DateTime? clientApprovalDate)
        {
            //
            // Populate the common fields
            //
            Helper.Populate(this, sessionPanelId, userId, clientRoleId,
                            clientApprovalFlag, restrictedAssignedFlag,
                            clientApprovedBy, clientApprovalDate);
            //
            // Now we do the non-common fields
            //
            this.ClientParticipantTypeId = clientParticipantTypeId;
            this.ParticipationMethodId = participantMethodId;
        }
        /// <summary>
        /// Obtain assigned user's first name
        /// </summary>
        /// <returns>assigned user's first name</returns>
        public string FirstName()
        {
            return this.User.FirstName();
        }
        /// <summary>
        /// Obtain assigned user's last name
        /// </summary>
        /// <returns>assigned user's last name</returns>
        public string LastName()
        {
            return this.User.LastName();
        }
        /// <summary>
        /// Obtain assigned user's primary email address
        /// </summary>
        /// <returns>assigned user's primary email address</returns>
        public string PrimaryUserEmailAddress()
        {
            return this.User.PrimaryUserEmailAddress();
        }
        /// <summary>
        /// Checks whether the user has any incomplete registrations
        /// </summary>
        /// <returns>True if registration is incomplete; otherwise false;</returns>
        public bool IsRegistrationIncomplete()
        {
            return this.PanelUserRegistrations.Any(x => x.RegistrationCompletedDate == null);
        }
        /// <summary>
        /// Returns the completion date of the Acknowledgment/NDA document
        /// </summary>
        /// <returns>DateTime document was completed; null if none exists</returns>
        public DateTime? DateTimeAcknowledgementNdaSigned ()
        {
            var document = FindDocumentByType(RegistrationDocumentType.Indexes.AcknowledgeNda);
            return (document == null) ? null : document.DateSigned;
        }
        /// <summary>
        /// Returns the completion date of the Bias/COI document
        /// </summary>
        /// <returns>DateTime document was completed; null if none exists</returns>
        public DateTime? DateTimeBiasCoiSigned()
        {
            var document = FindDocumentByType(RegistrationDocumentType.Indexes.BiasCoi);
            return (document == null) ? null : document.DateSigned;
        }
        /// <summary>
        /// Returns the completion date of the Contract document
        /// </summary>
        /// <returns>DateTime document was completed; null if none exists</returns>
        public DateTime? DateTimeContractSigned()
        {
            var document = FindDocumentByType(RegistrationDocumentType.Indexes.ContractualAgreement);
            return (document == null) ? null : document.DateSigned;
        }
        /// <summary>
        /// Locate a PanelUserRegistrationDocument by type
        /// </summary>
        /// <param name="typeId">RegistrationDocumentTypeId</param>
        /// <returns>PanelUserRegisterationDocumnet of null if none exist for the type specified</returns>
        private PanelUserRegistrationDocument FindDocumentByType(int typeId)
        {
            return PanelUserRegistrations.SelectMany(y => y.PanelUserRegistrationDocuments).FirstOrDefault(y => y.ClientRegistrationDocument.RegistrationDocumentTypeId == typeId);
        }
        /// <summary>
        /// Returns the PanelUserRegistrationDocumentId of the AckNDA document
        /// </summary>
        /// <returns>PanelUserRegistrationDocument entity identifier</returns>
        public int? RetrieveAcknowledgeNdaPanelUserRegistrationDocumentId()
        {
            var document = FindDocumentByType(RegistrationDocumentType.Indexes.AcknowledgeNda);
            return (document == null) ? (int?)null : document.PanelUserRegistrationDocumentId;
        }
        /// <summary>
        /// Returns the PanelUserRegistrationDocumentId of the Bias/COI document
        /// </summary>
        /// <returns>DateTime document was completed; null if none exists</returns>
        public int? RetrieveBiasCoiSignedPanelUserRegistrationDocumentId()
        {
            var document = FindDocumentByType(RegistrationDocumentType.Indexes.BiasCoi);
            return (document == null) ? (int?)null : document.PanelUserRegistrationDocumentId;
        }
        /// <summary>
        /// Returns the PanelUserRegistrationDocumentId of the Contract document
        /// </summary>
        /// <returns>DateTime document was completed; null if none exists</returns>
        public int? RetrieveContractSignedPanelUserRegistrationDocumentId()
        {
            var document = FindDocumentByType(RegistrationDocumentType.Indexes.ContractualAgreement);
            return (document == null) ? (int?)null : document.PanelUserRegistrationDocumentId;
        }

        /// <summary>
        /// Returns the ContactStsaus of the Contract document
        /// </summary>
        /// <parameters>
        /// documentid
        /// </parameters>
        /// <returns>Status int; null if none exists</returns>

        public ContractStatus RetrieveRegistrationDocumentContractStatus(int documentId)
        {
            var docs = PanelUserRegistrations.SelectMany(y => y.PanelUserRegistrationDocuments).FirstOrDefault(y => y.PanelUserRegistrationDocumentId == documentId);
            var docContract = docs.PanelUserRegistrationDocumentContracts.FirstOrDefault();
            return docContract?.ContractStatus;
        }

        /// <summary>
        /// Returns the Fee of the Contract document
        /// </summary>
        /// <parameters>
        /// documentid
        /// </parameters>
        /// <returns>Status int; null if none exists</returns>
        public decimal? RetrieveRegistrationDocumentFeeAmount(int documentId)
        {

            var docs = PanelUserRegistrations.SelectMany(y => y.PanelUserRegistrationDocuments).FirstOrDefault(y => y.PanelUserRegistrationDocumentId == documentId);
            var docContract = docs.PanelUserRegistrationDocumentContracts.FirstOrDefault();

           return (docContract == null || docContract.FeeAmount == null) ? ConsultantFeeAmount() : docContract.FeeAmount;

        }

        /// <summary>
        /// Returns the date/time the W9 was verified
        /// </summary>
        /// <returns>Returns the date/time the W9 was verified</returns>
        public DateTime? W9VerifiedDateTime()
        {
            return this.User.W9VerifiedDate;
        }
        /// <summary>
        /// Returns the W9 UserAddress entity object.
        /// </summary>
        /// <returns>W9 UserAddress entity</returns>
        public UserAddress W9Address()
        {
            UserAddress result = this.User.W9Address();
            return (result == null)? new UserAddress(): result;
        }
        /// <summary>
        /// Returns the datetime the resume was received.
        /// </summary>
        /// <returns>Datetime the resume was received: null if none received</returns>
        public DateTime? ResumeReceivedDateTime()
        {
            var result = this.User.UserInfoEntity().GetResume();
            return (result == null) ? (DateTime?)null : result.ReceivedDate;
        }
        /// <summary>
        /// Returns an indication if a fee is accepted for this panel registration.
        /// </summary>
        /// <returns>Value of PanelUserRegistrationDocumentItem if one exists; otherwise empty string.</returns>
        public string PaymentCategory()
        {
            //
            // Get the registration for this PanelUserRegistration.  Currently there is only one (or none).
            //
            var panelUserRegistrationEntity = this.PanelUserRegistrations.FirstOrDefault();
            //
            // If one is there (and it should be) but one never knows
            // return the value.
            //
            return (panelUserRegistrationEntity == null) ? string.Empty : panelUserRegistrationEntity.PaymentCategory();
        }
        /// <summary>
        /// Returns the PanelUserRegistrationDocumentId of the AckNDA document
        /// </summary>
        /// <returns></returns>
        public bool? RetrieveAcknowledgeNdaSignedOffline()
        {
            var document = FindDocumentByType(RegistrationDocumentType.Indexes.AcknowledgeNda);
            return (document == null) ? (bool?)null : document.SignedOfflineFlag;
        }
        /// <summary>
        /// Returns the PanelUserRegistrationDocumentId of the Bias/COI document
        /// </summary>
        /// <returns>DateTime document was completed; null if none exists</returns>
        public bool? RetrieveBiasCoiSignedSignedOffline()
        {
            var document = FindDocumentByType(RegistrationDocumentType.Indexes.BiasCoi);
            return (document == null) ? (bool?)null : document.SignedOfflineFlag;
        }
        /// <summary>
        /// Returns the PanelUserRegistrationDocumentId of the Contract document
        /// </summary>
        /// <returns>DateTime document was completed; null if none exists</returns>
        public bool? RetrieveContractSignedSignedOffline()
        {
            var document = FindDocumentByType(RegistrationDocumentType.Indexes.ContractualAgreement);
            return (document == null) ? (bool?)null : document.SignedOfflineFlag;
        }

        /// <summary>
        /// Determines whether honorarium is accepted.
        /// </summary>
        /// <returns>string true if accepted; otherwise false</returns>
        public string HonorariumAccepted()
        {
            var honorariumAccepted = this.PanelUserRegistrations.DefaultIfEmpty(new PanelUserRegistration())
                    .First()
                    .PanelUserRegistrationDocuments.SelectMany(x => x.PanelUserRegistrationDocumentItems)
                    .Where(x => x.RegistrationDocumentItemId == RegistrationDocumentItem.Indexes.ConsultantFeeAccepted)
                    .DefaultIfEmpty(new PanelUserRegistrationDocumentItem())
                    .First()
                    .Value ?? string.Empty;
            return honorariumAccepted;
        }
        /// <summary>
        /// The user's session pay rate.
        /// </summary>
        /// <remarks>SessionPayRate is optional for a PanelUserAssignment</remarks>
        /// <returns>SessionPayRate object for a PanelUserAssignment if it exists</returns>
        public ProgramSessionPayRate PanelUserSessionPayRate()
        {
                // SessionPayRates
                var payRate = this.SessionPanel.MeetingSession.ProgramSessionPayRates
                        .FirstOrDefault(x => x.ClientParticipantTypeId == this.ClientParticipantTypeId
                            && x.ProgramYearId == this.SessionPanel.ProgramPanels.First().ProgramYearId
                            && x.EmploymentCategory.Name == this.HonorariumAccepted()
                            && x.ParticipantMethodId == this.ParticipationMethodId
                            && x.RestrictedAssignedFlag == this.RestrictedAssignedFlag);
                return payRate;
        }
        /// <summary>
        /// The user's program pay rate.
        /// </summary>
        /// <returns>ProgramPayRate object for a PanelUserAssignment</returns>
        public ProgramSessionPayRate PanelUserProgramPayRate()
        {
            var payRate = this.SessionPanel.ProgramPanels.First()
                    .ProgramYear.ProgramSessionPayRates
                    .FirstOrDefault(x => x.ClientParticipantTypeId == this.ClientParticipantTypeId 
                        && x.EmploymentCategory.Name == this.HonorariumAccepted() 
                        && x.ParticipantMethodId == this.ParticipationMethodId 
                        && x.RestrictedAssignedFlag == this.RestrictedAssignedFlag
                        && x.MeetingTypeId == this.SessionPanel.MeetingSession.ClientMeeting.MeetingTypeId);
            return payRate;
        }
        /// <summary>
        /// The panel user's period of performance start date.
        /// </summary>
        /// <returns>Date of performance listed on the contract</returns>
        public DateTime? PeriodOfPerformanceStartDate()
        {
            return this.PanelUserSessionPayRate() != null ? PanelUserSessionPayRate().PeriodStartDate :
                PanelUserProgramPayRate()?.PeriodStartDate;
        }

        /// <summary>
        /// Panel user's performance end date for contract.
        /// </summary>
        /// <returns>End date of period of performance</returns>
        public DateTime? PeriodOfPerformanceEndDate()
        {
            return this.PanelUserSessionPayRate() != null ? PanelUserSessionPayRate().PeriodEndDate :
                PanelUserProgramPayRate()?.PeriodEndDate;
        }

        /// <summary>
        /// Panel user's semi-colon delimited manager list for contract.
        /// </summary>
        /// <returns>Delimited manager list</returns>
        public List<string> PanelManagerList()
        {
            string managerString = this.PanelUserSessionPayRate() != null ? PanelUserSessionPayRate().ManagerList :
                PanelUserProgramPayRate()?.ManagerList;
            var theManagerList = (managerString != null ? managerString.Split(';').ToList() : new List<string>());
            return theManagerList;
        }

        /// <summary>
        /// Panel's users consultant fee.
        /// </summary>
        /// <returns>Text description of a consultant fee</returns>
        public string ConsultantFeeText()
        {
            return this.PanelUserSessionPayRate() != null ? PanelUserSessionPayRate().ConsultantFeeText :
                PanelUserProgramPayRate()?.ConsultantFeeText;
        }

        /// <summary>
        /// Panel's users consultant fee amount.
        /// </summary>
        /// <returns>Amount of a consultant fee</returns>
        public decimal? ConsultantFeeAmount()
        {
            return this.PanelUserSessionPayRate() != null ? PanelUserSessionPayRate().ConsultantFee :
                PanelUserProgramPayRate()?.ConsultantFee;
        }

        /// <summary>
        /// The status of the participants registration contract document
        /// </summary>
        /// <returns>Identifier of current status</returns>
        public int? ContractStatusId()
        {
            return this.FindContractDocument().PanelUserRegistrationDocumentContracts.FirstOrDefault()?.ContractStatusId;
        }
        /// <summary>
        /// Panel user's description of work to be performed.
        /// </summary>
        /// <returns>Description of work</returns>
        public string DescriptionOfWork()
        {
            return this.PanelUserSessionPayRate() != null ? PanelUserSessionPayRate().DescriptionOfWork :
                PanelUserProgramPayRate()?.DescriptionOfWork;
        }
        /// <summary>
        /// Returns the contract document.
        /// </summary>
        /// <returns>PanelUserRegistrationDocument entity</returns>
        public PanelUserRegistrationDocument FindContractDocument()
        {
            return this.FindDocumentByType(RegistrationDocumentType.Indexes.ContractualAgreement);
        }
        /// <summary>
        /// Converts the restricted access value to a human readable value
        /// </summary>
        /// <returns>Partial if the RestrictedAssignedFlag is true; Full otherwise</returns>
        public string Level()
        {
            return Helper.Level(this.RestrictedAssignedFlag);
        }
        /// <summary>
        /// Determines if this reviewer is a potential chair.  By definition
        /// if one person recommended the reviewer as a potential chair then
        /// the result is true;
        /// </summary>
        /// <returns>True if one or more users recommended the reviewer as a chair false otherwise</returns>
        public bool IsPotentialChair()
        {
            return (this.ReviewerEvaluations.Any(x => x.RecommendChairFlag));
        }
        /// <summary>
        /// Returns the entity identifier.
        /// </summary>
        /// <returns>Entity identifier</returns>
        public int EntityId()
        {
            return this.PanelUserAssignmentId;
        }
        /// <summary>
        /// Returns the PanelAbbreviation the assignment is associated with.
        /// </summary>
        /// <returns>PanelAbbreviation</returns>
        public string GetPanelAbbreviation()
        {
            return this.SessionPanel.PanelAbbreviation;
        }
        /// <summary>
        /// Returns an indication if the reviewer has been assigned. 
        /// Since we are an "assignment" entity we return "true".
        /// </summary>
        /// <returns>True if the user has been assigned; false otherwise</returns>
        public bool IsAssigned()
        {
            return true;
        }
        /// <summary>
        /// Indicates if the reviewer has been recruited;
        /// </summary>
        /// <returns>Always false</returns>
        public bool IsRecruited()
        {
            return false;
        }
        /// <summary>
        /// Checks whether the user has any incomplete registrations
        /// </summary>
        /// <returns>True if registration is incomplete; otherwise false;</returns>
        public bool IsRegistrationComplete()
        {
            return this.PanelUserRegistrations.Any(x => x.RegistrationCompletedDate != null);
        }
        /// <summary>
        /// Indicates if the contract document is signed
        /// </summary>
        /// <returns>True if the contract document is signed; false otherwise</returns>
        public bool IsContractSigned()
        {
            PanelUserRegistrationDocument panelUserRegistrationDocumentEntity = this.FindContractDocument();
            return (panelUserRegistrationDocumentEntity != null) ? panelUserRegistrationDocumentEntity.IsSigned() : false;
        }
        /// <summary>
        /// Determines if the entity's fiscal year is greater than the target year.
        /// </summary>
        /// <param name="target">Target fiscal year</param>
        /// <returns>True if the fiscal year is greater than the the target; false otherwise</returns>
        public bool FiscalYearGreaterThan(int target)
        {
            return (SessionPanel.GetNumericFiscalYear() >= target);
        }
        /// <summary>
        /// Wrapper around ParticipationMethodid properties to reduced to the same typ.  In 
        /// PanelUserPotentialAssighment the property type is int? while the type is int in
        /// PanelUserAssighment.
        /// </summary>
        /// <returns>ParticipationMethodId</returns>
        public int? BoxParticipationMethodId()
        {
            return this.ParticipationMethodId as int?;
        }
        /// <summary>
        /// Wrapper around ClientParticipationTypeId properties to reduced to the same type.  In 
        /// PanelUserPotentialAssighment the property type is int? while the type is int in
        /// PanelUserAssighment.
        /// </summary>
        /// <returns>ClientParticipationTypeId</returns>
        public int? BoxClientParticipationTypeId()
        {
            return this.ClientParticipantTypeId as int?;
        }
        /// <summary>
        /// the primary contact phone number for a user.
        /// </summary>
        /// <returns>phone number as string</returns>
        public string PrimaryPhoneNumber()
        {
            return this.User.UserInfoEntity().GetPrimaryPhoneNumber();
        }
        /// <summary>
        /// Determines whether this instance of a panel user assignment is considered a moderator.
        /// </summary>
        /// <returns>Whether the user is a moderator for the panel</returns>
        public bool IsModerator()
        {
            return this.ClientParticipantType.IsSro();
        }
        /// <summary>
        /// Indicates if this assignment is for a chairperson and not a COI for the specified PanelApplication.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <returns>Whether the user is a moderator for the panel</returns>
        public bool IsChair(int panelApplicationId)
        {
            //
            // Find if they have a PanelApplicationReviewerAssignment on the panel
            //
            PanelApplicationReviewerAssignment assignment = this.PanelApplicationReviewerAssignments.FirstOrDefault(x => x.PanelApplicationId == panelApplicationId);
            //
            // If they have a PanelApplicationReviewerAssignment on this panel then check if it is a COI
            //
            if (assignment != null)
            {
                assignment = (assignment.ClientAssignmentType.IsCoi)? assignment: null;
            }
            //
            // they are a chair if they are a chair and not a COI
            //
            return this.ClientParticipantType.IsChair() & (assignment == null);
        }
        /// <summary>
        /// Indicates if this assignment is for a chairperson.
        /// </summary>
        /// <returns>Whether the user is a moderator for the panel</returns>
        public bool IsChair()
        {
            return this.ClientParticipantType.IsChair();
        }
        /// <summary>
        /// Determines whether [is cprit chair].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is cprit chair]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCpritChair()
        {
            return this.ClientParticipantType.IsCpritChair();
        }
        #endregion
    }
}
