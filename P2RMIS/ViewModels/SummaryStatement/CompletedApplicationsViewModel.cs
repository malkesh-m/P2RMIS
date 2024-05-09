using System;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.SummaryStatement;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.ViewModels.SummaryStatement
{
    public class CompletedApplicationsViewModel
    {
        public CompletedApplicationsViewModel() { }

        public CompletedApplicationsViewModel(List<IApplicationsProgress> applications)
        {
            Applications = applications.ConvertAll(x => new CompletedApplicationViewModel(x));
            CompletedApplicationViewModel.ScoreFormatter = ViewHelpers.ScoreFormatter;
            RefreshTime = ViewHelpers.FormatDateTime(GlobalProperties.P2rmisDateTimeNow);
        }
        /// <summary>
        /// Gets the applications.
        /// </summary>
        /// <value>
        /// The applications.
        /// </value>
        public List<CompletedApplicationViewModel> Applications { get; private set; }
        /// <summary>
        /// Gets or sets the refresh time.
        /// </summary>
        /// <value>
        /// The refresh time.
        /// </value>
        public string RefreshTime { get; set; }
    }
}