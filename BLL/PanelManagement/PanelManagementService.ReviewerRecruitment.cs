using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.ReviewerRecruitment;
using System;
using Sra.P2rmis.Bll.ModelBuilders.ReviewerRecruitment;
using Sra.P2rmis.Bll.Mail;

namespace Sra.P2rmis.Bll.PanelManagement
{
    /// <summary>
    /// Services provided for Reviewer Recruitment
    /// </summary>
    public partial class PanelManagementService
    {
        /// <summary>
        /// Retrieves the header & grid contents for the Evaluation Details modal
        /// </summary>
        /// <returns>Container of IRatingEvaluationModels</returns>
        /// <param name="userId">User entity identifier</param>
        public Container<IRatingEvaluationModel> RetrieveEvaluationDetails(int userId)
        {
            ValidateInt(userId, nameof(RetrieveEvaluationDetails), nameof(userId));

            var builder = new RatingEvaluationModelBuilder(UnitOfWork, userId);
            builder.BuildContainer();
            return builder.Results;
        }
    }
}
