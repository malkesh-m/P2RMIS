using System;
using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices.MessageServices;

namespace Sra.P2rmis.WebModels.Setup
{
    public interface IImportLogModel
    {
        /// <summary>
        /// Gets or sets the import log identifier.
        /// </summary>
        /// <value>
        /// The import log identifier.
        /// </value>
        int ImportLogId { get; set; }
        /// <summary>
        /// Gets or sets import date.
        /// </summary>
        DateTime ImportDate { get; set; }
        /// <summary>
        /// Generic message.
        /// </summary>
        string GenericMessage { get; set; }
        /// <summary>
        /// Gets or sets the application level messages.
        /// </summary>
        /// <value>
        /// The application level messages.
        /// </value>
        List<string> ApplicationMessages { get; set; }
        /// <summary>
        /// Gets or sets imported count.
        /// </summary>
        int ImportedCount { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [success flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [success flag]; otherwise, <c>false</c>.
        /// </value>
        bool? SuccessFlag { get; set; }
        /// <summary>
        /// Gets Messages.
        /// </summary>
        List<string> Messages { get; }
    }

    public class ImportLogModel : IImportLogModel
    {
        public ImportLogModel() { }

        public ImportLogModel(int importLogId, DateTime? importDate, 
            string genericMessage, List<string> applicationMessages, 
            int importedCount, bool? successFlag)
        {
            ImportLogId = importLogId;
            ImportDate = (DateTime)importDate;
            GenericMessage = genericMessage;
            ApplicationMessages = applicationMessages;
            ImportedCount = importedCount;
            SuccessFlag = successFlag;
        }
        /// <summary>
        /// Constant "All".
        /// </summary>
        public const string ALL = "All";

        /// <summary>
        /// Gets or sets the import log identifier.
        /// </summary>
        /// <value>
        /// The import log identifier.
        /// </value>
        public int ImportLogId { get; set; }
        /// <summary>
        /// Gets or sets import date.
        /// </summary>
        public DateTime ImportDate { get; set; }
        /// <summary>
        /// Generic message.
        /// </summary>
        public string GenericMessage { get; set; }
        /// <summary>
        /// Gets or sets the application level messages.
        /// </summary>
        /// <value>
        /// The application level messages.
        /// </value>
        public List<string> ApplicationMessages { get; set; } = new List<string>();
        /// <summary>
        /// Gets or sets imported count.
        /// </summary>
        public int ImportedCount { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [success flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [success flag]; otherwise, <c>false</c>.
        /// </value>
        public bool? SuccessFlag { get; set; }
        /// <summary>
        /// Messages
        /// </summary>
        public List<string> Messages {
            get
            {
                var msgs = new List<string>();
                if (SuccessFlag == true && !string.IsNullOrEmpty(GenericMessage))
                {
                    msgs.Add(GenericMessage);
                }
                else if (SuccessFlag == false)
                {
                    var prefix = ApplicationMessages.Count > 0 ? 
                        ApplicationMessages.Count.ToString() : ALL;
                    msgs.Add(string.Format(MessageService.ImportFailureTitle, prefix));
                    if (!string.IsNullOrEmpty(GenericMessage))
                    {
                        msgs.Add(GenericMessage);
                    }
                    msgs.AddRange(ApplicationMessages);
                }
                return msgs;
            }
        }
    }
}
