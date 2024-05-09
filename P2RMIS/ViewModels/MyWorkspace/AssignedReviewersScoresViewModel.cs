
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model definition for the assigned reviewers scores page
    /// </summary>
    public class AssignedReviewersScoresViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignedReviewersScoresViewModel"/> class.
        /// </summary>
        /// <param name="webModel">The reviewer critique summary web model.</param>
        /// <param name="appInfo">The application information.</param>
        public AssignedReviewersScoresViewModel(IReviewerCritiqueSummary webModel, IApplicationInformationModel appInfo)
        {
            this.ReviewerPhaseList = new List<ReviewerPhaseViewModel>();
            this.CriteriaGridData = new List<CriteriaReviewerScoreViewModel>();
            this.OverallGridData = new List<CriteriaReviewerScoreViewModel>();
            this.LogNumber = appInfo.LogNumber;
            this.ProgramAbbreviation = appInfo.ProgramAbbreviation;
            this.FiscalYear = appInfo.FiscalYear;
            this.PanelAbbreviation = appInfo.PanelAbbreviation;
            this.SessionPanelId = appInfo.SessionPanelId;
            this.PanelApplicationId = appInfo.PanelApplicationId;
            PopulateReviewerList(webModel.ReviewerList, webModel);
        }

        

        /// <summary>
        /// Populates the reviewer list.
        /// </summary>
        /// <param name="reviewerList">The reviewer list.</param>
        /// <param name="webModel">The web model.</param>
        internal void PopulateReviewerList(IEnumerable<IReviewerModel> reviewerList, IReviewerCritiqueSummary webModel)
        {
            foreach (var reviewer in reviewerList)
            {
                ReviewerPhaseList.Add(new ReviewerPhaseViewModel(reviewer, webModel));
            }
            this.ReviewerPhaseList = this.ReviewerPhaseList.OrderBy(x => x.SortOrder).ToList();
        }

        /// <summary>
        /// Gets or sets the reviewer phase list.
        /// </summary>
        /// <value>
        /// List of reviewers with phases nested.
        /// </value>
        public List<ReviewerPhaseViewModel> ReviewerPhaseList { get; set; }

        /// <summary>
        /// Gets or sets the criteria grid data.
        /// </summary>
        /// <value>
        /// The grid data of reviewer scores by criteria for non-overall.
        /// </value>
        public List<CriteriaReviewerScoreViewModel> CriteriaGridData { get; set; }

        /// <summary>
        /// Gets or sets the overall grid data.
        /// </summary>
        /// <value>
        /// The grid data of reviewer scores by criteria for overall.
        /// </value>
        public List<CriteriaReviewerScoreViewModel> OverallGridData { get; set; }

        /// <summary>
        /// Gets or sets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        public string PanelAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        public string FiscalYear { get; set; }

        /// <summary>
        /// Gets or sets the program abbreviation.
        /// </summary>
        /// <value>
        /// The program abbreviation.
        /// </value>
        public string ProgramAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        public string LogNumber { get; set; }

        /// <summary>
        /// Gets or sets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        public int PanelApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the session panel identifier.
        /// </summary>
        /// <value>
        /// The session panel identifier.
        /// </value>
        public int SessionPanelId { get; set; }
    }

    /// <summary>
    /// Sub model for phase information
    /// </summary>
    public class PhaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhaseViewModel"/> class.
        /// </summary>
        /// <param name="webModel">The web model representing phase information.</param>
        public PhaseViewModel(IPhaseModel webModel)
        {
            this.PhaseName = webModel.PhaseName;
            this.StepTypeId = webModel.StepTypeId = webModel.StepTypeId;
        }

        /// <summary>
        /// Gets the step type identifier.
        /// </summary>
        /// <value>
        /// The step type identifier.
        /// </value>
        public int StepTypeId { get; private set; }

        /// <summary>
        /// Gets the name of the phase.
        /// </summary>
        /// <value>
        /// The name of the phase.
        /// </value>
        public string PhaseName { get; private set; }

        /// <summary>
        /// Gets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public int SortOrder { get; private set; }
    }

    /// <summary>
    /// Sub view model for reviewer phase information
    /// </summary>
    public class ReviewerPhaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewerPhaseViewModel"/> class.
        /// </summary>
        /// <param name="revModel">The reviewer web model.</param>
        /// <param name="webModel">The web model representing reviewer critique summary information.</param>
        public ReviewerPhaseViewModel(IReviewerModel revModel, IReviewerCritiqueSummary webModel)
        {
            this.PanelUserAssignmentId = revModel.PanelUserAssignmentId;
            this.FirstName = revModel.FirstName;
            this.LastName = revModel.LastName;
            this.SortOrder = revModel.SortOrder;
            this.AssignmentTypeAbbreviation = revModel.AssignmentTypeAbbreviation;
            this.ParticipantRole = revModel.ParticipantRole;
            this.PhaseList = new List<PhaseViewModel>();
            PopulatePhaseList(webModel.PhaseList);
        }

        /// <summary>
        /// Gets or sets the participant role.
        /// </summary>
        /// <value>
        /// The participant role.
        /// </value>
        public string ParticipantRole { get; set; }

        /// <summary>
        /// Gets or sets the assignment type abbreviation.
        /// </summary>
        /// <value>
        /// The assignment type abbreviation.
        /// </value>
        public string AssignmentTypeAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int PanelUserAssignmentId { get; set; }

        /// <summary>
        /// Gets or sets the phase list.
        /// </summary>
        /// <value>
        /// The phase list.
        /// </value>
        public List<PhaseViewModel> PhaseList { get; set; }

        /// <summary>
        /// Populates the phase list.
        /// </summary>
        /// <param name="phaseList">The phase list.</param>
        internal void PopulatePhaseList(IEnumerable<IPhaseModel> phaseList)
        {
            foreach (var phase in phaseList)
            {
                PhaseList.Add(new PhaseViewModel(phase));
            }
            this.PhaseList = this.PhaseList.OrderBy(x => x.SortOrder).ToList();
        }
    }

    /// <summary>
    /// Sub View model representing reviewer scores by criteria 
    /// </summary>
    public class CriteriaReviewerScoreViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaReviewerScoreViewModel"/> class.
        /// </summary>
        public CriteriaReviewerScoreViewModel()
        {
            this.ReviewerPhaseScores = new List<ReviewerPhaseScoreViewModel>();
        }
        /// <summary>
        /// Gets or sets the name of the criteria.
        /// </summary>
        /// <value>
        /// The name of the criteria.
        /// </value>
        public string CriteriaName { get; set; }
        /// <summary>
        /// Gets or sets the criteria sort order.
        /// </summary>
        /// <value>
        /// The criteria sort order.
        /// </value>
        public int CriteriaSortOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the criteria is overall.
        /// </summary>
        /// <value>
        ///   <c>true</c> if criteria is overall; otherwise, <c>false</c>.
        /// </value>
        public bool OverallFlag { get; set; }
        /// <summary>
        /// Gets or sets the reviewer phase scores.
        /// </summary>
        /// <value>
        /// The reviewer phase scores.
        /// </value>
        public List<ReviewerPhaseScoreViewModel> ReviewerPhaseScores { get; set; } 
    }

    /// <summary>
    /// Sub view model for reviewer scores by phase
    /// </summary>
    public class ReviewerPhaseScoreViewModel
    {
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Gets or sets the step type identifier.
        /// </summary>
        /// <value>
        /// The step type identifier.
        /// </value>
        public int StepTypeId { get; set; }
        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public string Score { get; set; }
        /// <summary>
        /// Gets or sets the application workflow step identifier.
        /// </summary>
        /// <value>
        /// The application workflow step identifier.
        /// </value>
        public int ApplicationWorkflowStepId { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
    }
}