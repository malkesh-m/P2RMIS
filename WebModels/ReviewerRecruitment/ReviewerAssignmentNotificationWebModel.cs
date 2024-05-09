using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.ReviewerRecruitment
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReviewerAssignmentNotificationWebModel
    {
        /// <summary>
        /// Program fiscal year of panel
        /// </summary>
        string FiscalYear { get; }
        /// <summary>
        /// Panel abbreviation of the panel the reviewer was assigned to
        /// </summary>
        string PanelAbbrev { get; }
        /// <summary>
        /// Panel name of the panel the reviewer was assigned to
        /// </summary>
        string PanelName { get; }
        /// <summary>
        /// Reviewer's participant type
        /// </summary>
        string ParticipantType { get; }
        /// <summary>
        /// Program name
        /// </summary>
        string ProgramName { get; }
        /// <summary>
        /// Reviewer's first name
        /// </summary>
        string FirstName { get; }
        /// <summary>
        /// Reviewer's last name
        /// </summary>
        string LastName { get; }
        /// <summary>
        /// Reviewer's prefix
        /// </summary>
        string Prefix { get; }
        /// <summary>
        /// Reviewer's entity identifier
        /// </summary>
        int UserId { get; }
        /// <summary>
        /// Help desk address
        /// </summary>
        string HelpDeskAddress { get; }
        /// <summary>
        /// Reviewer's email address
        /// </summary>
        string ReviewerAddress { get; }
        /// <summary>
        /// RTA email address or addresses
        /// </summary>
        ICollection<string> RtaAddress { get; }
        /// <summary>
        /// SRO email address or addresses
        /// </summary>
        ICollection<string> SroAddress { get; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ReviewerAssignmentNotificationWebModel: IReviewerAssignmentNotificationWebModel
    {
        /// <summary>
        /// Program fiscal year of panel
        /// </summary>
        public string FiscalYear { get; private set; }
        /// <summary>
        /// Panel abbreviation of the panel the reviewer was assigned to
        /// </summary>
        public string PanelAbbrev { get; private set; }
        /// <summary>
        /// Panel name of the panel the reviewer was assigned to
        /// </summary>
        public string PanelName { get; private set; }
        /// <summary>
        /// Reviewer's participant type
        /// </summary>
        public string ParticipantType { get; private set; }
        /// <summary>
        /// Program name
        /// </summary>
        public string ProgramName { get; private set; }
        /// <summary>
        /// Reviewer's first name
        /// </summary>
        public string FirstName { get; private set; }
        /// <summary>
        /// Reviewer's last name
        /// </summary>
        public string LastName { get; private set; }
        /// <summary>
        /// Reviewer's prefix
        /// </summary>
        public string Prefix { get; private set; }
        /// <summary>
        /// Reviewer's entity identifier
        /// </summary>
        public int UserId { get; private set;}
        /// <summary>
        /// Help desk address
        /// </summary>
        public string HelpDeskAddress { get; private set; }
        /// <summary>
        /// Reviewer's email address
        /// </summary>
        public string ReviewerAddress { get; private set; }
        /// <summary>
        /// RTA email address or addresses
        /// </summary>
        public ICollection<string> RtaAddress { get; private set; }
        /// <summary>
        /// SRO email address or addresses
        /// </summary>
        public ICollection<string> SroAddress { get; private set; }
    }
}
