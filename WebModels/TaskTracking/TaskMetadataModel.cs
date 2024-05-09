using System;
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.TaskTracking
{
    public interface ITaskMetadataModel
    {
        /// <summary>
        /// Gets or sets the component list.
        /// </summary>
        /// <value>
        /// The component list.
        /// </value>
        IList<Tuple<int, string>> ComponentList { get; set; }

        /// <summary>
        /// Gets or sets the task type list.
        /// </summary>
        /// <value>
        /// The task type list.
        /// </value>
        IList<Tuple<int, string>> TaskTypeList { get; set; }
    }

    /// <summary>
    /// Container of ticket metadata used to supply valid values to task tracking system
    /// </summary>
    public class TaskMetadataModel : ITaskMetadataModel
    {
        public TaskMetadataModel(IList<Tuple<int, string>> componentList, IList<Tuple<int, string>> taskTypeList)
        {
            ComponentList = componentList;
            TaskTypeList = taskTypeList;
        }
        /// <summary>
        /// Gets or sets the component list.
        /// </summary>
        /// <value>
        /// The component list.
        /// </value>
        public IList<Tuple<int, string>> ComponentList { get; set; }
        /// <summary>
        /// Gets or sets the task type list.
        /// </summary>
        /// <value>
        /// The task type list.
        /// </value>
        public IList<Tuple<int, string>> TaskTypeList { get; set; }
    }
    
    
}
