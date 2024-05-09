using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sra.P2rmis.Dal.ResultModels.Reports;

namespace Sra.P2rmis.Bll.Views.Report
{
    /// <summary>
    /// comments:rdl
    /// </summary>
    public class ReportDescriptionContainer: IReportDescriptionContainer
    {
        #region constructor
       /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private ReportDescriptionContainer() { }
        /// <summary>
        /// Constructor.  Populate from the Data Layers SearchResultModel.  Always constructs
        /// a list of program descriptions even if none (0 length list)
        /// </summary>
        /// <param name="searchResult">report list results from data layer</param>
        public ReportDescriptionContainer(IReportDescriptionResultModel reportResults)
        {
             this.ProgramDescriptions = new List<Tuple<int, string, string, int>>();

            if (reportResults != null)
            {
                foreach (ReportDescriptionModel model in reportResults.ModelList)
                {
                    this.ProgramDescriptions.Add(Tuple.Create(model.ReportGroupId, model.GroupName, model.Description, model.SortOrder));
                }
                //
                // Point to the first report group entry (PL indexes from 1, not 0)
                //
                this.ReportGroup = ProgramDescriptions[0].Item2;
                this.SelectedIndex = 1;
            }
        }
        #endregion
        #region Properties
        /// <summary>
        /// The report group descriptions.
        /// </summary>
        public IList<Tuple<int, string, string, int>> ProgramDescriptions { get; private set; }
        /// <summary>
        /// The selected report group
        /// </summary>
        public string ReportGroup { get; private set; }
        /// <summary>
        /// Index of selected report group
        /// </summary>
        public int SelectedIndex { get; private set; }
        /// <summary>
        /// Return the description for the first program description.  This
        /// is intended to be used when a specific report group is returned.
        /// </summary>
        public string GetSpecificDescription
        {
            get
            {
                string result = string.Empty;
                if (this.ProgramDescriptions.Count >= 0)
                {
                    result = this.ProgramDescriptions[0].Item3;
                }
                return result;
            }
        }
        #endregion
    }
}
