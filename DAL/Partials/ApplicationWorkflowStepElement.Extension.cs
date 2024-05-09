using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ApplicationWorkflowStepElement object.
    /// </summary>
    public partial class ApplicationWorkflowStepElement: IStandardDateFields
    {
        /// <summary>
        /// Promotes the ApplicationWorkflowStepElementContent entity objects in this ApplicationWorkflowStepElement.
        /// </summary>
        /// <param name="ApplicationWorkflowStepId">ApplicationWorkflowStep identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="shouldRemoveMarkup">Whether to remove the markup</param>
        /// <returns>List of promoted ApplicationWorkflowStepElementContent</returns>
        public virtual List<ApplicationWorkflowStepElementContent> Promote(int ApplicationWorkflowStepId, int userId, bool shouldRemoveMarkup)
        {
            //
            // Create the container to hold the individual promoted contents and then for each content in this
            // step element promote it.
            //
            List<ApplicationWorkflowStepElementContent> results = new List<ApplicationWorkflowStepElementContent>();

            foreach (ApplicationWorkflowStepElementContent content in this.ApplicationWorkflowStepElementContents)
            {
                var newContent = content.Promote(ApplicationWorkflowStepId, userId, shouldRemoveMarkup);
                results.Add(newContent);
            }
            return results;
        }

        /// <summary>
        /// Constructs multiple ApplicationWorkflowStepElementContentHistory object for each
        /// ApplicationWorkflowStepElementContents object.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="worklogId">Worklog identifier</param>
        /// <returns>Collection of ApplicationWorkflowStepElementContentHistory objects<returns>
        public ICollection<ApplicationWorkflowStepElementContentHistory> CreateHistory(int userId, int worklogId)
        {
            List<ApplicationWorkflowStepElementContentHistory> result = new List<ApplicationWorkflowStepElementContentHistory>();
            foreach (var item in this.ApplicationWorkflowStepElementContents)
            {
                ApplicationWorkflowStepElementContentHistory oneElementResult = new ApplicationWorkflowStepElementContentHistory();
                oneElementResult.PopulateFromContent(item, userId, worklogId);
                
                result.Add(oneElementResult);
            }
            return result;
        }
        /// <summary>
        /// Locates the ApplicationElementStepElementContent by the ApplicationElementStepElementContentId 
        /// </summary>
        /// <param name="id">ApplicationElementStepElementContent identifier</param>
        /// <returns>ApplicationElementStepElementContent entity for the id or null if none exists</returns>
        public ApplicationWorkflowStepElementContent GetStepElementContentById(int id)
        {
            return this.ApplicationWorkflowStepElementContents.FirstOrDefault(x => x.ApplicationWorkflowStepElementId == id);
        }
        /// <summary>
        /// Returns the workflow score as a string value if one exists.
        /// </summary>
        /// <returns>Reviewer's score as a string</returns>
        public string Score()
        {
            ApplicationWorkflowStepElementContent applicationWorkflowStepElementContentsEntity = this.ApplicationWorkflowStepElementContents.FirstOrDefault();
            //
            // We can only return a score if:
            //   1) there is a content entity
            //   2) it is scored (no ClientScoringScale is present)
            //
            return ((applicationWorkflowStepElementContentsEntity == null) || (this.ClientScoringScale == null)) ? 
                //
                // In which case we return the empty string
                //
                string.Empty : 
                //
                // Otherwise we use the ClientScoringScale to render it into a string.
                //
                this.ClientScoringScale.ToString(applicationWorkflowStepElementContentsEntity.Score);
        }
        /// <summary>
        /// Gets the appropriate scoring legend for the criterion if one exists
        /// </summary>
        /// <returns>Details of a criterion scoring legend</returns>
        public IEnumerable<ScoringLegendItem> GetCriterionScoringLegend()
        {
            IEnumerable<ScoringLegendItem> result = null;
            if (this.ClientScoringScale.ClientScoringScaleLegendId != null)
            {
                result = this.ClientScoringScale.ClientScoringScaleLegend.ClientScoringScaleLegendItems.Select(
                    x => new ScoringLegendItem
                    {
                        ItemLabel = x.LegendItemLabel,
                        HighValue = x.HighValueLabel,
                        LowValue = x.LowValueLabel,
                        SortOrder = x.SortOrder
                    }).OrderBy(o => o.SortOrder);
            }
            return result;
        }
        /// <summary>
        /// Gets the appropriate adjectival scoring scale for the criterion if one exists
        /// </summary>
        /// <returns>Possible adjectival values of a criterion</returns>
        public IEnumerable<AdjectivalScoreValue> GetAdjectivalScoringScale()
        {
            IEnumerable<AdjectivalScoreValue> result = new List<AdjectivalScoreValue>();

            if (this.ClientScoringScale != null)
            {
                result = this.ClientScoringScale.ClientScoringScaleAdjectivals.Select(
                    x => new AdjectivalScoreValue
                    {
                        ClientScoringScaleAdjectivalId = x.ClientScoringScaleAdjectivalId,
                        NumericValue = x.NumericEquivalent.ToString("N1"),
                        AdjectivalLabel = x.ScoreLabel,
                        SortOrder = x.NumericEquivalent
                    }).OrderBy(o => o.SortOrder);
            }

            return result;
        }
        /// <summary>
        /// Checks if a score exists.  By exists means that there is a applicationWorkflowStepElementContents
        /// entity for this ApplicationWorkflowStepElement.
        /// </summary>
        /// <returns>True if a score exists; false if not./returns>
        public bool HasScore()
        {
            var applicationWorkflowStepElementContentsEntity = this.ApplicationWorkflowStepElementContents.FirstOrDefault();

            return (applicationWorkflowStepElementContentsEntity != null);
        }
        /// <summary>
        /// Get the ApplicationWorkflowStepElementContentId.
        /// </summary>
        /// <returns>ApplicationWorkflowStepElementContentId; 0 if none</returns>
        public int ApplicationWorkflowStepElementContentId()
        {
            var applicationWorkflowStepElementContentsEntity = this.ApplicationWorkflowStepElementContents.FirstOrDefault();
            return (applicationWorkflowStepElementContentsEntity == null) ? 0 : applicationWorkflowStepElementContentsEntity.ApplicationWorkflowStepElementContentId;
        }
        /// <summary>
        /// Indicates if this ApplicationWorkflowStepElement is part of a step that has been resolved.
        /// </summary>
        /// <returns>True if the ApplicationWorkflowStepElement is part of an ApplicationWorkflowStep that has been resolved; false otherwise</returns>
        public bool IsResolved()
        {
            return this.ApplicationWorkflowStep.Resolution;
        }

        /// <summary>
        /// gets the content of the workflow step element.
        /// </summary>
        /// <returns>a single application workflow step element content</returns>
        public ApplicationWorkflowStepElementContent ApplicationWorkflowStepElementContent()
        {
            return this.ApplicationWorkflowStepElementContents.DefaultIfEmpty(new ApplicationWorkflowStepElementContent())
                .First();
        }

        /// <summary>
        /// Returns the ApplicationWorkflowStepElementContent entity for this ApplicationWorkflowStepElement
        /// </summary>
        /// <returns>ApplicationWorkflowStepElementContent entity</returns>
        public ApplicationWorkflowStepElementContent GetContents()
        {
            return this.ApplicationWorkflowStepElementContents.FirstOrDefault();
        }
        /// <summary>
        /// Indicates if a score is required for the element
        /// </summary>
        /// <returns>Indicates if a score entry is required</returns>
        public bool RequiresScore()
        {
            return this.ApplicationTemplateElement.MechanismTemplateElement.ScoreFlag;
        }
        /// <summary>
        /// Indicates if text is required for the element
        /// </summary>
        /// <returns>Indicates if a text entry is required</returns>
        public bool RequireText()
        {
            return this.ApplicationTemplateElement.MechanismTemplateElement.TextFlag;
        }
        /// <summary>
        /// Checks if the step has text data.  If no text data is required, then it has text by definition.
        /// </summary>
        /// <param name="applicationWorkflowStepElementContentEntity">ApplicationWorkflowStepElementContent entity identifier</param>
        /// <returns>True if the step element has text data; false otherwise</returns>
        public bool HasTextData(ApplicationWorkflowStepElementContent applicationWorkflowStepElementContentEntity)
        {
            return (!RequireText()) ? true : (applicationWorkflowStepElementContentEntity == null) ? 
                false : (!string.IsNullOrWhiteSpace(HtmlServices.RemoveMarkup(HttpUtility.HtmlDecode(applicationWorkflowStepElementContentEntity.ContentText))));
        }
        /// <summary>
        /// Checks if the step has score data.  If no score data is required, then it has score data by definition.
        /// </summary>
        /// <param name="applicationWorkflowStepElementContentEntity">ApplicationWorkflowStepElementContent entity identifier</param>
        /// <returns>True if the step element has sore data; false otherwise</returns>
        public bool HasScoreData(ApplicationWorkflowStepElementContent applicationWorkflowStepElementContentEntity)
        {
            return (!RequiresScore()) ? true : (applicationWorkflowStepElementContentEntity == null) ? false : (applicationWorkflowStepElementContentEntity.Score != null);
        }
        /// <summary>
        /// Indicates if data has been entered for this workflow step element
        /// </summary>
        /// <returns>True if the step element has sore and/or text data; false otherwise</returns>
        public bool HasData()
        {
            ApplicationWorkflowStepElementContent applicationWorkflowStepElementContentEntity = GetContents();
            bool result = false;

            if (applicationWorkflowStepElementContentEntity != null)
            {
                //
                // They could have abstained
                //
                if (applicationWorkflowStepElementContentEntity.Abstain)
                {
                    result = true;
                }
                //
                // they could have entered a criteria & score.
                //
                else if (
                            (HasTextData(applicationWorkflowStepElementContentEntity)) &
                            (HasScoreData(applicationWorkflowStepElementContentEntity))
                        )
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// Returns the criteria description
        /// </summary>
        /// <returns>Element description</returns>
        public string GetCriteriaElementDescription()
        {
            return this.ApplicationTemplateElement.GetCriteriaElementDescription();
        }

        /// <summary>
        /// the score type for an element.
        /// </summary>
        /// <returns>the score type for an element</returns>
        public string ScoreType()
        {
            return this.ClientScoringScale != null ? this.ClientScoringScale.ScoreType : string.Empty;
        }

        /// <summary>
        /// Gets the criteria sort order.
        /// </summary>
        /// <returns>The sort order of the criteria</returns>
        public int GetCriteriaSortOrder()
        {
            return this.ApplicationTemplateElement.GetCriteriaSortOrder();
        }

        /// <summary>
        /// Gets the numeric score value associated.
        /// </summary>
        /// <returns>Decimal score</returns>
        public decimal? ContentScore()
        {
            return this.ApplicationWorkflowStepElementContent().Score;
        }

        /// <summary>
        /// Gets the adjectival equivalent score associated.
        /// </summary>
        /// <returns>The adjectival equivalent, or an empty string if not applicable</returns>
        public string ContentAdjectivalEquivalent()
        {
            return this.ApplicationWorkflowStepElementContent().AdjectivalEquivalent();
        }

        /// <summary>
        /// Determines whether this instance is abstained.
        /// </summary>
        /// <returns>True if criteria was abstained; otherwise false</returns>
        public bool IsAbstained()
        {
            return this.ApplicationWorkflowStepElementContent().Abstain;
        }
        /// <summary>
        /// the score high value for an element.
        /// </summary>
        /// <returns>the score high for an element</returns>
        public decimal ScoreHighValue()
        {
            return this.ClientScoringScale != null ? this.ClientScoringScale.HighValue : 0;
        }
        /// <summary>
        /// the score low value for an element.
        /// </summary>
        /// <returns>the score low value for an element</returns>
        public decimal ScoreLowValue()
        {
            return this.ClientScoringScale != null ? this.ClientScoringScale.LowValue : 0;
        }

        /// <summary>
        /// Determines whether this element instance is an overall evaluation.
        /// </summary>
        /// <returns>True if it is overall; otherwise false</returns>
        public bool IsOverall()
        {
            return this.ApplicationTemplateElement.MechanismTemplateElement.OverallFlag;
        }
        /// <summary>
        /// Indicates if the critique content is for a pre-meeting critique.  
        /// </summary>
        /// <returns>True if the workflow is for a pre-meeting phase; false otherwise</returns>
        public bool IsContentForPreMeetingCritique()
        {
            return this.ApplicationWorkflowStep.ApplicationWorkflow.ApplicationStage.ReviewStage.ReviewStageId == ReviewStage.Indexes.Asynchronous;
        }
        /// <summary>
        /// Indicates if the critique associated with this workflow has been submitted.  If the workflow step
        /// is marked as Resolved = true then the critique has been submitted
        /// </summary>
        /// <returns>True if the critique has been submitted; false otherwise</returns>
        public bool IsSubmitted()
        {
            return this.ApplicationWorkflowStep.Resolution;
        }
    }
}
