using Sra.P2rmis.Dal;
using Sra.P2rmis.Bll.Views;

namespace Sra.P2rmis.Bll.ModelBuilders.ReviewerRecruitment
{
    /// <summary>
    /// Builds one or more WorkList web models for the specified client.
    /// </summary>
    internal class ViewReviewerTitleModelBuilder: ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        public ViewReviewerTitleModelBuilder(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            // TODO: needs to create container when model defined

            //this.Results = new Container<IWorkList>();
        }
        #endregion
        #region Attributes
        /// <summary>
        /// ContainerModelBuilder results.
        /// </summary>
        // public Container<IWorkList> Results { get; private set; }
        #endregion
        #region Builder
        /// <summary>
        /// Build a container of WorkList models.
        /// </summary>
        /// <returns>Model container of WorkList models.</returns>
        public override void BuildContainer()
        {
            //
            // Retrieve any UserProfile changes for this user
            //
            //IEnumerable<UserInfoChangeLog> changes = RetrieveChanges();
            //
            // Now build the results & put them in the container
            //
            //BuildResults(changes);
        }
        #endregion
    }
}
