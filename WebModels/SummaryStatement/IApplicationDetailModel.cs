using System;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Data model for displaying general application information.
    /// </summary>
    public interface IApplicationDetailModel
    {
        /// <summary>
        /// the applications unique identifier
        /// </summary>
        int ApplicationId { get; set; }
        /// <summary>
        /// Application label
        /// </summary>
        string LogNumber { get; set; }
        /// <summary>
        /// Panel application identifier
        /// </summary>
        int PanelApplicationId { get; set; }
        /// <summary>
        /// Principal investigator last name
        /// </summary>
        string PiLastName { get; set; }
        /// <summary>
        /// Principal investigator first name
        /// </summary>
        string PiFirstName { get; set; }
        /// <summary>
        /// Title of the application
        /// </summary>
        string ApplicationTitle { get; set; }
        /// <summary>
        /// Principal investigator's institution or organization
        /// </summary>
        string PiOrganization { get; set; }
        /// <summary>
        /// Abbreviation for research program that application was submitted under.
        /// </summary>
        string ProgramAbbreviation { get; set; }
        /// <summary>
        /// Year of research program that application was submitted under.
        /// </summary>
        string Year { get; set; }
        /// <summary>
        /// Name of mechanism that application was submitted under.
        /// </summary>
        string MechanismName { get; set; }
        /// <summary>
        /// Name of panel that reviewed application.
        /// </summary>
        string PanelName { get; set; }
        /// <summary>
        /// Abbreviation of panel that reviewed application.
        /// </summary>
        string PanelAbbreviation { get; set; }
        /// <summary>
        /// the applications workflow id
        /// </summary>
        int WorkflowId { get; set; }
        /// <summary>
        /// Requested budget
        /// </summary>
        Nullable<int> RequestedTotal { get; set; }
        /// <summary>
        /// Grant Id
        /// </summary>
        string GrantID { get; set; }
        /// <summary>
        /// Panel start date
        /// </summary>
        Nullable<System.DateTime> StartDate { get; set; }
        /// <summary>
        /// Panel end date
        /// </summary>
        Nullable<System.DateTime> EndDate { get; set; }
        /// <summary>
        /// Application budget requested
        /// </summary>
        Nullable<int> TotalBudget { get; set; }
        /// <summary>
        /// Application duration
        /// </summary>
        Nullable<decimal> Duration { get; set; }
        /// <summary>
        /// Application direct costs
        /// </summary>
        Nullable<int> DirectCosts { get; set; }
        /// <summary>
        /// Application indirect costs
        /// </summary>
        Nullable<int> IndirectCosts { get; set; }
        /// <summary>
        /// Contracting organization name
        /// </summary>
        string AdminOrgName { get; set; }
        /// <summary>
        /// Unique identifier for the client the application was submitted under
        /// </summary>
        int ClientId { get; set; }
        /// <summary>
        /// Research area for which the application was submitted
        /// </summary>
        string ResearchArea { get; set; }

        /// <summary>
        /// Whether application is considered priority 1
        /// </summary>
        bool Priority1 { get; set; }
        /// <summary>
        /// Whether application is considered priority 2
        /// </summary>
        bool Priority2 { get; set; }

        /// <summary>
        /// Receipt cycle of the application
        /// </summary>
        int? Cycle { get; set; }

        /// <summary>
        /// Date/time the summary was checked out by user
        /// </summary>
        DateTime? CheckoutDateTime { get; set; }

        /// <summary>
        /// Whether the application was triaged
        /// </summary>
        bool IsTriaged { get; set; }
    }
}
