using System;
using System.Collections.Generic;
using Sra.P2rmis.Dal.ResultModels.DocManagement;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Database access methods for document management server requests.
    /// </summary>
    public interface IDocumentManagementRepository
    {
        /// <summary>
        /// Constructs & returns a container holding a list of the panel reviewers document information.
        /// </summary>
        /// <param name="panelId">Unique panel identifier</param>
        /// <returns>Container holding the query result as a list of DocListResultModel</returns>
        DocListResultModel GetReviewerDocuments(int panelId);
        /// <summary>
        /// Constructs & returns a container holding a list of the panel reviewers document information for specific document type.
        /// </summary>
        /// <param name="panelId">Unique panel identifier</param>
        /// <param naem="docType">Document type</param>
        /// <returns>Container holding the query result as a list of DocListResultModel for specific document type</returns>
        DocListResultModel GetReviewerDocuments(int panelId, string docType);
        /// <summary>
        /// Constructs & returns a container holding a list of a reviewers document information.
        /// </summary>
        /// <param name="personId">Unique person identifier</param>
        /// <param name="clients">List of user's clients</param>
        /// <returns>Container holding the query result as a list of DocListResultModel</returns>
        DocListResultModel GetReviewerDocumentsByReviewer(int personId, ICollection<int> clients);
        /// <summary>
        /// Constructs & returns a container holding a list a reviewers document information for specific document type.
        /// </summary>
        /// <param name="personId">Unique person identifier</param>
        /// <param name="clients">List of user's clients</param>
        /// <param naem="docType">Document type</param>
        /// <returns>Container holding the query result as a list of DocListResultModel for specific document type</returns>
        DocListResultModel GetReviewerDocumentsByReviewer(int personId, ICollection<int> clients, string docType);
        /// <summary>
        /// Constructs & returns a container holding a list of a reviewers document information for a program.
        /// </summary>
        /// <param name="programAbrv">Program abbreviation</param>
        /// <param name="personId">Unique person identifier</param>
        /// <param name="clients">List of user's clients</param>
        /// <returns>Container holding the query result as a list of DocListResultModel</returns>
        DocListResultModel GetReviewerDocumentsByReviewer(string programAbrv, int personId, ICollection<int> clients);
        /// <summary>
        /// Constructs & returns a container holding a list a reviewers document information for specific document type.
        /// </summary>
        /// <param name="programAbrv">Program abbreviation</param>
        /// <param name="personId">Unique person identifier</param>
        /// <param name="clients">List of user's clients</param>
        /// <param naem="docType">Document type</param>
        /// <returns>Container holding the query result as a list of DocListResultModel for specific document type</returns>
        DocListResultModel GetReviewerDocumentsByReviewer(string programAbrv, int personId, ICollection<int> clients, string docType);
        /// <summary>
        /// Returns a binary representation of the specified document.
        /// </summary>
        /// <param naem="docId">Document Id</param>
        /// <returns>Binary content of reviewer document</returns>
        Byte[] GetReviewerDocumentFileById(int docId);
    }
}
