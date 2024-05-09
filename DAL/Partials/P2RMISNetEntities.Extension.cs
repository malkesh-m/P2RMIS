using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
 
{
    public partial class P2RMISNETEntities
    {
        private const string DeletedByName = "DeletedBy";
        private const string DeletedDateName = "DeletedName";
        /// <summary>
        /// Saves changes to an entity object. If Deleted performs a soft delete instead.
        /// </summary>
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Deleted))
                SoftDelete(entry);
            return base.SaveChanges();
        }
        /// <summary>
        /// Re-writes the SQL Query for Delete to update DeletedFlag instead Deleting the row from the database.
        /// Necessary to use SQL since the flag is not present in the entity model.
        /// </summary>
        private void SoftDelete(DbEntityEntry entry)
        {
            const string deletedByName = "DeletedBy";
            const string deletedDateName = "DeletedDate";
            Type entryEntityType = entry.Entity.GetType();
            entry.State = EntityState.Modified;

            string tableName = GetTableName(entryEntityType);
            string primaryKeyName = GetPrimaryKeyName(entryEntityType);
            string sql =
                string.Format(
                    "UPDATE {0} SET DeletedFlag = 1, DeletedBy = @deletedBy, DeletedDate = @deletedDate WHERE {1} = @id",
                    tableName, primaryKeyName);
            Database.ExecuteSqlCommand(
                sql,
                new SqlParameter("@id", entry.OriginalValues[primaryKeyName]),
                new SqlParameter("@deletedBy", entry.CurrentValues[deletedByName]),
                new SqlParameter("@deletedDate", entry.CurrentValues[deletedDateName]));
            // prevent hard delete            
            entry.State = EntityState.Detached;
        }
        private static Dictionary<Type, EntitySetBase> _mappingCache =
            new Dictionary<Type, EntitySetBase>();
        /// <summary>
        /// Gets the table name from the entity set to write the SQL Query
        /// </summary>
        private string GetTableName(Type type)
        {
            EntitySetBase es = GetEntitySet(type);
            return string.Format("[{0}].[{1}]",
                es.MetadataProperties["Schema"].Value,
                es.MetadataProperties["Name"].Value);
        }
        /// <summary>
        /// Gets the primary key name from entity set to uniquely identify the record to delete.
        /// </summary>
        private string GetPrimaryKeyName(Type type)
        {
            EntitySetBase es = GetEntitySet(type);
            return es.ElementType.KeyMembers[0].Name;
        }
        /// <summary>
        /// Retrieves the entity set from object context to expose information about the database schema.
        /// </summary>
        private EntitySetBase GetEntitySet(Type type)
        {
            if (!_mappingCache.ContainsKey(type))
            {
                string typeName = ObjectContext.GetObjectType(type).Name;
                EntitySetBase es = GetEntitySetBase(typeName);
                //
                // There are some cases where the EntitySet name is not the type name.  In these
                // cases the entity supplies the EntitySet name to search for.
                //
                if ((es == null) & (typeof(ISpecifyEntitySetName).IsAssignableFrom(type)))
                {
                    ISpecifyEntitySetName a = Activator.CreateInstance(type) as ISpecifyEntitySetName;
                    es = GetEntitySetBase(a.EntitySetName);
                }

                if (es == null)
                {
                    throw new ArgumentException("Entity type not found in GetTableName", typeName);
                }
                _mappingCache.Add(type, es);
            }
            return _mappingCache[type];
        }
        /// <summary>
        /// Retrieves the EntitySetBase for the specified TypeName from the Metadata Workspace.
        /// </summary>
        /// <param name="typeName">Type's name</param>
        /// <returns>EntitySetBase; null if not located</returns>
        private EntitySetBase GetEntitySetBase(string typeName)
        {
            ObjectContext octx = ((IObjectContextAdapter)this).ObjectContext;
            EntitySetBase es = octx.MetadataWorkspace
                .GetItemCollection(DataSpace.SSpace)
                .GetItems<EntityContainer>()
                .SelectMany(c => c.BaseEntitySets
                    .Where(e => e.Name == typeName))
                .FirstOrDefault();
            return es;

        }
    }
}