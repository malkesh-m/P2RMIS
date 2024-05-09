using System;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Bll.SummaryStatements
{
    public partial class SummaryManagementService : ISummaryManagementService
    {

        #region Provided Services
        /// <summary>
        /// Retrieves the admin comments for a specified application.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <returns>Zero or more transactions of an application workflow</returns>
        public Container<ISummaryAdminCommentModel> GetApplicationAdminComments(string applicationId)
        {
            ///
            /// Set up default return
            /// 
            Container<ISummaryAdminCommentModel> container = new Container<ISummaryAdminCommentModel>();

            if (!string.IsNullOrWhiteSpace(applicationId))
            {
                //
                // Call the DL and retrieve the admin comments for this application
                //
                var results = UnitOfWork.SummaryManagementRepository.GetApplicationAdminComments(applicationId);
                //
                // Then create the view to return to the PI layer & return
                //

                container.SetModelList(results);
            }

            return container;
        }
        #endregion

        #region Helpers
        #endregion

    }
}
