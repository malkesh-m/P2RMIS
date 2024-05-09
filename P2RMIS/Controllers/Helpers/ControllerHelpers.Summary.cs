using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.ViewModels.SummaryStatement;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Web.Controllers
{
    /// <summary>
    /// Utility; common & shared methods used by controllers.  This source file contains methods that are
    /// shared between summary statement controllers.
    /// </summary>
    internal partial class ControllerHelpers
    {
        #region Summary Statement Specific Methods
        /// <summary>
        /// Formatting for the Content entry title line on the summary statement editor form.
        /// </summary>
        private const string ContentTitleLine = "{0} (Average Score: {1})"; 
        /// <summary>
        /// Sorts and orders content for editing presentation.  Sorting is a two step
        /// process.  First the collection is sorted the IStepContentModel into a dictionary
        /// that uses the ElementName as the key and collecting all the IStepContentModel together
        /// with the same key.  Second step is to construct the section title line which is used
        /// as a key into for the final dictionary.
        /// </summary>
        /// <param name="collection">step content webmodel collection</param>
        /// <returns>dictionary of string and step content model</returns>
        internal static IDictionary<string, List<IStepContentModel>> OrderContentForPresentation(IEnumerable<IStepContentModel> collection)
        {
            IDictionary<string, List<IStepContentModel>> step1Result = OrderContentByElementName(collection);
            IDictionary<string, List<IStepContentModel>> finalResult = ConstructSummarySectionTitle(step1Result);
            return finalResult;
        }
        internal static Tuple<IDictionary<string, List<IStepContentModel>>, IDictionary<string, List<IStepContentModel>>, IDictionary<string, List<IStepContentModel>>> NewOrderContentForPresentation(IEnumerable<IStepContentModel> collection)
        {
            IDictionary<string, List<IStepContentModel>> step1Result = NewOrderContentByElementName(collection);
            IDictionary<string, List<IStepContentModel>> finalResult = ConstructSummarySectionTitle(step1Result);
            Tuple<IDictionary<string, List<IStepContentModel>>, IDictionary<string, List<IStepContentModel>>, IDictionary<string, List<IStepContentModel>>> results = SortIntoScoredUnscored(finalResult);

            return results;
        }
        /// <summary>
        /// Sorts the dictionary of content into scored & un-scored collections
        /// </summary>
        /// <param name="collection">step content webmodel collection</param>
        /// <returns>dictionary of string and step content model</returns>
        private static Tuple<IDictionary<string, List<IStepContentModel>>, IDictionary<string, List<IStepContentModel>>, IDictionary<string, List<IStepContentModel>>> SortIntoScoredUnscored(IDictionary<string, List<IStepContentModel>> dictionary)
        {
            IDictionary<string, List<IStepContentModel>> scored = new Dictionary<string, List<IStepContentModel>>();
            IDictionary<string, List<IStepContentModel>> unScored = new Dictionary<string, List<IStepContentModel>>();
            IDictionary<string, List<IStepContentModel>> overall = new Dictionary<string, List<IStepContentModel>>();
            //
            // 
            //
            foreach (var key in dictionary.Keys)
            {
                List<IStepContentModel> contentList = dictionary[key];
                bool isScored = false;
                bool isOverview = false;
                //
                //
                //
                foreach (var content in contentList)
                {
                    //
                    // Force the overview content out of both sections & into it's own
                    //
                    if (content.ElementSortOrder <= 2)
                    {
                        isOverview = true;
                        break;
                    }
                    if (content.ElementScoreFlag == true)
                    {
                        isScored = true;
                        break;
                    }
                }
                //
                //
                //
                if (isScored)
                {
                    scored[key] = contentList;
                }
                else if (isOverview)
                {
                    overall[key] = contentList;
                }
                else
                {
                    unScored[key] = contentList;
                }
              
            }
            Tuple<IDictionary<string, List<IStepContentModel>>, IDictionary<string, List<IStepContentModel>>, IDictionary<string, List<IStepContentModel>>> result = new Tuple<IDictionary<string, List<IStepContentModel>>, IDictionary<string, List<IStepContentModel>>, IDictionary<string, List<IStepContentModel>>>(overall, scored, unScored);

            return result;
        }
        /// <summary>
        /// sorts and orders content for editing presentation by item ElementName
        /// </summary>
        /// <param name="collection">step content webmodel collection</param>
        /// <returns>dictionary of string and step content model</returns>
        private static IDictionary<string, List<IStepContentModel>> NewOrderContentByElementName(IEnumerable<IStepContentModel> collection)
        {
            IDictionary<string, List<IStepContentModel>> result = new Dictionary<string, List<IStepContentModel>>(collection.Count());
            string MagicString = "- Discussion Notes";
            foreach (var item in collection)
            {
                string key = (item.ElementName.EndsWith(MagicString))? item.ElementName.Replace(MagicString, string.Empty).Trim() :item.ElementName.Trim();
                if (!result.Keys.Contains(key))
                {
                    result[key] = new List<IStepContentModel>();
                }
                result[key].Add(item);
            }
            return result;
        }
        /// <summary>
        /// TODO: document me
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private static IDictionary<string, List<IStepContentModel>> OrderContentByElementName(IEnumerable<IStepContentModel> collection)
        {
            IDictionary<string, List<IStepContentModel>> result = new Dictionary<string, List<IStepContentModel>>(collection.Count());

            foreach (var item in collection)
            {
                string key = item.ElementName;
                if (!result.Keys.Contains(key))
                {
                    result[key] = new List<IStepContentModel>();
                }
                result[key].Add(item);
            }
            return result;
        }
        /// <summary>
        /// Constructs the section title lines.
        /// </summary>
        /// <param name="dictionary">step content webmodel collection</param>
        /// <returns>dictionary of string and step content model</returns>
        private static IDictionary<string, List<IStepContentModel>> ConstructSummarySectionTitle(IDictionary<string, List<IStepContentModel>> dictionary)
        {
            IDictionary<string, List<IStepContentModel>> result = new Dictionary<string, List<IStepContentModel>>(dictionary.Count());
            //
            // 
            //
            foreach (var key in dictionary.Keys)
            {
                List<IStepContentModel> contentList = dictionary[key];
                string newKey = string.Empty;
                foreach (IStepContentModel model in contentList)
                {
                    //
                    // Construct a title line
                    //
                    newKey = FormatContentSectionTitleLine(model.ElementName, model.ElementContentAverageScore, model.ElementOverallFlag, model.ElementTextFlag);
                    if (model.ElementContentAverageScore != null)
                    {
                        //
                        // We have a good key so leave; otherwise try the next entry
                        //
                        break;
                    }
                }
                result[newKey] = new List<IStepContentModel>(contentList);
            }
            return result;
        }
        /// <summary>
        /// Formats the Content section title line.  If the content describes an average score
        /// then the average score is concatenated to the element name.
        /// </summary>
        /// <param name="elementName">content element name</param>
        /// <param name="averageScore">Average score value</param>
        /// <param name="isOverallScore">Overall score flag; indicates if the score is an overall score</param>
        /// <returns>The section title formatted for display</returns>
        private static string FormatContentSectionTitleLine(string elementName, decimal? averageScore, bool isOverallScore, bool hasTextToDisplay)
        {
            decimal value = averageScore.HasValue ? averageScore.Value : 0;

            string formattedText = string.Empty;

            if (hasTextToDisplay)
            {
                if (value != 0)
                {
                    formattedText = string.Format(ContentTitleLine, elementName, ViewHelpers.ScoreFormatter(value));
                }
                else
                {
                    formattedText = elementName;
                }
            }

            return formattedText;
        }
        #endregion
    }
}