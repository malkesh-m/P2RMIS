using System.Collections.Generic;
using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Bll.Views
{
    /// <summary>
    /// Container holding the results of an open program query.
    /// </summary>
    public class ApplicationManagementView
    {
        #region Construction; Setup & Disposal
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ApplicationManagementView()
        {
        }
        #endregion
        #region Properties 
        /// <summary>
        /// Container holding the -----
        /// </summary>
        internal IEnumerable<ProgramListResultModel> Programs { get; set; }
        public IEnumerable<Application> Applications { get { return TransformApplications(); } }
        public int? PersonID { get; set; }
        #endregion
        #region Business Layer
        /// <summary>
        /// Transform the data layer Application view into the business layer view
        /// </summary>
        /// <returns>-----</returns>
        private IEnumerable<Application> TransformApplications()
        {
            List<Application> result = new List<Application>();
            foreach (var x in Programs)
            {
                result.Add(new Application{ProgramID = x.ProgramFY.ProgramYearId, Abbreviation = x.ClientProgram.ProgramAbbreviation, Description = x.ClientProgram.ProgramDescription, FiscalYear = x.ProgramFY.Year});
            }
            return result;
        }
        #endregion
    }
}
