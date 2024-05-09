using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sra.P2rmis.Dal.ResultModels.Reports;

namespace Sra.P2rmis.Bll.Views.Report
{
    // DELETE ME PLEASE !!
    ///// <summary>
    ///// Container returning the fiscal years for one or more programs
    ///// </summary>
    //public class ReportFiscalYearContainer : IReportFiscalYearContainer
    //{
    //    #region constructor
    //   /// <summary>
    //    /// Default constructor.  Private default constructor along with the private property setters
    //    /// controls construction & instantiation.
    //    /// </summary>
    //    private ReportFiscalYearContainer() { }
    //    /// <summary>
    //    /// Constructor.  Populate from the Data Layers SearchResultModel.  Always constructs
    //    /// a list of program descriptions even if none (0 length list)
    //    /// </summary>
    //    /// <param name="searchResult">report list results from data layer</param>
    //    public ReportFiscalYearContainer(IReportFiscalYearResultModel reportResults)
    //    {
    //        this.FiscalYearDescriptions = new List<string>();

    //        if (reportResults != null)
    //        {
    //            foreach (ReportFiscalYearModel model in reportResults.ModelList)
    //            {
    //                this.FiscalYearDescriptions.Add(model.FiscalYear);
    //            }
    //        }
    //    }
    //    #endregion
    //    #region Properties
    //    /// <summary>
    //    /// List of fiscal years for one or more programs.  There will always be
    //    /// an instantiated list.  If there are no fiscal years for the programs
    //    /// then the list will be of 0 length
    //    /// </summary>
    //    public IList<string> FiscalYearDescriptions { get; private set; }
    //    #endregion
    //}
}
