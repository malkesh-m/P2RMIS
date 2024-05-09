using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.Repository.Setup;
using Sra.P2rmis.CrossCuttingServices;
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Helper methods for EntityFramework objects.  
    /// </summary>
    public partial class Helper
    {
        /// <summary>
        /// Updates the Modified fields.  Hated to have to write the same two lines of code.
        /// </summary>
        /// <remarks>
        /// See remarks in IDateFiled file.
        /// </remarks>
        /// <param name="entityObject">Entity object</param>
        /// <param name="userId">User id</param>
        public static void UpdateModifiedFields(IDateFields entityObject, int userId)
        {
            entityObject.ModifiedBy = userId;
            entityObject.ModifiedDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Updates the Created fields. Hated to have to write the same two lines of code.
        /// </summary>
        /// <remarks>
        /// See remarks in IDateFiled file.
        /// </remarks>
        /// <param name="entityObject">Entity object</param>
        /// <param name="userId">User id</param>
        public static void UpdateCreatedFields(IDateFields entityObject, int userId)
        {
            entityObject.CreatedBy = userId;
            entityObject.CreatedDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Updates the Modified fields.  Hated to have to write the same two lines of code.
        /// </summary>
        /// <param name="entityObject">Entity object</param>
        /// <param name="userId">User id</param>
        public static void UpdateModifiedFields(IStandardDateFields entityObject, int userId)
        {
            entityObject.ModifiedBy = userId;
            entityObject.ModifiedDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Updates the Created fields. Hated to have to write the same two lines of code.
        /// </summary>
        /// <param name="entityObject">Entity object</param>
        /// <param name="userId">User id</param>
        public static void UpdateCreatedFields(IStandardDateFields entityObject, int userId)
        {
            entityObject.CreatedBy = userId;
            entityObject.CreatedDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Updates the Deleted fields.
        /// </summary>
        /// <param name="entityObject">Entity object</param>
        /// <param name="userId">User id</param>        
        public static void UpdateDeletedFields(IStandardDateFields entityObject, int userId)
        {
            entityObject.DeletedBy = userId;
            entityObject.DeletedDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Updates the Modified fields.  Hated to have to write the same two lines of code.
        /// </summary>
        /// <param name="entityObject">Entity object</param>
        /// <param name="userId">User id</param>
        public static void UpdateModifiedFields(IAlternateStandardDateFields entityObject, int userId)
        {
            entityObject.ModifiedBy = userId;
            entityObject.ModifiedDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Updates the Created fields. Hated to have to write the same two lines of code.
        /// </summary>
        /// <param name="entityObject">Entity object</param>
        /// <param name="userId">User id</param>
        public static void UpdateCreatedFields(IAlternateStandardDateFields entityObject, int userId)
        {
            entityObject.CreatedBy = userId;
            entityObject.CreatedDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Updates the Modified fields if the entity has been modified.  This version is for the use on most 
        /// entity objects.
        /// </summary>
        /// <typeparam name="T">Entity class</typeparam>
        /// <param name="unitOfWork">The UnitOfWork for the database</param>
        /// <param name="entityObject">Entity object</param>
        /// <param name="userId">User id</param>
        public static void UpdateModifiedFields<T>(IUnitOfWork unitOfWork, T entity, int userId) where T : class, IStandardDateFields
        {
            //
            // If the entity was modified, then update the 
            // modified fields.
            //
            if (unitOfWork.HasEntityBeenModified(entity))
            {
                Helper.UpdateModifiedFields(entity, userId);
            }
        }
        /// <summary>
        /// Updates the Modified fields if the entity has been modified.  This version is for the User object and is a 
        /// one off.  The CreatedDate field's type could not be changed from System.Datetime to Nullable<System.Datetime>.
        /// </summary>
        /// <typeparam name="T">Entity class</typeparam>
        /// <param name="unitOfWork">The UnitOfWork for the database</param>
        /// <param name="entityObject">Entity object</param>
        /// <param name="userId">User id</param>
        public static void UpdateUserModifiedFields(IUnitOfWork unitOfWork, User entity, int userId) 
        {
            //
            // If the entity was modified, then update the 
            // modified fields.
            //
            if (unitOfWork.HasEntityBeenModified(entity))
            {
                Helper.UpdateUserModifiedFields(entity, userId);
            }
        }
        /// <summary>
        /// Updates the Modified fields if the entity has been modified.  This version is for the User object and is a 
        /// one off.  The CreatedDate field's type could not be changed from System.Datetime to Nullable<System.Datetime>.
        /// </summary>
        /// <typeparam name="T">Entity class</typeparam>
        /// <param name="unitOfWork">The UnitOfWork for the database</param>
        /// <param name="entityObject">Entity object</param>
        /// <param name="userId">User id</param>
        public static void UpdateUserCreateedFields(User entity, int userId)
        {
            entity.CreatedBy = userId;
            entity.CreatedDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Updates the Create fields i This version is for the User object and is a 
        /// one off.  The CreatedDate field's type could not be changed from System.Datetime to Nullable<System.Datetime>.
        /// </summary>
        /// <param name="entityObject">Entity object</param>
        /// <param name="userId">User id</param>
        public static void UpdateUserModifiedFields(User entity, int userId)
        {
            entity.ModifiedBy = userId;
            entity.ModifiedDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Tests if a property is the correct length and updates the error list if not.
        /// </summary>
        /// <param name="size">Field size</param>
        /// <param name="valueProperty">Property to invoke</param>
        /// <param name="thisError">Error message to add to list if invalid</param>
        /// <returns><True if all length is less than maximum; false otherwise/returns>
        public static bool CheckLength(int size, string value, SaveProfileStatus thisError, IList<SaveProfileStatus> errors)
        {
            bool result = true;
            if ((value != null) && (value.Length > size))
            {
                errors.Add(thisError);
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Tests if a required property is provided and updates the error list if not.
        /// </summary>
        /// <param name="value">Value to test</param>
        /// <param name="thisError">Error message to add to list if invalid</param>
        /// <returns>True if not null; false otherwise</returns>
        public static bool CheckRequired(string value, SaveProfileStatus thisError, IList<SaveProfileStatus> errors)
        {
            bool result = true;
            if (value == null)
            {
                errors.Add(thisError);
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Tests if a positive integer property is provided and updates the error list if not.
        /// </summary>
        /// <param name="value">Value to test</param>
        /// <param name="thisError">Error message to add to list if invalid</param>
        /// <returns><True if not null; false otherwise/returns>
        public static bool CheckPositive(int value, SaveProfileStatus thisError, IList<SaveProfileStatus> errors)
        {
            bool result = true;
            if (value <= 0)
            {
                errors.Add(thisError);
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Tests is index is within range for index values.
        /// </summary>
        /// <param name="target">target index</param>
        /// <param name="mininum">Minimum index value</param>
        /// <param name="maximum">Maximum value</param>
        /// <returns>True if value is within range (or null); false otherwise</returns>
        public static bool IsValidIndex(int? target, int mininum, int maximum, SaveProfileStatus thisError, IList<SaveProfileStatus> errors)
        {
            return ((target == null) || IsValidIndex(target.Value, mininum, maximum, thisError, errors));
        }
        /// <summary>
        /// Tests is index is within range for index values.
        /// </summary>
        /// <param name="target">target index</param>
        /// <param name="mininum">Minimum index value</param>
        /// <param name="maximum">Maximum value</param>
        /// <returns>True if value is within range; false otherwise</returns>
        public static bool IsValidIndex(int target, int mininum, int maximum, SaveProfileStatus thisError, IList<SaveProfileStatus> errors)
        {
            bool result = true;

            if ((target < mininum) || (target > maximum))
            {
                result = false;
                errors.Add(thisError);
            }
            return result;
        }
        /// <summary>
        /// Standard way to test if an entity is being added.
        /// </summary>
        /// <param name="targetId"></param>
        /// <returns></returns>
        public static bool IsAdd(int? targetId)
        {
            return (targetId == null || targetId == 0) ;
        }
        /// <summary>
        /// Standard way to test if an entity is being added.
        /// </summary>
        /// <param name="targetId"></param>
        /// <returns></returns>
        public static bool IsAdd(int? targetId, bool isDelete)
        {
            return (!isDelete) && IsAdd(targetId);
        }
        public static bool IsDelete(int? targetId)
        {
            return (!IsAdd(targetId));
        }
        public static bool HasData(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
        /// <summary>
        /// Records error if a primary phone count is valid.
        /// </summary>
        /// <param name="value">Value indicating that the count is or is not valid</param>
        /// <param name="thisError">Error message to add to list if invalid</param>
        /// <returns><True if not null; false otherwise/returns>
        public static bool IsPrimaryPhoneCountValid(bool value, SaveProfileStatus thisError, IList<SaveProfileStatus> errors)
        {
            bool result = true;

            if (!value)
            {
                result = false;
                errors.Add(thisError);
            }
            return result;
        }

        /// <summary>
        /// Helper method to update time fields.  The created time fields are updated if called for an add.
        /// </summary>
        /// <param name="entityObject">Entity object</param>
        /// <param name="entityId">Id of entity object</param>
        /// <param name="userId">User id</param>
        /// <remarks>needs unit testing</remarks>
        public static void UpdateTimeFields(IStandardDateFields entityObject, int entityId, int userId)
        {
            if (IsAdd(entityId))
            {
                Helper.UpdateCreatedFields(entityObject, userId);
            }
            Helper.UpdateModifiedFields(entityObject, userId);
        }
        /// <summary>
        /// Create encoded hash value based on supplied plain value
        /// (produces same hashed value as obsolete 'HashPasswordForStoringInConfigFile')
        /// </summary>
        /// <param name="password">The plain text value to hash</param>
        /// <returns>The encoded hashed value</returns>
        public static string CreateEncodedHash(string value)
        {
            SHA1 algorithm = SHA1.Create();
            byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
            string sh1 = "";
            for (int i = 0; i < data.Length; i++)
            {
                sh1 += data[i].ToString("x2").ToUpperInvariant();
            }
            return sh1;
        }
        /// <summary>
        /// Converts the restricted access value to a human readable value
        /// </summary>
        /// <returns>Partial if the RestrictedAssignedFlag is true; Full otherwise</returns>
        public static string Level(bool restrictedAssignedFlag)
        {
            return (restrictedAssignedFlag) ? "Partial" : "Full";
        }
    }
}
