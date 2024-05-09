using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Library;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// Training document view model
    /// </summary>
    public class TrainingDocumentViewModel
    {
        /// <summary>
        /// Initializes a new instance of the ITrainingDocumentModel class.
        /// </summary>
        /// <param name="trainingDocument">The training document.</param>
        public TrainingDocumentViewModel(ITrainingDocumentModel trainingDocument)
        {
            Category = trainingDocument.TrainingCategoryName;
            Title = trainingDocument.Name;
            Description = trainingDocument.Description;
            FileType = trainingDocument.FileType ?? "N/A";
            IsReviewed = trainingDocument.ReviewedDate.HasValue;
            ContentUrl = trainingDocument.ContentUrl;
            ReviewedDate = ViewHelpers.FormatDate(trainingDocument.ReviewedDate);
            DocumentId = trainingDocument.TrainingDocumentId;
            ContentType = trainingDocument.ContentType;
            IsVideo = trainingDocument.IsVideo;
            IsLink = trainingDocument.IsLink;
        }

        /// <summary>
        /// Gets the document identifier.
        /// </summary>
        /// <value>
        /// The document identifier.
        /// </value>
        public int DocumentId { get; private set; }
        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; private set; }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; private set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the file location.
        /// </summary>
        /// <value>
        /// The file location.
        /// </value>
        public string ContentUrl { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the type of the file.
        /// </summary>
        /// <value>
        /// The type of the file.
        /// </value>
        public string FileType { get; private set; }

        /// <summary>
        /// Gets the reviewed date.
        /// </summary>
        /// <value>
        /// The reviewed date.
        /// </value>
        public string ReviewedDate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is reviewed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is reviewed; otherwise, <c>false</c>.
        /// </value>
        public bool IsReviewed { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is video.
        /// </summary>
        /// <value>
        ///   if this instance is video; otherwise, <c>false</c>.
        /// </value>
        public bool IsVideo { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is link (URL).
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is link; otherwise, <c>false</c>.
        /// </value>
        public bool IsLink { get; private set; }
    }
}