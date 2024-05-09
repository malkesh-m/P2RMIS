using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Bll.SummaryStatements
{
    /// <summary>
    /// Services provided for Summary processing.
    /// </summary>
    public class SummaryProcessingService : ISummaryProcessingService
    {
        #region Properties
        /// <summary>
        /// Indicates if the object has been disposed but not garbage collected.
        /// </summary>
        private bool _disposed;
        /// <summary>
        /// Instantiation of O/RM persistence pattern: the Unit Of Work pattern.
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; set; }
        #endregion        
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public SummaryProcessingService()
        {
            UnitOfWork = new UnitOfWork();
        }
        /// <summary>
        /// Dispose of the service.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Dispose of the service
        /// </summary>
        /// <param name="disposing">Indicates if the object should be disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            ///
            /// if the object has not been disposed & we should dispose the object
            /// 
            if ((!this._disposed) && (disposing))
            {
                if (UnitOfWork != null)
                {
                    ((IDisposable)UnitOfWork).Dispose();
                    this._disposed = true;
                }
            }
        }
        #endregion
        #region Provided Services
        /// <summary>
        /// Retrieves information about the application a workflow is for.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <returns>Application details for a single application</returns>
        public IApplicationDetailModel GetApplicationDetail(int applicationWorkflowId)
        {
            ///
            /// Set up default return
            /// 
            IApplicationDetailModel results = new ApplicationDetailModel();
            if (applicationWorkflowId > 0)
            {
                //
                // Call the DL and retrieve the progress for the given applicationWorkflowId
                //
                results = UnitOfWork.SummaryManagementRepository.GetApplicationDetail(applicationWorkflowId);
            }
            return results;
        }
        /// <summary>
        /// Retrieves the details of an application workflow step including content and element metadata.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <returns>Zero or more application workflow step elements</returns>
        public Container<IStepContentModel> GetApplicationStepContent(int applicationWorkflowId)
        {
            ///
            /// Set up default return
            /// 
            Container<IStepContentModel> container = new Container<IStepContentModel>();

            if (applicationWorkflowId > 0)
            {
                //
                // Call the DL and retrieve the progress for the given applicationWorkflowId
                //
                var results = UnitOfWork.SummaryManagementRepository.GetApplicationStepContent(applicationWorkflowId);
                //
                // Then create the view to return to the PI layer & return
                //
                container.SetModelList(results);
            }

            return container;
        }
        /// <summary>
        /// Retrieves workflow information for application that user is assigned to one or more child steps.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Zero or more assigned application workflows</returns>
        public Container<ISummaryAssignedModel> GetAssignedSummaries(int userId)
        {
            ///
            /// Set up default return
            /// 
            Container<ISummaryAssignedModel> container = new Container<ISummaryAssignedModel>();

            if (userId > 0)
            {
                var results = UnitOfWork.SummaryManagementRepository.GetAssignedSummaries(userId);
                container.SetModelList(results);
            }
            return container;
        }
        /// <summary>
        /// Retrieves information about workflow progress for each application that a user is assigned.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Zero or more workflow steps progress for an assigned application</returns>
        public Container<IWorkflowProgress> GetWorkflowProgress(int userId)
        {
            ///
            /// Set up default return
            /// 
            Container<IWorkflowProgress> container = new Container<IWorkflowProgress>();

            if (userId > 0)
            {
                var results = UnitOfWork.SummaryManagementRepository.GetWorkflowProgress(userId);
                container.SetModelList(results);

            }
            return container;
        }
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
        public Container<ISummaryAssignedModel> GetDraftSummmariesAvailableForCheckout(int userId, int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId,
            bool canAccessDiscussionNote, bool canAccessGeneralNote, bool canAccessUnassignedReviewerNote)
        {
            //
            // Set up default return
            // 
            Container<ISummaryAssignedModel> container = new Container<ISummaryAssignedModel>();

            if (IsGetDraftSummmariesAvailableForCheckoutParametersValid(userId, program, fiscalYear, cycle, panelId, awardTypeId))
            {
                var results = UnitOfWork.SummaryManagementRepository.GetDraftSummmariesAvailableForCheckout(userId, program, fiscalYear, cycle, panelId, awardTypeId,
                    canAccessDiscussionNote, canAccessGeneralNote, canAccessUnassignedReviewerNote);
                container.SetModelList(results);
            }
            return container;
        }
        /// <summary>
        /// Gets the draft summmaries checkedout.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="canAccessDiscussionNote">if set to <c>true</c> [can access discussion note].</param>
        /// <param name="canAccessGeneralNote">if set to <c>true</c> [can access general note].</param>
        /// <param name="canAccessUnassignedReviewerNote">if set to <c>true</c> [can access unassigned reviewer note].</param>
        /// 
        /// <returns></returns>
        public Container<ISummaryAssignedModel> GetDraftSummmariesCheckedout(int userId, bool canAccessDiscussionNote, bool canAccessGeneralNote, bool canAccessUnassignedReviewerNote)
        {
            //
            // Set up default return
            // 
            Container<ISummaryAssignedModel> container = new Container<ISummaryAssignedModel>();

            if (IsGetDraftSummmariesCheckedout(userId))
            {
                var results = UnitOfWork.SummaryManagementRepository.GetDraftSummmariesCheckedout(userId, canAccessDiscussionNote, canAccessGeneralNote, canAccessUnassignedReviewerNote);
                container.SetModelList(results);
            }
            return container;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Validates the parameters for IsGetDraftSummmariesAvailableForCheckout:
        /// - user Id is greater than 0
        /// - program is not null or empty
        /// - fiscal year is not null or empty
        /// - cycle is null or if not null greater than 0
        /// - panel Id is null or if not null greater than 0
        /// </summary>
        /// <param name="program">Program abbreviation</param>
        /// <param name="Fy">Fiscal year</param>
        /// <param name="cycle">the cycle</param>
        /// <param name="panelId">the panel id</param>
        /// <returns>True if parameters valid; false otherwise</returns>
        private bool IsGetDraftSummmariesAvailableForCheckoutParametersValid(int userId, int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId)
        {
            return (
                (userId > 0 ) &&
                (program > 0) &&
                (fiscalYear > 0 &&
                (BllHelper.IdOk(cycle)) &&
                (BllHelper.IdOk(panelId)) &&
                ((awardTypeId == null) || awardTypeId > 0)
                ));
        }
        /// <summary>
        /// Validates the parameters for GetDraftSummmariesCheckedout:
        /// - user Id is greater than 0
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>True if parameters valid; false otherwise</returns>
        private bool IsGetDraftSummmariesCheckedout(int userId)
        {
            return (userId > 0);
        }

        #endregion
    }
}
