using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Sra.P2rmis.Dal.EntityChanges
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntityPropertyChange
    {
        /// <summary>
        /// The previous value for this field of this record
        /// </summary>
        string OldValue { get; set; }
        /// <summary>
        /// The new value for this field of this record
        /// </summary>
        string NewValue { get; set; }
        /// <summary>
        /// The ChangeType for this field of this record
        /// </summary>
        EntityState ChangeType { get; set; }
        /// <summary>
        /// The field name
        /// </summary>
        string EntityFieldName { get; set; }
        /// <summary>
        /// The table name
        /// </summary>
        string EntityTableName { get; set; }
        /// <summary>
        /// The record's identifier
        /// </summary>
        int EntityId { get; set; }
    }/// <summary>
    /// 
    /// </summary>
    public class EntityPropertyChange : IEntityPropertyChange
    {
        #region Construction & Setup
        /// <summary>
        /// Creates an EntityPropertyChangeRecord
        /// </summary>
        /// <param name="key"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="changeType"></param>
        /// <param name="entityTableName"></param>
        /// <param name="entityId"></param>
        /// <returns>EntityPropertyChange record</returns>
        public static EntityPropertyChange Create(string key, string oldValue, string newValue, EntityState changeType, string entityTableName, int entityId,  bool overRide)
        {
            EntityPropertyChange result = null;
            //
            // Create a record of the property if the values change or there is an indication 
            // that the value should be recorded regardless.
            //
            if ((oldValue != newValue) || (overRide))
            {
                result = new EntityPropertyChange(key, oldValue, newValue, changeType, entityTableName, entityId);
            }
            return result;
        }
        /// <summary>
        /// Creates an EntityPropertyChange record by combining the sources for the new and old values
        /// </summary>
        /// <param name="baseRecord">The record containing the key (field name), new value, change type, table name and entity id</param>
        /// <param name="oldValue">The old value</param>
        /// <param name="overRide">true if record to be written unconditionally</param>
        /// <returns>EntityPropertyChnage record</returns>
        public static EntityPropertyChange CreateComposite(EntityPropertyChange baseRecord, string oldValue, bool overRide)
        {
            return Create(baseRecord.EntityFieldName, oldValue, baseRecord.NewValue, baseRecord.ChangeType, baseRecord.EntityTableName, baseRecord.EntityId, overRide);
        }
        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="entityFieldName"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="changeType"></param>
        /// <param name="entityTableName"></param>
        /// <param name="entityId"></param>
        private EntityPropertyChange (string entityFieldName, string oldValue, string newValue, EntityState changeType, string entityTableName, int entityId)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
            this.EntityFieldName = entityFieldName;
            this.ChangeType = changeType;
            this.EntityTableName = entityTableName;
            this.EntityId = entityId;
        }
        #endregion
        #region Properties
        /// <summary>
        /// The previous value for this field of this record
        /// </summary>
        public string OldValue { get; set; }
        /// <summary>
        /// The new value for this field of this record
        /// </summary>
        public string NewValue { get; set; }
        /// <summary>
        /// The ChangeType for this field of this record
        /// </summary>
        public EntityState ChangeType { get; set; }
        /// <summary>
        /// The field name
        /// </summary>
        public string EntityFieldName { get; set; }
        /// <summary>
        /// The table name
        /// </summary>
        public string EntityTableName { get; set; }
        /// <summary>
        /// The record's identifier
        /// </summary>
        public int EntityId { get; set; }
        #endregion
       
    }
}
