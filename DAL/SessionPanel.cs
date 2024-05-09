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
    
    public partial class SessionPanel
    {
        public SessionPanel()
        {
            this.PanelApplications = new HashSet<PanelApplication>();
            this.PanelUserAssignments = new HashSet<PanelUserAssignment>();
            this.ProgramPanels = new HashSet<ProgramPanel>();
            this.PanelStages = new HashSet<PanelStage>();
            this.CommunicationLogs = new HashSet<CommunicationLog>();
            this.PanelUserPotentialAssignments = new HashSet<PanelUserPotentialAssignment>();
            this.AssignmentTypeThresholds = new HashSet<AssignmentTypeThreshold>();
            this.ReferralMappingDatas = new HashSet<ReferralMappingData>();
        }
    
        public int SessionPanelId { get; set; }
        public Nullable<int> LegacyPanelId { get; set; }
        public Nullable<int> MeetingSessionId { get; set; }
        public string PanelAbbreviation { get; set; }
        public string PanelName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    
        public virtual MeetingSession MeetingSession { get; set; }
        public virtual ICollection<PanelApplication> PanelApplications { get; set; }
        public virtual ICollection<PanelUserAssignment> PanelUserAssignments { get; set; }
        public virtual ICollection<ProgramPanel> ProgramPanels { get; set; }
        public virtual ICollection<PanelStage> PanelStages { get; set; }
        public virtual ICollection<CommunicationLog> CommunicationLogs { get; set; }
        public virtual ICollection<PanelUserPotentialAssignment> PanelUserPotentialAssignments { get; set; }
        public virtual ICollection<AssignmentTypeThreshold> AssignmentTypeThresholds { get; set; }
        public virtual ICollection<ReferralMappingData> ReferralMappingDatas { get; set; }
    }
}
