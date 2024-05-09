using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Views.Report
{
    /// <summary>
    /// Container returning the fiscal years for one or more programs
    /// </summary>
    public interface IReportFiscalYearContainer
    {
        /// <summary>
        /// List of fiscal years for one or more programs.  There will always be
        /// an instantiated list.  If there are no fiscal years for the programs
        /// then the list will be of 0 length
        /// </summary>
        IList<string> FiscalYearDescriptions { get; }
    }
}
