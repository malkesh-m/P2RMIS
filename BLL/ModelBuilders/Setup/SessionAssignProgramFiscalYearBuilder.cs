using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Lists;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    /// <summary>
    /// Model builder to construct a container of models for the Fiscal year drop down on the 
    /// Session's Add Panel with No Program modal.
    /// </summary>
    internal class SessionAssignProgramFiscalYearBuilder: AssignProgramFiscalYearModelBuilder
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="clientId">Client entity identifier</param>
        public SessionAssignProgramFiscalYearBuilder(IUnitOfWork unitOfWork, int clientId)
            : base(unitOfWork, clientId)
        {}
        #endregion
        #region Attributes
        /// <summary>
        /// Builder results.
        /// </summary>
        internal new Container<IGenericListEntry<int, string>> Results { get; private set; } = new Container<IGenericListEntry<int, string>>();
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
            base.BuildContainer();
            FilterResults();
        }
        /// <summary>
        /// Filter the search results.  We only want the client's distinct ProgramYears.
        /// </summary>
        protected void FilterResults()
        {
            //
            // Filter the base results to produce a distinct list of program years.
            //
            this.Results.ModelList = base.Results.ModelList
                                              //
                                              // We only want the active ProgramYears
                                              //
                                              .Where(x => x.IsActive)
                                              //
                                              // Then we group it by the year.  (There are multiple programs
                                              // in each year.)
                                              //
                                              .GroupBy(x => x.Value)
                                              //
                                              // Then we just select the years (which is the group's key)
                                              //
                                              .Select(y => new GenericListEntry<int, string>{ Index = this.ClientId , Value = y.Key });
        }
        #endregion
    }
    /// <summary>
    /// Model builder to construct a container of models for the Program drop down on the 
    /// Session's Add Panel with No Program modal.
    /// </summary>
    internal class SessionAssignProgramProgramsBuilder : AssignProgramFiscalYearModelBuilder
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="year">Program (fiscal) Year</param>
        public SessionAssignProgramProgramsBuilder(IUnitOfWork unitOfWork, int clientId, string year)
            : base(unitOfWork, clientId)
        {
            this.Year = year;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// The selected ProgramYear
        /// </summary>
        protected string Year { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal new Container<IGenericListEntry<int, string>> Results { get; private set; } = new Container<IGenericListEntry<int, string>>();
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
            base.BuildContainer();
            FilterResults();
        }
        /// <summary>
        /// Filter the search results.  We only want the Programs for the selected year.
        /// </summary>
        protected void FilterResults()
        {
            //
            // Filter the base results to produce a distinct list of programs.
            //
            this.Results.ModelList = base.Results.ModelList
                                              //
                                              // We only want the active ProgramYears
                                              //
                                              .Where(x => x.IsActive)
                                              //
                                              // Then only for the selected year
                                              //
                                              .Where(x => x.Value == this.Year)
                                              //
                                              // Then we just select the programs
                                              //
                                              .Select(y => new GenericListEntry<int, string> { Index = y.ProgramYearId, Value = y.ProgramAbbreviation });
        }
        #endregion
    }
}
