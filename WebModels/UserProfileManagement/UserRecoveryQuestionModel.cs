using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// User Recovery Question Information
    /// </summary>
    public interface IUserRecoveryQuestionModel
    {
        /// <summary>
        /// The users account recovery identifier
        /// </summary>
        int UserAccountRecoveryId { get; set; }
        /// <summary>
        /// The recovery question identifier
        /// </summary>
        int RecoveryQuestionId { get; set; }
        /// <summary>
        ///  The plain text recovery question
        /// </summary>
        string QuestionText { get; set; }
        /// <summary>
        /// The question number
        /// </summary>
        int QuestionOrder { get; set; }
    }    /// <summary>
         /// User Recovery Question Information
         /// </summary>
    public class UserRecoveryQuestionModel : IUserRecoveryQuestionModel
    {
        /// <summary>
        /// The users account recovery identifier
        /// </summary>
        public int UserAccountRecoveryId { get; set; }
        /// <summary>
        /// The recovery question identifier
        /// </summary>
        public int RecoveryQuestionId { get; set; }
        /// <summary>
        ///  The plain text recovery question
        /// </summary>
        public string QuestionText { get; set; }
        /// <summary>
        /// The question number
        /// </summary>
        public int QuestionOrder { get; set; }
    }
}
