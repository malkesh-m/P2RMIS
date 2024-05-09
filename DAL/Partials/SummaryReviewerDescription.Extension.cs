using System;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's SummaryReviewerDescription object.
    /// </summary>
    public partial class SummaryReviewerDescription : IStandardDateFields
    {
        /// <summary>
        /// Populates an instance of a SummaryReviewerDescription
        /// </summary>
        /// <param name="assignmentOrder">Original assignment order to which the custom order/display name is applied</param>
        /// <param name="customOrder">Custom order the assignment displays in SS</param>
        /// <param name="displayName">Custom name the assignemnt displays in SS</param>
        public void Populate(int assignmentOrder, int customOrder, string displayName)
        {
            AssignmentOrder = assignmentOrder;
            CustomOrder = customOrder;
            DisplayName = displayName;
        }
    }
}
