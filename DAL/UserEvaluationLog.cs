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
    
    public partial class UserEvaluationLog
    {
        public int UserEvaluationLogId { get; set; }
        public int UserInfoId { get; set; }
        public bool BlockFlag { get; set; }
        public bool ShowCommentFlag { get; set; }
        public string EvaluationComment { get; set; }
        public System.DateTime EvaluationDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> LegacyRevEvalId { get; set; }
        public Nullable<int> LegacyRevEvalBlockId { get; set; }
    
        public virtual UserInfo UserInfo { get; set; }
    }
}