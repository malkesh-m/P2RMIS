using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    public partial class UserAccountRecovery : IStandardDateFields
    {
        #region constants
        public class Limits
        {
            /// <summary>
            /// Required number of security questions a user shall have.
            /// </summary>
            public static int RequiredNumberSecurityQuestions = 3;
        }
        #endregion

        /// <summary>
        /// Create answer hash based on supplied plain text answer value
        /// (produces same hashed value as obsolete 'HashPasswordForStoringInConfigFile')
        /// </summary>
        /// <param name="password">The plain text answer to hash</param>
        /// <returns>The hashed answer</returns>
        public static string CreateAnswerHash(string answer)
        {
            return Helper.CreateEncodedHash(answer);
        }
        /// <summary>
        /// Populates a new UserAccountRecovery entity object in preparation for addition to the repository.
        /// </summary>
        /// <param name="questionId">The question identifier</param>
        /// <param name="answer">The plain text answer to the question</param>
        /// <param name="order">The display order of the questions</param>
        /// <param name="userId">The identifier of the user the answer applies to and the user making the change</param>
        public void Populate(int questionId, string answer, int order, int userId)
        {
            this.UserId = userId;
            Update(questionId, answer, order, userId);

            Helper.UpdateCreatedFields(this, userId);
        }
        /// <summary>
        /// Updates an existing UserAccountRecovery entity object with the new question/answer pairing and
        /// updates the modify time.
        /// </summary>
        /// <param name="answerId">The User Recovery question Identifier</param>
        /// <param name="answer">The plain text answer to the question</param>
        /// <param name="order">The display order of the questions</param>
        /// <param name="userId">User Identifier</param>
        public void Update(int questionId, string answer, int order, int userId)
        {
            this.Answer = CreateAnswerHash(answer);
            this.RecoveryQuestionId = questionId;
            this.QuestionOrder = order;
            Helper.UpdateModifiedFields(this, userId);
        }
    }
}
