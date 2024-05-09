using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.WebModels.ReviewerRecruitment;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// Data container that drives the ReviewerEvaluationModal page
    /// </summary>
    public class ViewReviewerEvaluationViewModel
    {
        #region Properties
        /// <summary>
        /// Reviewer average rating
        /// </summary>
        public decimal? AverageRating { get; private set; }
        /// <summary>
        /// Number of individual ratings.  This is the number of 
        /// evaluations that were used in the average rating calculation.
        /// </summary>
        public int NumberOfRatings { get; private set; }
        /// <summary>
        /// Number of potential chair recommendations.
        /// </summary>
        public int NumberOfPotentialChairRecomendations { get; private set; }
        /// <summary>
        /// Collection of individual reviewer rating.  Nullable.
        /// </summary>
        public List<RatingViewModel> Ratings { get; set; }
        #endregion
        #region Methods
        /// <summary>
        /// Sets the data for the view model.
        /// </summary>
        /// <param name="evalData">The eval data.</param>
        public void SetData(List<IRatingEvaluationModel> evalData)
        {
            Ratings = new List<RatingViewModel>();
            //This should always be one item in the collection currently
            foreach (var item in evalData)
            {
                AverageRating = (item.AverageRating.HasValue) ? ViewHelpers.P2rmisRound(item.AverageRating.Value): item.AverageRating;
                NumberOfRatings = item.NumberOfRatings;
                NumberOfPotentialChairRecomendations = item.NumberOfPotentialChairRecomendations;
                foreach (var rating in item.Ratings)
                {
                    Ratings.Add(new RatingViewModel(rating));
                }

            }
        }

        public static string FormatRatingLine(string raterName, DateTime? ratingDateTime)
        {
            var dateTimePartial = raterName != null && ratingDateTime != null ? $" - {ViewHelpers.FormatDate(ratingDateTime)}" : string.Empty;
            var result = $"{raterName}{dateTimePartial}";
            return result;
        }
        #endregion
        #region Nested types

        /// <summary>
        /// Class representing the list of evaluations related to a reviewer
        /// </summary>
        public class RatingViewModel
        {
            #region Constructor
            /// <summary>
            /// Initializes a new instance of the <see cref="RatingViewModel"/> class.
            /// </summary>
            /// <param name="model">The rating model.</param>
            public RatingViewModel(IRatingModel model)
            {
                FiscalYear = model.FiscalYear;
                Program = model.Program;
                Panel = model.Panel;
                ParticipationTypeLabel = ConstructParticipantTypeMultiPart(model.ParticipationType, model.ParticipationMethod, model.ParticipationLevel, model.ClientRole);
                Rating = model.Rating;
                PotentialChair = model.PotentialChair;
                Comments = model.Comments ?? string.Empty;
                RaterLine = FormatRatingLine(ViewHelpers.ConstructShortName(model.RaterFirstName, model.RaterLasttName), model.RatingCreationDate);
            }
            #endregion
            #region Properties
            /// <summary>
            /// Program fiscal year
            /// </summary>
            [JsonProperty(PropertyName = "fy")]
            public string FiscalYear { get; private set; }
            /// <summary>
            /// Program
            /// </summary>
            [JsonProperty(PropertyName = "program")]
            public string Program { get; private set; }
            /// <summary>
            /// Program panel
            /// </summary>
            [JsonProperty(PropertyName = "panel")]
            public string Panel { get; private set; }
            /// <summary>
            /// Participation type (SR)
            /// </summary>
            [JsonProperty(PropertyName = "participation")]
            public string ParticipationTypeLabel { get; private set; }
            /// <summary>
            /// Reviewer rating
            /// </summary>
            [JsonProperty(PropertyName = "rating")]
            public int? Rating { get; private set; }
            /// <summary>
            /// Potential chair indication
            /// </summary>
            [JsonProperty(PropertyName = "potentialChair")]
            public bool PotentialChair { get; private set; }
            /// <summary>
            /// Reviewer evaluation comments
            /// </summary>
            [JsonProperty(PropertyName = "comment")]
            public string Comments { get; private set; }
            [JsonProperty(PropertyName = "ratingLine")]
            public string RaterLine { get; private set; }
            #endregion
            #region Methods
            /// <summary>
            /// Constructs the participant type with multiple parts (type, method and level).
            /// </summary>
            /// <param name="participationType">Type of the participation (Reviewer, Specialist).</param>
            /// <param name="participationMethod">The participation method (In-person, Remote).</param>
            /// <param name="participationLevel">The participation level (Partial, full).</param>
            /// <param name="clientRole">Client role</param>
            /// <returns>Formatted string</returns>
            private static string ConstructParticipantTypeMultiPart(string participationType, string participationMethod, string participationLevel, string clientRole)
            {
                return $"{participationType}-{clientRole}-{participationMethod}-{participationLevel}";
            }
            #endregion
        }
        #endregion
    }
}