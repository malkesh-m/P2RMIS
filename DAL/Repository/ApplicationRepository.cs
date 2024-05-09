using System;
using System.Linq;
using System.Collections.Generic;
using Sra.P2rmis.Dal.Common;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for ApplicationRepository objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>   
    public class ApplicationRepository : GenericRepository<Application>, IApplicationRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ApplicationRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion
        #region Services Provided
        /// <summary>
        /// Add an application.
        /// </summary>
        /// <param name="logNumber">Log number</param>
        /// <param name="programMechanismId">Program mechanism identifier</param>
        /// <param name="title">Title</param>
        /// <param name="keywords">Keywords</param>
        /// <param name="projectStartDate">Project start date</param>
        /// <param name="projectEndDate">Project end date</param>
        /// <param name="withdrawnDate">Withdrawn date</param>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        public Application Add(string logNumber, int programMechanismId, string title, string keywords, DateTime? projectStartDate, DateTime? projectEndDate, DateTime? withdrawnDate, int userId)
        {
            var o = new Application();
            o.LogNumber = logNumber;
            o.ProgramMechanismId = programMechanismId;
            o.ApplicationTitle = title;
            o.Keywords = keywords;
            o.ProjectStartDate = projectStartDate;
            o.ProjectEndDate = projectEndDate;
            if (withdrawnDate != null)
            {
                o.WithdrawnFlag = true;
                o.WithdrawnDate = withdrawnDate;
            }
            Helper.UpdateCreatedFields(o, userId);
            Helper.UpdateModifiedFields(o, userId);
            Add(o);
            return o;
        }
        /// <summary>
        /// Locates the Application by log number or returns null if none exist.
        /// </summary>
        /// <param name="logNumber">Log number of application</param>
        /// <returns>Application if one exists; null otherwise</returns>
        public Application GetByLogNumber(string logNumber)
        {
            return (string.IsNullOrWhiteSpace(logNumber))? 
                null: 
                context.Applications.FirstOrDefault(x => x.LogNumber == logNumber);
        }
        /// <summary>
        /// Locates the Application by panel application identifier
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns></returns>
        public Application FindApplicationByPanelApplicationId(int panelApplicationId)
        {
            return context.PanelApplications.FirstOrDefault(x => x.PanelApplicationId == panelApplicationId)?.Application;
        }
        /// <summary>
        /// Pushes the selected application(s) to the processing queue.
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="workflowId">Over ridden workflow identifier (optional)</param>
        public void StartApplications(int panelApplicationId, int userId, int? workflowId)
        {
            context.uspBeginApplicationSummaryWorkflow(panelApplicationId, userId, workflowId);
        }
        /// <summary>
        /// Get panel existing application released
        /// </summary>
        /// <param name="programYearId"></param>
        /// <param name="receiptCycle"></param>
        /// <param name="clientId"></param>
        /// <param name="fiscalYear"></param>
        /// <returns></returns>
        public List<ReferralMappingModel> GetExistingApplications(int programYearId, int receiptCycle)
        {
            return RepositoryHelpers.GetExistingApplication(context, programYearId, receiptCycle).ToList();
        }
        /// <summary>
        /// Update an application.
        /// </summary>
        /// <param name="application">Application entity</param>
        /// <param name="title">Title</param>
        /// <param name="keywords">Keywords</param>
        /// <param name="projectStartDate">Project start date</param>
        /// <param name="projectEndDate">Project end date</param>
        /// <param name="withdrawnDate">Withdrawn date</param>
        /// <param name="userId">User identifier</param>
        public void Update(Application application, string title, string keywords, DateTime? projectStartDate, DateTime? projectEndDate, DateTime? withdrawnDate, int userId)
        {
            if (application.ApplicationTitle != title || application.Keywords != keywords ||
                application.ProjectStartDate != projectStartDate || application.ProjectEndDate != projectEndDate ||
                application.WithdrawnDate != withdrawnDate)
            {
                application.ApplicationTitle = title;
                application.Keywords = keywords;
                application.ProjectStartDate = projectStartDate;
                application.ProjectEndDate = projectEndDate;
                if (withdrawnDate != null)
                {
                    application.WithdrawnFlag = true;
                    application.WithdrawnDate = withdrawnDate;
                }
                Helper.UpdateModifiedFields(application, userId);
            }
        }
        #endregion
        #region Services Not Provided
        /// <summary>
        /// Delete an object
        /// </summary>
        /// <param name="id"></param>
        public override void Delete(object id)
        {
            
            string message = string.Format(Constants.NotSupportedMessage, "Delete(id)");
            throw new NotSupportedException(message);
        }

        public override void Delete(Application entityToDelete)
        {
            string message = string.Format(Constants.NotSupportedMessage, "Delete(object)");
            throw new NotSupportedException(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="programYearId"></param>
        /// <param name="receiptCycle"></param>
        /// <returns></returns>
        public List<string> FindApplications(int programYearId, int receiptCycle)
        {
            return RepositoryHelpers.FindApplications(context, programYearId, receiptCycle);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="programYearId"></param>
        /// <param name="receiptCycle"></param>
        /// <returns></returns>
        public List<string> FindPanelAbbreviations(int programYearId, int receiptCycle)
        {
            return RepositoryHelpers.FindPanelAbbreviations(context, programYearId, receiptCycle);
        }

        #endregion
    }
}
