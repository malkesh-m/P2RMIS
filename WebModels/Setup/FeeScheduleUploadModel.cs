using System;

namespace Sra.P2rmis.WebModels.Setup
{
    #region Model
    /// <summary>
    /// Data model for the Fee Schedule (Client-Fiscal Year & Session) grids
    /// </summary>
    public interface IFeeScheduleUploadModel
    {
        /// <summary>
        /// Gets or sets the participant type abbreviation.
        /// </summary>
        /// <value>
        /// The participant type abbreviation.
        /// </value>
        string ParticipantTypeAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the participation method label.
        /// </summary>
        /// <value>
        /// The participation method label.
        /// </value>
        string ParticipationMethodLabel { get; set; }
        /// <summary>
        /// Gets or sets the restricted access flag.
        /// </summary>
        /// <value>
        /// The restricted access flag.
        /// </value>
        string RestrictedAccessFlag { get; set; }
        /// <summary>
        /// Gets or sets the consultant fee text.
        /// </summary>
        /// <value>
        /// The consultant fee text.
        /// </value>
        string ConsultantFeeText { get; set; }
        /// <summary>
        /// Gets or sets the honorarium accepted.
        /// </summary>
        /// <value>
        /// The honorarium accepted.
        /// </value>
        string HonorariumAccepted { get; set; }
        /// <summary>
        /// Gets or sets the consultant fee.
        /// </summary>
        /// <value>
        /// The consultant fee.
        /// </value>
        string ConsultantFee { get; set; }
        /// <summary>
        /// Gets or sets the period start date.
        /// </summary>
        /// <value>
        /// The period start date.
        /// </value>
        string PeriodStartDate { get; set; }
        /// <summary>
        /// Gets or sets the period end date.
        /// </summary>
        /// <value>
        /// The period end date.
        /// </value>
        string PeriodEndDate { get; set; }
        /// <summary>
        /// Gets or sets the manager list.
        /// </summary>
        /// <value>
        /// The manager list.
        /// </value>
        string ManagerList { get; set; }
        /// <summary>
        /// Gets or sets the description of work.
        /// </summary>
        /// <value>
        /// The description of work.
        /// </value>
        string DescriptionOfWork { get; set; }
    }
    #endregion
    #region Model
    /// <summary>
    /// Data model for the Fee Schedule (Client-Fiscal Year & Session) grids
    /// </summary>
    public class FeeScheduleUploadModel : IFeeScheduleUploadModel
    {
        public FeeScheduleUploadModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeeScheduleModel"/> class.
        /// </summary>
        /// <param name="participantTypeAbbreviation">The participant type abbreviation.</param>
        /// <param name="participationMethodLabel">The participation method label.</param>
        /// <param name="restrictedAccessFlag">The restricted access flag.</param>
        /// <param name="honorariumAccepted">The honorarium accepted.</param>
        /// <param name="consultantFeeText">The consultant fee text.</param>
        /// <param name="consultantFee">The consultant fee.</param>
        /// <param name="periodStartDate">The period start date.</param>
        /// <param name="periodEndDate">The period end date.</param>
        /// <param name="managerList">The manager list.</param>
        /// <param name="descriptionOfWork">The description of work.</param>
        public FeeScheduleUploadModel(string participantTypeAbbreviation, string participationMethodLabel, string restrictedAccessFlag, string honorariumAccepted, string consultantFeeText,
            string consultantFee, string periodStartDate, string periodEndDate, string managerList, string descriptionOfWork)
        {
            ParticipantTypeAbbreviation = participantTypeAbbreviation.Trim();
            ParticipationMethodLabel = participationMethodLabel.Trim();
            RestrictedAccessFlag = restrictedAccessFlag.Trim();
            HonorariumAccepted = honorariumAccepted.Trim();
            ConsultantFee = consultantFee.Trim();
            ConsultantFeeText = consultantFeeText.Trim();
            PeriodStartDate = periodStartDate.Trim();
            PeriodEndDate = periodEndDate.Trim();
            ManagerList = managerList.Trim();
            DescriptionOfWork = descriptionOfWork.Trim();
        }

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
        public string ConsultantFee { get; set; }
        /// <summary>
        /// Start date of schedule
        /// </summary>
        public string PeriodStartDate { get; set; }
        /// <summary>
        /// End date of schedule
        /// </summary>
        public string PeriodEndDate { get; set; }
        /// <summary>
        /// Managers list
        /// </summary>
        public string ManagerList { get; set; }
        /// <summary>
        /// Description of work required
        /// </summary>
        public string DescriptionOfWork { get; set; }
    }
    #endregion
}
