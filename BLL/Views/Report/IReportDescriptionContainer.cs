using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Bll.Views.Report
{
    /// <summary>
    /// comments:rdl
    /// </summary>
    public interface IReportDescriptionContainer
    {
        /// <summary>
        /// comment:rdl
        /// </summary>
        IList<Tuple<int, string, string, int>> ProgramDescriptions { get; }
        /// <summary>
        /// The selected report group
        /// </summary>
        string ReportGroup { get; }
        /// <summary>
        /// Index of selected report group
        /// </summary>
        int SelectedIndex { get; }
        /// <summary>
        /// Return the description for the first program description.  This
        /// is intended to be used when a specific report group is returned.
        /// </summary>
        string GetSpecificDescription { get; }
    }
}
