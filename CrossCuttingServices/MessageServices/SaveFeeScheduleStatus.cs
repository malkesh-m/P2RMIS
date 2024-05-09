using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.CrossCuttingServices.MessageServices
{
    /// <summary>
    /// Status for saving fee schedule
    /// </summary>
    public enum SaveFeeScheduleStatus
    {
        /// <summary>
        /// All enums should have a default
        /// </summary>
        Default = 0,
        /// <summary>
        /// Reviewer was successfully assigned
        /// </summary>
        Success = 1,
        /// <summary>
        /// The already exists
        /// </summary>
        AlreadyExists = 2,
        /// <summary>
        /// The file format incorrect
        /// </summary>
        FileFormatIncorrect = 3,
        /// <summary>
        /// The participant method not supplied
        /// </summary>
        ParticipantMethodNotSupplied = 4,
        /// <summary>
        /// The participant method invalid
        /// </summary>
        ParticipantMethodInvalid = 5,
        /// <summary>
        /// The participant level not supplied
        /// </summary>
        ParticipantLevelNotSupplied = 6,
        /// <summary>
        /// The participant level invalid
        /// </summary>
        ParticipantLevelInvalid = 7,
        /// <summary>
        /// The employment category not supplied
        /// </summary>
        EmploymentCategoryNotSupplied = 8,
        /// <summary>
        /// The employment category invalid
        /// </summary>
        EmploymentCategoryInvalid = 9,
        /// <summary>
        /// The consultant text not supplied
        /// </summary>
        ConsultantTextNotSupplied = 10,
        /// <summary>
        /// The consultant text invalid
        /// </summary>
        ConsultantTextInvalid = 11,
        /// <summary>
        /// The consultant fee not supplied
        /// </summary>
        ConsultantFeeNotSupplied = 12,
        /// <summary>
        /// The consultant fee invalid
        /// </summary>
        ConsultantFeeInvalid = 13,
        /// <summary>
        /// The start date not supplied
        /// </summary>
        StartDateNotSupplied = 14,
        /// <summary>
        /// The start date invalid
        /// </summary>
        StartDateInvalid = 15,
        /// <summary>
        /// The end date not supplied
        /// </summary>
        EndDateNotSupplied = 16,
        /// <summary>
        /// The end date invalid
        /// </summary>
        EndDateInvalid = 17,
        /// <summary>
        /// The date range invalid
        /// </summary>
        DateRangeInvalid = 18,
        /// <summary>
        /// The manager list invalid
        /// </summary>
        ManagerListInvalid = 19,
        /// <summary>
        /// The work description invalid
        /// </summary>
        WorkDescriptionInvalid = 20,
        /// <summary>
        /// The participation type not supplied
        /// </summary>
        ParticipationTypeNotSupplied = 21,
        /// <summary>
        /// The participation type invalid
        /// </summary>
        ParticipationTypeInvalid = 22
    }
}
