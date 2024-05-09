
namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// The user security question object
    /// </summary>
    public class UserSecurityQuestionAnswerModel : IUserSecurityQuestionAnswerModel
    {
        /// <summary>
        /// Minimum entries
        /// </summary>
        public const int MinimumEntries = 3;
        /// <summary>
        /// Initialize model
        /// </summary>
        /// <param name="model">the UserSecurityQuestionAnswerModel</param>
        public static void InitializeModel(UserSecurityQuestionAnswerModel model) { }
        /// <summary>
        /// The user account recovery identifier
        /// </summary>
        public int UserAccountRecoveryId { get; set; }
        /// <summary>
        /// The user recovery question identifieer
        /// </summary>
        public int RecoveryQuestionId { get; set; }
        /// <summary>
        /// The answer to the user recovery question
        /// </summary>
        public string AnswerText { get; set; }
        /// <summary>
        /// Whether a security question/answer set has been modified by a user
        /// </summary>
        public bool IsModified { get; set; }
    }
}
