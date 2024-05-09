using System;
using System.Collections.Generic;
using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Bll.Views.Report
{
    /// <summary>
    /// Business Layer representing client information.
    /// </summary>
    public class ReportClientContainer
    {
        #region constructor
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private ReportClientContainer() { }
        /// <summary>
        /// Constructor.  Populate from the Data Layers Client.
        /// </summary>
        /// <param name="reportResults">report list results from data layer</param>
        public ReportClientContainer(IReportClientListResultModel reportResults)
        {
            this.ClientList = new List<Tuple<int, string, string>>();

            if (reportResults != null)
            {
                foreach (ReportClientModel model in reportResults.ClientList)
                {
                    this.ClientList.Add(Tuple.Create(model.ClientIdentifier, model.ClientAbbreviation, model.ClientDescription));
                }
            }
        }
        #endregion
        #region Properties
        /// <summary>
        /// List of client objects to populate dropdown
        /// </summary>
        public IList<Tuple<int, string, string>> ClientList { get; private set; }
        #endregion
    }
}
