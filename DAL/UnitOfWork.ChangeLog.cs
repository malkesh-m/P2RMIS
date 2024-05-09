using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using Sra.P2rmis.Dal.EntityChanges;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal
{
    public partial interface IUnitOfWork
    {
        List<EntityPropertyChange> GetChangedFields();
    }
    /// <summary>
    /// UnitOfWork methods implemented for determining entity properties that have changed.
    /// </summary>
    public partial class UnitOfWork
    {
        /// <summary>
        /// Create a record for each changed item in the database whose change is to be logged in the UserInfoChangeLog table
        /// </summary>
        /// <returns>List of EntityPropertyChange representing the properties that have changed</returns>
        public List<EntityPropertyChange> GetChangedFields()
        {
            int tempId = -1;  // provides temporary unique id for added records
            //
            // Find  all the records that have changed & create a list to return
            //
            var changedRecords = this._context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged);
            List<EntityPropertyChange> resultsList = new List<EntityPropertyChange>();
            //
            // Now for each record that changed
            //
            foreach (var aChange in changedRecords)
            {
                //
                // Get the type of entity object represented by the type that is 
                // represented by the change.  If that type has properties that need
                // to be written to the UserInfoChangeLogs then process it.
                //
                Type type = aChange.Entity.GetType();

                if (typeof(ILogEntityChanges).IsAssignableFrom(type))
                {
                    //
                    // Retrieve the current values & original values for the entity
                    //                                                         
                    var currentValues = aChange.State != EntityState.Deleted && aChange.CurrentValues != null ? aChange.CurrentValues : null;
                    var originalValues = aChange.State != EntityState.Added && aChange.OriginalValues != null ? aChange.OriginalValues : null;
                    EntityState entityState = aChange.State;
                    //
                    // Create an instance of the type represented by the changedRecord so
                    // we can get the list of properties & types
                    //
                    ILogEntityChanges c = Activator.CreateInstance(type) as ILogEntityChanges;
                    var entityPropertiesToLog = c.PropertiesToLog();
                    //
                    // The final step of set up is to retrieve the id of the entity that has changed.
                    //
                    var entityId = originalValues != null ? originalValues.GetValue<int>(c.KeyPropertyName) : currentValues.GetValue<int>(c.KeyPropertyName);

                    if (entityId == 0)
                    {
                        entityId = tempId--;
                    }
                    foreach (string key in entityPropertiesToLog.Keys)
                    {
                        //
                        // Get the old value & get the new value
                        //
                        var oldValue = GetValueAsString(entityPropertiesToLog[key].PropertType, originalValues, key);
                        var newValue = GetValueAsString(entityPropertiesToLog[key].PropertType, currentValues, key);

                        // Now go & create a record of the change.  Create will return a null if nothing changed.  We have written
                        // an extension method to encapsulate the functionality of not adding a null entry.
                        //
                        var theChange = EntityPropertyChange.Create(key, oldValue, newValue, aChange.State, 
                            aChange.State == EntityState.Deleted || aChange.State == EntityState.Modified ? type.BaseType.Name : type.Name, 
                            entityId, entityPropertiesToLog[key].OverRide);
                        resultsList.AddNonNull(theChange);
                    }
                }
            }
            return resultsList;
        }
        /// <summary>
        /// Converts the value to a string based on the value's type
        /// </summary>
        /// <param name="type">The value's type</param>
        /// <param name="propertyValues">A DbPropertyValues object containing the values in the record</param>
        /// <param name="key">A 'key' indicating which value in the record is to be converted to a string</param>
        /// <returns>The value returned as a string</returns>
        internal string GetValueAsString(Type type, DbPropertyValues propertyValues, string key)
        {
            string value;
            if (type == typeof(int))
            {
                value = propertyValues != null ? propertyValues.GetValue<int>(key).ToString() : string.Empty;
            }
            else if (type == typeof(int?))
            {
                value = propertyValues != null ? propertyValues.GetValue<int?>(key)?.ToString() : string.Empty;
            }
            else if (type == typeof(bool))
            {
                value = propertyValues != null ? propertyValues.GetValue<bool>(key).ToString() : string.Empty;
            }
            else if (type == typeof(bool?))
            {
                value = propertyValues != null ? propertyValues.GetValue<bool?>(key)?.ToString() : string.Empty;
                value = value ?? string.Empty;
            }
            else
            {
                // assumes string type
                value = propertyValues != null ? propertyValues.GetValue<string>(key) : string.Empty;
            }

            return value;
        }
    }
}
