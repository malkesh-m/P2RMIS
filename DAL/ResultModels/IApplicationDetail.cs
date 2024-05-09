using System;

namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Model representing an Application's Details
    /// </summary>
    public interface IApplicationDetail
    {
        /// <summary>
        /// TODO:: document me
        /// </summary>
        string ApplicationId { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        string ApplicationTitle { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        string AwardDescription { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        string AwardShortDesc { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        string PiFirstName { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        string PiLastName { get; set; }
        /// <summary>
        /// Panel Unique Identifier
        /// </summary>
        int PanelId { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        string PiOrgName { get; set; }
        /// <summary>
        /// Panels corresponding session start date
        /// </summary> 
        Nullable<System.DateTime> SessionStartDate { get; set; }
        /// <summary>
        /// Panels corresponding session end date
        /// </summary> 
        Nullable<System.DateTime> SessionEndDate { get; set; }
        /// <summary>
        /// Applications program Id
        /// </summary>
        int ProgramId { get; set; }
    }
}
