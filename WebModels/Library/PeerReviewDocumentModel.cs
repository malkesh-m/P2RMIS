using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.Library
{
    /// <summary>
    /// Peer review document model
    /// </summary>
    /// <seealso cref="Sra.P2rmis.WebModels.Library.IPeerReviewDocumentModel" />
    public class PeerReviewDocumentModel : IPeerReviewDocumentModel
    {
        public PeerReviewDocumentModel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PeerReviewDocumentModel"/> class.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="heading">The heading.</param>
        /// <param name="description">The description.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="contentTypeAccessMethod">The content type access method.</param>
        /// <param name="documentTypeId">The document type identifier.</param>
        /// <param name="documentType">Type of the document.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="program">The program.</param>
        /// <param name="trainingCategoryId">The training category identifier.</param>
        /// <param name="trainingCategoryName">Name of the training category.</param>
        /// <param name="contentUrl">The content URL.</param>
        /// <param name="contentFileLocation">The content file location.</param>
        /// <param name="active">if set to <c>true</c> [active].</param>
        public PeerReviewDocumentModel(int documentId, int clientId, string heading, string description, string contentType, string contentTypeAccessMethod, int? documentTypeId, string documentType, 
            string fiscalYear, string program, int? trainingCategoryId, string trainingCategoryName, string contentUrl, string contentFileLocation, bool active)
        {
            DocumentId = documentId;
            ClientId = clientId;
            Heading = heading;
            Description = description;
            ContentType = contentType;
            ContentTypeAccessMethod = contentTypeAccessMethod;
            DocumentTypeId = documentTypeId;
            DocumentType = documentType;
            FiscalYear = fiscalYear;
            Program = program;
            TrainingCategoryId = trainingCategoryId;
            TrainingCategoryName = trainingCategoryName;
            ContentUrl = contentUrl;
            ContentFileLocation = contentFileLocation;
            Active = active;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PeerReviewDocumentModel"/> class.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="heading">The heading.</param>
        /// <param name="description">The description.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="contentTypeAccessMethod">The content type access method.</param>
        /// <param name="documentTypeId">The document type identifier.</param>
        /// <param name="documentType">Type of the document.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="program">The program.</param>
        /// <param name="trainingCategoryId">The training category identifier.</param>
        /// <param name="trainingCategoryName">Name of the training category.</param>
        /// <param name="contentUrl">The content URL.</param>
        /// <param name="contentFileLocation">The content file location.</param>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <param name="createdDate">The created date.</param>
        /// <param name="createdByName">Name of the created by.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <param name="modifiedByName">Name of the modified by.</param>
        public PeerReviewDocumentModel(int documentId, int clientId, string heading, string description, string contentType, string contentTypeAccessMethod, int? documentTypeId, string documentType, 
            string fiscalYear, string program, int? trainingCategoryId, string trainingCategoryName, string contentUrl, string contentFileLocation, bool active, 
            DateTime? createdDate, string createdByName, DateTime? modifiedDate, string modifiedByName) : this(documentId, clientId, heading, description, contentType, contentTypeAccessMethod,
                documentTypeId, documentType, fiscalYear, program, trainingCategoryId, trainingCategoryName, contentUrl, contentFileLocation, active)
        {
            CreatedDate = createdDate;
            CreatedByName = createdByName;
            ModifiedDate = modifiedDate;
            ModifiedByName = modifiedByName;
        }
        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <value>
        /// The document identifier.
        /// </value>
        public int DocumentId { get; set; }
        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public int ClientId { get; set; }
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
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; }
        /// <summary>
        /// Gets or sets the content type access method.
        /// </summary>
        /// <value>
        /// The content type access method.
        /// </value>
        public string ContentTypeAccessMethod { get; set; }
        /// <summary>
        /// Gets or sets the document type identifier.
        /// </summary>
        /// <value>
        /// The document type identifier.
        /// </value>
        public int? DocumentTypeId { get; set; }
        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        /// <value>
        /// The type of the document.
        /// </value>
        public string DocumentType { get; set; }
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
        /// Gets or sets the training category identifier.
        /// </summary>
        /// <value>
        /// The training category identifier.
        /// </value>
        public int? TrainingCategoryId { get; set; }
        /// <summary>
        /// Gets or sets the name of the training category.
        /// </summary>
        /// <value>
        /// The name of the training category.
        /// </value>
        public string TrainingCategoryName { get; set; }
        /// <summary>
        /// Gets or sets the content URL.
        /// </summary>
        /// <value>
        /// The content URL.
        /// </value>
        public string ContentUrl { get; set; }
        /// <summary>
        /// Gets or sets the content file location.
        /// </summary>
        /// <value>
        /// The content file location.
        /// </value>
        public string ContentFileLocation { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IPeerReviewDocumentModel" /> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the created by.
        /// </summary>
        /// <value>
        /// The name of the created by.
        /// </value>
        public string CreatedByName { get; set; }
        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the modified by.
        /// </summary>
        /// <value>
        /// The name of the modified by.
        /// </value>
        public string ModifiedByName { get; set; }
    }

    /// <summary>
    /// Peer review document interface
    /// </summary>
    public interface IPeerReviewDocumentModel
    {
        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <value>
        /// The document identifier.
        /// </value>
        int DocumentId { get; set; }
        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        int ClientId { get; set; }
        /// <summary>
        /// Gets or sets the heading.
        /// </summary>
        /// <value>
        /// The heading.
        /// </value>
        string Heading { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { get; set; }
        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        string ContentType { get; set; }
        /// <summary>
        /// Gets or sets the content type access method.
        /// </summary>
        /// <value>
        /// The content type access method.
        /// </value>
        string ContentTypeAccessMethod { get; set; }
        /// <summary>
        /// Gets or sets the document type identifier.
        /// </summary>
        /// <value>
        /// The document type identifier.
        /// </value>
        int? DocumentTypeId { get; set; }
        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        /// <value>
        /// The type of the document.
        /// </value>
        string DocumentType { get; set; }
        /// <summary>
        /// Gets or sets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        string FiscalYear { get; set; }
        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        string Program { get; set; }
        /// <summary>
        /// Gets or sets the training category identifier.
        /// </summary>
        /// <value>
        /// The training category identifier.
        /// </value>
        int? TrainingCategoryId { get; set; }
        /// <summary>
        /// Gets or sets the name of the training category.
        /// </summary>
        /// <value>
        /// The name of the training category.
        /// </value>
        string TrainingCategoryName { get; set; }
        /// <summary>
        /// Gets or sets the content URL.
        /// </summary>
        /// <value>
        /// The content URL.
        /// </value>
        string ContentUrl { get; set; }
        /// <summary>
        /// Gets or sets the content file location.
        /// </summary>
        /// <value>
        /// The content file location.
        /// </value>
        string ContentFileLocation { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IPeerReviewDocumentModel"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        bool Active { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the created by.
        /// </summary>
        /// <value>
        /// The name of the created by.
        /// </value>
        string CreatedByName { get; set; }
        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the modified by.
        /// </summary>
        /// <value>
        /// The name of the modified by.
        /// </value>
        string ModifiedByName { get; set; }
    }
}
