

using Sra.P2rmis.Dal.Interfaces;
using System;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's MeetingRegistrationHotel object. 
    /// </summary>
    public partial class MeetingRegistrationComment : IStandardDateFields
    {
        /// <summary>
        /// Populates the specified internal comments.
        /// </summary>
        /// <param name="internalComments">The internal comment.</param>
        public void Populate(string internalComments)
        {
            this.InternalComments = internalComments;
        }
    }
}