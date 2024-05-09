using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Sra.P2rmis.WebModels.Setup;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    public class FeeScheduleViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeeScheduleViewModel"/> class.
        /// </summary>
        public FeeScheduleViewModel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="FeeScheduleViewModel"/> class.
        /// </summary>
        /// <param name="feeSchedule">The fee schedule.</param>
        public FeeScheduleViewModel(IFeeScheduleModel feeSchedule)
        {
            ParticipantType = String.Format("{0}-{1}-{2}", 
                feeSchedule.ParticipantTypeAbbreviation, feeSchedule.ParticipationMethodLabel, feeSchedule.RestrictedAccessFlag);
            ConsultantFeeText = feeSchedule.ConsultantFeeText;
            Paid = feeSchedule.HonorariumAccepted;
            Fee = feeSchedule.ConsultantFee;
            StartDate = ViewHelpers.FormatDate(feeSchedule.PeriodStartDate);
            EndDate = ViewHelpers.FormatDate(feeSchedule.PeriodEndDate);
            SraManagers = feeSchedule.ManagerList;
            WorkDescription = feeSchedule.DescriptionOfWork;
            UploadedBy = ViewHelpers.ConstructName(feeSchedule.LastName, feeSchedule.FirstName);
            UploadedDate = ViewHelpers.FormatDate(feeSchedule.UploadDate);
        }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        [JsonProperty("index")]
        public int Index { get; set; }
        /// <summary>
        /// Gets the type of the participant.
        /// </summary>
        /// <value>
        /// The type of the participant.
        /// </value>
        [JsonProperty("participantType")]
        public string ParticipantType { get; private set; }
        /// <summary>
        /// Gets the consultant fee text.
        /// </summary>
        /// <value>
        /// The consultant fee text.
        /// </value>
        [JsonProperty("consultantFeeText")]
        public string ConsultantFeeText { get; private set; }
        /// <summary>
        /// Gets the paid.
        /// </summary>
        /// <value>
        /// The paid.
        /// </value>
        [JsonProperty("paid")]
        public string Paid { get; private set; }
        /// <summary>
        /// Gets the fee.
        /// </summary>
        /// <value>
        /// The fee.
        /// </value>
        [JsonProperty("fee")]
        public decimal Fee { get; private set; }
        /// <summary>
        /// Gets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        [JsonProperty("startDate")]
        public string StartDate { get; private set; }
        /// <summary>
        /// Gets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        [JsonProperty("endDate")]
        public string EndDate { get; private set; }
        /// <summary>
        /// Gets the sra managers.
        /// </summary>
        /// <value>
        /// The sra managers.
        /// </value>
        [JsonProperty("sraManagers")]
        public string SraManagers { get; private set; }
        /// <summary>
        /// Gets the work description.
        /// </summary>
        /// <value>
        /// The work description.
        /// </value>
        [JsonProperty("workDescription")]
        public string WorkDescription { get; private set; }
        /// <summary>
        /// Gets the uploaded by.
        /// </summary>
        /// <value>
        /// The uploaded by.
        /// </value>
        [JsonProperty("uploadedBy")]
        public string UploadedBy { get; private set; }
        /// <summary>
        /// Gets the uploaded date.
        /// </summary>
        /// <value>
        /// The uploaded date.
        /// </value>
        [JsonProperty("uploadedDate")]
        public string UploadedDate { get; private set; }
    }
}