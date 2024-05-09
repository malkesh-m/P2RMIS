using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Setup;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.WebModels.Lists;

namespace Sra.P2rmis.Web.UI.Models
{
    public class CriterionWizardViewModel
    {
        public const int WordMaxCount = 500;
        public const string Full = "Full";
        public const string Short = "Short";

        /// <summary>
        /// Initializes a new instance of the <see cref="CriterionWizardViewModel"/> class.
        /// </summary>
        public CriterionWizardViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriterionWizardViewModel"/> class.
        /// </summary>
        /// <param name="criterionModel">The criterion model.</param>
        /// <param name="criteriaList">The criteria list.</param>
        /// <remarks>Add Criterion</remarks>
        public CriterionWizardViewModel(IEvaluationCriteriaAdditionModel criterionModel, List<IGenericDescriptionList<int, string, string>> criteriaList)
        {
            HasOverall = false;
            RequireScore = true;
            RequireCritique = true;
            WordMax = WordMaxCount;
            ShowAbbreviationOnScoreboard = criterionModel.ShowAbbreviationOnScoreboard;
            ScoreboardDescList.Add(new KeyValuePair<bool, string>(false, Full));
            ScoreboardDescList.Add(new KeyValuePair<bool, string>(true, Short));
            CritiqueOrder = criterionModel.MaxSortOrder + 1;
            CritiqueOrderOptions = Enumerable.Range(1, CritiqueOrder).ToList();
            HideSummaryStatementCriterion = false;
            SummaryStatementOrder = criterionModel.MaxSummaryStatementSortOrder + 1;
            SummaryStatementOrderOptions = Enumerable.Range(1, SummaryStatementOrder).ToList();
            AvailableCriteriaList = criteriaList.Where(y => !criterionModel.EvaluationCriteria.Contains(y.Index)).ToList()
                .ConvertAll(x => new Tuple<int, string, string>(x.Index, x.Value, x.Description));
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CriterionWizardViewModel"/> class.
        /// </summary>
        /// <param name="criterionModel">The criterion model.</param>
        /// <param name="criteriaList">The criteria list.</param>
        /// <remarks>Edit Criterion</remarks>
        public CriterionWizardViewModel(IEvaluationCriteriaModalModel criterionModel, List<IGenericDescriptionList<int, string, string>> criteriaList)
        {
            HasOverall = criterionModel.OverallFlag;
            RequireScore = criterionModel.ScoreFlag;
            RequireCritique = criterionModel.TextFlag;
            WordMax = criterionModel.TextFlag ?
                (criterionModel.RecommendedWordCount ?? WordMaxCount) : (int?)null;
            ShowAbbreviationOnScoreboard = criterionModel.ShowAbbreviationOnScoreboard;
            ScoreboardDescList.Add(new KeyValuePair<bool, string>(false, Full));
            ScoreboardDescList.Add(new KeyValuePair<bool, string>(true, Short));
            EvalTotal = criterionModel.EvaluationCriteria.Count();
            CritiqueOrder = criterionModel.SortOrder != 0 ? criterionModel.SortOrder : 1;
            CritiqueOrderOptions = Enumerable.Range(1, EvalTotal).ToList();
            HideSummaryStatementCriterion = !criterionModel.SummaryIncludeFlag;
            SummaryStatementOrder = criterionModel.SummarySortOrder != null ?
                (criterionModel.SummarySortOrder != 0 ?
                    (int)criterionModel.SummarySortOrder : 1) : 0;
            SummaryStatementOrderOptions = Enumerable.Range(1, EvalTotal).ToList();
            AvailableCriteriaList = criteriaList.Where(y => !criterionModel.EvaluationCriteria.Contains(y.Index)).ToList()
                .ConvertAll(x => new Tuple<int, string, string>(x.Index, x.Value, x.Description));
            Description = criterionModel.InstructionText;
            ClientElementId = criterionModel.ClientElementId;
            EvaluationCriteria = AvailableCriteriaList.Where(x => x.Item1 == ClientElementId).SingleOrDefault()?.Item3;
        }

        /// <summary>
        /// Gets the client element identifier.
        /// </summary>
        /// <value>
        /// The client element identifier.
        /// </value>
        public int? ClientElementId { get; private set; }

        /// <summary>
        /// Gets the evaluation criteria.
        /// </summary>
        /// <value>
        /// The evaluation criteria.
        /// </value>
        public string EvaluationCriteria { get; private set; }

        /// <summary>
        /// Gets the available criteria list.
        /// </summary>
        /// <value>
        /// The available criteria list.
        /// </value>
        public List<Tuple<int, string, string>> AvailableCriteriaList { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance has overall.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has overall; otherwise, <c>false</c>.
        /// </value>
        public bool HasOverall { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [require score].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [require score]; otherwise, <c>false</c>.
        /// </value>
        public bool RequireScore { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [require critique].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [require critique]; otherwise, <c>false</c>.
        /// </value>
        public bool RequireCritique { get; private set; }

        /// <summary>
        /// Gets the word maximum.
        /// </summary>
        /// <value>
        /// The word maximum.
        /// </value>
        public int? WordMax { get; private set; }

        /// <summary>
        /// Gets the critique order.
        /// </summary>
        /// <value>
        /// The critique order.
        /// </value>
        public int CritiqueOrder { get; private set; }
        /// <summary>
        /// Score board description: full or abbreviation.
        /// </summary>
        public bool ShowAbbreviationOnScoreboard { get; private set; }
        /// <summary>
        /// Score board description list.
        /// </summary>
        public List<KeyValuePair<bool, string>> ScoreboardDescList { get; private set; } = new List<KeyValuePair<bool, string>>();

        /// <summary>
        /// Gets the critique order options.
        /// </summary>
        /// <value>
        /// The critique order options.
        /// </value>
        public List<int> CritiqueOrderOptions { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [hide summary statement criterion].
        /// </summary>
        /// <value>
        /// <c>true</c> if [hide summary statement criterion]; otherwise, <c>false</c>.
        /// </value>
        public bool HideSummaryStatementCriterion { get; private set; }

        /// <summary>
        /// Gets the summary statement order.
        /// </summary>
        /// <value>
        /// The summary statement order.
        /// </value>
        public int SummaryStatementOrder { get; private set; }

        /// <summary>
        /// Gets the summary statement order options.
        /// </summary>
        /// <value>
        /// The summary statement order options.
        /// </value>
        public List<int> SummaryStatementOrderOptions { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; private set; }

        /// <summary>
        /// This gets the count for the dropdown menus
        /// </summary>
        public int EvalTotal { get; private set;  }
    }
}