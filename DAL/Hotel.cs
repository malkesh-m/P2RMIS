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
    
    public partial class Hotel
    {
        public Hotel()
        {
            this.ClientMeetings = new HashSet<ClientMeeting>();
            this.MeetingRegistrationHotels = new HashSet<MeetingRegistrationHotel>();
        }
    
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelAbbreviation { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public string ZipCode { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> LegacyHotelId { get; set; }
    
        public virtual ICollection<ClientMeeting> ClientMeetings { get; set; }
        public virtual ICollection<MeetingRegistrationHotel> MeetingRegistrationHotels { get; set; }
    }
}
