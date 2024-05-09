using System;
using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.ViewModels.Setup
{
    /// <summary>
    /// Import error view model
    /// </summary>
    public class ImportErrorViewModel
    {
        public ImportErrorViewModel(List<string> messages, int failedCount,
            DateTime lastImportDate)
        {
            Messages = messages;
            // Trim title message as needed
            if (Messages.Count > 1)
                Messages.RemoveAt(0);
            FailedCount = failedCount;
            LastImportDate = ViewHelpers.FormatDate(lastImportDate);
        }

        /// <summary>
        /// Messages.
        /// </summary>
        public List<string> Messages { get; set; }
        /// <summary>
        /// Failed count.
        /// </summary>
        public int FailedCount { get; set; }
        /// <summary>
        /// Last import date.
        /// </summary>
        public string LastImportDate { get; set; }
    }
}