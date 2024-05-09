
using System.Linq;
using Sra.P2rmis.CrossCuttingServices.FileServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Files;

namespace Sra.P2rmis.Bll.ModelBuilders.SummaryStatement
{
    /// <summary>
    /// Builder for constructing a FileResultModel for a summary statement
    /// </summary>
    /// <seealso cref="Sra.P2rmis.Bll.ModelBuilders.ContainerModelBuilderBase" />
    internal class SummaryDocumentWorkLogFileModelBuilder : ContainerModelBuilderBase
    {
        #region constructor

        public SummaryDocumentWorkLogFileModelBuilder(IUnitOfWork unitOfWork, int applicationWorkflowStepWorkLogId,
            string action) : base(unitOfWork)
        {
            ApplicationWorkflowStepWorkLogId = applicationWorkflowStepWorkLogId;
            Action = action;
        }
        #endregion
        #region properties
        internal int ApplicationWorkflowStepWorkLogId { get; }
        internal string Action { get; }
        /// <summary>
        /// Gets or sets the result of the model builder.
        /// </summary>
        /// <value>
        /// The result of the model builder.
        /// </value>
        public IFileResultModel Result { get; set; }
        #endregion
        #region Services
        /// <summary>
        /// Build the result "container".
        /// </summary>
        public override void BuildContainer()
        {
            PopulateModel();
        }
        /// <summary>
        /// Populates the result model.
        /// </summary>
        internal void PopulateModel()
        {
            //get data from db (split into 2 steps for readability)
            var result = UnitOfWork.ApplicationWorkflowStepWorkLogRepository.Select()
                .Where(x => x.ApplicationWorkflowStepWorkLogId == ApplicationWorkflowStepWorkLogId)
                .Select(x => new
                {
                    LogNumber = x.ApplicationWorkflowStep.ApplicationWorkflow.ApplicationStage.PanelApplication.Application.LogNumber,
                    FileContent = Action == ApplicationWorkflowStepWorkLog.CheckinAction ? x.CheckinBackupFile : x.CheckoutBackupFile
                }).FirstOrDefault();
            //populate return model
            Result = new FileResultModel(
                result.LogNumber,
                FileConstants.MimeTypes.Docx,
                result.FileContent
                );
        }

        #endregion
    }
}
