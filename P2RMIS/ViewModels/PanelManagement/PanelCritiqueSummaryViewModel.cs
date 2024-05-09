using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.Web.Common;

namespace Sra.P2rmis.Web.ViewModels.PanelManagement
{
    public interface IPanelCritiqueSummaryViewModel
    {
        /// <summary>
        /// Gets or sets the application workflow identifier.
        /// </summary>
        /// <value>
        /// The application workflow identifier.
        /// </value>
        int ApplicationWorkflowId { get; set; }
        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        int PanelApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        string LogNumber { get; set; }
        /// <summary>
        /// Gets or sets the award.
        /// </summary>
        /// <value>
        /// The award.
        /// </value>
        string Award { get; set; }
        /// <summary>
        /// Gets or sets the pi.
        /// </summary>
        /// <value>
        /// The pi.
        /// </value>
        string Pi { get; set; }
        /// <summary>
        /// Gets or sets the reviewer.
        /// </summary>
        /// <value>
        /// The reviewer.
        /// </value>
        string Reviewer { get; set; }
        /// <summary>
        /// Gets or sets the reviewer identifier.
        /// </summary>
        /// <value>
        /// The reviewer identifier.
        /// </value>
        int? ReviewerId { get; set; }
        /// <summary>
        /// Gets or sets the type of the assignment.
        /// </summary>
        /// <value>
        /// The type of the assignment.
        /// </value>
        string AssignmentType { get; set; }
        /// <summary>
        /// Gets or sets the pre status.
        /// </summary>
        /// <value>
        /// The pre status.
        /// </value>
        string PreStatus { get; set; }
        /// <summary>
        /// Gets or sets the pre action.
        /// </summary>
        /// <value>
        /// The pre action.
        /// </value>
        List<string> PreAction { get; set; }
        /// <summary>
        /// Gets or sets the pre application workflow step identifier.
        /// </summary>
        /// <value>
        /// The pre application workflow step identifier.
        /// </value>
        int PreApplicationWorkflowStepId { get; set; }
        /// <summary>
        /// Gets or sets the pre application stage step identifier.
        /// </summary>
        /// <value>
        /// The pre application stage step identifier.
        /// </value>
        int PreApplicationStageStepId { get; set; }
        /// <summary>
        /// Gets or sets the revised status.
        /// </summary>
        /// <value>
        /// The revised status.
        /// </value>
        string RevisedStatus { get; set; }
        /// <summary>
        /// Gets or sets the revised action.
        /// </summary>
        /// <value>
        /// The revised action.
        /// </value>
        List<string> RevisedAction { get; set; }
        /// <summary>
        /// Gets or sets the revised application workflow step identifier.
        /// </summary>
        /// <value>
        /// The revised application workflow step identifier.
        /// </value>
        int RevisedApplicationWorkflowStepId { get; set; }
        /// <summary>
        /// Gets or sets the revised application stage step identifier.
        /// </summary>
        /// <value>
        /// The revised application stage step identifier.
        /// </value>
        int RevisedApplicationStageStepId { get; set; }
        /// <summary>
        /// Gets or sets the online status.
        /// </summary>
        /// <value>
        /// The online status.
        /// </value>
        string OnlineStatus { get; set; }
        /// <summary>
        /// Gets or sets the online action.
        /// </summary>
        /// <value>
        /// The online action.
        /// </value>
        List<string> OnlineAction { get; set; }
        /// <summary>
        /// Gets or sets the online discussion.
        /// </summary>
        /// <value>
        /// The online discussion.
        /// </value>
        string OnlineDiscussion { get; set; }
        /// <summary>
        /// Gets or sets the online application workflow step identifier.
        /// </summary>
        /// <value>
        /// The online application workflow step identifier.
        /// </value>
        int OnlineApplicationWorkflowStepId { get; set; }
        /// <summary>
        /// Gets or sets the online application stage step identifier.
        /// </summary>
        /// <value>
        /// The online application stage step identifier.
        /// </value>
        int OnlineApplicationStageStepId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can view action.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can view action; otherwise, <c>false</c>.
        /// </value>
        bool CanViewAction { get; set; }

        /// <summary>
        /// Whether the currently logged in user is a COI for the application
        /// </summary>
        bool IsCurrentUserCoi { get; set; }
    }

    public class PanelCritiqueSummaryViewModel : IPanelCritiqueSummaryViewModel
    {
        public const string Completed = "Completed";
        public const string Active = "Active";
        public const string Start = "Start";

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelCritiqueSummaryViewModel"/> class.
        /// </summary>
        /// <param name="logNumber">The log number.</param>
        /// <param name="award">The award.</param>
        /// <param name="piFirstName">First name of the pi.</param>
        /// <param name="piLastName">Last name of the pi.</param>
        /// <param name="reviewerFirstName">First name of the reviewer.</param>
        /// <param name="reviewerLastName">Last name of the reviewer.</param>
        /// <param name="reviewerId">The reviewer identifier.</param>
        /// <param name="assignmentDescription">The assignment description.</param>
        /// <param name="assignmentOrder">The assignment order.</param>
        /// <param name="isCoi">if set to <c>true</c> [is coi].</param>
        /// <param name="isReader">if set to <c>true</c> [is reader].</param>
        public PanelCritiqueSummaryViewModel(string logNumber, string award, string piLastName, string piFirstName, string reviewerLastName, string reviewerFirstName, int reviewerId,
            string assignmentDescription, int assignmentOrder, bool isCoi, bool isReader, int panelApplicationId, bool isCurrentUserCoi)
        {
            LogNumber = logNumber;
            Award = award;
            Pi = ViewHelpers.ConstructName(piLastName, piFirstName);
            Reviewer = ViewHelpers.ConstructName(reviewerLastName, reviewerFirstName);
            ReviewerId = reviewerId;
            AssignmentType = String.Format("{0} {1}", assignmentDescription, assignmentOrder.ToString());
            CanViewAction = !isCoi && !isReader;
            PanelApplicationId = panelApplicationId;
            IsCurrentUserCoi = isCurrentUserCoi;
            
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PanelCritiqueSummaryViewModel"/> class based on partial field data.
        /// </summary>
        /// <param name="logNumber">The log number.</param>
        /// <param name="reviewerFirstName">First name of the reviewer.</param>
        /// <param name="reviewerLastName">Last name of the reviewer.</param>
        /// <param name="reviewerId">The reviewer identifier.</param>
        /// <param name="assignmentDescription">The assignment description.</param>
        /// <param name="assignmentOrder">The assignment order.</param>
        /// <param name="isCoi">if set to <c>true</c> [is coi].</param>
        /// <param name="isReader">if set to <c>true</c> [is reader].</param>
        public PanelCritiqueSummaryViewModel(string logNumber, bool isCoi, bool isReader, int panelApplicationId, bool isCurrentUserCoi)
        {
            LogNumber = logNumber;
            CanViewAction = !isCoi && !isReader;
            PanelApplicationId = panelApplicationId;
            IsCurrentUserCoi = isCurrentUserCoi;

        }
        /// <summary>
        /// Gets or sets the application workflow identifier.
        /// </summary>
        /// <value>
        /// The application workflow identifier.
        /// </value>
        public int ApplicationWorkflowId { get; set; }
        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        public string LogNumber { get; set; }
        /// <summary>
        /// Gets or sets the award.
        /// </summary>
        /// <value>
        /// The award.
        /// </value>
        public string Award { get; set; }
        /// <summary>
        /// Gets or sets the pi.
        /// </summary>
        /// <value>
        /// The pi.
        /// </value>
        public string Pi { get; set; }
        /// <summary>
        /// Gets or sets the reviewer.
        /// </summary>
        /// <value>
        /// The reviewer.
        /// </value>
        public string Reviewer { get; set; }
        /// <summary>
        /// Gets or sets the reviewer identifier.
        /// </summary>
        /// <value>
        /// The reviewer identifier.
        /// </value>
        public int? ReviewerId { get; set; }
        /// <summary>
        /// Gets or sets the type of the assignment.
        /// </summary>
        /// <value>
        /// The type of the assignment.
        /// </value>
        public string AssignmentType { get; set; }
        /// <summary>
        /// Gets or sets the pre status.
        /// </summary>
        /// <value>
        /// The pre status.
        /// </value>
        public string PreStatus { get; set; }
        /// <summary>
        /// Gets or sets the pre action.
        /// </summary>
        /// <value>
        /// The pre action.
        /// </value>
        public List<string> PreAction { get; set; }
        /// <summary>
        /// Gets or sets the pre application workflow step identifier.
        /// </summary>
        /// <value>
        /// The pre application workflow step identifier.
        /// </value>
        public int PreApplicationWorkflowStepId { get; set; }
        /// <summary>
        /// Gets or sets the pre application stage step identifier.
        /// </summary>
        /// <value>
        /// The pre application stage step identifier.
        /// </value>
        public int PreApplicationStageStepId { get; set; }
        /// <summary>
        /// Gets or sets the revised status.
        /// </summary>
        /// <value>
        /// The revised status.
        /// </value>
        public string RevisedStatus { get; set; }
        /// <summary>
        /// Gets or sets the revised action.
        /// </summary>
        /// <value>
        /// The revised action.
        /// </value>
        public List<string> RevisedAction { get; set; }
        /// <summary>
        /// Gets or sets the revised application workflow step identifier.
        /// </summary>
        /// <value>
        /// The revised application workflow step identifier.
        /// </value>
        public int RevisedApplicationWorkflowStepId { get; set; }
        /// <summary>
        /// Gets or sets the revised application stage step identifier.
        /// </summary>
        /// <value>
        /// The revised application stage step identifier.
        /// </value>
        public int RevisedApplicationStageStepId { get; set; }
        /// <summary>
        /// Gets or sets the online status.
        /// </summary>
        /// <value>
        /// The online status.
        /// </value>
        public string OnlineStatus { get; set; }
        /// <summary>
        /// Gets or sets the online action.
        /// </summary>
        /// <value>
        /// The online action.
        /// </value>
        public List<string> OnlineAction { get; set; }
        /// <summary>
        /// Gets or sets the online application workflow step identifier.
        /// </summary>
        /// <value>
        /// The online application workflow step identifier.
        /// </value>
        public int OnlineApplicationWorkflowStepId { get; set; }
        /// <summary>
        /// Gets or sets the online application stage step identifier.
        /// </summary>
        /// <value>
        /// The online application stage step identifier.
        /// </value>
        public int OnlineApplicationStageStepId { get; set; }
        /// <summary>
        /// Gets or sets the online discussion.
        /// </summary>
        /// <value>
        /// The online discussion.
        /// </value>
        public string OnlineDiscussion { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can view action.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can view action; otherwise, <c>false</c>.
        /// </value>
        public bool CanViewAction { get; set; }
        /// <summary>
        /// Gets or sets the score formatter.
        /// </summary>
        /// <value>
        /// The score formatter.
        /// </value>
        public static ScoreFormatter ScoreFormatter { get; set; }

        /// <summary>
        /// Whether the currently logged in user is a COI for the application
        /// </summary>
        public bool IsCurrentUserCoi { get; set; } 

        #region Helpers                
        /// <summary>
        /// Sets the phase.
        /// </summary>
        /// <param name="phase">The phase.</param>
        /// <param name="action">The action.</param>
        /// <param name="phaseIndex">Index of the phase.</param>
        /// <param name="isModPhase">if set to <c>true</c> [is mod phase].</param>
        /// <param name="isModActive">if set to <c>true</c> [is mod active].</param>
        /// <param name="isModReady">if set to <c>true</c> [is mod ready].</param>
        /// <param name="isModClosed">if set to <c>true</c> [is mod closed].</param>
        public void SetPhase(ICritiquePhaseInformation phase, List<string> action, int phaseIndex, bool isModPhase, bool isModActive, bool isModReady, bool isModClosed, int? applicationStageStepId)
        {

            ApplicationWorkflowId = phase.ApplicationWorkflowId;

            List<string> sps = new List<string>();
            if (phase.IsSubmitted)
            {
                sps.Add(Invariables.Labels.PanelManagement.Critiques.Submitted);
                sps.Add(ViewHelpers.FormatDate(phase.DateSubmitted));
                var formattedScore = phase.PhaseStartDate <= GlobalProperties.P2rmisDateTimeNow ? ScoreFormatter(phase.ScoreRating, phase.ScoreType, phase.AdjectivalRating, phase.IsSubmitted) : null;
                sps.Add(formattedScore);
            }
            else
            {
                var formattedStatus = (phase.ContentExists && phase.PhaseStartDate <= GlobalProperties.P2rmisDateTimeNow) ?
                    Invariables.Labels.PanelManagement.Critiques.NotSubmitted :
                    Invariables.Labels.PanelManagement.Critiques.NotStarted;
                sps.Add(formattedStatus);
            }
            
            if (phaseIndex == 0)
                SetPrePhase(string.Join(" ", sps), action, phase.ApplicationWorkflowStepId, (int)applicationStageStepId);
            else if (phaseIndex == 1 && !isModPhase)
                SetRevisedPhase(string.Join(" ", sps), action, phase.ApplicationWorkflowStepId, (int)applicationStageStepId);
            else if (isModPhase)
                SetOnlinePhase(string.Join(" ", sps), action, phase.ApplicationWorkflowStepId, (int)applicationStageStepId, isModActive, isModReady, isModClosed);
        }
        /// <summary>
        /// Sets the pre phase.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="action">The action.</param>
        /// <param name="applicationWorkflowStepId">The application workflow step identifier.</param>
        /// <param name="applicationStageStepId">The application stage step identifier.</param>
        private void SetPrePhase(string status, List<string> action, int applicationWorkflowStepId, int applicationStageStepId)
        {
            PreStatus = status;
            PreAction = action;
            PreApplicationWorkflowStepId = applicationWorkflowStepId;
            PreApplicationStageStepId = applicationStageStepId;
        }
        /// <summary>
        /// Sets the revised phase.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="action">The action.</param>
        /// <param name="applicationWorkflowStepId">The application workflow step identifier.</param>
        /// <param name="applicationStageStepId">The application stage step identifier.</param>
        private void SetRevisedPhase(string status, List<string> action, int applicationWorkflowStepId, int applicationStageStepId)
        {
            RevisedStatus = status;
            RevisedAction = action;
            RevisedApplicationWorkflowStepId = applicationWorkflowStepId;
            RevisedApplicationStageStepId = applicationStageStepId;
        }
        /// <summary>
        /// Sets the online phase.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="action">The action.</param>
        /// <param name="applicationWorkflowStepId">The application workflow step identifier.</param>
        /// <param name="applicationStageStepId">The application stage step identifier.</param>
        /// <param name="isModActive">if set to <c>true</c> [is mod active].</param>
        /// <param name="isModReady">if set to <c>true</c> [is mod ready].</param>
        /// <param name="isModClosed">if set to <c>true</c> [is mod closed].</param>
        private void SetOnlinePhase(string status, List<string> action, int applicationWorkflowStepId, int applicationStageStepId, bool isModActive, bool isModReady, bool isModClosed)
        {
            OnlineStatus = status;
            OnlineAction = action;
            OnlineApplicationWorkflowStepId = applicationWorkflowStepId;
            OnlineApplicationStageStepId = applicationStageStepId;
            if (isModClosed)
                OnlineDiscussion = Completed;
            else if (isModReady)
                OnlineDiscussion = (isModActive) ? Active : Start;
            else
                OnlineDiscussion = (isModActive) ? Completed : null;
        }
        #endregion
    }
}