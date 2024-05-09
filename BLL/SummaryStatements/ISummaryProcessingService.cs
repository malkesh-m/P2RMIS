using System.Collections.Generic;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Bll.SummaryStatements
{
    /// <summary>
    /// Services provided for Summary processing.
    /// </summary>
    public interface ISummaryProcessingService
    {
        /// <summary>
        /// Retrieves information about the application a workflow is for.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <returns>Application details for a single application</returns>
        IApplicationDetailModel GetApplicationDetail(int applicationWorkflowId);
        /// <summary>
        /// Retrieves the details of an application workflow step including content and element metadata.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <returns>Zero or more application workflow step elements</returns>
        Container<IStepContentModel> GetApplicationStepContent(int applicationWorkflowId);
        /// <summary>
        /// Retrieves workflow information for application that user is assigned to one or more child steps.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Zero or more assigned application workflows</returns>
        Container<ISummaryAssignedModel> GetAssignedSummaries(int userId);
        /// <summary>
        /// Retrieves information about workflow progress for each application that a user is assigned.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Zero or more workflow steps progress for an assigned application</returns>
        Container<IWorkflowProgress> GetWorkflowProgress(int userId);
        /// <summary>
        /// Gets the draft summmaries available for checkout.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="program">The program.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <param name="canAccessDiscussionNote">if set to <c>true</c> [can access discussion note].</param>
        /// <param name="canAccessGeneralNote">if set to <c>true</c> [can access general note].</param>
        /// <param name="canAccessUnassignedReviewerNote">if set to <c>true</c> [can access unassigned reviewer note].</param>
        /// 
        /// <returns></returns>
        Container<ISummaryAssignedModel> GetDraftSummmariesAvailableForCheckout(int userId, int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId, bool canAccessDiscussionNote, bool canAccessGeneralNote, bool canAccessUnassignedReviewerNote);
        /// <summary>
        /// Gets the draft summmaries checkedout.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="canAccessDiscussionNote">if set to <c>true</c> [can access discussion note].</param>
        /// <param name="canAccessGeneralNote">if set to <c>true</c> [can access general note].</param>
        /// <param name="canAccessUnassignedReviewerNote">if set to <c>true</c> [can access unassigned reviewer note].</param>
        /// 
        /// <returns></returns>
        Container<ISummaryAssignedModel> GetDraftSummmariesCheckedout(int userId, bool canAccessDiscussionNote, bool canAccessGeneralNote, bool canAccessUnassignedReviewerNote);
    }
}
