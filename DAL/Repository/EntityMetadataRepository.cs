using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Sra.P2rmis.Dal.Repository
{
    public class EntityMetadataRepository
    {
        #region Services Provided
        /// <summary>
        /// Gets the MaxLength of a specified entity property
        /// </summary>
        /// <typeparam name="T">Parent entity</typeparam>
        /// <param name="column"></param>
        /// <returns>Max length of entity column</returns>
        public static int? GetMaxLength<T>(Expression<Func<T, string>> column)
        {
            int? result = null;
            using (var context = new P2RMISNETEntities())
            {
                var entType = typeof(T);
                var columnName = ((MemberExpression)column.Body).Member.Name;

                var objectContext = ((IObjectContextAdapter)context).ObjectContext;
                var test = objectContext.MetadataWorkspace.GetItems(DataSpace.CSpace);

                if (test == null)
                    return null;

                var q = test
                    .Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                    .SelectMany(meta => ((EntityType)meta).Properties
                    .Where(p => p.Name == columnName && p.TypeUsage.EdmType.Name == "String"));

                var queryResult = q.Where(p =>
                {
                    var match = p.DeclaringType.Name == entType.Name;
                    if (!match)
                        match = entType.Name == p.DeclaringType.Name;

                    return match;

                })
                    .Select(sel => sel.TypeUsage.Facets["MaxLength"].Value)
                    .ToList();

                if (queryResult.Any())
                    result = Convert.ToInt32(queryResult.First());

                return result;
            }
        }
        /*
        /// <summary>
        /// Gets the default label of a specified entity property from long description
        /// </summary>
        /// <typeparam name="T">Parent entity</typeparam>
        /// <param name="column"></param>
        /// <returns>Label for entity column</returns>
        /// <remarks>
        /// This is not used because I fear we could inadvertently delete the long description
        /// when updating the entity model from the database. If we ever create a custom tool or
        /// switch from using edmx to code first and power tools it might be useful
        /// </remarks>
        
        public static string GetFieldLabel<T>(Expression<Func<T, string>> column)
        {
            string result = String.Empty;
            using (var context = new P2RMISNETEntities())
            {
                var entType = typeof(T);
                var columnName = ((MemberExpression)column.Body).Member.Name;

                var objectContext = ((IObjectContextAdapter)context).ObjectContext;
                var test = objectContext.MetadataWorkspace.GetItems(DataSpace.CSpace);

                if (test == null)
                    return String.Empty;

                var q = test
                    .Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                    .SelectMany(meta => ((EntityType)meta).Properties
                    .Where(p => p.Name == columnName));

                var queryResult = q.Where(p =>
                {
                    var match = p.DeclaringType.Name == entType.Name;
                    if (!match)
                        match = entType.Name == p.DeclaringType.Name;

                    return match;

                })
                    .Select(sel => sel.Documentation.LongDescription)
                    .ToList();
                return result;
            }
        }*/

        #endregion
    }
}
