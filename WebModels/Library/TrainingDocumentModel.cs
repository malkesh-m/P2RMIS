using System;
using Sra.P2rmis.WebModels;

namespace Sra.P2rmis.WebModels.Library
{
    /// <summary>
    /// Interface for the training document information
    /// </summary>
    public interface ITrainingDocumentModel : IEditable, IBuiltModel
    {
        /// <summary>
        /// The training category identifier
        /// </summary>
        int TrainingCategoryId { get; }
        /// <summary>
        /// The training document identifier
        /// </summary>
        int TrainingDocumentId { get; }
        /// <summary>
        /// The training category name
        /// </summary>
        string TrainingCategoryName { get; }
        /// <summary>
        /// The document name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The document description
        /// </summary>
        string Description { get; }
        /// <summary>
        /// The file type
        /// </summary>
        string FileType { get; }
        /// <summary>
        /// The file reviewed date
        /// </summary>
        DateTime? ReviewedDate { get; }
        /// <summary>
        /// File reviewed
        /// </summary>
        bool? Reviewed { get; }
        /// <summary>
        /// Gets the type of the training document content (link, file, video).
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        string ContentType { get; }

        /// <summary>
        /// Gets the file location (URL).
        /// </summary>
        /// <value>
        /// The file location (URL).
        /// </value>
        string ContentUrl { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is video.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is video; otherwise, <c>false</c>.
        /// </value>
        bool IsVideo { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is link.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is link; otherwise, <c>false</c>.
        /// </value>
        bool IsLink { get; }
    }
    /// <summary>
    /// Class containing the training document information
    /// </summary>
    public class TrainingDocumentModel : Editable, ITrainingDocumentModel
    {
        #region Constructor & Setup

        #endregion
        #region Attribute
        /// <summary>
        /// The training category identifier
        /// </summary>
        public int TrainingCategoryId { get; set; }
        /// <summary>
        /// The training document identifier
        /// </summary>
        public int TrainingDocumentId { get; set; }
        /// <summary>
        /// The training category name
        /// </summary>
        public string TrainingCategoryName { get; set; }
        /// <summary>
        /// The document name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The document description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The file type
        /// </summary>
        public string FileType { get; set; }
        /// <summary>
        /// The file reviewed date
        /// </summary>
        public DateTime? ReviewedDate { get; set; }
        /// <summary>
        /// File reviewed
        /// </summary>
        public bool? Reviewed { get; set; }
        /// <summary>
        /// Gets the type of the training document content (link, file, video).
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; }
        #endregion
        #region Editable
        /// <summary>
        /// Delete the reviewed date
        /// </summary>
        public override bool IsDeletable { get; set; }
        /// <summary>
        /// Delete the reviewed date 
        /// </summary>
        /// <returns></returns>
        public override bool IsDeleted()
        {
            return this.IsDeletable;
        }
        /// <summary>
        /// Does the review date have data?
        /// </summary>
        /// <returns>True if the model has data; false otherwise</returns>
        public override bool HasData()
        {
            return ReviewedDate.HasValue;
        }

        /// <summary>
        /// Gets the file location (URL).
        /// </summary>
        /// <value>
        /// The file location (URL).
        /// </value>
        public string ContentUrl { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is video.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is video; otherwise, <c>false</c>.
        /// </value>
        public bool IsVideo { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is link.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is link; otherwise, <c>false</c>.
        /// </value>
        public bool IsLink { get; set; }
        #endregion
    }
}
