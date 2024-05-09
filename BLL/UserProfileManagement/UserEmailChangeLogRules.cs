using System.Collections.Generic;
using System.Linq;

using Sra.P2rmis.Dal.EntityChanges;
using Sra.P2rmis.Bll;
using System.Data;

namespace Sra.P2rmis.Bll.UserProfileManagement
{
    /// <summary>
    /// interface encapsulating the rules and methods for user email changes to create a single EntityPropertyChange record
    /// from a collection of EntityPropertyChange records
    /// </summary>
    public interface IUserEmailChangeLogRules
    {
    }

    /// <summary>
    /// Class encapsulating the rules and methods for user email changes to create a single EntityPropertyChange record
    /// from a collection of EntityPropertyChange records
    /// </summary>
    public class UserEmailChangeLogRules : IUserEmailChangeLogRules
    {
        /// <summary>
        /// Field names of interest for creating change record
        /// </summary>
        public static string PrimaryFlag = "PrimaryFlag";
        public static string Email = "Email";
        public static string UserEmail = "UserEmail";
        /// <summary>
        /// Builds a single EntityPropertyChange record from a collection of EntityPropertyChange records
        /// </summary>
        /// <param name="changes">Enumerated list of EntityPropertyChange records</param>
        /// <returns>EntityPropertyhange record containing change or null if no change</returns>
        public static EntityPropertyChange ComputeUserInfoChangeRecord(IEnumerable<EntityPropertyChange> changes)
        {
            EntityPropertyChange resultantRecord = null;

            // get type of change
            bool deletedPreferred = changes.Where(x => x.EntityFieldName == PrimaryFlag && x.ChangeType == System.Data.Entity.EntityState.Deleted && x.OldValue == "True").Count() > 0;
            bool addedPreferred = changes.Where(x => x.EntityFieldName == PrimaryFlag && x.ChangeType == System.Data.Entity.EntityState.Added && x.NewValue == "True").Count() > 0;
            bool modifiedPerferredFlag = changes.Where(x => x.EntityFieldName == PrimaryFlag && x.ChangeType == System.Data.Entity.EntityState.Modified && x.NewValue != x.OldValue).Count() > 0;
            bool multipleRecordChange = modifiedPerferredFlag || deletedPreferred || addedPreferred;

            if (multipleRecordChange)
            {
                resultantRecord = CompositeInfoChangeRecord(modifiedPerferredFlag, deletedPreferred, addedPreferred, changes);
            }
            else
            {
                // both emails could have changed the Email field, get the one that is primary
                resultantRecord = SingleRecordInfoChangeRecord(changes);
            }
            return resultantRecord;
        }
        /// <summary>
        /// Change of primary email was not from a single record change.  Compute a single change record from the composite of the changed records
        /// </summary>
        /// <param name="modifiedPerferred"></param>
        /// <param name="deletedPreferred"></param>
        /// <param name="addedPreferred"></param>
        /// <param name="changes">List of EntityPropertyChange records</param>
        /// <returns>EntityPropertyChange record object</returns>
        protected static EntityPropertyChange CompositeInfoChangeRecord(bool modifiedPerferred, bool deletedPreferred, bool addedPreferred, IEnumerable<EntityPropertyChange> changes)
        {
            EntityPropertyChange resultantRecord;

            if (deletedPreferred && addedPreferred)
            {
                // deleted previous preferred, new preferred added
                resultantRecord = CompositeAddedDeletedPreferred(changes);
            }
            else if (modifiedPerferred && deletedPreferred)
            {
                // deleted previous preferred, previous non preferred now preferred
                resultantRecord = CompositeModifiedDeletePreferred(changes);
            }
            else if (modifiedPerferred && addedPreferred)
            {
                // previous preferred now not preferred, added new preferred
                resultantRecord = CompositeModifiedAddedPreferred(changes);
            }
            else if (deletedPreferred)
            {
                resultantRecord = CompositeDelete(changes);
            }
            else if (addedPreferred)
            {
                resultantRecord = SingleRecordInfoChangeRecord(changes);
            }
            else // modified
            {
                // change of preferred email from existing email records
                resultantRecord = CompositeInfoChangeRecord(changes);
            }
            return resultantRecord;
        }
        /// <summary>
        /// Combine EntityPropertyChange records when the email is removed.
        /// </summary>
        /// <param name="changes">List of EntityPropertyChange records</param>
        /// <returns>EntityPropertyChange record object</returns>
        protected static EntityPropertyChange CompositeDelete(IEnumerable<EntityPropertyChange> changes)
        {
            int deletedId = changes.Where(x => x.ChangeType == System.Data.Entity.EntityState.Deleted).FirstOrDefault().EntityId;

            EntityPropertyChange oldEmailEntity = changes.Where(x => x.ChangeType == System.Data.Entity.EntityState.Deleted && x.EntityFieldName == Email).FirstOrDefault();

            return EntityPropertyChange.CreateComposite(oldEmailEntity, oldEmailEntity.OldValue, false);
        }
        /// <summary>
        /// Compute the change record involving the addition and deletion of a record
        /// </summary>
        /// <param name="changes">List of EntityPropertyChange records</param>
        /// <returns>EntityPropertyChange record object</returns>
        protected static EntityPropertyChange CompositeAddedDeletedPreferred(IEnumerable<EntityPropertyChange> changes)
        {
            int deletedId = changes.Where(x => x.ChangeType == System.Data.Entity.EntityState.Deleted).FirstOrDefault().EntityId;
            string oldValue = changes.Where(x => x.EntityId == deletedId && x.EntityFieldName == Email).FirstOrDefault().OldValue;

            EntityPropertyChange  added = changes.Where(x => x.ChangeType == System.Data.Entity.EntityState.Added && x.EntityFieldName == Email).FirstOrDefault();

            return EntityPropertyChange.CreateComposite(added, oldValue, false);
        }
        /// <summary>
        /// Compute the change record involving the modification and deletion of a record
        /// </summary>
        /// <param name="changes">List of EntityPropertyChange records</param>
        /// <returns>EntityPropertyChange record object</returns>
        protected static EntityPropertyChange CompositeModifiedDeletePreferred(IEnumerable<EntityPropertyChange> changes)
        {
            int deletedId = changes.Where(x => x.ChangeType == System.Data.Entity.EntityState.Deleted).FirstOrDefault().EntityId;
            string oldValue = changes.Where(x => x.EntityId == deletedId && x.EntityFieldName == Email).FirstOrDefault().OldValue;

            EntityPropertyChange modified = changes.Where(x => x.ChangeType == System.Data.Entity.EntityState.Modified && x.EntityFieldName == Email).FirstOrDefault();
            return EntityPropertyChange.CreateComposite(modified, oldValue, false);
        }
        /// <summary>
        /// Compute the change record involving the modification and addition of a record
        /// </summary>
        /// <param name="changes">List of EntityPropertyChange records</param>
        /// <returns>EntityPropertyChange record object</returns>
        protected static EntityPropertyChange CompositeModifiedAddedPreferred(IEnumerable<EntityPropertyChange> changes)
        {
            int oldId = changes.Where(x => x.EntityFieldName == PrimaryFlag && x.OldValue == "True").FirstOrDefault().EntityId;
            string oldValue = changes.Where(x => x.EntityId == oldId && x.EntityFieldName == Email).FirstOrDefault().OldValue;

            EntityPropertyChange added = changes.Where(x => x.ChangeType == System.Data.Entity.EntityState.Added && x.EntityFieldName == Email).FirstOrDefault();

            return EntityPropertyChange.CreateComposite(added, oldValue, false);
        }
        /// <summary>
        /// Combines changes in more than one EntityPropertyChange record to form a single record
        /// </summary>
        /// <param name="changes">List of EntityPropertyChange records</param>
        /// <returns>The single resultant EntityProperty record object</returns>
        protected static EntityPropertyChange CompositeInfoChangeRecord(IEnumerable<EntityPropertyChange> changes)
        {
            int oldId;
            string oldEmail;
            int newId;
            EntityPropertyChange newEmailRecord = null;

            // gets the old value for modify 
            var oldPrimary = changes.Where(x => x.EntityFieldName == PrimaryFlag && x.ChangeType == System.Data.Entity.EntityState.Modified && x.OldValue == "True");
            // get the new value for modify
            var newPrimary = changes.Where(x => x.EntityFieldName == PrimaryFlag && x.ChangeType == System.Data.Entity.EntityState.Modified && x.NewValue == "True");

            // get new email record
            if (newPrimary.Count() > 0)
            {
                newId = newPrimary.FirstOrDefault().EntityId;
                newEmailRecord = changes.Where(x => x.EntityFieldName == Email && x.EntityId == newId).FirstOrDefault();
            }

            // get old primary value
            if (oldPrimary.Count() > 0)
            {
                oldId = oldPrimary.FirstOrDefault().EntityId;
                oldEmail = changes.Where(x => x.EntityFieldName == Email && x.EntityId == oldId).FirstOrDefault().OldValue;
            }
            else if (newPrimary.Count() > 0)
            {
                // if oldPrimary is null, use the original value of the newPrimary for the old value - covers the case of having added a non preferred record
                oldId = newPrimary.FirstOrDefault().EntityId;
                oldEmail = changes.Where(x => x.EntityFieldName == Email && x.EntityId == oldId).FirstOrDefault().OldValue;
            }
            else
            {
                oldEmail = string.Empty;
            }

            EntityPropertyChange resultantRecord = EntityPropertyChange.CreateComposite(newEmailRecord, oldEmail, false);

            return resultantRecord;
        }
        /// <summary>
        /// Selects which EntityPropertyChangerecord to use to build the single change record
        /// </summary>
        /// <param name="changes">List of EntityPropertyChange records</param>
        /// <returns>The single resultant EntityProperty record object</returns>
        protected static EntityPropertyChange SingleRecordInfoChangeRecord(IEnumerable<EntityPropertyChange> changes)
        {
            EntityPropertyChange resultantRecord = null;
            var primary = changes.Where(x => x.EntityFieldName == PrimaryFlag && x.NewValue == "True");

            if (primary.Count() > 0)
            {
                int newId = changes.Where(x => x.EntityFieldName == PrimaryFlag && x.NewValue == "True").FirstOrDefault().EntityId;


                 var newEmailRecord = changes.Where(x => x.EntityFieldName == Email && x.EntityId == newId).FirstOrDefault();

                resultantRecord = EntityPropertyChange.CreateComposite(newEmailRecord, newEmailRecord.OldValue, false);
            }

            return resultantRecord;
        }
    }
}
