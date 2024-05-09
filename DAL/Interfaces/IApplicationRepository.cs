using System.Collections.Generic;
using Sra.P2rmis.WebModels.Setup;

using System;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for Application objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public interface IApplicationRepository : IGenericRepository<Application>
    {
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
        Application Add(string logNumber, int programMechanismId, string title, string keywords, DateTime? projectStartDate, DateTime? projectEndDate, DateTime? withdrawnDate, int userId);
        /// <summary>
        /// Locates the Application by log number or returns null if none exist.
        /// </summary>
        /// <param name="logNumber">Log number of application</param>
        /// <returns>Application if one exists; null otherwise</returns>
        Application GetByLogNumber(string logNumber);
        /// <summary>
        /// Locates the Application by panel application identifier
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns></returns>
        Application FindApplicationByPanelApplicationId(int panelApplicationId);
        /// <summary>
        /// Pushes the selected application(s) to the processing queue.
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="workflowId">Over ridden workflow identifier (optional)</param>
        void StartApplications(int panelApplicationId, int userId, int? workflowId);
        /// <summary>
        /// Get existing panel application released
        /// </summary>
        /// <param name="programYearId"></param>
        /// <param name="receiptCycle"></param>
        /// <param name="clientId"></param>
        /// <param name="fiscalYear"></param>
        /// <returns></returns>
        List<ReferralMappingModel> GetExistingApplications(int programYearId, int receiptCycle);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="programYearId"></param>
        /// <param name="receiptCycle"></param>
        /// <returns></returns>
        List<string> FindApplications(int programYearId, int receiptCycle);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="programYearId"></param>
        /// <param name="receiptCycle"></param>
        /// <returns></returns>
        List<string> FindPanelAbbreviations(int programYearId, int receiptCycle);
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
        void Update(Application application, string title, string keywords, DateTime? projectStartDate, DateTime? projectEndDate, DateTime? withdrawnDate, int userId);
    }
}
