namespace Sra.P2rmis.Dal
{ 
    /// <summary>
    /// Custom methods for Entity Framework's ClientParticipantType object. 
    /// </summary>
    public partial class ClientParticipantType
    {
        /// <summary>
        /// Specialist flag
        /// </summary>
        public const string SPR = "SPR";

        /// <summary>
        /// The sro participant type abbreviation
        /// </summary>
        public const string SRO = "SRO";

        /// <summary>
        /// The rta participant type abbreviation
        /// </summary>
        public const string RTA = "RTA";
        /// <summary>
        /// Determines if the ClientParticipantType is an SRO
        /// </summary>
        /// <returns>True if the user is an SRO</returns>
        /// <remarks>TODO: Make this a column in the db</remarks>
        public bool IsSro()
        {
            return this.ParticipantTypeAbbreviation == SRO;
        }
        /// <summary>
        /// Determines if the ClientParticipantType is an RTA
        /// </summary>
        /// <returns>True if the user is an RTA</returns>
        /// <remarks>TODO: Make this a column in the db</remarks>
        public bool IsRta()
        {
            return this.ParticipantTypeAbbreviation == RTA;
        }
        /// <summary>
        /// Determines if the ClientParticipantType is a chair person.
        /// </summary>
        /// <returns>True if the user is an chair person</returns>
        public bool IsChair()
        {
            return this.ChairpersonFlag;
        }
        /// <summary>
        /// Determines whether [is cprit chair].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is cprit chair]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCpritChair()
        {
            return this.ElevatedChairpersonFlag;
        }
    }
}
