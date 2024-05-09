using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Views
{
    /// <summary>
    /// Business Layer representing the report list results
    /// </summary>
    public class ReportContainer : IReportContainer
    {
        #region Constructors
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private ReportContainer() { }
        /// <summary>
        /// Constructor.  Populate from the Data Layers SearchResultModel
        /// </summary>
        /// <param name="searchResult">report list results from data layer</param>
        public ReportContainer(IReportDetailResultModel reportResults)
        {
            this.Reports = reportResults.Reports.ToList<IReportResultModel>().ConvertAll(new Converter<IReportResultModel, IReportFacts>(IReportResultModelToIResultFacts));
        }
        #endregion

        #region Properties

        public IEnumerable<IReportFacts> Reports { get; internal set; }

        #endregion

        #region Helpers
        /// <summary>
        /// Converts a data layer Report object into a business layer ReportFacts object.
        /// </summary>
        /// <param name="item">-----</param>
        /// <returns>-----</returns>
        private static IReportFacts IReportResultModelToIResultFacts(IReportResultModel item)
        {
            return new ReportFacts(item);
        }
        #endregion
    }
}
