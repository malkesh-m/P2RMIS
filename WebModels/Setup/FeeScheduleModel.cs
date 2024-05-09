using System;

namespace Sra.P2rmis.WebModels.Setup
{
    #region Model
    /// <summary>
    /// Data model for the Fee Schedule (Client-Fiscal Year & Session) grids
    /// </summary>
    public interface IFeeScheduleModel
    {
        string ParticipantTypeAbbreviation { get; set; }
        string ParticipationMethodLabel { get; set; }
        string RestrictedAccessFlag { get; set; }
        string ConsultantFeeText { get; set; }
        string HonorariumAccepted { get; set; }
        decimal ConsultantFee { get; set; }
        DateTime PeriodStartDate { get; set; }
        DateTime PeriodEndDate { get; set; }
        string ManagerList { get; set; }
        string DescriptionOfWork { get; set; }
        int ScheduleEntityId { get; set; }
        Nullable<DateTime> UploadDate { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
    #endregion
    #region Model
    /// <summary>
    /// Data model for the Fee Schedule (Client-Fiscal Year & Session) grids
    /// </summary>
    public class FeeScheduleModel : IFeeScheduleModel
    {
        /// <summary>
        /// Participation type abbreviation (Participation Type)
        /// </summary>
        public string ParticipantTypeAbbreviation { get; set; }
        /// <summary>
        /// Participation method (Participation Type)
        /// </summary>
        public string ParticipationMethodLabel { get; set; }
        /// <summary>
        /// Participation access full or partial (Participation Type)
        /// </summary>
        public string RestrictedAccessFlag { get; set; }
        /// <summary>
        /// Consultant fee
        /// </summary>
        public string ConsultantFeeText { get; set; }
        /// <summary>
        /// Is honorarium accepted?
        /// </summary>
        public string HonorariumAccepted { get; set; }
        /// <summary>
        /// Fee paid
        /// </summary>
        public decimal ConsultantFee { get; set; }
        /// <summary>
        /// Start date of schedule
        /// </summary>
        public DateTime PeriodStartDate { get; set; }
        /// <summary>
        /// End date of schedule
        /// </summary>
        public DateTime PeriodEndDate { get; set; }
        /// <summary>
        /// Managers list
        /// </summary>
        public string ManagerList { get; set; }
        /// <summary>
        /// Description of work required
        /// </summary>
        public string DescriptionOfWork { get; set; }
        /// <summary>
        /// Represents the ProgramPayRate or SessionPayRate entity identifier
        /// </summary>
        public int ScheduleEntityId { get; set; }
        /// <summary>
        /// DateTime the pay rate schedule was uploaded
        /// </summary>
        public Nullable<DateTime> UploadDate {get; set;}
        /// <summary>
        /// First name of user who uploaded pay rate schedule
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of user who uploaded pay rate schedule
        /// </summary>
        public string LastName { get; set; }
        
    }
    #endregion
}
