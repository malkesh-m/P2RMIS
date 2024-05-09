using System;
using System.Linq;
using Sra.P2rmis.Bll.ModelBuilders.Library;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Library;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.LibraryService
{
    /// <summary>
    /// Library provides services related to library documents
    /// </summary>
    public class LibraryService : ServerBase,  ILibraryService
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public LibraryService()
        {
            UnitOfWork = new UnitOfWork();
        }
        #endregion
        #region Services
        /// <summary>
        /// Gets a collection of the training document model representing those training 
        /// documents for the specified program that are available for the user to view
        /// </summary>
        /// <param name="programYearId">The program year identifier</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="hasUnrestrictedPermission">Whether the user has unrestricted access to library documents for the given program year</param>
        /// <returns>Program year model object</returns>
        public Container<ITrainingDocumentModel> GetTrainingDocuments(int programYearId, int userId, bool hasUnrestrictedPermission)
        {
            Validate("LibraryService.GetTrainingDocuments", programYearId, nameof(programYearId), userId, nameof(userId));

            var builder = new TrainingDocumentModelBuilder(UnitOfWork, programYearId, userId, hasUnrestrictedPermission);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Gets the peer review documents.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <returns></returns>
        public List<IPeerReviewDocumentModel> GetPeerReviewDocuments(int clientId, string fiscalYear, int? clientProgramId)
        {
            Validate("LibraryService.GetPeerReviewDocuments", clientId, nameof(clientId));

            var docs = UnitOfWork.PeerReviewDocumentRepository.GetList(clientId, fiscalYear, clientProgramId);
            var models = docs.AsEnumerable().Select(x => new PeerReviewDocumentModel(x.PeerReviewDocumentId, x.ClientId, x.Heading, x.Description, x.PeerReviewContentType.ContentType, 
                x.PeerReviewContentType.AccessMethod, x.PeerReviewDocumentTypeId, x.PeerReviewDocumentType.DocumentType,
                x.FiscalYear, x.ClientProgram?.ProgramAbbreviation, x.TrainingCategoryId,
                x.TrainingCategory?.CategoryName, x.ContentUrl, x.ContentFileLocation, !x.ArchivedFlag) as IPeerReviewDocumentModel).ToList();
            return models;
        }
        /// <summary>
        /// Gets the peer review document.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <returns></returns>
        public IPeerReviewDocumentModel GetPeerReviewDocument(int documentId)
        {
            Validate("LibraryService.GetPeerReviewDocument", documentId, nameof(documentId));

            var doc = UnitOfWork.PeerReviewDocumentRepository.GetByID(documentId);
            string createdByName = doc.CreatedBy != null ? UnitOfWork.UserRepository.GetByID(doc.CreatedBy).FullName() : null;
            string modifiedByName = doc.ModifiedBy != null ? UnitOfWork.UserRepository.GetByID(doc.ModifiedBy).FullName() : null;
            var model = new PeerReviewDocumentModel(doc.PeerReviewDocumentId, doc.ClientId, doc.Heading, doc.Description, doc.PeerReviewContentType.ContentType, 
                doc.PeerReviewContentType.AccessMethod, doc.PeerReviewDocumentTypeId, doc.PeerReviewDocumentType.DocumentType,
                doc.FiscalYear, doc.ClientProgram?.ProgramAbbreviation, doc.TrainingCategoryId, doc.TrainingCategory?.CategoryName, 
                doc.ContentUrl, doc.ContentFileLocation, !doc.ArchivedFlag, doc.CreatedDate, createdByName, doc.ModifiedDate, modifiedByName) as IPeerReviewDocumentModel;
            return model;
        }
        /// <summary>
        /// Gets the peer review document access list by document identifier.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <returns></returns>
        public IPeerReviewDocumentAccessModel GetPeerReviewDocumentAccessByDocumentId(int documentId)
        {
            Validate("LibraryService.GetPeerReviewDocument", documentId, nameof(documentId));

            var access = UnitOfWork.PeerReviewDocumentRepository.GetAccessByDocumentId(documentId);
            var model = access != null ? new PeerReviewDocumentAccessModel(access.MeetingTypeIds, access.ClientParticipantTypeIds,
                access.ParticipationMethodIds, access.RestrictedAssignedFlag) as IPeerReviewDocumentAccessModel :
                new PeerReviewDocumentAccessModel() as IPeerReviewDocumentAccessModel;
            return model;
        }
        /// <summary>
        /// Gets the content types.
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<int, string>> GetContentTypes()
        {
            var types = UnitOfWork.PeerReviewDocumentRepository.GetContentTypes().OrderBy(x => x.SortOrder);
            var models = types.ToList().ConvertAll(x => new KeyValuePair<int, string>(x.PeerReviewContentTypeId, x.ContentType));
            return models;
        }
        /// <summary>
        /// Gets the document types.
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<int, string>> GetDocumentTypes()
        {
            var types = UnitOfWork.PeerReviewDocumentRepository.GetDocumentTypes().OrderBy(x => x.SortOrder);
            var models = types.ToList().ConvertAll(x => new KeyValuePair<int, string>(x.PeerReviewDocumentTypeId, x.DocumentType));
            return models;
        }
        /// <summary>
        /// Gets the training categories.
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<int, string>> GetTrainingCategories()
        {
            var categories = UnitOfWork.TrainingCategoryRepository.GetAll().OrderBy(x => x.SortOrder);
            var models = categories.ToList().ConvertAll(x => new KeyValuePair<int, string>(x.TrainingCategoryId, x.CategoryName));
            return models;
        }
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
        public IPeerReviewDocumentModel AddPeerReviewDocument(int clientId, string fiscalYear, int? clientProgramId,
            string heading, string description, int contentTypeId, int documentTypeId, int? trainingCategoryId, string contentUrl,
            string contentFileLocation, int userId)
        {
            ValidateAddPeerReviewDocument("LibraryService.AddPeerReviewDocument", clientId, contentTypeId, documentTypeId, contentUrl, contentFileLocation, userId);

            var contentType = UnitOfWork.PeerReviewDocumentRepository.GetContentType(contentTypeId);
            var documentType = UnitOfWork.PeerReviewDocumentRepository.GetDocumenType(documentTypeId);
            var fileType = contentFileLocation != null ? System.IO.Path.GetExtension(contentFileLocation) : null;
            var doc = UnitOfWork.PeerReviewDocumentRepository.Add(clientId, fiscalYear, clientProgramId, heading, description, contentType, documentType, trainingCategoryId,
                contentUrl, contentFileLocation, fileType, userId);
            UnitOfWork.Save();
            var model = new PeerReviewDocumentModel(doc.PeerReviewDocumentId, doc.ClientId, doc.Heading, doc.Description, doc.PeerReviewContentType.ContentType, 
                doc.PeerReviewContentType.AccessMethod, doc.PeerReviewDocumentTypeId, doc.PeerReviewDocumentType.DocumentType,
                doc.FiscalYear, doc.ClientProgram?.ProgramAbbreviation, doc.TrainingCategoryId, doc.TrainingCategory?.CategoryName, 
                doc.ContentUrl, doc.ContentFileLocation, !doc.ArchivedFlag) as IPeerReviewDocumentModel;
            return model;
        }
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
        public void UpdatePeerReviewDocument(int documentId, string fiscalYear, int? clientProgramId,
            string heading, string description, int documentTypeId, int? trainingCategoryId, int userId)
        {
            Validate("LibraryService.UpdatePeerReviewDocument", documentId, nameof(documentId), documentTypeId, nameof(documentTypeId), userId, nameof(userId));

            UnitOfWork.PeerReviewDocumentRepository.Update(documentId, fiscalYear, clientProgramId, heading, description, documentTypeId, trainingCategoryId, userId);
            UnitOfWork.Save();
        }
        /// <summary>
        /// Adds the peer review document access.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="meetingTypeIds">The meeting type ids.</param>
        /// <param name="participantTypeIds">The participant type ids.</param>
        /// <param name="participationMethodId">The participation method identifier.</param>
        /// <param name="participationLevel">The participation level.</param>
        /// <param name="userId">The user identifier.</param>
        public void AddPeerReviewDocumentAccess(int documentId, string meetingTypeIds, string participantTypeIds, string participationMethodIds, bool? participationLevel, int userId)
        {
            Validate("LibraryService.AddPeerReviewDocumentAccess", documentId, nameof(documentId), userId, nameof(userId));

            UnitOfWork.PeerReviewDocumentRepository.AddAccess(documentId, meetingTypeIds, participantTypeIds, participationMethodIds, participationLevel, userId);    
            UnitOfWork.Save();
        }
        /// <summary>
        /// Updates the peer review document access.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="meetingTypeIds">The meeting type ids.</param>
        /// <param name="participantTypeIds">The participant type ids.</param>
        /// <param name="participationMethodId">The participation method identifier.</param>
        /// <param name="participationLevel">The participation level.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdatePeerReviewDocumentAccess(int documentId, string meetingTypeIds, string participantTypeIds, string participationMethodIds, bool? participationLevel, int userId)
        {
            Validate("LibraryService.UpdatePeerReviewDocumentAccess", documentId, nameof(documentId), userId, nameof(userId));
            
            UnitOfWork.PeerReviewDocumentRepository.UpdateAccess(documentId, meetingTypeIds, participantTypeIds, participationMethodIds, participationLevel, userId);
            UnitOfWork.Save();
        }
        /// <summary>
        /// Archives the document.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void ArchiveDocument(int documentId, int userId)
        {
            Validate("LibraryService.ArchiveDocument", documentId, nameof(documentId), userId, nameof(userId));

            UnitOfWork.PeerReviewDocumentRepository.Archive(documentId, userId);
            UnitOfWork.Save();
        }
        /// <summary>
        /// Unarchives the document.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void UnarchiveDocument(int documentId, int userId)
        {
            Validate("LibraryService.UnarchiveDocument", documentId, nameof(documentId), userId, nameof(userId));

            UnitOfWork.PeerReviewDocumentRepository.Unarchive(documentId, userId);
            UnitOfWork.Save();
        }
        /// <summary>
        /// Deletes the document.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteDocument(int documentId, int userId)
        {
            Validate("LibraryService.DeleteDocument", documentId, nameof(documentId), userId, nameof(userId));

            UnitOfWork.PeerReviewDocumentRepository.Delete(documentId, userId);
            UnitOfWork.Save();
        }


        /// <summary>
        /// Marks the document reviewed.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="peerReviewDocumentId">The peer review document identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void MarkDocumentReviewed(int programYearId, int peerReviewDocumentId, int userId)
        {
            Validate("LibraryService.MarkDocumentReviewed", peerReviewDocumentId, nameof(peerReviewDocumentId), userId, nameof(userId), programYearId, nameof(programYearId));

            UnitOfWork.UserPeerReviewDocumentRepository.Add(programYearId, peerReviewDocumentId, userId);
            UnitOfWork.Save();
        }
        #endregion 
        #region Helpers
        /// <summary>
        /// validates the parameters for GetTrainingDocuments
        /// </summary>
        /// <param name="programYearId">The program year identifier</param>
        /// <param name="userId">The user identifier</param>
        private void ValidateGetTrainingDocumentsParameters(int programYearId, int userId)
        {
            ValidateInteger(programYearId, "LibraryController.GetTrainingDocuments", "programYearId");
            ValidateInteger(userId, "LibraryController.GetTrainingDocuments", "userId");
        }
        /// <summary>
        /// validates the parameters for SetTrainingDocumentReviewStatus
        /// </summary>
        /// <param name="userTrainingDocumentId"></param>
        /// <param name="userId">The user identifier</param>
        private void ValidateSetTrainingDocumentReviewStatusParameters(int trainingDocumentId, int userId)
        {
            ValidateInteger(trainingDocumentId, "LibraryController.SetTrainingDocumentReviewStatus", "trainingDocumentId");
            ValidateInteger(userId, "LibraryController.SetTrainingDocumentReviewStatus", "userId");
        }
        /// <summary>
        /// Validates the add peer review document.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="contentTypeId">The content type identifier.</param>
        /// <param name="documentTypeId">The document type identifier.</param>
        /// <param name="contentUrl">The content URL.</param>
        /// <param name="contentFileLocation">The content file location.</param>
        /// <param name="userId">The user identifier.</param>
        private void ValidateAddPeerReviewDocument(string methodName, int clientId, int contentTypeId, int documentTypeId, string contentUrl,
            string contentFileLocation, int userId)
        {
            Validate(methodName, clientId, nameof(clientId), contentTypeId, nameof(contentTypeId),
                documentTypeId, nameof(documentTypeId), userId, nameof(userId));

            if (contentTypeId == PeerReviewDocument.Indexes.FileOrImage)
            {
                ValidateString(contentFileLocation, methodName, nameof(contentFileLocation));
            }
            else
            {
                ValidateString(contentUrl, methodName, nameof(contentUrl));
            }
        }
        #endregion
    }
}
