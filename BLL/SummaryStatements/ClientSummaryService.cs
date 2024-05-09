using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Bll.SummaryStatements
{
    /// <summary>
    /// Services provided by the client only portion of the summary statement service.
    /// </summary>
    public partial class ClientSummaryService: ServerBase, IClientSummaryService
    {
        /// <summary>
        /// Exception messages thrown when parameter validation fails.
        /// </summary>
        private class ExceptionMessages
        {
            public const string GetRequestReviewApplications = "ClientSummaryService.GetRequestReviewApplications() received an invalid parameter. program is null or empty? = {0}; year is null or empty? = {1}; cycle is empty? = {2}; panel id is invalid? = {3}; award type id is empty? = {4}";
        }
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public ClientSummaryService()
        {
            UnitOfWork = new UnitOfWork();
        }
        #endregion
        #region SSM-820 Review Summary Statement
        /// <summary>
        /// Gets the request review applications.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns></returns>
        public Container<ISummaryStatementRequestReview> GetRequestReviewApplications(int programId, int yearId, int? cycle, int? panelId, int? awardTypeId)
        {
            ValidateGetRequestReviewApplicationsParameters(programId, yearId, cycle, panelId, awardTypeId);
            Container<ISummaryStatementRequestReview> container = new Container<ISummaryStatementRequestReview>();
            //
            // Call the DL and retrieve any programs
            //
            var results = UnitOfWork.SummaryManagementRepository.GetRequestReviewApplications(programId, yearId, cycle, panelId, awardTypeId);
            //
            // Then create the view to return to the PI layer & return
            //
            container.SetModelList(results);

            return container;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Validates the get request review applications parameters.
        /// </summary>
        /// <param name="program">The program.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <exception cref="ArgumentException"></exception>
        private void ValidateGetRequestReviewApplicationsParameters(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId)
        {
            if ((program <= 0) ||
                (fiscalYear <= 0) ||
                ((cycle != null) && (cycle <= 0)) ||
                (!BllHelper.IdOk(panelId)) ||
                ((awardTypeId != null) && (awardTypeId <= 0))
                )
            {
                bool isPanelIdInvalid = !BllHelper.IdOk(panelId);
                bool isAwardTypeIdEmpty = (awardTypeId != null) && (awardTypeId <= 0);
                string message = string.Format(ExceptionMessages.GetRequestReviewApplications,
                    program <= 0, fiscalYear <= 0, cycle <= 0, isPanelIdInvalid, isAwardTypeIdEmpty);
                throw new ArgumentException(message);
            }
        }
        #endregion
    }
}
