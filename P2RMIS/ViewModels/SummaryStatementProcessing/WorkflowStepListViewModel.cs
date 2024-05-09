using Sra.P2rmis.WebModels.Lists;
using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices;
using System.Linq;

namespace Sra.P2rmis.Web.ViewModels.SummaryStatementProcessing
{
    /// <summary>
    /// View model for the WorkflowStep Selection modal
    /// </summary>
    public class WorkflowStepListViewModel
    {
        #region Constants
        /// <summary>
        /// Default ApplicationWorkflowStep if none located
        /// </summary>
        private const int DefaultApplicationWorkflowStep = 0;
        #endregion
        #region Constructor & Setup
        /// <summary>
        /// Default constructor.  Model constructed is sufficient to 
        /// </summary>
        internal WorkflowStepListViewModel()
        {
            this.WorkflowStepList = new List<IListEntry>();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="workflowStepList">List of IListEntrys representing the ApplicationWorkflowStep in the ApplicationWorkflow</param>
        /// <param name="workflowId">ApplicationWorkflow entity identifier</param>
        /// <param name="workflowStepId">ApplicationWorkflowStep entity identifier</param>
        /// <param name="isClient">Indicates if the  user is a client</param>
        public WorkflowStepListViewModel(List<IListEntry> workflowStepList, int workflowId, int workflowStepId, bool isClient)
        {
            this.WorkflowId = workflowId;
            var nextStep = NextStep(workflowStepList, workflowStepId);
            this.ApplicationWorkflowStepEntityId = nextStep.Index;
            this.WorkflowStep = nextStep.Value;
            if (isClient) { 
                workflowStepList.Clear();
                workflowStepList.Add(nextStep);
            }
            this.WorkflowStepList = workflowStepList;
            this.IsClient = isClient;
        }
        /// <summary>
        /// Format the list entries.
        /// </summary>
        /// <param name="workflowStepList">Ordered list of ApplicationWorkflowSteps</param>
        /// <returns>Ordered list of formatted ApplicationWorkflowSteps</returns>
        internal static List<IListEntry> FormatList(List<IListEntry> workflowStepList)
        {
            List<IListEntry> result = new List<IListEntry>();
            int x = 1;

            workflowStepList.ForEach(item => result.Add(new ListEntry(item.Index, ViewHelpers.WorkflowStepName(x++, item.Value))));
            return result;
        }
        /// <summary>
        /// Locates the next ApplicationWorkflowStep in the list of ApplicationWorkflowSteps
        /// </summary>
        /// <param name="workflowStepList">WorkflowStep list</param>
        /// <param name="workflowStepId">ApplicationWorkflowStep entity identifier</param>
        /// <returns>ApplicationWorkflowStep entity Identifier of the next ApplicationWorkflowStep; 0 if none located</returns>
        private IListEntry NextStep(List<IListEntry> workflowStepList, int workflowStepId)
        {
            IListEntry entry = workflowStepList.SkipWhile(x => x.Index != workflowStepId).Skip(1).FirstOrDefault();
            return entry;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Workflow entity identifier
        /// </summary>
        public int WorkflowId { get; private set; }
        /// <summary>
        /// List of workflow steps.
        /// </summary>
        public List<IListEntry> WorkflowStepList { get; private set; }
        /// <summary>
        /// Entity identifier of the selected workflow step.
        /// </summary>
        public int ApplicationWorkflowStepEntityId { get; set; }
        /// <summary>
        /// Gets the workflow step.
        /// </summary>
        /// <value>
        /// The workflow step.
        /// </value>
        public string WorkflowStep { get; private set; }
        /// <summary>
        /// Indicates if the user is a client.
        /// </summary>
        public bool IsClient { get; private set; }
        #endregion
    }
}