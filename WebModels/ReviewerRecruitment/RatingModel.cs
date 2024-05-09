using System;

namespace Sra.P2rmis.WebModels.ReviewerRecruitment
{
    /// <summary>
    /// Web model for individual Reviewer Recruitment Rating/Evaluations modal
    /// </summary>
    public interface IRatingModel
    {
        /// <summary>
        /// Program fiscal year
        /// </summary>
        string FiscalYear { get; }
        /// <summary>
        /// Program
        /// </summary>
        string Program { get; }
        /// <summary>
        /// Program panel
        /// </summary>
        string Panel { get; }
        /// <summary>
        /// Participation type (SR)
        /// </summary>
        string ParticipationType { get; }
        /// <summary>
        /// Participation method (onsite/remote)
        /// </summary>
        string ParticipationMethod { get; }
        /// <summary>
        /// Participation level (Full/Partial)
        /// </summary>
        string ParticipationLevel { get; }
        /// <summary>
        /// Reviewer rating
        /// </summary>
        int? Rating { get; }
        /// <summary>
        /// Potential chair indication
        /// </summary>
        bool PotentialChair { get; }
        /// <summary>
        /// Reviewer evaluation comments
        /// </summary>
        string Comments { get; }
        /// <summary>
        /// First name of user who entered the rating.
        /// </summary>
        string RaterFirstName { get; }
        /// <summary>
        /// Last name of user who entered the rating
        /// </summary>
        string RaterLasttName { get; }
        /// <summary>
        /// DateTime rating was created.
        /// </summary>
        DateTime? RatingCreationDate { get;}
        /// <summary>
        /// ClientRole name
        /// </summary>
        string ClientRole { get; }
    }
    /// <summary>
    /// Web model for individual Reviewer Recruitment Rating/Evaluations modal
    /// </summary>
    public class RatingModel : IRatingModel
    {
        #region Constructor & Setup
        /// <summary>
        /// Initializes the rating information.
        /// </summary>
        /// <param name="rating"></param>
        /// <param name="comment"></param>
        /// <param name="potentialChair"></param>
        public void SetRating(int? rating, string comment, bool potentialChair)
        {
            this.Rating = rating;
            this.Comments = comment;
            this.PotentialChair = potentialChair;
        }
        /// <summary>
        /// Set the participation information
        /// </summary>
        /// <param name="level">Participation level (ex: Full)</param>
        /// <param name="method">Participation method (ex: Onsite)</param>
        /// <param name="type">Participation type (ex: SR)</param>
        /// <param name="role">Client role name</param>
        public void SetParticipation(string level, string method, string type, string role)
        {
            this.ParticipationLevel = level;
            this.ParticipationMethod = method;
            this.ParticipationType = type;
            this.ClientRole = role;
        }
        /// <summary>
        /// Initializes the panel information
        /// </summary>
        /// <param name="panelAbbreviation">Panel abbreviation</param>
        /// <param name="fiscalYear">Fiscal year of panel</param>
        /// <param name="program">Program the panel is associated with</param>
        public void SetPanelInformation(string panelAbbreviation, string fiscalYear, string program)
        {
            this.Panel = panelAbbreviation;
            this.FiscalYear = fiscalYear;
            this.Program = program;
        }
        /// <summary>
        /// Initializes the information about the rater.
        /// </summary>
        /// <param name="firstName">First name of rater</param>
        /// <param name="lastName">Last name of rater</param>
        /// <param name="creationtime">DateTime rating was entered</param>
        public void SetReviewer(string firstName, string lastName, DateTime? creationtime)
        {
            this.RaterFirstName = firstName;
            this.RaterLasttName = lastName;
            this.RatingCreationDate = creationtime;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Program fiscal year
        /// </summary>
        public string FiscalYear { get; private set; }
        /// <summary>
        /// Program
        /// </summary>
        public string Program { get; private set; }
        /// <summary>
        /// Program panel
        /// </summary>
        public string Panel { get; private set; }
        /// <summary>
        /// Participation type (SR)
        /// </summary>
        public string ParticipationType { get; private set; }
        /// <summary>
        /// Participation method (onsite/remote)
        /// </summary>
        public string ParticipationMethod { get; private set; }
        /// <summary>
        /// Participation level (Full/Partial)
        /// </summary>
        public string ParticipationLevel { get; private set; }
        /// <summary>
        /// Reviewer rating
        /// </summary>
        public int? Rating { get; private set; }
        /// <summary>
        /// Potential chair indication
        /// </summary>
        public bool PotentialChair { get; private set; }
        /// <summary>
        /// Reviewer evaluation comments
        /// </summary>
        public string Comments { get; private set; }
        /// <summary>
        /// First name of user who entered the rating.
        /// </summary>
        public string RaterFirstName { get; private set; }
        /// <summary>
        /// Last name of user who entered the rating
        /// </summary>
        public string RaterLasttName { get; private set; }
        /// <summary>
        /// DateTime rating was created.
        /// </summary>
        public DateTime? RatingCreationDate { get; private set; }
        /// <summary>
        /// ClientRole name
        /// </summary>
        public string ClientRole { get; private set; }
        #endregion
    }
}
