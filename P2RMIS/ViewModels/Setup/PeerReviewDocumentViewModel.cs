using System;
using Sra.P2rmis.WebModels.Library;

namespace Sra.P2rmis.Web.ViewModels.Setup
{
    public class PeerReviewDocumentViewModel
    {
        private const string All = "All";
        private const string UrlAccessMethod = "web";
        private const string VideoAccessMethod = "embed";

        public PeerReviewDocumentViewModel(IPeerReviewDocumentModel model)
        {
            DocumentId = model.DocumentId;
            Heading = model.Heading;
            Description = model.Description;
            IsUrl = model.ContentTypeAccessMethod == UrlAccessMethod;
            IsVideo = model.ContentTypeAccessMethod == VideoAccessMethod;
            DocType = model.TrainingCategoryName != null ?
                String.Format("{0} - {1}", model.DocumentType, model.TrainingCategoryName) : model.DocumentType;
            FiscalYear = (model.FiscalYear) == null ? All : model.FiscalYear;
            Program = (model.Program) == null ? All : model.Program;
            Path = !String.IsNullOrEmpty(model.ContentUrl) ? model.ContentUrl : model.ContentFileLocation;
            DocumentTypeId = model.DocumentTypeId ?? 0;
            TrainingCategoryId = model.TrainingCategoryId ?? 0;
            Active = model.Active;
        }
        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <value>
        /// The document identifier.
        /// </value>
        public int DocumentId { get; set; }
        /// <summary>
        /// Gets or sets the heading.
        /// </summary>
        /// <value>
        /// The heading.
        /// </value>
        public string Heading { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is video.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is video; otherwise, <c>false</c>.
        /// </value>
        public bool IsVideo { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is URL.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is URL; otherwise, <c>false</c>.
        /// </value>
        public bool IsUrl { get; set; }
        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        /// <value>
        /// The type of the document.
        /// </value>
        public string DocType { get; set; }
        /// <summary>
        /// Gets or sets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        public string FiscalYear { get; set; }
        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        public string Program { get; set; }
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; set; }
        /// <summary>
        /// Gets the Training category id.
        /// </summary>
        public int TrainingCategoryId { get; set; }
        /// <summary>
        /// Gets the document id for the document category
        /// </summary>
        public int DocumentTypeId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PeerReviewDocumentViewModel"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

    }
}