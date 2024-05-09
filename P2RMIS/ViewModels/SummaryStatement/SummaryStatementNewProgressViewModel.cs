using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Web.ViewModels.SummaryStatement
{
    public class SummaryStatementNewProgressViewModel
    {
        public SummaryStatementNewProgressViewModel()
        {
            Applications = new List<SummaryStatementProgressViewModel>();
            this.RefreshTime = String.Empty;
        }

        public SummaryStatementNewProgressViewModel(List<ISummaryStatementProgressModel> applications)
        {
            Applications = applications.ToList().ConvertAll(x => new SummaryStatementProgressViewModel(x));
            this.RefreshTime = String.Empty;
            // Set workflow if not exists
        }
        /// <summary>
        /// Gets or sets the applications.
        /// </summary>
        /// <value>
        /// The applications.
        /// </value>
        public List<SummaryStatementProgressViewModel> Applications { get; set; }
        public string RefreshTime { get; set; }
    }
}