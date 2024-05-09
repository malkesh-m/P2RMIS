using Sra.P2rmis.WebModels.SummaryStatement;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.ViewModels.SummaryStatementReview
{
    public class RequestReviewApplicationViewModel
    {
        public RequestReviewApplicationViewModel(ISummaryStatementRequestReview application)
        {
            LogNumber = application.LogNumber;
            Cycle = application.Cycle;
            Panel = application.Panel;
            Award = application.AwardMechanismAbbreviation;
            Priority1 = ViewHelpers.FormatBoolean(application.Priority1);
            Priority2 = ViewHelpers.FormatBoolean(application.Priority2);
            Score = ViewHelpers.ScoreFormatter(application.OverallScore);
            TopicArea = application.ResearchTopicArea;
            PanelApplicationId = application.PanelApplicationId;
            ApplicationId = application.ApplicationId;
            HasPrecedingReviewStep = application.HasPrecedingReviewStep;
            Index = application.Index;
        }
        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        public string LogNumber { get; set; }
        /// <summary>
        /// Gets or sets the cycle.
        /// </summary>
        /// <value>
        /// The cycle.
        /// </value>
        public int? Cycle { get; set; }
        /// <summary>
        /// Gets or sets the panel.
        /// </summary>
        /// <value>
        /// The panel.
        /// </value>
        public string Panel { get; set; }
        /// <summary>
        /// Gets or sets the award.
        /// </summary>
        /// <value>
        /// The award.
        /// </value>
        public string Award { get; set; }
        /// <summary>
        /// Gets or sets the priority1.
        /// </summary>
        /// <value>
        /// The priority1.
        /// </value>
        public string Priority1 { get; set; }
        /// <summary>
        /// Gets or sets the priority2.
        /// </summary>
        /// <value>
        /// The priority2.
        /// </value>
        public string Priority2 { get; set; }
        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public string Score { get; set; }
        /// <summary>
        /// Gets or sets the topic area.
        /// </summary>
        /// <value>
        /// The topic area.
        /// </value>
        public string TopicArea { get; set; }
        /// <summary>
        /// Gets or sets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public int ApplicationId { get; set; }
        /// <summary>
        /// Gets a value indicating whether this instance has preceding review step.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has preceding review step; otherwise, <c>false</c>.
        /// </value>
        public bool HasPrecedingReviewStep { get; private set; }
        public int Index { get; set; }
    }
}