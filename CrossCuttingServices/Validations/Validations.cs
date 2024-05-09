using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.CrossCuttingServices.Validations
{
    /// <summary>
    /// Collection of common validation methods.
    /// </summary>
    public static class Validations
    {
        /// <summary>
        /// Tests the target string for a valid URI address
        /// </summary>
        /// <param name="target">Target string</param>
        /// <returns>True if the string is a valid URL; False otherwise</returns>
        public static bool IsValidUri(string target)
        {
            Uri uriResult;
            bool result = (Uri.TryCreate(target, UriKind.Absolute, out uriResult)) && 
                     (uriResult.Scheme == Uri.UriSchemeHttp 
                     || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }
        /// <summary>
        /// Determines if the date range is valid.
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>True if the date range is valid; false otherwise</returns>
        public static bool IsValidDateRange(DateTime? startDate, DateTime? endDate)
        {
            return ((startDate.HasValue && endDate.HasValue) && (startDate <= endDate));
        }

        /// <summary>
        /// Checks whether all elements in the target list are contained in the source list.
        /// </summary>
        /// <param name="sourceList">The source list.</param>
        /// <param name="targetList">The target list.</param>
        /// <returns>true is all elements are found; otherwise false</returns>
        public static bool DoListsMatch(IList<int> sourceList, IList<int> targetList)
        {
            return !targetList.Except(sourceList).Any();
        }
    }
}
