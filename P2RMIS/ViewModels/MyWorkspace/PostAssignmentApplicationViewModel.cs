using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.ViewModels;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Newtonsoft.Json;

namespace Sra.P2rmis.Web.UI.Models
{
    public class PostAssignmentApplicationViewModel
    {
        public PostAssignmentApplicationViewModel(IPostAssignmentModel postAssignmentApplication)
        {
            ApplicationId = postAssignmentApplication.ApplicationId;
            ApplicationLogNumber = postAssignmentApplication.ApplicationLogNumber;
            Title = postAssignmentApplication.Title;
            TitleCropped = ViewHelpers.CropText(Title, Invariables.MyWorkspace.MaxTitleLengthBeforeCropping);
            PiName = postAssignmentApplication.Blinded ? Invariables.MyWorkspace.Blinded : ViewHelpers.ConstructNameWithComma(postAssignmentApplication.PIFirstName, postAssignmentApplication.PILastName);
            PiNameCropped = ViewHelpers.CropText(PiName, Invariables.MyWorkspace.MaxPiNameLengthBeforeCropping);
            Mechanism = postAssignmentApplication.Mechanism;
            AssignmentText = GetAssignmentText(postAssignmentApplication.IsAssigned, postAssignmentApplication.PresentationOrder, postAssignmentApplication.ConflictFlag);            
            IsCoi = postAssignmentApplication.ConflictFlag != null && (bool)postAssignmentApplication.ConflictFlag;
            OverallScore =  (!postAssignmentApplication.IsAssigned || IsCoi) ? Invariables.MyWorkspace.NonApplicable : postAssignmentApplication.OverallScore;
            IsCritiqueSubmitted = postAssignmentApplication.IsCritiqueSubmitted;
            ExpertiseLevelOrCoi = IsCoi ? Invariables.MyWorkspace.Coi : postAssignmentApplication.ExpertiseLevel;
            var canView = PhaseStateConverter.CanView(postAssignmentApplication.NewCritiqueActionState);
            var isPrelim = postAssignmentApplication.CritiquePhaseName == Invariables.MyWorkspace.Preliminary;
            var canStart = PhaseStateConverter.CanStart(postAssignmentApplication.NewCritiqueActionState);
            var canEdit = PhaseStateConverter.CanEdit(postAssignmentApplication.NewCritiqueActionState);
            var isExpired = PhaseStateConverter.IsExpired(postAssignmentApplication.NewCritiqueActionState);
            EnabledNormalView = PhaseStateConverter.EnabledNormalView(postAssignmentApplication.NewCritiqueActionState);
            DisabledNormalView = PhaseStateConverter.DisabledNormalView(postAssignmentApplication.NewCritiqueActionState);
            EnabledNormalEdit = PhaseStateConverter.EnabledNormalEdit(postAssignmentApplication.NewCritiqueActionState);
            EnabledAbnormalEdit = PhaseStateConverter.EnabledAbnormalEdit(postAssignmentApplication.NewCritiqueActionState);
            DisabledAbnormalEdit = PhaseStateConverter.DisabledAbnormalEdit(postAssignmentApplication.NewCritiqueActionState);
            EnabledSubmittedView = PhaseStateConverter.EnabledSubmittedView(postAssignmentApplication.NewCritiqueActionState);
            DisabledSubmittedView = PhaseStateConverter.DisabledSubmittedView(postAssignmentApplication.NewCritiqueActionState);
            CanAccessDiscussionBoard = OnLineDiscussions.ShowDiscussionBoard(postAssignmentApplication.IsChairPerson, postAssignmentApplication.IsAssigned, postAssignmentApplication.OnLineDiscussionStatus.IsThereDiscussion);
            ApplicationStageStepId = postAssignmentApplication.OnLineDiscussionStatus.ApplicationStageStepId;
            var isReopened = postAssignmentApplication.IsReopened;
            ContainCritiqueLink = !IsCoi && (canView || canStart || canEdit);
            CritiqueText = IsCoi ? Invariables.MyWorkspace.NonApplicable : 
                (isExpired ? Invariables.MyWorkspace.Expired : 
                ((canStart && isPrelim) ? (isReopened ? Invariables.MyWorkspace.StartPrelim : Invariables.MyWorkspace.Start) : 
                        (canEdit ? ((isReopened && isPrelim) ? Invariables.MyWorkspace.EditPrelim : Invariables.MyWorkspace.Edit) : 
                            (canView ? Invariables.MyWorkspace.View : string.Empty))));
            PanelApplicationId = postAssignmentApplication.PanelApplicationId;
            PanelUserAssignmentId = postAssignmentApplication.PanelUserAssignmentId;
            SessionPanelId = postAssignmentApplication.SessionPanelId;
            HasExpertiseLevelOrCoi = !string.IsNullOrEmpty(ExpertiseLevelOrCoi);
            HasAssigned = postAssignmentApplication.IsAssigned && !IsCoi;
            NeedCritiqueAction = !IsCritiqueSubmitted;
        }

        #region Properties
        /// <summary>
        /// PanelApplication entity identifier
        /// </summary>
        [JsonProperty("panelApplicationId")]
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// Application entity identifier
        /// </summary>
        [JsonProperty("applicationId")]
        public int ApplicationId { get; set; }
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        [JsonProperty("sessionPanelId")]
        public int SessionPanelId { get; set; }
        /// <summary>
        /// PanelUserAssignment entity identifier
        /// </summary>
        [JsonProperty("panelUserAssignmentId")]
        public int? PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Application log number
        /// </summary>
        [JsonProperty("logNumber")]
        public string ApplicationLogNumber { get; private set; }
        /// <summary>
        /// Application title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; private set; }
        /// <summary>
        /// Application title cropped
        /// </summary>
        public string TitleCropped { get; private set; }
        /// <summary>
        /// Principal investigator name
        /// </summary>
        [JsonProperty("piName")]
        public string PiName { get; private set; }
        /// <summary>
        /// Principal investigator name cropped
        /// </summary>
        public string PiNameCropped { get; private set; }
        /// <summary>
        /// Mechanism
        /// </summary>
        [JsonProperty("mechanism")]
        public string Mechanism { get; private set; }
        /// <summary>
        /// User's experience level or COI
        /// </summary>
        [JsonProperty("expertiseLevelOrCoi")]
        public string ExpertiseLevelOrCoi { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance has expertise level or coi.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has expertise level or coi; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("hasExpertiseLevelOrCoi")]
        public bool HasExpertiseLevelOrCoi { get; private set; }
        /// <summary>
        /// Flag to indicate if the user is a COI
        /// </summary>
        [JsonProperty("isCoi")]
        public bool IsCoi { get; private set; }
        /// <summary>
        /// Overall score
        /// </summary>
        [JsonProperty("overallScore")]
        public string OverallScore { get; private set; }
        /// <summary>
        /// Flag to indicate if the view contains a critique link
        /// </summary>
        public bool ContainCritiqueLink { get; private set; }
        /// <summary>
        /// Critique's display text
        /// </summary>
        public string CritiqueText { get; private set; }
        /// <summary>
        /// Flag to indicate if this instance of critique is submitted. 
        /// </summary>
        public bool IsCritiqueSubmitted { get; private set; }
        /// <summary>
        /// Assignment status
        /// </summary>
        [JsonProperty("assignmentText")]
        public string AssignmentText { get; private set; }
        /// <summary>
        /// Panel step identifier.
        /// </summary>
        public int PanelStepId { get; set; }
        /// <summary>
        /// Critique step identifier.
        /// </summary>
        public int CritiqueStepId { get; set; }
        /// <summary>
        /// Gets a value indicating whether [enabled normal view].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enabled normal view]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("enabledNormalView")]
        public bool EnabledNormalView { get; private set; }
        /// <summary>
        /// Gets a value indicating whether [disabled normal view].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [disabled normal view]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("disabledNormalView")]
        public bool DisabledNormalView { get; private set; }
        /// <summary>
        /// Gets a value indicating whether [enabled normal edit].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enabled normal edit]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("enabledNormalEdit")]
        public bool EnabledNormalEdit { get; private set; }
        /// <summary>
        /// Gets a value indicating whether [enabled abnormal edit].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enabled abnormal edit]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("enabledAbnormalEdit")]
        public bool EnabledAbnormalEdit { get; private set; }
        /// <summary>
        /// Gets a value indicating whether [disabled abnormal edit].
        /// </summary>
        /// <value>
        /// <c>true</c> if [disabled abnormal edit]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("disabledAbnormalEdit")]
        public bool DisabledAbnormalEdit { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the Enabled Submitted view icon should be shown
        /// </summary>
        [JsonProperty("enabledSubmittedView")]
        public bool EnabledSubmittedView { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the Disabled Submitted view icon should be shown
        /// </summary>
        [JsonProperty("disabledSubmittedView")]
        public bool DisabledSubmittedView { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance can access discussion board.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can access discussion board; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("canAccessDiscussionBoard")]
        public bool CanAccessDiscussionBoard { get; private set; }
        /// <summary>
        /// Gets the application stage step identifier.
        /// </summary>
        /// <value>
        /// The application stage step identifier.
        /// </value>
        [JsonProperty("applicationStageStepId")]
        public int ApplicationStageStepId { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has assigned.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has assigned; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("hasAssigned")]
        public bool HasAssigned { get; set; }
        /// <summary>
        /// Gets a value indicating whether [need critique action].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [need critique action]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("needCritiqueAction")]
        public bool NeedCritiqueAction { get; private set; }
        /// <summary>
        /// Indicates if the reviewer is a chairperson
        /// </summary>
        //public bool IsChairPerson { get; private set; }
        #endregion

        /// <summary>
        /// Get assignment text
        /// </summary>
        /// <param name="isAssigned">Whether the current user is assigned</param>
        /// <param name="presentationOrder">Presentation order</param>
        /// <param name="conflictFlag">Conflict flag</param>
        /// <returns>Assigned or unassigned</returns>
        /// <remarks>TO BE REFACTORED</remarks>
        private string GetAssignmentText(bool isAssigned, int? presentationOrder, bool? conflictFlag)
        {
            string assignmentText;
            if (conflictFlag != null && (bool)conflictFlag)
            {
                assignmentText = Invariables.MyWorkspace.Coi;
            }
            else
            {
                string po = (presentationOrder == null) ? string.Empty : " - " + presentationOrder;
                assignmentText = isAssigned ? Invariables.MyWorkspace.Assigned + po : Invariables.MyWorkspace.Unassigned;
            }
            return assignmentText;
        }
    }
}