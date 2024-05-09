using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's PanelApplicationReviewerAssignment object. 
    /// </summary>	
    public partial class PanelApplicationReviewerAssignment : IStandardDateFields
    {
        /// <summary>
        /// Sort order values indicating presentation order
        /// </summary>
        public class PresentationOrder
        {
            /// <summary>
            /// Sort order value indicating the reviewer is first presenter.
            /// </summary>
            public const int First = 1;
            /// <summary>
            /// Sort order value indicating the reviewer is Second presenter.
            /// </summary>
            public const int Second = 2;
            /// <summary>
            /// Sort order value indicating the reviewer is a presenter (should be used with '>').
            /// </summary>
            public const int Any = 0;
        }
        /// <summary>
        /// Default for Reviewer Presentation Order if Null
        /// </summary>
        /// <remarks>Used for sorting so nulls go to the bottom to match typical requirement.</remarks>
        public const int DefaultSortOrder = 99;
        /// <summary>
        /// Default value for Linq search.
        /// </summary>
        private static PanelApplicationReviewerAssignment _default;
        public static PanelApplicationReviewerAssignment Default
        {
            get 
            {
                //
                // Lazy load the default
                //
                if (_default == null)
                {
                    _default = new PanelApplicationReviewerAssignment();
                    _default.ClientAssignmentType = new ClientAssignmentType();
                }
                return _default; 
            }
        }
        /// <summary>
        /// Returns null if no SortOrder assigned.  Primarily used for Linq retrieval
        /// to indicate there was no selection.
        /// </summary>
        public int? NullableSortOrder
        {
            get
            {
                int? result = null;

                if (this.SortOrder > 0)
                {
                    result = this.SortOrder;
                }
                return result;
            }
        }
        /// <summary>
        /// Determines if the sort order is set by another user.
        /// </summary>
        /// <param name="presentationOrder">Presentation order</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <returns>True if the presentation order is set by another user</returns>
        public bool IsPresentationOrderSetForOtherUser(int presentationOrder, int panelUserAssignmentId)
        {
            return ((SortOrder == presentationOrder) && (PanelUserAssignmentId != panelUserAssignmentId));
        }
        /// <summary>
        /// Checks to see if either the sort order or client assignment type changed
        /// </summary>
        /// <param name="presentationOrder">Presentation order</param>
        /// <param name="clientAssignmentTypeId">Client assignment type identifier</param>
        /// <returns></returns>
        public bool IsSame(int? presentationOrder, int clientAssignmentTypeId)
        {
            return ((this.SortOrder == presentationOrder) && (this.ClientAssignmentTypeId == clientAssignmentTypeId));
        }
        /// <summary>
        /// Modify the sort order & client assignment type
        /// </summary>
        /// <param name="presentationOrder">New sort order</param>
        /// <param name="clientAssighmentTypeId">New client assignment type identifer</param>
        /// <param name="userId">User identifier of user making the change.</param>
        public PanelApplicationReviewerAssignment Modify(int? presentationOrder, int clientAssighmentTypeId, int userId)
        {
            this.SortOrder = presentationOrder;
            this.ClientAssignmentTypeId = clientAssighmentTypeId;
            Helper.UpdateModifiedFields(this, userId);

            return this;
        }
        /// <summary>
        /// Populates a new PanelApplicationReviewerAssignment entity object.
        /// </summary>
        /// <param name="presentationOrder">New sort order</param>
        /// <param name="clientAssighmentTypeId">New client assignment type identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="userId">User identifier of user making the change.</param>
        public PanelApplicationReviewerAssignment Populate(int? presentationOrder, int clientAssighmentTypeId, int panelApplicationId, int panelUserAssignmentId, int userId)
        {
            //
            // Set the data
            //
            this.SortOrder = presentationOrder;
            this.ClientAssignmentTypeId = clientAssighmentTypeId;
            this.PanelApplicationId = panelApplicationId;
            this.PanelUserAssignmentId = panelUserAssignmentId;
            //
            // Now set the standard fields
            //
            Helper.UpdateCreatedFields(this, userId);
            Helper.UpdateModifiedFields(this, userId);

            return this;
        }
        /// <summary>
        /// Determines if the last premeeting critique has been submitted for a reviewer assignment
        /// </summary>
        /// <returns>True if critiques exist, otherwise false</returns>
        public bool IsPreMeetingCritiqueSubmitted()
        {
            var panelAppRevAssignment = this;
            var returnVal = 
                AssignmentType.CritiqueAssignments.Contains(this.ClientAssignmentType.AssignmentTypeId) ? 
                this.PanelApplication.ApplicationStages.FirstOrDefault(x => x.ReviewStageId == ReviewStage.Asynchronous)
                    .ApplicationWorkflows.FirstOrDefault(x => x.PanelUserAssignmentId == this.PanelUserAssignmentId)
                    .ApplicationWorkflowSteps.OrderByDescending(o => o.StepOrder).FirstOrDefault().Resolution : false;
            return returnVal;
        }
        /// <summary>
        /// Wrapper indicating if this assignment is a COI assignment
        /// </summary>
        /// <returns>True if the assignment is a COI; false otherwise</returns>
        public bool IsCoi()
        {
            return this.ClientAssignmentType.IsCoi;
        }

        /// <summary>
        /// Gets the assignment abbreviation.
        /// </summary>
        /// <returns>The assignment abbreviation</returns>
        public string AssignmentAbbreviation()
        {
            return this.ClientAssignmentType.AssignmentAbbreviation;
        }

        /// <summary>
        /// Gets the role name.
        /// </summary>
        /// <returns>The role name of the panel user assignment</returns>
        public string RoleName()
        {
            return this.PanelUserAssignment.ClientRole != null ? this.PanelUserAssignment.ClientRole.RoleName : string.Empty;
        }
    }
}
