using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Bll.Views.Report
{
    // DELETE ME PLEASE !!
    /// <summary>
    /// Business Layer representing the programs avaliable for one or more user clients.
    /// </summary>
    //public class ReportPorgramContainer
    //{
    //    #region constructor
    //   /// <summary>
    //    /// Default constructor.  Private default constructor along with the private property setters
    //    /// controls construction & instantiation.
    //    /// </summary>
    //    private ReportPorgramContainer() { }
    //    /// <summary>
    //    /// Constructor.  Populate from the Data Layers SearchResultModel.  Always constructs
    //    /// a list of program descriptions even if none (0 length list)
    //    /// </summary>
    //    /// <param name="searchResult">report list results from data layer</param>
    //    public ReportPorgramContainer(IReportClientProgramListResultModel reportResults)
    //    {
    //         this.ProgramDescriptions = new List<Tuple<string, string>>();

    //        if (reportResults != null)
    //        {
    //            foreach (ReportClientProgramModel model in reportResults.ProgramList)
    //            {
    //                this.ProgramDescriptions.Add(Tuple.Create(model.ClientProgramAbbreviation, model.ClientProgramDescription));
    //            }
    //        }
    //    }
    //    #endregion
    //    #region Properties
    //    public IList<Tuple<string, string>> ProgramDescriptions { get; private set; }
    //    #endregion
    //}
}
