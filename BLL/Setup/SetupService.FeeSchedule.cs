using Sra.P2rmis.Bll.ModelBuilders.Setup;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.Setup;
using Sra.P2rmis.WebModels.Lists;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Setup
{
    /// <summary>
    /// Provides services for System Setup (related to fee schedules).
    /// </summary>
    public partial interface ISetupService
    {
        /// <summary>
        /// Retrieves the fiscal year fee schedule grid.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="meetingTypeId">The meeting type identifier.</param>
        /// <returns></returns>
        Container<IFeeScheduleModel> RetrieveFiscalYearFeeScheduleGrid(int programYearId, int meetingTypeId, int? sessionId);
        /// <summary>
        /// Retrieves the session fee schedule meeting list.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="year">The year.</param>
        /// <param name="meetingTypeId">Meeting type.</param>
        /// <returns></returns>
        Container<IGenericListEntry<int, string>> RetrieveSessionFeeScheduleMeetingList(int clientId, string year, int programYearId, int meetingTypeId);
        
        /// <summary>
        /// Gets the meeting types.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <returns></returns>
        List<KeyValuePair<int, string>> GetMeetingTypes(int programYearId);
        /// <summary>
        /// Adds the fee schedule list.
        /// </summary>
        /// <param name="feeScheduleList">The fee schedule list.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="meetingTypeId">The meeting type identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        ICollection<KeyValuePair<int, SaveFeeScheduleStatus>> AddFeeScheduleList(List<FeeScheduleUploadModel> feeScheduleList, int programYearId, int meetingTypeId, int? sessionId, int userId);
        /// <summary>
        /// Deletes the mechanism scoring template.
        /// </summary>
        /// <param name="programYearId">The program identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void DeleteSessionFeeSchedule(int programYearId, int meetingTypeId, int meetingSessionId, int userId);
        /// <summary>
        /// Deletes the program mechanism scoring template.
        /// </summary>
        /// <param name="mechanismScoringTemplateId">The mechanism scoring template identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void DeleteProgramFeeSchedule(int programYearId, int meetingTypeId, int userId);
    }
    /// <summary>
    /// Provides services for System Setup (related to fee schedules).
    /// </summary>
    public partial class SetupService
    {
        public const string FULL = "FULL";
        public const string PARTIAL = "PARTIAL";
        public const string PAID = "PAID";
        public const string UNPAID = "UNPAID";
        /// <summary>
        /// Retrieves the fiscal year fee schedule grid.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="meetingTypeId">The meeting type identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <returns></returns>
        public virtual Container<IFeeScheduleModel> RetrieveFiscalYearFeeScheduleGrid(int programYearId, int meetingTypeId, int? sessionId)
        {
            ValidateInt(programYearId, FullName(nameof(SetupService), nameof(RetrieveFiscalYearFeeScheduleGrid)), nameof(programYearId));

            ProgramPayRatesGridModelBuilder builder = new ProgramPayRatesGridModelBuilder(this.UnitOfWork, programYearId, meetingTypeId, sessionId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieves a container for the Meeting dropdown on the Session Fee Schedule view.
        /// </summary>
        /// <param name="clientId">ClientId entity identifier</param>
        /// <param name="year">Selected year value</param>
        /// <param name="programYearId">Program Year</param>
        /// <param name="meetingTypeId">Meeting type</param>
        /// <returns>Container of IFeeScheduleModel model for the given MeetingSession entity identifier</returns>
        public virtual Container<IGenericListEntry<int, string>> RetrieveSessionFeeScheduleMeetingList(int clientId, string year, int programYearId, int meetingTypeId)
        {
            string name = FullName(nameof(SetupService), nameof(RetrieveSessionFeeScheduleMeetingList));
            ValidateInt(clientId, name, nameof(clientId));
            ValidateString(year, name, nameof(year));
            ValidateInt(programYearId, name, nameof(programYearId));

            ClientYearMeetingListBuilder builder = new ClientYearMeetingListBuilder(this.UnitOfWork, clientId, year, programYearId, meetingTypeId);
            builder.BuildContainer();
            return builder.Results;
        }
        

        /// <summary>
        /// Gets the meeting types.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <returns></returns>
        public List<KeyValuePair<int, string>> GetMeetingTypes(int programYearId)
        {
            ValidateInt(programYearId, FullName(nameof(SetupService), nameof(GetMeetingTypes)), nameof(programYearId));

            var results = UnitOfWork.ProgramMeetingRepository.Select().Where(y => y.ProgramYearId == programYearId)
                .GroupBy(x => x.ClientMeeting.MeetingTypeId).ToList();
            var modelResults = new List<KeyValuePair<int, string>>();
            for (var i = 0; i < results.Count; i++)
            {
                modelResults.Add(new KeyValuePair<int, string>(results[i].FirstOrDefault().ClientMeeting.MeetingTypeId,
                    results[i].FirstOrDefault().ClientMeeting.MeetingType.MeetingTypeAbbreviation));
            }
            return modelResults;
        }
        /// <summary>
        /// Adds the fee schedule list.
        /// </summary>
        /// <param name="feeScheduleList">The fee schedule list.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="meetingTypeId">The meeting type identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ICollection<KeyValuePair<int, SaveFeeScheduleStatus>> AddFeeScheduleList(List<FeeScheduleUploadModel> feeScheduleList, int programYearId, int meetingTypeId, int? sessionId, int userId)
        {
            var statusList = new List<KeyValuePair<int, SaveFeeScheduleStatus>>();
            var participantTypes = UnitOfWork.ClientParticipantTypeRepository.GetAll();
            var participantMethods = UnitOfWork.ParticipationMethodRepository.GetAll();
            var employmentCategories = UnitOfWork.EmploymentCategoryRepository.GetAll();
            var programYear = UnitOfWork.ProgramYearRepository.GetByID(programYearId);
                        
            for (var i = 0; i < feeScheduleList.Count; i++)
            {
                var feeSchedule = feeScheduleList[i];
                if (i > 0 && feeScheduleList.Take(i).Count(x => x.ParticipantTypeAbbreviation.ToUpper() == feeSchedule.ParticipantTypeAbbreviation.ToUpper()
                    && x.ParticipationMethodLabel.ToUpper() == feeSchedule.ParticipationMethodLabel.ToUpper()
                    && x.RestrictedAccessFlag.ToUpper() == feeSchedule.RestrictedAccessFlag.ToUpper()
                    && x.HonorariumAccepted.ToUpper() == feeSchedule.HonorariumAccepted.ToUpper()) > 0)
                {
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.AlreadyExists));
                }
                if (String.IsNullOrEmpty(feeSchedule.ParticipantTypeAbbreviation))
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.ParticipationTypeNotSupplied));
                if (String.IsNullOrEmpty(feeSchedule.ParticipationMethodLabel))
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.ParticipantMethodNotSupplied));
                var participantType = participantTypes.Where(x => x.ParticipantTypeAbbreviation.ToUpper() == feeSchedule.ParticipantTypeAbbreviation.ToUpper()
                        && x.ClientId == programYear.ClientProgram.ClientId).FirstOrDefault();
                if (participantType == null)
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.ParticipationTypeInvalid));
                var participantMethod = participantMethods.Where(x => x.ParticipationMethodLabel.ToUpper() == feeSchedule.ParticipationMethodLabel.ToUpper()).FirstOrDefault();
                if (participantMethod == null)
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.ParticipantMethodInvalid));

                bool? restrictedAccessFlag = default(bool?);
                if (String.IsNullOrEmpty(feeSchedule.RestrictedAccessFlag))
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.ParticipantLevelNotSupplied));
                else
                {
                    restrictedAccessFlag = feeSchedule.RestrictedAccessFlag.ToUpper() == FULL ? (bool?)false : feeSchedule.RestrictedAccessFlag.ToUpper() == PARTIAL ? (bool?)true : null;
                    if (restrictedAccessFlag == null)
                        statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.ParticipantLevelInvalid));
                }

                if (String.IsNullOrEmpty(feeSchedule.HonorariumAccepted))
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.EmploymentCategoryNotSupplied));
 				var employmentCategory = employmentCategories.Where(x => x.Name.ToUpper() == feeSchedule.HonorariumAccepted.ToUpper()).FirstOrDefault();
                if (employmentCategory == null)
                        statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.EmploymentCategoryInvalid));

                if (String.IsNullOrEmpty(feeSchedule.ConsultantFeeText))
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.ConsultantTextNotSupplied));
                else if (feeSchedule.ConsultantFeeText.Length > 200)
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.ConsultantTextInvalid));
                if (String.IsNullOrEmpty(feeSchedule.ConsultantFee))
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.ConsultantFeeNotSupplied));
                decimal consultantFee;
                var isConsultantFeeValid = decimal.TryParse(feeSchedule.ConsultantFee, NumberStyles.Currency, CultureInfo.CurrentCulture, out consultantFee);
                if (!isConsultantFeeValid || consultantFee < 0)
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.ConsultantFeeInvalid));

                if (String.IsNullOrEmpty(feeSchedule.PeriodStartDate))
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.StartDateNotSupplied));
                if (String.IsNullOrEmpty(feeSchedule.PeriodEndDate))
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.EndDateNotSupplied));
                DateTime startDate = default(DateTime);
                double dStartDate;
                var isStartDateValid = double.TryParse(feeSchedule.PeriodStartDate, out dStartDate);
                if (!isStartDateValid)
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.StartDateInvalid));
                else
                    startDate = DateTime.FromOADate(dStartDate);
                DateTime endDate = default(DateTime);
                double dEndDate;
                var isEndDateValid = double.TryParse(feeSchedule.PeriodEndDate, out dEndDate);
                if (!isEndDateValid)
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.EndDateInvalid));
                else
                    endDate = DateTime.FromOADate(dEndDate);

                if (feeSchedule.ManagerList.Length > 500)
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.ManagerListInvalid));
                if (feeSchedule.DescriptionOfWork.Length > 8000)
                    statusList.Add(new KeyValuePair<int, SaveFeeScheduleStatus>(i + 1, SaveFeeScheduleStatus.WorkDescriptionInvalid));

                if (statusList.Count == 0)
                {
                    // Add program pay rate
                    UnitOfWork.ProgramSessionPayRateRepository.Add(programYearId, sessionId, participantType.ClientParticipantTypeId, participantMethod.ParticipationMethodId, (bool)restrictedAccessFlag,
                        meetingTypeId, employmentCategory.EmploymentCategoryId, employmentCategory.Name, feeSchedule.ConsultantFeeText, consultantFee, startDate,
                        endDate, feeSchedule.ManagerList, feeSchedule.DescriptionOfWork, userId);
                }
            }
            if (statusList.Count == 0)
                UnitOfWork.Save();
            return statusList;
        }
        /// <summary>
        /// Deletes the Session Fee Schedule.
        /// </summary>
        /// <param name="meetingSessionId">The meetingSession identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public void DeleteSessionFeeSchedule(int programYearId, int meetingTypeId, int meetingSessionId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(DeleteSessionFeeSchedule));
            ValidateInteger(meetingSessionId, name, nameof(meetingSessionId));

            UnitOfWork.ProgramSessionPayRateRepository.DeleteByMeetingSessionId(programYearId, meetingTypeId, meetingSessionId, userId);
            UnitOfWork.Save();
        }
        /// <summary>
        /// Deletes the Program Fee Schedule.
        /// </summary>
        /// <param name="meetingSessionId">The meetingSession identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public void DeleteProgramFeeSchedule(int programYearId, int meetingTypeId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(DeleteProgramFeeSchedule));
            ValidateInteger(programYearId, name, nameof(programYearId));
            ValidateInteger(meetingTypeId, name, nameof(meetingTypeId));

            UnitOfWork.ProgramSessionPayRateRepository.DeleteByProgramSessionId(programYearId, meetingTypeId, userId);
            UnitOfWork.Save();
        }
    }
}
