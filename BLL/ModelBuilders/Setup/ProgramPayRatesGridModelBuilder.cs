using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Setup;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    #region Program PayRate builder
    /// <summary>
    /// Base Model builder to construct one or more models for a Fee Schedule grid
    /// (Client Fiscal Year or Session)
    /// </summary>
    internal class PayRatesGridBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="entityIdentifier">Entity identifier of selected program</param>
        public PayRatesGridBuilder(IUnitOfWork unitOfWork, int entityIdentifier)
            : base(unitOfWork)
        {
            this.EntityIdentifier = entityIdentifier;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// User who uploaded fee schedules
        /// </summary>
        protected User UserEntity { get; set; }
        /// <summary>
        /// Program to display
        /// </summary>
        protected virtual int EntityIdentifier { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected virtual IEnumerable<IFeeSchedule> SearchResults { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<IFeeScheduleModel> Results { get; private set; } = new Container<IFeeScheduleModel>();
        /// <summary>
        /// Dictionary for ParticipationMethod value.  Since there is only two values (currently) this
        /// seemed more efficient than hitting the database each time.
        /// </summary>
        protected static Dictionary<int, string> Methods { get; set; } = new Dictionary<int, string>();
        /// <summary>
        /// Gets or sets the employment categories.
        /// </summary>
        /// <value>
        /// The employment categories.
        /// </value>
        protected static Dictionary<int, string> EmploymentCategories { get; set; } = new Dictionary<int, string>();
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
            Search();
            MakeModels();
        }
        /// <summary>
        /// Does all the heavy lifting for retrieving the Program Setup grid data.
        /// </summary>
        internal virtual void Search()
        {
            //
            // The derived class must override this method with the 
            // specifics.
            //
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        internal virtual void MakeModels()
        {
            //
            // Now that we have the records from the database that will populate the grid
            // we can model the data & return it.
            //
            var modelResults = SearchResults.Select(x => new FeeScheduleModel
            {
                //
                // Pull out the data for the model
                //
                ParticipantTypeAbbreviation = x.ClientParticipantType.ParticipantTypeAbbreviation,
                ParticipationMethodLabel = ParticipationMethodLabel(x.ParticipantMethodId),
                RestrictedAccessFlag = Helper.Level(x.RestrictedAssignedFlag),
                ConsultantFeeText = x.ConsultantFeeText,
                HonorariumAccepted = HonrariumAccepted((int)x.EmploymentCategoryId),
                ConsultantFee = x.ConsultantFee,
                PeriodStartDate = x.PeriodStartDate,
                PeriodEndDate = x.PeriodEndDate,
                ManagerList = x.ManagerList,
                DescriptionOfWork = x.DescriptionOfWork,
                UploadDate = x.ModifiedDate,
                FirstName = FirstName(x.ModifiedBy),
                LastName = LastName(x.ModifiedBy),
                //
                // and now pull out the identifiers
                //
                ScheduleEntityId = x.Index()
            });

            this.Results.ModelList = modelResults.ToList();
        }
        /// <summary>
        /// Retrieves the participation method label based on the entity identifier
        /// </summary>
        /// <param name="participationMethodId">ParticipationMethod entity identifier</param>
        /// <returns>ParticipationMethod label</returns>
        protected string ParticipationMethodLabel(int participationMethodId)
        {
            if (!Methods.Keys.Contains(participationMethodId))
            {
                var a = UnitOfWork.ParticipationMethodRepository.GetByID(participationMethodId);
                Methods.Add(participationMethodId, a.ParticipationMethodLabel);
            }
            return Methods[participationMethodId];
        }
        /// <summary>
        /// Determines if an honorarium is accepted by converting the value in the 
        /// Fee entity (true or false) to a bool.
        /// </summary>
        /// <param name="accepted">String value</param>
        /// <returns>True if the value is any permutation of true (TRUE; True etc); false otherwise</returns>
        protected string HonrariumAccepted(int employmentCategoryId)
        {
            if (!EmploymentCategories.Keys.Contains(employmentCategoryId))
            {
                var a = UnitOfWork.EmploymentCategoryRepository.GetByID(employmentCategoryId);
                EmploymentCategories.Add(employmentCategoryId, a.Name);
            }
            return EmploymentCategories[employmentCategoryId];
        }
        /// <summary>
        /// Returns the first name of the user who uploaded the fee schedule
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>First name of the user who upload the fee schedule</returns>
        protected string FirstName(Nullable<int> userId)
        {
            LazyLoadUserEntity(userId);
            return (UserEntity != null)? UserEntity.FirstName(): string.Empty;
        }
        /// <summary>
        /// Returns the last name of the user who uploaded the fee schedule
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Last name of the user who upload the fee schedule</returns>
        protected string LastName(Nullable<int> userId)
        {
            LazyLoadUserEntity(userId);
            return (UserEntity != null) ? UserEntity.LastName() : string.Empty;
        }
        /// <summary>
        /// Lazy load the user.  We expect the same user to be in all entities for the
        /// same fee schedule.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        protected virtual void LazyLoadUserEntity(Nullable<int> userId)
        {
            if ((UserEntity == null) & (userId.HasValue))
            {
                UserEntity = UnitOfWork.UserRepository.GetByID(userId.Value);
            }
        }
        #endregion
    }
    #endregion
    #region Program PayRate builder
    /// <summary>
    /// Model builder to construct one or more models for the Client-Fiscal Year Fee Schedules grid
    /// </summary>
    internal class ProgramPayRatesGridModelBuilder : PayRatesGridBuilder
    {
        #region Construction & Setup        
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramPayRatesGridModelBuilder"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="meetingTypeId">The meeting type identifier.</param>
        public ProgramPayRatesGridModelBuilder(IUnitOfWork unitOfWork, int programYearId, int meetingTypeId, int? meetingSessionId)
            : base(unitOfWork, programYearId)  {
            ProgramYearId = programYearId;
            MeetingTypeId = meetingTypeId;
            MeetingSessionId = meetingSessionId.HasValue ? meetingSessionId : null;
        }
        #endregion
        #region Attributes        
        /// <summary>
        /// Gets or sets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        public int ProgramYearId { get; set; }
        /// <summary>
        /// Gets or sets the meeting type identifier.
        /// </summary>
        /// <value>
        /// The meeting type identifier.
        /// </value>
        public int MeetingTypeId { get; set; }
        /// <summary>
        /// Gets or sets the meeting type identifier.
        /// </summary>
        /// <value>
        /// The session identifier.
        /// </value>
        public int? MeetingSessionId { get; set; }

        #endregion
        #region Services
        /// <summary>
        /// Does all the heavy lifting for retrieving the Program Setup grid data.
        /// </summary>
        internal override void Search()
        {
            // Need to make sure to get the legacy data for Session pay
            if (MeetingSessionId.HasValue )
            {
                this.SearchResults = UnitOfWork.ProgramSessionPayRateRepository.Select()
                                     .Where(x => x.ProgramYearId == ProgramYearId && (x.MeetingTypeId == MeetingTypeId || x.MeetingTypeId == null) && x.MeetingSessionId == MeetingSessionId);
            }
            else {
            this.SearchResults = UnitOfWork.ProgramSessionPayRateRepository.Select()
                                .Where(x => x.ProgramYearId == ProgramYearId && x.MeetingTypeId == MeetingTypeId && x.MeetingSessionId == MeetingSessionId);
            }
        }
        #endregion
    }
    #endregion

}
