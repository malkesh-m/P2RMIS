using Sra.P2rmis.WebModels;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Views
{
    /// <summary>
    /// Return the result of a requested operation from a service.
    /// </summary>
    public interface IServiceState
    {
        bool IsSuccessful { get; }
        IEnumerable<string> Messages { get; }
        IEnumerable<IEntityInfo> EntityInfo { get; }
    }
        /// <summary>
        /// Return the result of a requested operation from a service.
        /// </summary>
        public class ServiceState: IServiceState
    {
        #region Construction & Setup        
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceState"/> class.
        /// </summary>
        /// <param name="isSuccessful">if set to <c>true</c> [is successful].</param>
        internal ServiceState(bool isSuccessful)
        {
            this.IsSuccessful = isSuccessful;
            this.Messages = new List<string>();
            this.EntityInfo = new List<IEntityInfo>();
        }
        /// <summary>
        /// Constructor when no data values are returned
        /// </summary>
        /// <param name="isSuccessful">Indicates if the service action was successful</param>
        /// <param name="messages">Any error messages</param>
        internal ServiceState(bool isSuccessful, IEnumerable<string> messages)
        {
            this.IsSuccessful = isSuccessful;
            this.Messages = messages;
            this.EntityInfo = new List<IEntityInfo>();
        }
        /// <summary>
        /// Constructor when entity data values are returned
        /// </summary>
        /// <param name="isSuccessful">Indicates if the service action was successful</param>
        /// <param name="messages">Any error messages</param>
        /// <param name="identifier">Entity identifier of newly created object</param>
        internal ServiceState(bool isSuccessful, IEnumerable<string> messages, IEnumerable<IEntityInfo> entityInfo):
            this(isSuccessful, messages)
        {
            this.EntityInfo = entityInfo;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// The state of the service request
        /// </summary>
        public bool IsSuccessful { get; private set; }
        /// <summary>
        /// Rule error messages.
        /// </summary>
        public IEnumerable<string> Messages { get; private set; }
        /// <summary>
        /// Information about the entity manipulated by the CRUD operation
        /// </summary>
        public IEnumerable<IEntityInfo> EntityInfo { get; private set; }
        #endregion
        #region Services
        /// <summary>
        /// Combine multiple ServiceState values into a single ServiceState
        /// </summary>
        /// <param name="results">List of ServiceState results to merge</param>
        /// <returns>ServiceState with values returned</returns>
        internal static ServiceState Merge(IEnumerable<ServiceState> results)
        {
            ServiceState theResult = new ServiceState(true, new List<string>());

            foreach (ServiceState aResult in results)
            {
                theResult.Combine(aResult);
            }
            return theResult;
        }
        /// <summary>
        /// Combines a second ServiceState values
        /// </summary>
        /// <param name="result">ServiceState to combine from</param>
        /// <returns>Combined service state</returns>
        internal ServiceState Combine(ServiceState result)
        {
            this.IsSuccessful &= result.IsSuccessful;
            //
            // Combine the target's messages with ours
            //
            List<string> messageList = new List<string>();
            messageList.AddRange(this.Messages);
            messageList.AddRange(result.Messages);
            this.Messages = messageList;
            //
            // Combine the target's EntityInfo with ours
            //
            List<IEntityInfo> entityInfoList = new List<IEntityInfo>();
            entityInfoList.AddRange(this.EntityInfo);
            entityInfoList.AddRange(result.EntityInfo);
            this.EntityInfo = entityInfoList;

            return this;
        }
        #endregion
    }
}
