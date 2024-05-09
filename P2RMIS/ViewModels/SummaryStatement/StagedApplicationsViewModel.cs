using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Web.ViewModels.SummaryStatement
{
    public class StagedApplicationsViewModel
    {
        public StagedApplicationsViewModel()
        {
            Applications = new List<StagedApplicationViewModel>();
            WorkflowOptions = new List<KeyValuePair<int, string>>();
            this.RefreshTime = String.Empty;
        }

        public StagedApplicationsViewModel(List<ISummaryStatementApplicationModel> applications,
                List<IMenuItem> workflowOptions)
        {
            Applications = applications.ToList().ConvertAll(x => new StagedApplicationViewModel(x));
            WorkflowOptions = workflowOptions.ConvertAll(x => new KeyValuePair<int, string>(x.Id, x.Name));
        }
        /// <summary>
        /// Gets or sets the applications.
        /// </summary>
        /// <value>
        /// The applications.
        /// </value>
        public List<StagedApplicationViewModel> Applications { get; set; }
        /// <summary>
        /// Gets or sets the workflow options.
        /// </summary>
        /// <value>
        /// The workflow options.
        /// </value>
        public List<KeyValuePair<int, string>> WorkflowOptions { get; set; }
        /// <summary>
        /// list of priority options available
        /// </summary>
        /// Only for drop down if needed
        //public List<Tuple<int, string>> PriorityOptions { get; set; }
        public string RefreshTime;
    }
}