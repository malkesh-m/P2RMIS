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
    
    public partial class PeerReviewDocumentAccess
    {
        public int PeerReviewDocumentAccessId { get; set; }
        public int PeerReviewDocumentId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<bool> RestrictedAssignedFlag { get; set; }
        public string MeetingTypeIds { get; set; }
        public string ClientParticipantTypeIds { get; set; }
        public string ParticipationMethodIds { get; set; }
    
        public virtual PeerReviewDocument PeerReviewDocument { get; set; }
    }
}