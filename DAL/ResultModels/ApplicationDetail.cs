using System;

namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Model representing an Application's Details
    /// </summary>
    public class ApplicationDetail : IApplicationDetail
    {
        #region Properties
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string ApplicationId { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string PiFirstName { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string PiLastName { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string PiOrgName { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string ApplicationTitle { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string AwardDescription { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string AwardShortDesc { get; set; }
        /// <summary>
        /// Panel Unique Identifier
        /// </summary>
        public int PanelId { get; set; }
        /// <summary>
        /// Panels corresponding session start date
        /// </summary> 
        public Nullable<System.DateTime> SessionStartDate { get; set; }
        /// <summary>
        /// Panels corresponding session end date
        /// </summary> 
        public Nullable<System.DateTime> SessionEndDate { get; set; }
        /// <summary>
        /// Applications program Id
        /// </summary>
        public int ProgramId { get; set; }
        #endregion
    }
}
