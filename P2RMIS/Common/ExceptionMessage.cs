using System;
using System.Linq;

namespace Sra.P2rmis.Web.Common
{
    public class ExceptionMessage
    {
        /// <summary>
        /// Gets the missing critiques.
        /// </summary>
        /// <value>
        /// The missing critiques.
        /// </value>
        public static string MissingReviewerCritiques
        {
            get { return "There are no reviewer critiques found."; }
        }
    }
}