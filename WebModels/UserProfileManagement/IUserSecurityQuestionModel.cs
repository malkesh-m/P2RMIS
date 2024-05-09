
namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// User Question and Answer Model
    /// </summary>
    public interface IUserSecurityQuestionAnswerModel
    {
        /// <summary>
        /// The user account recovery identifier
        /// </summary>
        int UserAccountRecoveryId { get; set; }
        /// <summary>
        /// The user recovery question identifier
        /// </summary>
        int RecoveryQuestionId { get; set; }
        /// <summary>
        /// The answer to the user recovery question
        /// </summary>
        string AnswerText { get; set; }
        /// <summary>
        /// Whether a security question/answer set has been modified by a user
        /// </summary>
        bool IsModified { get; set; }
    }
}
