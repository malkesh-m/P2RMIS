using System;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Data model for displaying general application information.
    /// </summary>
    public class ApplicationDetailModel : IApplicationDetailModel
    {
        /// <summary>
        /// the applications unique identifier
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// Application label
        /// </summary>
        public string LogNumber { get; set; }
        /// <summary>
        /// Panel application identifier
        /// </summary>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// Principal investigator last name
        /// </summary>
        public string PiLastName { get; set; }
        /// <summary>
        /// Principal investigator first name
        /// </summary>
        public string PiFirstName { get; set; }
        /// <summary>
        /// Title of the application
        /// </summary>
        public string ApplicationTitle { get; set; }
        /// <summary>
        /// Principal investigator's institution or organization
        /// </summary>
        public string PiOrganization { get; set; }
        /// <summary>
        /// Abbreviation for research program that application was submitted under.
        /// </summary>
        public string ProgramAbbreviation { get; set; }
        /// <summary>
        /// Year of research program that application was submitted under.
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// Name of mechanism that application was submitted under.
        /// </summary>
        public string MechanismName { get; set; }
        /// <summary>
        /// Name of panel that reviewed application.
        /// </summary>
        public string PanelName { get; set; }
        /// <summary>
        /// Abbreviation of panel that reviewed application.
        /// </summary>
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// the applications workflow id
        /// </summary>
        public int WorkflowId { get; set; }
        /// <summary>
        /// Requested budget
        /// </summary>
        public Nullable<int> RequestedTotal { get; set; }
        /// <summary>
        /// Grant Id
        /// </summary>
        public string GrantID { get; set; }
        /// <summary>
        /// Panel start date
        /// </summary>
        public Nullable<System.DateTime> StartDate { get; set; }
        /// <summary>
        /// Panel end date
        /// </summary>
        public Nullable<System.DateTime> EndDate { get; set; }
        /// <summary>
        /// Application budget requested
        /// </summary>
        public Nullable<int> TotalBudget { get; set; }
        /// <summary>
        /// Application duration
        /// </summary>
        public Nullable<decimal> Duration { get; set; }
        /// <summary>
        /// Application direct costs
        /// </summary>
        public Nullable<int> DirectCosts { get; set; }
        /// <summary>
        /// Application indirect costs
        /// </summary>
        public Nullable<int> IndirectCosts { get; set; }
        /// <summary>
        /// Contracting organization name
        /// </summary>
        public string AdminOrgName { get; set; }
        /// <summary>
        /// Unique identifier for the client the application was submitted under
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// Research area for which the application was submitted
        /// </summary>
        public string ResearchArea  { get; set; }
        /// <summary>
        /// Whether application is considered priority 1
        /// </summary>
        public bool Priority1 { get; set; }
        /// <summary>
        /// Whether application is considered priority 2
        /// </summary>
        public bool Priority2 { get; set; }
        /// <summary>
        /// Receipt cycle of the application
        /// </summary>
        public int? Cycle { get; set; }
        /// <summary>
        /// Date/time the summary was checked out by user
        /// </summary>
        public DateTime? CheckoutDateTime { get; set; }
        /// <summary>
        /// Whether the application was triaged
        /// </summary>
        public bool IsTriaged { get; set; }
    }


}
