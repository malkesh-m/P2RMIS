using Sra.P2rmis.Dal;
using System;

namespace Sra.P2rmis.Bll.Setup.Blocks
{
    internal class EvaluationCriteriaBlock: CrudBlock<MechanismTemplateElement>, ICrudBlock
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor for Adding a new MechanismTemplateElement
        /// </summary>
        /// <param name="clientElementId">ClientElement entity identifier selection</param>
        /// <param name="mechanismTemplateId">Container element for the criteria</param>
        /// <param name="overallFlag">Overall Criterion selection</param>
        /// <param name="scoreFlag">Require Score selection</param>
        /// <param name="textFlag">Require Critique selection</param>
        /// <param name="recommendedWordCount">Word Max selection</param>
        /// <param name="sortOrder">Critique order selection</param>
        /// <param name="summaryIncludeFlag">Hide Criterion selection</param>
        /// <param name="summarySortOrder">Summary Order selection</param>
        /// <param name="instructionText">Instruction text</param>
        /// <param name="showAbbreviationOnScoreboard">Indicates if the criteria abbreviation is displayed on the scoreboard</param>
        internal EvaluationCriteriaBlock(int clientElementId, int mechanismTemplateId, bool overallFlag, bool scoreFlag, bool textFlag, int? recommendedWordCount, int sortOrder, bool summaryIncludeFlag, Nullable<int> summarySortOrder, string instructionText, bool showAbbreviationOnScoreboard)
        {
            this.ClientElementId = clientElementId;
            this.MechanismTemplateId = mechanismTemplateId;
            this.OverallFlag = overallFlag;
            this.ScoreFlag = scoreFlag;
            this.TextFlag = textFlag;
            this.RecommendedWordCount = recommendedWordCount;
            this.SortOrder = sortOrder;
            this.SummaryIncludeFlag = summaryIncludeFlag;
            this.SummarySortOrder = summarySortOrder;
            this.InstructionText = instructionText;
            this.ShowAbbreviationOnScoreboard = showAbbreviationOnScoreboard;
        }
        /// <summary>
        /// Constructor for Editing an existing MechanismTemplateElement
        /// </summary>
        /// <param name="clientElementId">ClientElement entity identifier selection</param>
        /// <param name="mechanismTemplateId">Container element for the criteria</param>
        /// <param name="mechanismTemplateElementId">MechanismTemplateElement entity identifier</param>
        /// <param name="overallFlag">Overall Criterion selection</param>
        /// <param name="scoreFlag">Require Score selection</param>
        /// <param name="textFlag">Require Critique selection</param>
        /// <param name="recommendedWordCount">Word Max selection</param>
        /// <param name="sortOrder">Critique order selection</param>
        /// <param name="summaryIncludeFlag">Hide Criterion selection</param>
        /// <param name="summarySortOrder">Summary Order selection</param>
        /// <param name="instructionText">Instruction text</param>
        /// <param name="showAbbreviationOnScoreboard">Indicates if the criteria abbreviation is displayed on the scoreboard</param>
        internal EvaluationCriteriaBlock(int clientElementId, int mechanismTemplateId, int mechanismTemplateElementId, bool overallFlag, bool scoreFlag, bool textFlag, int? recommendedWordCount, int sortOrder, bool summaryIncludeFlag, Nullable<int> summarySortOrder, string instructionText, bool showAbbreviationOnScoreboard) :
            this(clientElementId, mechanismTemplateId, overallFlag, scoreFlag, textFlag, recommendedWordCount, sortOrder, summaryIncludeFlag, summarySortOrder, instructionText, showAbbreviationOnScoreboard)
        {
            this.MechanismTemplateElementId = mechanismTemplateElementId;
        }
        /// <summary>
        /// Constructor for Deleting an existing MechanismTemplateElement
        /// </summary>
        /// <param name="mechanismTemplateElementId">MechanismTemplateElement entity identifier</param>
        internal EvaluationCriteriaBlock(int mechanismTemplateElementId)
        {
            this.MechanismTemplateElementId = mechanismTemplateElementId;
        }
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureAdd()
        {
            IsAdd = true;
        }
        /// <summary>
        /// Configures block for an Modify operation
        /// </summary>
        internal void ConfigureModify()
        {
            IsModify = true;
        }
        /// <summary>
        /// Configures block for an Delete operation
        /// </summary>
        internal void ConfigureDelete()
        {
            IsDelete = true;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// ClientElement selected from the drop down
        /// </summary>
        public int ClientElementId { get; private set;}
        /// <summary>
        /// Container entity for the criteria
        /// </summary>
        public int MechanismTemplateId { get; private set; }
        /// <summary>
        /// Criteria element entity identifier
        /// </summary>
        public int MechanismTemplateElementId { get; private set; }
        /// <summary>
        /// Overall flag from the checkbox
        /// </summary>
        public bool OverallFlag { get; private set;}
        /// <summary>
        /// Require score checkbox value
        /// </summary>
        public bool ScoreFlag { get; private set;}
        /// <summary>
        /// Require Critique checkbox value
        /// </summary>
        public bool TextFlag { get; private set;}
        /// <summary>
        /// Word Max value
        /// </summary>
        public int? RecommendedWordCount { get; private set;}
        /// <summary>
        /// Critique order
        /// </summary>
        public int SortOrder {get; private set;}
        /// <summary>
        /// Hide Criterion checkbox
        /// </summary>
        public bool SummaryIncludeFlag { get; private set;}
        /// <summary>
        /// Summary order drop down selection
        /// </summary>
        public Nullable<int> SummarySortOrder { get; private set;}
        /// <summary>
        /// Description text 
        /// </summary>
        public string InstructionText { get; private set;}
        /// <summary>
        /// Indicates if the Criteria abbreviation is displayed on the scoreboard
        /// </summary>
        public bool ShowAbbreviationOnScoreboard { get; private set; }
        #endregion
        #region Methods
        /// <summary>
        /// Sets individual properties on the CRUD entity.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <param name="entity">MechanismTemplateElement to populate</param>
        internal override void Populate(int userId, MechanismTemplateElement entity)
        {
            entity.ClientElementId = this.ClientElementId;
            entity.InstructionText = this.InstructionText;
            entity.MechanismTemplateId = this.MechanismTemplateId;
            entity.OverallFlag = this.OverallFlag;
            entity.RecommendedWordCount = this.RecommendedWordCount;
            entity.ScoreFlag = this.ScoreFlag;
            entity.SortOrder = this.SortOrder;
            entity.SummaryIncludeFlag = this.SummaryIncludeFlag;
            entity.SummarySortOrder = this.SummarySortOrder;
            entity.TextFlag = this.TextFlag;
            //
            // This should be uncommented if/when the UI is updated to display this property
            //
            //entity.ShowAbbreviationOnScoreboard = this.ShowAbbreviationOnScoreboard;
        }
        /// <summary>
        /// Indicates if the block has data.
        /// </summary>
        /// <returns>True if the block contains data; false otherwise</returns>
        internal override bool HasData() { return (IsAdd || IsModify); }
        #endregion
    }
}
