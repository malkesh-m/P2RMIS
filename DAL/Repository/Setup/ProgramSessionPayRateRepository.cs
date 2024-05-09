using System.Collections.Generic;
using System.Linq;
using System;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    /// <summary>
    /// Provides database access to  entities.
    /// </summary>
    public interface IProgramSessionPayRateRepository : IGenericRepository<ProgramSessionPayRate>
    {
        /// <summary>
        /// Adds the specified program year identifier.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="meetingSessionId">The session identifier.</param>
        /// <param name="clientParticipantTypeId">The client participant type identifier.</param>
        /// <param name="participantMethodId">The participant method identifier.</param>
        /// <param name="restrictedAssignedFlag">if set to <c>true</c> [restricted assigned flag].</param>
        /// <param name="meetingTypeId">The meeting type identifier.</param>
        /// <param name="employmentCategoryId">The employment category identifier.</param>
        /// <param name="honoraiumAccepted">The honoraium accepted.</param>
        /// <param name="consultantFeeText">The consultant fee text.</param>
        /// <param name="consultantFee">The consultant fee.</param>
        /// <param name="periodStartDate">The period start date.</param>
        /// <param name="periodEndDate">The period end date.</param>
        /// <param name="managerList">The manager list.</param>
        /// <param name="descriptionOfWork">The description of work.</param>
        /// <param name="userId">The user identifier.</param>
        void Add(int programYearId, int? meetingSessionId, int clientParticipantTypeId, int participantMethodId, bool restrictedAssignedFlag, int meetingTypeId,
            int employmentCategoryId, string honoraiumAccepted, string consultantFeeText, decimal consultantFee,
            DateTime periodStartDate, DateTime periodEndDate, string managerList, string descriptionOfWork, int userId);
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void Delete(int id, int userId);
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="userId">The user identifier.</param>
        void Delete(ProgramSessionPayRate entity, int userId);
        /// <summary>
        /// Deletes the by program identifier.
        /// </summary>
        /// <param name="programYearId">The program identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void DeleteByProgramSessionId(int programYearId, int meetingTypeId, int userId);
        /// <summary>
        /// Gets the by program session identifier.
        /// </summary>
        /// <param name="meetingSessionId">The program year identifier.</param>
        /// <returns></returns>
        IEnumerable<ProgramSessionPayRate> GetByProgramSessionId(int programYearId, int meetingTypeId);
        /// <summary>
        /// Deletes the by program mechanism identifier.
        /// </summary>
        /// <param name="programYearId">The program identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void DeleteByMeetingSessionId(int programYearId, int meetingTypeId, int meetingSessionId, int userId);
        /// <summary>
        /// Gets the by meeting session identifier.
        /// </summary>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <returns></returns>
        IEnumerable<ProgramSessionPayRate> GetByMeetingSessionId(int programYearId, int meetingTypeId, int meetingSessionId);
        

    }

        public class ProgramSessionPayRateRepository : GenericRepository<ProgramSessionPayRate>, IProgramSessionPayRateRepository
    {
        public ProgramSessionPayRateRepository(P2RMISNETEntities context) : base(context) { }

        /// <summary>
        /// Adds the specified program year identifier.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="meetingSessionId">The session identifier.</param>
        /// <param name="clientParticipantTypeId">The client participant type identifier.</param>
        /// <param name="participantMethodId">The participant method identifier.</param>
        /// <param name="restrictedAssignedFlag">if set to <c>true</c> [restricted assigned flag].</param>
        /// <param name="meetingTypeId">The meeting type identifier.</param>
        /// <param name="employmentCategoryId">The employment category identifier.</param>
        /// <param name="honoraiumAccepted">The honoraium accepted.</param>
        /// <param name="consultantFeeText">The consultant fee text.</param>
        /// <param name="consultantFee">The consultant fee.</param>
        /// <param name="periodStartDate">The period start date.</param>
        /// <param name="periodEndDate">The period end date.</param>
        /// <param name="managerList">The manager list.</param>
        /// <param name="descriptionOfWork">The description of work.</param>
        /// <param name="userId">The user identifier.</param>
        public void Add(int programYearId, int? meetingSessionId, int clientParticipantTypeId, int participantMethodId, bool restrictedAssignedFlag, int meetingTypeId,
            int employmentCategoryId, string honoraiumAccepted, string consultantFeeText, decimal consultantFee,
            DateTime periodStartDate, DateTime periodEndDate, string managerList, string descriptionOfWork, int userId)
        {
            var entity = new ProgramSessionPayRate();
            entity.ProgramYearId = programYearId;
            entity.MeetingSessionId = meetingSessionId;
            entity.ClientParticipantTypeId = clientParticipantTypeId;
            entity.ParticipantMethodId = participantMethodId;
            entity.RestrictedAssignedFlag = restrictedAssignedFlag;
            entity.MeetingTypeId = meetingTypeId;
            entity.EmploymentCategoryId = employmentCategoryId;
            entity.HonorariumAccepted = honoraiumAccepted;
            entity.ConsultantFeeText = consultantFeeText;
            entity.ConsultantFee = consultantFee;
            entity.PeriodStartDate = periodStartDate;
            entity.PeriodEndDate = periodEndDate;
            entity.ManagerList = managerList;
            entity.DescriptionOfWork = descriptionOfWork;
            Helper.UpdateCreatedFields(entity, userId);
            Helper.UpdateModifiedFields(entity, userId);
            Add(entity);
        }
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <param name="userId">User identifier</param>
        public void Delete(int id, int userId)
        {
            var entity = GetByID(id);
            Helper.UpdateDeletedFields(entity, userId);
            Delete(id);
        }
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <param name="userId">User identifier</param>
        public void Delete(ProgramSessionPayRate entity, int userId)
        {
            Helper.UpdateDeletedFields(entity, userId);
            Delete(entity);
        }
        /// <summary>
        /// Deletes the by program identifier.
        /// </summary>
        /// <param name="programYearId">The program identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteByProgramSessionId(int programYearId, int meetingTypeId, int userId)
        {
            var newRates = GetByProgramSessionId(programYearId, meetingTypeId);
            foreach (var entity in newRates)
            {
                Delete(entity, userId);
            }
        }

        /// <summary>
        /// Deletes the by program mechanism identifier.
        /// </summary>
        /// <param name="programYearId">The program identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteByMeetingSessionId(int programYearId, int meetingTypeId, int meetingSessionId, int userId)
        {
            var newRates = GetByMeetingSessionId(programYearId, meetingTypeId, meetingSessionId);
            foreach (var entity in newRates)
            {
                Delete(entity, userId);
            }
        }

        /// <summary>
        /// Gets the by program session identifier.
        /// </summary>
        /// <param name="programYearId"></param>
        /// <returns></returns>
        public IEnumerable<ProgramSessionPayRate> GetByProgramSessionId(int programYearId, int meetingTypeId)
        {
            var programRates = context.ProgramSessionPayRates.Where(x => (x.ProgramYearId == programYearId) && (x.MeetingTypeId == meetingTypeId) && x.MeetingSessionId == null);
            return programRates;
        }

        /// <summary>
        /// Gets the by meeting session identifier.
        /// </summary>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <returns></returns>
        public IEnumerable<ProgramSessionPayRate> GetByMeetingSessionId(int programYearId, int meetingTypeId, int meetingSessionId)
        {
            var newRates = context.ProgramSessionPayRates.Where(x => x.ProgramYearId == programYearId && (x.MeetingTypeId == meetingTypeId || x.MeetingTypeId == null) && x.MeetingSessionId == meetingSessionId);
            return newRates;
        }

    }
}