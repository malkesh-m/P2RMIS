using System;
using Sra.P2rmis.WebModels.Library;
using Sra.P2rmis.Bll.Views;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.LibraryService
{
    /// <summary>
    /// Library provides services related to library documents
    /// </summary>
    public interface ILibraryService
    {
        /// <summary>
        /// Gets a collection of the training document model representing those
        /// training documents available for the user to view
        /// </summary>
        /// <param name="programYearId">The program year identifier</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="hasUnrestrictedPermission">if set to <c>true</c> [has unrestricted permission].</param>
        /// <returns>
        /// Program year model object
        /// </returns>
        Container<ITrainingDocumentModel> GetTrainingDocuments(int programYearId, int userId, bool hasUnrestrictedPermission);
        /// <summary>
        /// Gets the peer review documents.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <returns></returns>
        List<IPeerReviewDocumentModel> GetPeerReviewDocuments(int clientId, string fiscalYear, int? clientProgramId);
        /// <summary>
        /// Gets the peer review document.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <returns></returns>
        IPeerReviewDocumentModel GetPeerReviewDocument(int documentId);
        /// <summary>
        /// Gets the peer review document access by document identifier.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <returns></returns>
        IPeerReviewDocumentAccessModel GetPeerReviewDocumentAccessByDocumentId(int documentId);
        /// <summary>
        /// Gets the content types.
        /// </summary>
        /// <returns></returns>
        List<KeyValuePair<int, string>> GetContentTypes();
        /// <summary>
        /// Gets the document types.
        /// </summary>
        /// <returns></returns>
        List<KeyValuePair<int, string>> GetDocumentTypes();
        /// <summary>
        /// Gets the training categories.
        /// </summary>
        /// <returns></returns>
        List<KeyValuePair<int, string>> GetTrainingCategories();
        /// <summary>
        /// Adds the peer review document.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <param name="heading">The heading.</param>
        /// <param name="description">The description.</param>
        /// <param name="contentTypeId">The content type identifier.</param>
        /// <param name="documentTypeId">The document type identifier.</param>
        /// <param name="trainingCategoryId">The training category identifier.</param>
        /// <param name="contentUrl">The content URL.</param>
        /// <param name="contentFileLocation">The content file location.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IPeerReviewDocumentModel AddPeerReviewDocument(int clientId, string fiscalYear, int? clientProgramId,
            string heading, string description, int contentTypeId, int documentTypeId, int? trainingCategoryId, string contentUrl,
            string contentFileLocation, int userId);
        /// <summary>
        /// Updates the peer review document.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <param name="heading">The heading.</param>
        /// <param name="description">The description.</param>
        /// <param name="documentTypeId">The document type identifier.</param>
        /// <param name="trainingCategoryId">The training category identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void UpdatePeerReviewDocument(int documentId, string fiscalYear, int? clientProgramId,
            string heading, string description, int documentTypeId, int? trainingCategoryId, int userId);
        /// <summary>
        /// Adds the peer review document access.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="meetingTypeIds">The meeting type ids.</param>
        /// <param name="participantTypeIds">The participant type ids.</param>
        /// <param name="participationMethodIds">The participation method ids.</param>
        /// <param name="participationLevel">The participation level.</param>
        /// <param name="userId">The user identifier.</param>
        void AddPeerReviewDocumentAccess(int documentId, string meetingTypeIds, string participantTypeIds, string participationMethodIds, bool? participationLevel, int userId);
        /// <summary>
        /// Updates the peer review document access.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="meetingTypeIds">The meeting type ids.</param>
        /// <param name="participantTypeIds">The participant type ids.</param>
        /// <param name="participationMethodIds">The participation method ids.</param>
        /// <param name="participationLevel">The participation level.</param>
        /// <param name="userId">The user identifier.</param>
        void UpdatePeerReviewDocumentAccess(int documentId, string meetingTypeIds, string participantTypeIds, string participationMethodIds, bool? participationLevel, int userId);
        /// <summary>
        /// Archives the document.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void ArchiveDocument(int documentId, int userId);
        /// <summary>
        /// Unarchives the document.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void UnarchiveDocument(int documentId, int userId);
        /// <summary>
        /// Deletes the document.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void DeleteDocument(int documentId, int userId);
        /// <summary>
        /// Marks the document reviewed.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void MarkDocumentReviewed(int programYearId, int documentId, int userId);
    }
}
