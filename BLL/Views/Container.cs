using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.CrossCuttingServices.MessageServices;

namespace Sra.P2rmis.Bll.Views
{
    /// <summary>
    /// Container to hold service results
    /// </summary>
    /// <typeparam name="T">Model type contents</typeparam>
    public class Container<T>
    {
        #region constructor
       /// <summary>
        /// Default constructor defines default state.
        /// </summary>
        public Container() 
        {
            this.ModelList = new List<T>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Returns the first model.  Particularly useful when Container
        /// is known to only contain a single model.
        /// </summary>
        public T Model { get { return ModelList.FirstOrDefault(); } }
        /// <summary>
        /// Model list of WebModel results
        /// </summary>
        private IEnumerable<T> _modelList;
        public IEnumerable<T> ModelList
        {
            get { return _modelList; }
            set
            {
                if (value != null)
                {
                    _modelList = value;
                }
            }
        }
        /// <summary>
        /// Container to hold messages & message store.
        /// </summary>
        private List<ServiceMessage> _messageList { get; set; } = new List<ServiceMessage>(1);
        public IEnumerable<ServiceMessage> Messages { get { return _messageList.AsReadOnly(); } }
        /// <summary>
        /// Indicates if there are messages.
        /// </summary>
        public bool HasMessage {  get { return this._messageList.Count != 0; } }
        /// <summary>
        /// Indicates the number of messages
        /// </summary>
        public int MessageCount { get { return this._messageList.Count; } }
        #endregion
        #region Methods
        /// <summary>
        /// Set the container's model from a result model.
        /// </summary>
        /// <param name="result"></param>
        public void SetModelList(ResultModel<T> result)
        {
            if (result != null)
            {
                this.ModelList = result.ModelList.Distinct();
            }
        }
        /// <summary>
        /// Add a message to the list
        /// </summary>
        /// <param name="message">Message to add</param>
        internal void AddMessage(ServiceMessage message)
        {
            this._messageList.Add(message);
        }
        #endregion
    }
}
