//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sra.P2rmis.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class PeerReviewDocument
    {
        public PeerReviewDocument()
        {
            this.PeerReviewDocumentAccesses = new HashSet<PeerReviewDocumentAccess>();
            this.UserPeerReviewDocuments = new HashSet<UserPeerReviewDocument>();
        }
    
        public int PeerReviewDocumentId { get; set; }
        public int ClientId { get; set; }
        public Nullable<int> ClientProgramId { get; set; }
        public string FiscalYear { get; set; }
        public int PeerReviewDocumentTypeId { get; set; }
        public int PeerReviewContentTypeId { get; set; }
        public Nullable<int> TrainingCategoryId { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string ContentUrl { get; set; }
        public string ContentFileLocation { get; set; }
        public bool ArchivedFlag { get; set; }
        public Nullable<System.DateTime> ArchiveDate { get; set; }
        public Nullable<int> ArchivedBy { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string FileType { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual ClientProgram ClientProgram { get; set; }
        public virtual PeerReviewContentType PeerReviewContentType { get; set; }
        public virtual PeerReviewDocumentType PeerReviewDocumentType { get; set; }
        public virtual TrainingCategory TrainingCategory { get; set; }
        public virtual ICollection<PeerReviewDocumentAccess> PeerReviewDocumentAccesses { get; set; }
        public virtual ICollection<UserPeerReviewDocument> UserPeerReviewDocuments { get; set; }
    }
}