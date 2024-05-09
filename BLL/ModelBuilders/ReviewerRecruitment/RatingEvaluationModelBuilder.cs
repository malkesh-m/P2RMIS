using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.ReviewerRecruitment;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Sra.P2rmis.Bll.ModelBuilders.ReviewerRecruitment
{
    /// <summary>
    /// Builds the RatingEvaluationModel for the Rating/Evaluation Modal.
    /// </summary>
    internal class RatingEvaluationModelBuilder: ContainerModelBuilderBase
    {
        #region Constructin & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="userId">User entity identifier</param>
        public RatingEvaluationModelBuilder(IUnitOfWork unitOfWork, int userId)
            : base(unitOfWork, userId)
        {
            this.Results = new Container<IRatingEvaluationModel>();
            this.List = new List<IRatingModel>();

            this.EvaluationList = new List<IRatingEvaluationModel>(1);
            this.Results.ModelList = this.EvaluationList;

            this.Sum = 0;
            this.NumberOfRatings = 0;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Running total of the reviewers ratings
        /// </summary>
        protected int Sum { get; set; }
        /// <summary>
        /// Number of ratings to use when calculating the average.
        /// </summary>
        protected int NumberOfRatings { get; set; }
        /// <summary>
        /// Number of Chair recommendations.
        /// </summary>
        protected int NumberOfChairRecommentations { get; set; }
        /// <summary>
        /// The ModelBuilder results.
        /// </summary>
        public Container<IRatingEvaluationModel> Results { get; private set; }
        /// <summary>
        /// List for individual rating results
        /// </summary>
        protected List<IRatingModel> List { get; private set; }
        /// <summary>
        /// List for rating evaluation model.  There will be only a single entry.
        /// </summary>
        protected List<IRatingEvaluationModel> EvaluationList { get; private set; }
        #endregion
        #region Builder
        /// <summary>
        /// Build a container of WorkList models.
        /// </summary>
        /// <returns>Model container of WorkList models.</returns>
        public override void BuildContainer()
        {
            User userEntity = GetThisUser(UserId);
            //
            // Now we simply iterate over the user's PanelUserAssignments.  We filter out the assignments that don't have evaluations.
            //
            foreach (PanelUserAssignment panelUserAssignmentEntity in userEntity.PanelUserAssignments.Where(x => x.ReviewerEvaluations.Count() > 0))
            {
                Retrieve(panelUserAssignmentEntity);
            }
            //
            // Now we build the RatingEvaluationModel
            //
            Retrieve(userEntity);
        }
        /// <summary>
        /// Retrieves the reviewer identification information; calculates the averages &
        /// builds the container model.
        /// </summary>
        /// <param name="userEntity">User entity identifier</param>
        protected void Retrieve(User userEntity)
        {
            //
            // Now that we have that done (which is the stuff for the grid) we can determine the modal
            // header values.
            //
            UserInfo userInfoEntity = userEntity.UserInfoEntity();
            //
            // Create the container model & populate it.
            //
            if(userInfoEntity != null)
            {
                var model = new RatingEvaluationModel(userInfoEntity.FirstName, userInfoEntity.LastName);
                model.SetRatings(this.List, Rate(), this.NumberOfRatings, this.NumberOfChairRecommentations);
                this.EvaluationList.Add(model);
            }
        }
        /// <summary>
        /// Retrieves the individual ratings and constructs a model for each.
        /// </summary>
        /// <param name="panelUserAssignmentEntity">PanelUserAssignment entity</param>
        protected void Retrieve(PanelUserAssignment panelUserAssignmentEntity)
        {
            //
            // So we don't repeat the Linq executions declare some local variables:
            //
            string level = panelUserAssignmentEntity.Level();
            string label = panelUserAssignmentEntity.ParticipationMethod?.ParticipationMethodLabel;
            string type = panelUserAssignmentEntity.ClientParticipantType?.ParticipantTypeAbbreviation;
            string role = panelUserAssignmentEntity.ClientRole?.RoleName;
            //
            string panelAbbreviation = panelUserAssignmentEntity.GetPanelAbbreviation();
            string year = panelUserAssignmentEntity.SessionPanel.GetFiscalYear();
            string program = panelUserAssignmentEntity.SessionPanel.GetProgramAbbreviation();
            //
            // Now iterate over the evaluations
            //
            foreach (ReviewerEvaluation reviewerEvaluationEntity in panelUserAssignmentEntity.ReviewerEvaluations)
            {
                RatingModel model = new RatingModel();
                model.SetRating(reviewerEvaluationEntity.Rating, reviewerEvaluationEntity.Comments, reviewerEvaluationEntity.RecommendChairFlag);
                model.SetParticipation(level, label, type, role);
                model.SetPanelInformation(panelAbbreviation, year, program);
                //
                // Now we need to determine who created the review.
                //
                UserInfo userInfoEntity = GetUserInfoForUser(reviewerEvaluationEntity.CreatedBy);
                if(userInfoEntity != null)
                {
                    model.SetReviewer(userInfoEntity.FirstName, userInfoEntity.LastName, reviewerEvaluationEntity.CreatedDate);
                    //
                    // Sum up the ratings.  We do it "old school" 
                    //
                    SumRating(reviewerEvaluationEntity.Rating, reviewerEvaluationEntity.RecommendChairFlag);
                    this.List.Add(model);
                }

            }
        }
        /// <summary>
        /// Sum up the rating values.  Might as well do it old school since
        /// we are iterating over the ratings.
        /// </summary>
        /// <param name="rating">Rating value</param>
        /// <param name="recommendedChairFlag">Recommend chair flag</param>
        protected void SumRating(int? rating, bool recommendedChairFlag)
        {
            //
            //  If there is a rating sum it up.
            //
            if (rating.HasValue)
            {
                this.Sum += rating.Value;
                this.NumberOfRatings++;
            }
            if (recommendedChairFlag)
            {
                this.NumberOfChairRecommentations++;
            }
        }
        /// <summary>
        /// Calculates the reviewers average rating.  If there were no rating values then
        /// null is returned.
        /// </summary>
        /// <returns>Calculated rating</returns>
        protected decimal? Rate()
        {   
            return (this.NumberOfRatings > 0) ? (Convert.ToDecimal(Sum) / this.NumberOfRatings) : (decimal?)null;
        }
        /// <summary>
        /// Retrieves the UserInfo entity for the specified userId.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>UserInfo entity</returns>
        protected UserInfo GetUserInfoForUser(int userId)
        {
            User userEntity = GetThisUser(userId);
            return userEntity?.UserInfoEntity();
        }
        #endregion
    }
}
