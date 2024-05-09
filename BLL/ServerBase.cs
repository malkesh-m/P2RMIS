using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// Common services provided by all servers.
    /// </summary>
    public partial class ServerBase: IServerBase
    {
        #region Properties
        /// <summary>
        /// Indicates if the object has been disposed but not garbage collected.
        /// </summary>
        protected bool _disposed;
        /// <summary>
        /// Instantiation of O/RM persistence pattern: the Unit Of Work pattern.
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; set; }
        #endregion
        #region Setup & Disposal
        /// <summary>
        /// Dispose of the service.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Dispose of the service
        /// </summary>
        /// <param name="disposing">Indicates if the object should be disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            ///
            /// if the object has not been disposed & we should dispose the object
            /// 
            if ((!this._disposed) && (disposing))
            {
                DisposeServerUnmanagedResources();
                if (UnitOfWork != null)
                {
                    ((IDisposable)UnitOfWork).Dispose();
                    this._disposed = true;
                }
            }
        }
        /// <summary>
        /// Dispose of any unmanaged resources.  Derived classes should override this method
        /// to dispose of specific unmanaged resources.
        /// </summary>
        protected virtual void DisposeServerUnmanagedResources()
        {

        }
        /// <summary>
        /// Initialize the servers database access.
        /// </summary>
        /// <param name="context">Wrapper object providing access to a UnitOfWork when transactional support is necessary between servers</param>
        public virtual void SetUoW(ServiceContext context)
        {
            this.UnitOfWork = context.UnitOfWork;
        }
        #endregion
        #region Services
        /// <summary>
        /// Checks whether the user can access their panel assignment due to having incomplete assignments
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <returns>True if access is permitted; otherwise false;</returns>
        public bool NewCanUserAccessPanelAssignment(int panelUserAssignmentId)
        {
            var panelUserAssignment = UnitOfWork.PanelUserAssignmentRepository.GetByID(panelUserAssignmentId);
            return !panelUserAssignment.IsRegistrationIncomplete();
        }
        /// <summary>
        /// Checks whether the user can access their panel assignment due to having incomplete assignments
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sessionPanelId"></param>
        /// <returns>True if access is permitted; otherwise false;</returns>
        public bool NewCanUserAccessPanelAssignment(int userId, int sessionPanelId)
        {
            var panelUserAssignment = UnitOfWork.PanelUserAssignmentRepository.Get(x => x.SessionPanelId == sessionPanelId && x.UserId == userId).DefaultIfEmpty(new PanelUserAssignment()).First();
            return !panelUserAssignment.IsRegistrationIncomplete();
        }
        /// <summary>
        /// Clients the identifier.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        public int? ClientId(int sessionPanelId)
        {
            var sessionPanel = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
            return sessionPanel != null ? (int?)sessionPanel.ClientId() : null;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Validates the specified method name.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters.</param>
        /// <remarks>To be implemented.</remarks>
        protected void Validate(string methodName, params object[] parameters)
        {
            var ps = parameters.ToList();
            for (var i = 0; i < ps.Count; i += 2)
            {
                if (ps[i] is int)
                {
                    int p = (int)ps[i];
                    ValidateInteger(p, methodName, (string)ps[i + 1]);
                }
                else if (parameters[i] is string)
                {
                    string p = (string)ps[i];
                    ValidateString(p, methodName, (string)ps[i + 1]);
                }
            }
        }
        /// <summary>
        /// Generic validation for an integer value.
        /// </summary>
        /// <param name="value">Parameter value</param>
        /// <param name="caller">Calling method name</param>
        /// <param name="parameter">Parameter name</param>
        protected void ValidateInteger(int value, string caller, string parameter)
        {
            ValidateInt(value, caller, parameter);
        }
        /// <summary>
        /// Generic validation for a integer value.  Static for access for IES reused controllers
        /// </summary>
        /// <param name="value">Parameter value</param>
        /// <param name="caller">Calling method name</param>
        /// <param name="parameter">Parameter name</param>
        internal static void ValidateInt(int value, string caller, string parameter)
        {
            if (value <= 0)
            {
                string message = string.Format("{0} detected an invalid parameter: {1} was {2}", caller, parameter, value);
                throw new ArgumentException(message);
            }            
        }
        #region Collection Validations
        // Non primary collection types should use the generic ValidateCollection and 
        //   implement, for that non primary type, the ToSaveObject interface
        // Primary collection types will need to create a ValidateCollection, 
        //   if one for that primary type does not already exist.
        // - see below for those that exist. 


        /// <summary>
        /// Generic validation for a collection value.
        /// </summary>
        /// <typeparam name="T">Type to validate.  Type must interface ToSaveObject</typeparam>
        /// <param name="collection">Collection of SetOrderOfReviewToSave objects</param>
        protected void ValidateCollection<T>(ICollection<T> collection, string caller, string parameter) where T : ToSaveObject
        {
            if (collection == null)
            {
                string message = string.Format("{0} detected an invalid parameter: {1} was {2}", caller, parameter, "null");
                throw new ArgumentException(message);
            }
            //
            // Validate each item
            //
            foreach (var item in collection)
            {
                item.Validate();
            }
        }
        /// <summary>
        /// Validate a collection of integers
        /// </summary>
        /// <param name="collection">integer collection</param>
        /// <param name="serverMethod">Service & method name in the format Service.Method</param>
        /// <param name="parameterName">Parameter name</param>
        /// <exception cref="ArgumentException">Thrown if parameter is invalid (<0)</exception>
        protected void ValidateCollection(ICollection<int> collection, string serverMethod, string parameter)
        {
            if (collection == null)
            {
                string message = string.Format("{0} detected an invalid parameter: {1} was {2}", serverMethod, parameter, "null");
                throw new ArgumentException(message);
            }
            //
            // Validate each item
            //
            foreach (var item in collection)
            {
                ValidateParameter(item, serverMethod, parameter + " element");
            }
        }
        /// <summary>
        /// Validate a collection of strings
        /// </summary>
        /// <param name="collection">integer collection</param>
        /// <param name="serverMethod">Service & method name in the format Service.Method</param>
        /// <param name="parameterName">Parameter name</param>
        /// <exception cref="ArgumentException">Thrown if parameter is invalid (<0)</exception>
        protected void ValidateCollection(ICollection<string> collection, string serverMethod, string parameter)
        {
            if (collection == null)
            {
                string message = string.Format("{0} detected an invalid parameter: {1} was {2}", serverMethod, parameter, "null");
                throw new ArgumentException(message);
            }
            //
            // Validate each item
            //
            foreach (var item in collection)
            {
                ValidateParameter(item, serverMethod, parameter + " element");
            }
        }
        /// <summary>
        /// Validates a collection exists.
        /// </summary>
        /// <typeparam name="T">Type to validate.  Type must interface ToSaveObject</typeparam>
        /// <param name="collection">Collection of SetOrderOfReviewToSave objects</param>
        /// <param name="serverMethod">Service & method name in the format Service.Method</param>
        /// <param name="parameterName">Parameter name</param>
        protected void ValidateCollectionExists<T>(ICollection<T> collection, string serverMethod, string parameter)
        {
            if (collection == null)
            {
                string message = string.Format("{0} detected an invalid parameter: {1} was {2}", serverMethod, parameter, "null");
                throw new ArgumentException(message);
            }
        }
        /// <summary>
        /// Validates a collection exists.
        /// </summary>
        /// <typeparam name="T">Type to validate.  Type must interface ToSaveObject</typeparam>
        /// <param name="collection">Collection of SetOrderOfReviewToSave objects</param>
        /// <param name="serverMethod">Service & method name in the format Service.Method</param>
        /// <param name="parameterName">Parameter name</param>
        protected void ValidateModelExists<T>(T theObject, string serverMethod, string parameter)
        {
            if (theObject == null)
            {
                string message = string.Format("{0} detected an invalid parameter: {1} was {2}", serverMethod, parameter, "null");
                throw new ArgumentException(message);
            }
        }
        #endregion
        /// <summary>
        /// Generic validation for a nullable integer.
        /// </summary>
        /// <param name="value">Parameter value</param>
        /// <param name="caller">Calling method name</param>
        /// <param name="parameter">Parameter name</param>
        protected void ValidateNullableInteger(int? value, string caller, string parameter)
        {
            if (value.HasValue)
            {
                ValidateInt(value.Value, caller, parameter);
            }
        }
        /// <summary>
        /// Generic validation for a string value. 
        /// </summary>
        /// <param name="value">Parameter value</param>
        /// <param name="caller">Calling method name</param>
        /// <param name="parameter">Parameter name</param>
        protected void ValidateString(string value, string caller, string parameter)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                string message = string.Format("{0} detected an invalid parameter: {1} was [{2}]", caller, parameter, value);
                throw new ArgumentException(message);
            }
        }
        /// <summary>
        /// Generic validation for a string value. 
        /// </summary>
        /// <param name="value">Parameter value</param>
        /// <param name="caller">Calling method name</param>
        /// <param name="parameter">Parameter name</param>
        protected static void ValString(string value, string caller, string parameter)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                string message = string.Format("{0} detected an invalid parameter: {1} was [{2}]", caller, parameter, value);
                throw new ArgumentException(message);
            }
        }
        /// <summary>
        /// Generic validation for a string value. 
        /// </summary>
        /// <param name="value">Parameter value</param>
        /// <param name="caller">Calling method name</param>
        /// <param name="parameter">Parameter name</param>
        protected void ValidateOptionalString(string value, string caller, string parameter)
        {
            if (value != null)
            {
                ValidateString(value, caller, parameter);
            }
        }
        /// <summary>
        /// Validates a single integer parameter. 
        /// Call this if service method only has a single integer parameter that needs validation.
        /// </summary>
        /// <param name="parameter">Parameter value</param>
        /// <param name="serverMethod">Service & method name in the format Service.Method</param>
        /// <param name="parameterName">Parameter name</param>
        /// <exception cref="ArgumentException">Thrown if parameter is invalid (<0)</exception>
        protected void ValidateParameter(int parameter, string serverMethod, string parameterName)
        {
            this.ValidateInteger(parameter, serverMethod, parameterName);
        }
        /// <summary>
        /// Validates a single integer parameter. 
        /// Call this if service method only has a single integer parameter that needs validation.
        /// </summary>
        /// <param name="parameter">Parameter value</param>
        /// <param name="serverMethod">Service & method name in the format Service.Method</param>
        /// <param name="parameterName">Parameter name</param>
        /// <exception cref="ArgumentException">Thrown if parameter is invalid (<0)</exception>
        protected void ValidateParameter(string parameter, string serverMethod, string parameterName)
        {
            this.ValidateString(parameter, serverMethod, parameterName);
        }
        /// <summary>
        /// Validates a DateTime value.
        /// </summary>
        /// <param name="value">Parameter value</param>
        /// <param name="caller">Calling method name</param>
        /// <param name="parameter">Parameter name</param>
        protected void ValidateDateTime(DateTime value, string caller, string parameter)
        {
            if (value == null)
            {
                string message = string.Format("{0} detected an invalid parameter: {1} was [{2}]", caller, parameter, "null");
                throw new ArgumentException(message);
            }
        }
        /// <summary>
        /// Determines if a date is within a date range.  Within is defined as equal to the lower bound
        /// or upper bound.  If either the upper or lower bound dates are null then false is returned.
        /// </summary>
        /// <param name="lowerBound">Lower bound of DateTime range</param>
        /// <param name="upperBound">Upper bound of DateTime range</param>
        /// <param name="dateTimeToTest">DateTime to test</param>
        /// <returns>True if a date is within a date range; false otherwise</returns>
        public static bool WithinDateRange(DateTime? lowerBound, DateTime? upperBound, DateTime dateTimeToTest)
        {
            return ((lowerBound.HasValue) && (upperBound.HasValue) &&
                    (DateTime.Compare(lowerBound.Value, dateTimeToTest) <= 0) && (DateTime.Compare(dateTimeToTest, upperBound.Value) <= 0));
        }
        /// <summary>
        /// Helper method to construct full method name
        /// </summary>
        /// <param name="className">Name of class</param>
        /// <param name="method">Name of method</param>
        /// <returns>Full method name</returns>
        public static string FullName(string className, string method)
        {
            return $"{className}.{method}";
        }
        /// <summary>
        /// Generic validation for a nullable integer.
        /// </summary>
        /// <param name="value">Parameter value</param>
        /// <param name="caller">Calling method name</param>
        /// <param name="parameter">Parameter name</param>
        protected void ValidateNullableIntegerMustHaveValue(int? value, string caller, string parameter)
        {
            if (value.HasValue)
            {
                ValidateInt(value.Value, caller, parameter);
            }
            else
            {
                throw new ArgumentException(MakeErrorMessage(caller, parameter, "null"));
            }
        }
        /// <summary>
        /// Common error message
        /// </summary>
        /// <param name="caller">Calling method name</param>
        /// <param name="parameter">Parameter name</param>
        /// <param name="value">Parameter value</param>
        /// <returns>Error message</returns>
        private string MakeErrorMessage(string caller, string parameter, string value)
        {
            return $"{caller} detected an invalid parameter: {parameter} was [{value}]";
        }
        #endregion
    }
}
