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
    
    public partial class TravelMode
    {
        public TravelMode()
        {
            this.MeetingRegistrationTravels = new HashSet<MeetingRegistrationTravel>();
        }
    
        public int TravelModeId { get; set; }
        public string TravelModeAbbreviation { get; set; }
        public int SortOrder { get; set; }
        public string LegacyTravelModeAbbreviation { get; set; }
    
        public virtual ICollection<MeetingRegistrationTravel> MeetingRegistrationTravels { get; set; }
    }
}
