
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.ReviewerRecruitment;

namespace Sra.P2rmis.Bll.ModelBuilders.ReviewerRecruitment
{
    internal class ProfileUpdateListModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        public ProfileUpdateListModelBuilder(IUnitOfWork unitOfWork, int userInfoId)
            : base(unitOfWork, 0)
        {
            // TODO: ModelBuilderBase needs a constructor of only UnitOfWork
            this.UserInfoId = userInfoId;
            this.Results = new Container<IProfileUpdateList>();
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Client entity identifier
        /// </summary>
        protected int UserInfoId { get; private set; }
        /// <summary>
        /// ContainerModelBuilder results.
        /// </summary>
        public Container<IProfileUpdateList> Results { get; private set; }
        #endregion
        #region Builder
        /// <summary>
        /// Build a container of WorkList models.
        /// </summary>
        /// <returns>Model container of ProfileUpdateList models.</returns>
        public override void BuildContainer()
        {
            //
            //  First we retrieve the User Info that was selected.
            //
            UserInfo UserInfoEntity = GetThisUserInfoEntity(UserInfoId);
            //
            // build an enumeration of ProfileUpdateList so that they can see those changes that have not been reviewed
            //
            IEnumerable<UserInfoChangeLog> profileUpdateChanges = DetermineProfileChangesNotReviewed(UserInfoEntity);
            //
            // An construct a Container to return & set it up for return
            //
            Results.ModelList = ConstructModel(profileUpdateChanges);

        }

        /// <summary>
        /// Filters the training documents by ProgramYear & UserParticipationType
        /// </summary>
        /// <param name="programYearEntity">ProgramYear entity</param>
        /// <param name="userParticipationTypes">List of userParticipationTypes</param>
        /// <param name="meetingTypes">List of the user's MeetingType entity identifiers</param>
        /// <returns>Enumeration of TraingingDocuments</returns>
        private IEnumerable<UserInfoChangeLog> DetermineProfileChangesNotReviewed(UserInfo userInfoEntity)
        {
            ///
            // Get the Training documents for the program year
            // 
            IEnumerable<UserInfoChangeLog> result = userInfoEntity.UserInfoChangeLogs.Where(x => x.ReviewedFlag == false);

            return result;
        }
        /// <summary>
        /// Construct a List of ProfileUpdateList from an IEnumerable collection of UserInfoChangeLog.
        /// </summary>
        /// <param name="userInfoChangeLogList">List of userInfoChangeLog</param>
        private List<ProfileUpdateList> ConstructModel(IEnumerable<UserInfoChangeLog> userInfoChangeLogList)
        {
            //
            // Create a list & populate it with models constructed from the TrainingDocuments
            //
            List<ProfileUpdateList> list = new List<ProfileUpdateList>();

            foreach (UserInfoChangeLog p in userInfoChangeLogList)
            {
                var model = new ProfileUpdateList();
                model.SetUserInformation(p.UserInfoChangeType.Label, p.OldValue, p.NewValue, p.UserInfoChangeType.SortOrder, p.UserInfoChangeLogId, p.UserInfoId);

                list.Add(model);
            }

            return list;
        }

        #endregion
    }
}
