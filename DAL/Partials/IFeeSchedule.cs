using System;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Interface defining the common properties between ProgramPayRates & SessionPayRates
    /// </summary>
    public interface IFeeSchedule
    {
        /// <summary>
        /// Gets or sets the employment category identifier.
        /// </summary>
        /// <value>
        /// The employment category identifier.
        /// </value>
        int? EmploymentCategoryId { get; set; }
        /// <summary>
        /// Gets or sets the honorarium accepted.
        /// </summary>
        /// <value>
        /// The honorarium accepted.
        /// </value>
        string HonorariumAccepted { get; set; }
        /// <summary>
        /// Gets or sets the consultant fee text.
        /// </summary>
        /// <value>
        /// The consultant fee text.
        /// </value>
        string ConsultantFeeText { get; set; }
        /// <summary>
        /// Gets or sets the consultant fee.
        /// </summary>
        /// <value>
        /// The consultant fee.
        /// </value>
        decimal ConsultantFee { get; set; }
        /// <summary>
        /// Gets or sets the period start date.
        /// </summary>
        /// <value>
        /// The period start date.
        /// </value>
        System.DateTime PeriodStartDate { get; set; }
        /// <summary>
        /// Gets or sets the period end date.
        /// </summary>
        /// <value>
        /// The period end date.
        /// </value>
        System.DateTime PeriodEndDate { get; set; }
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
        /// <summary>
        /// Gets or sets the participant method identifier.
        /// </summary>
        /// <value>
        /// The participant method identifier.
        /// </value>
        int ParticipantMethodId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [restricted assigned flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [restricted assigned flag]; otherwise, <c>false</c>.
        /// </value>
        bool RestrictedAssignedFlag { get; set; }
        /// <summary>
        /// Gets or sets the type of the client participant.
        /// </summary>
        /// <value>
        /// The type of the client participant.
        /// </value>
        ClientParticipantType ClientParticipantType { get; set; }
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        int Index();
        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        Nullable<DateTime> ModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>
        /// The modified by.
        /// </value>
        Nullable<int> ModifiedBy { get; set; }
    }
}
