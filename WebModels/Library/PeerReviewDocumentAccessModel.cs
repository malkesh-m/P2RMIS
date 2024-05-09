
namespace Sra.P2rmis.WebModels.Library
{
    /// <summary>
    /// Data model for peer review document access
    /// </summary>
    /// <seealso cref="Sra.P2rmis.WebModels.Library.IPeerReviewDocumentAccessModel" />
    public class PeerReviewDocumentAccessModel : IPeerReviewDocumentAccessModel
    {
        public PeerReviewDocumentAccessModel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PeerReviewDocumentAccessModel"/> class.
        /// </summary>
        /// <param name="meetingTypeIds">The meeting type identifier.</param>
        /// <param name="clientParticipantTypeIds">The client participant type identifier.</param>
        /// <param name="participationMethodIds">The participation method identifier.</param>
        /// <param name="restrictedAssignedFlag">The restricted assigned flag.</param>
        public PeerReviewDocumentAccessModel(string meetingTypeIds, string clientParticipantTypeIds, string participationMethodIds, bool? restrictedAssignedFlag)
        {
            MeetingTypeIds = meetingTypeIds;
            ClientParticipantTypeIds = clientParticipantTypeIds;
            ParticipationMethodIds = participationMethodIds;
            RestrictedAssignedFlag = restrictedAssignedFlag;
        }
        /// <summary>
        /// Gets or sets the meeting type identifier.
        /// </summary>
        /// <value>
        /// The meeting type identifier.
        /// </value>
        public string MeetingTypeIds { get; set; }
        /// <summary>
        /// Gets or sets the client participant type identifier.
        /// </summary>
        /// <value>
        /// The client participant type identifier.
        /// </value>
        public string ClientParticipantTypeIds { get; set; }
        /// <summary>
        /// Gets or sets the participation method identifier.
        /// </summary>
        /// <value>
        /// The participation method identifier.
        /// </value>
        public string ParticipationMethodIds { get; set; }
        /// <summary>
        /// Gets or sets the restricted assigned flag.
        /// </summary>
        /// <value>
        /// The restricted assigned flag.
        /// </value>
        public bool? RestrictedAssignedFlag { get; set; }
    }

    public interface IPeerReviewDocumentAccessModel
    {
        /// <summary>
        /// Gets or sets the meeting type identifier.
        /// </summary>
        /// <value>
        /// The meeting type identifier.
        /// </value>
        string MeetingTypeIds { get; set; }
        /// <summary>
        /// Gets or sets the client participant type identifier.
        /// </summary>
        /// <value>
        /// The client participant type identifier.
        /// </value>
        string ClientParticipantTypeIds { get; set; }
        /// <summary>
        /// Gets or sets the participation method identifier.
        /// </summary>
        /// <value>
        /// The participation method identifier.
        /// </value>
        string ParticipationMethodIds { get; set; }
        /// <summary>
        /// Gets or sets the restricted assigned flag.
        /// </summary>
        /// <value>
        /// The restricted assigned flag.
        /// </value>
        bool? RestrictedAssignedFlag { get; set; }
    }
}
