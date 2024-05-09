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
    
    public partial class ApplicationPersonnel
    {
        public int ApplicationPersonnelId { get; set; }
        public int ApplicationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string OrganizationName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int ClientApplicationPersonnelTypeId { get; set; }
        public bool PrimaryFlag { get; set; }
        public string Source { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string StateAbbreviation { get; set; }
    
        public virtual Application Application { get; set; }
        public virtual ClientApplicationPersonnelType ClientApplicationPersonnelType { get; set; }
    }
}