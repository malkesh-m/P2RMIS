using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Setup;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    /// <summary>
    /// Model builder to construct a model for the Evaluation Criteria Setup wizard when adding 
    /// </summary>
    internal class EvaluationCriteriaAdditionModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="mechanismTemplateId">MechanismTemplate entity identifier of award</param>
        public EvaluationCriteriaAdditionModelBuilder(IUnitOfWork unitOfWork, Nullable<int> mechanismTemplateId)
            : base(unitOfWork)
        {
            this.MechanismTemplateId = mechanismTemplateId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// MechanismTemplate entity identifier to add the MechanimsTemplateElement to.
        /// </summary>
        protected Nullable<int> MechanismTemplateId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<MechanismTemplate> SearchResults { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<IEvaluationCriteriaAdditionModel> Results { get; private set; } = new Container<IEvaluationCriteriaAdditionModel>();
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
            Search();
            MakeModels();
        }
        /// <summary>
        /// Does all the heavy lifting for retrieving the information needing to populate the 
        /// Evaluation Criteria Setup modal when a new  Criteria is created.
        /// </summary>
        internal virtual void Search()
        {
            if (this.MechanismTemplateId.HasValue)
            {
                SearchResults = UnitOfWork.MechanismTemplateRepository.Select()
                    //
                    // We want a specific MechanismTemplate
                    //
                    .Where(x => x.MechanismTemplateId == this.MechanismTemplateId);
            }
            else
            {
                SearchResults = new List<MechanismTemplate>().AsQueryable();
            }
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        internal virtual void MakeModels()
        {
            //
            // Now that we have the records from the database that will populate the grid
            // we can model the data & return it.
            //
            IQueryable<IEvaluationCriteriaAdditionModel> modelResults = SearchResults.Select(x => new EvaluationCriteriaAdditionModel
            {
                //
                // Pull out the data for the model
                //
                HasOverall = x.MechanismTemplateElements.Any(y=> y.OverallFlag),
                MaxSortOrder = x.MechanismTemplateElements.Count(),
                MaxSummaryStatementSortOrder = x.MechanismTemplateElements.Where(y => y.SummarySortOrder != null).Count(),
                EvaluationCriteria = x.MechanismTemplateElements.Select(y => y.MechanismTemplateElementId),
                //
                // and now pull out the identifiers
                //
                MechanismTemplateId = this.MechanismTemplateId
            });
            //
            // And we execute the query.
            //
            List< IEvaluationCriteriaAdditionModel> list = modelResults.ToList();
            //
            // There is one case where there are no results.  That is when we are retrieving
            // the modal model for the first time (i.e. no criteria).  In which case we have to do
            // a little bit of manipulation of the result 
            //
            this.Results.ModelList = (list.Count() == 0)? new List<IEvaluationCriteriaAdditionModel>() { EvaluationCriteriaAdditionModel.CreateEmptyModel() } : list;
        }
        #endregion
    }
}
