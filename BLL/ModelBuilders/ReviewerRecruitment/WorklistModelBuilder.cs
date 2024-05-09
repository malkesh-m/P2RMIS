using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.ReviewerRecruitment;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Sra.P2rmis.Bll.ModelBuilders.ReviewerRecruitment
{
    /// <summary>
    /// Builds one or more WorkList web models for the specified client.
    /// </summary>
    internal class WorklistModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        public WorklistModelBuilder(IUnitOfWork unitOfWork, int clientId)
            : base(unitOfWork)
        {
            this.ClientId = clientId;
            this.Results = new Container<IWorkList>();
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Client entity identifier
        /// </summary>
        protected int ClientId { get; private set; }
        /// <summary>
        /// ContainerModelBuilder results.
        /// </summary>
        public Container<IWorkList> Results { get; private set; }
        #endregion
        #region Builder
        /// <summary>
        /// Build a container of WorkList models.
        /// </summary>
        /// <returns>Model container of WorkList models.</returns>
        public override void BuildContainer()
        {
            //
            // Retrieve any UserProfile changes for this user
            //
            IQueryable<UserInfoChangeLog> changes = RetrieveChanges();
            //
            // Now build the results & put them in the container
            //
            BuildResults(changes);
        }
        /// <summary>
        /// Retrieve UserInfoChangeLog entries that have not been reviewed.
        /// </summary>
        /// <returns>Enumeration of UserInfoChangeLog entities that have not been reviewed.</returns>
        private IQueryable<UserInfoChangeLog> GetAllUnreviewedChangeLogs()
        {
            return this.UnitOfWork.UserInfoChangeLogRepository.Select().Where(x => !x.ReviewedFlag).Select(x => x);
        }
        /// <summary>
        /// Retrieve all the UserProfile changes for any users assigned to the client's
        /// panels.
        /// </summary>
        /// <returns>Enumeration of UserInfoChangeLog entities</returns>
        private IQueryable<UserInfoChangeLog> RetrieveChanges()
        {
            //
            // First we get all the change logs that have not been reviewed.
            //
            IQueryable<UserInfoChangeLog> results = GetAllUnreviewedChangeLogs().
                //
                // Then we filter them those that belong to the specific client.
                //
                Where(x => x.UserInfo.User.UserClients.Any(y => y.ClientID == ClientId));

            return results;
        }
        /// <summary>
        /// Construct a list of UserProfie changes.  Entries have been "de-duped" meaning
        /// changes to individual fields that were made by separate users are marked for display
        /// on once.
        /// Duplicated changes are included but are not marked for display.  The duplicate records
        /// indicate which UserInfoChangeLog entities should be marked as "reviewed".  This prevents
        /// the marking as "reviewed" changes that were not represented in the original display.
        /// </summary>
        /// <param name="changes">Enumeration of UserInfoChangeLog entities for the specified Client</param>
        private void BuildResults(IQueryable<UserInfoChangeLog> changes)
        {
            //
            // For each of the UserInfoChangeLog identified create a WorkList item and add it to a list
            //
        
            List<WorkList> list = changes.Select(x => new WorkList()
            {
                FirstName = x.UserInfo.FirstName,
                LastName = x.UserInfo.LastName,
                ModifedByFirstName = x.ModifiedByUser.UserInfoes.FirstOrDefault().FirstName,
                ModifedByLastName = x.ModifiedByUser.UserInfoes.FirstOrDefault().LastName,
                ModifiedOn = x.ModifiedDate,
                RoleName = x.UserInfo.User.UserSystemRoles.FirstOrDefault().SystemRole.SystemRoleName,
                UserInfoId = x.UserInfo.UserInfoID,
                ClientId = ClientId,
                UserInfoChangeLogId = x.UserInfoChangeLogId
            }).ToList();
            //
            // Now we toggle the display switch of the first items
            //
            ToggleWorkListItemsDisplayFlag(list);

            Results.ModelList = list;
        }
        /// <summary>
        /// Groups the WorkList items by user, sorts them in descending order then selects
        /// the latest WorkList item.  These then are the WorkList items to be displayed.  Once
        /// selected the WorkList display switch is flipped.
        /// </summary>
        /// <param name="list">List of WorkList items</param>
        private void ToggleWorkListItemsDisplayFlag(List<WorkList> list)
        {
            //
            // Determine which are the that should be displayed.  first the WorkList is ordered by
            // the date/time the entry was created
            //
            var displayableWorkListEntries = list.OrderByDescending(x => x.ModifiedOn).
                //
                // then we groupBy the UserId
                //
                GroupBy(x => x.UserInfoId).
                //
                // then we select the first entry in each group 
                //
                Select(x => new { Value = x.First() });
            //
            // Now it is just as simple as iterating through the enumeration of first entries
            // and toggling the display switch to be on
            //
            foreach (var workListEntry in displayableWorkListEntries)
            {
                workListEntry.Value.Display = true;
            }

        }
    }
        #endregion
}

