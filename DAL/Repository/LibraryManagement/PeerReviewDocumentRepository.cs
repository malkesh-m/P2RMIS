
using System.Linq;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal.Repository.LibraryManagement
{
    /// <summary>
    /// Provides database access to PeerReviewDocument entities.
    /// </summary>
    public interface IPeerReviewDocumentRepository : IGenericRepository<PeerReviewDocument>
    {
        /// <summary>
        /// Adds the specified client identifier.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <param name="heading">The heading.</param>
        /// <param name="description">The description.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="documentType">Type of the document.</param>
        /// <param name="trainingCateogryId">The training cateogry identifier.</param>
        /// <param name="contentUrl">The content URL.</param>
        /// <param name="contentFileLocation">The content file location.</param>
        /// <param name="fileType">The file type.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        PeerReviewDocument Add(int clientId, string fiscalYear, int? clientProgramId, string heading, string description, PeerReviewContentType contentType,
            PeerReviewDocumentType documentType, int? trainingCateogryId, string contentUrl, string contentFileLocation, string fileType, int userId);
        /// <summary>
        /// Updates the specified document identifier.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <param name="heading">The heading.</param>
        /// <param name="description">The description.</param>
        /// <param name="documentTypeId">The document type identifier.</param>
        /// <param name="trainingCategoryId">The training category identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void Update(int documentId, string fiscalYear, int? clientProgramId, string heading, string description, int documentTypeId, int? trainingCategoryId, int userId);
        /// <summary>
        /// Adds the access.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="meetingTypeId">The meeting type identifier.</param>
        /// <param name="participantTypeId">The participant type identifier.</param>
        /// <param name="participationMethodId">The participation method identifier.</param>
        /// <param name="participationLevel">The participation level.</param>
        /// <param name="userId">The user identifier.</param>
        void AddAccess(int documentId, string meetingTypeId, string participantTypeId, string participationMethodId, bool? participationLevel, int userId);
        /// <summary>
        /// Updates the access.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="meetingTypeIds">The meeting type ids.</param>
        /// <param name="participantTypeIds">The participant type ids.</param>
        /// <param name="participationMethodIds">The participation method ids.</param>
        /// <param name="participationLevel">The participation level.</param>
        /// <param name="userId">The user identifier.</param>
        void UpdateAccess(int documentId, string meetingTypeIds, string participantTypeIds, string participationMethodIds, bool? participationLevel, int userId);
        /// <summary>
        /// Archives the specified document identifier.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void Archive(int documentId, int userId);
        /// <summary>
        /// Unarchives the specified document identifier.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void Unarchive(int documentId, int userId);
        /// <summary>
        /// Deletes the specified document identifier.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void Delete(int documentId, int userId);
        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <returns></returns>
        IQueryable<PeerReviewDocument> GetList(int clientId, string fiscalYear, int? clientProgramId);
        /// <summary>
        /// Gets the access by document identifier.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <returns></returns>
        PeerReviewDocumentAccess GetAccessByDocumentId(int documentId);
        /// <summary>
        /// Gets the content types.
        /// </summary>
        /// <returns></returns>
        IQueryable<PeerReviewContentType> GetContentTypes();
        /// <summary>
        /// Gets the document types.
        /// </summary>
        /// <returns></returns>
        IQueryable<PeerReviewDocumentType> GetDocumentTypes();
        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <param name="contentTypeId">The content type identifier.</param>
        /// <returns></returns>
        PeerReviewContentType GetContentType(int contentTypeId);
        /// <summary>
        /// Gets the type of the documen.
        /// </summary>
        /// <param name="documentTypeId">The document type identifier.</param>
        /// <returns></returns>
        PeerReviewDocumentType GetDocumenType(int documentTypeId);
    }
    /// <summary>
    /// Provides database access to PeerReviewDocument entities.
    /// </summary>
    public class PeerReviewDocumentRepository : GenericRepository<PeerReviewDocument>, IPeerReviewDocumentRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public PeerReviewDocumentRepository(P2RMISNETEntities context)
            : base(context)
        {
        }
        #endregion
        #region Services                        
        /// <summary>
        /// Adds the specified client identifier.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <param name="heading">The heading.</param>
        /// <param name="description">The description.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="documentType">Type of the document.</param>
        /// <param name="trainingCateogryId">The training cateogry identifier.</param>
        /// <param name="contentUrl">The content URL.</param>
        /// <param name="contentFileLocation">The content file location.</param>
        /// <param name="fileType">The file type.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public PeerReviewDocument Add(int clientId, string fiscalYear, int? clientProgramId, string heading, string description, PeerReviewContentType contentType,
            PeerReviewDocumentType documentType, int? trainingCateogryId, string contentUrl, string contentFileLocation, string fileType, int userId)
        {
            PeerReviewDocument model = new PeerReviewDocument();
            model.ClientId = clientId;
            model.FiscalYear = fiscalYear;
            model.ClientProgramId = clientProgramId;
            model.Heading = heading;
            model.Description = description;
            model.PeerReviewContentType = contentType;
            model.PeerReviewDocumentType = documentType;
            model.TrainingCategoryId = trainingCateogryId;
            model.ContentUrl = contentUrl;
            model.ContentFileLocation = contentFileLocation;
            model.FileType = fileType;
            Helper.UpdateCreatedFields(model, userId);
            context.PeerReviewDocuments.Add(model);

            return model;
        }

        /// <summary>
        /// Updates the specified document identifier.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <param name="heading">The heading.</param>
        /// <param name="description">The description.</param>
        /// <param name="documentTypeId">The document type identifier.</param>
        /// <param name="trainingCategoryId">The training category identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(int documentId, string fiscalYear, int? clientProgramId, string heading, string description, int documentTypeId, int? trainingCategoryId, int userId)
        {
            var model = GetByID(documentId);
            model.FiscalYear = fiscalYear;
            model.ClientProgramId = clientProgramId;
            model.Heading = heading;
            model.Description = description;
            model.PeerReviewDocumentTypeId = documentTypeId;
            model.TrainingCategoryId = trainingCategoryId;
            Helper.UpdateModifiedFields(model, userId);
        }
        /// <summary>
        /// Archives the specified document identifier.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void Archive(int documentId, int userId)
        {
            var model = GetByID(documentId);
            model.ArchivedFlag = true;
            model.ArchiveDate = GlobalProperties.P2rmisDateTimeNow;
            model.ArchivedBy = userId;
        }
        /// <summary>
        /// Unarchives the specified document identifier.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void Unarchive(int documentId, int userId)
        {
            var model = GetByID(documentId);
            model.ArchivedFlag = false;
            model.ArchiveDate = GlobalProperties.P2rmisDateTimeNow;
            model.ArchivedBy = userId;
        }
        /// <summary>
        /// Deletes the specified document identifier.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int documentId, int userId)
        {
            var model = GetByID(documentId);
            Helper.UpdateDeletedFields(model, userId);
            Delete(model);
        }
        /// <summary>
        /// Adds the access.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="meetingTypeIds">The meeting type identifier.</param>
        /// <param name="participantTypeIds">The participant type identifier.</param>
        /// <param name="participationMethodIds">The participation method identifier.</param>
        /// <param name="participationLevel">The participation level.</param>
        /// <param name="userId">The user identifier.</param>
        public void AddAccess(int documentId, string meetingTypeIds, string participantTypeIds, string participationMethodIds, bool? participationLevel, int userId)
        {
            PeerReviewDocumentAccess model = new PeerReviewDocumentAccess();
            model.PeerReviewDocumentId = documentId;
            model.MeetingTypeIds = meetingTypeIds;
            model.ClientParticipantTypeIds = participantTypeIds;
            model.ParticipationMethodIds = participationMethodIds;
            model.RestrictedAssignedFlag = participationLevel;
            Helper.UpdateCreatedFields(model, userId);
            context.PeerReviewDocumentAccesses.Add(model);
        }
        /// <summary>
        /// Updates the access.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="meetingTypeIds">The meeting type ids.</param>
        /// <param name="participantTypeIds">The participant type ids.</param>
        /// <param name="participationMethodIds">The participation method ids.</param>
        /// <param name="participationLevel">The participation level.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateAccess(int documentId, string meetingTypeIds, string participantTypeIds, string participationMethodIds, bool? participationLevel, int userId)
        {
            var model = GetAccess(documentId);
            if (model != null)
            {
                model.MeetingTypeIds = meetingTypeIds;
                model.ClientParticipantTypeIds = participantTypeIds;
                model.ParticipationMethodIds = participationMethodIds;
                model.RestrictedAssignedFlag = participationLevel;
                Helper.UpdateModifiedFields(model, userId);
            }
            else
            {
                AddAccess(documentId, meetingTypeIds, participantTypeIds, participationMethodIds, participationLevel, userId);
            }
        }
        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <returns></returns>
        public IQueryable<PeerReviewDocument> GetList(int clientId, string fiscalYear, int? clientProgramId)
        {
            var docList = this.Select().Where(x => x.ClientId == clientId);
            if (clientProgramId != null)
                docList = docList.Where(x => x.ClientProgramId == clientProgramId || x.ClientProgramId == null);
            if (fiscalYear != null)
                docList = docList.Where(x => x.FiscalYear == fiscalYear || x.FiscalYear == null);
            return docList;
        }
        /// <summary>
        /// Gets the access.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <returns></returns>
        public PeerReviewDocumentAccess GetAccess(int documentId)
        {
            var access = context.PeerReviewDocumentAccesses.FirstOrDefault(x => x.PeerReviewDocumentId == documentId);
            return access;
        }
        /// <summary>
        /// Gets the access by document identifier.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <returns></returns>
        public PeerReviewDocumentAccess GetAccessByDocumentId(int documentId)
        {
            var access = context.PeerReviewDocumentAccesses.FirstOrDefault(x => x.PeerReviewDocumentId == documentId);
            return access;
        }
        /// <summary>
        /// Gets the content types.
        /// </summary>
        /// <returns></returns>
        public IQueryable<PeerReviewContentType> GetContentTypes()
        {
            var list = context.PeerReviewContentTypes.AsQueryable<PeerReviewContentType>();
            return list;
        }
        /// <summary>
        /// Gets the document types.
        /// </summary>
        /// <returns></returns>
        public IQueryable<PeerReviewDocumentType> GetDocumentTypes()
        {
            var list = context.PeerReviewDocumentTypes.AsQueryable<PeerReviewDocumentType>();
            return list;
        }
        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <param name="contentTypeId">The content type identifier.</param>
        /// <returns></returns>
        public PeerReviewContentType GetContentType(int contentTypeId)
        {
            return context.PeerReviewContentTypes.FirstOrDefault(x => x.PeerReviewContentTypeId == contentTypeId);
        }

        /// <summary>
        /// Gets the type of the documen.
        /// </summary>
        /// <param name="documentTypeId">The document type identifier.</param>
        /// <returns></returns>
        public PeerReviewDocumentType GetDocumenType(int documentTypeId)
        {
            return context.PeerReviewDocumentTypes.FirstOrDefault(x => x.PeerReviewDocumentTypeId == documentTypeId);
        }

        #endregion
    }
}
