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
    
    public partial class ResearchCategoryType
    {
        public ResearchCategoryType()
        {
            this.ApplicationResearchCategories = new HashSet<ApplicationResearchCategory>();
        }
    
        public int ResearchCategoryTypeId { get; set; }
        public string ResearchCategoryName { get; set; }
    
        public virtual ICollection<ApplicationResearchCategory> ApplicationResearchCategories { get; set; }
    }
}