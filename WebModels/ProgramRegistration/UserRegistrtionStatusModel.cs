
namespace Sra.P2rmis.WebModels.ProgramRegistration
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRegistrtionStatusModel : IBuiltModel
    {
        #region construction & set up
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="oneRegistrationComplete"></param>
        /// <param name="noRegistrations"></param>
        public UserRegistrtionStatusModel(bool oneRegistrationComplete, bool noRegistrations, int programYearId, int userId)
        {
            this.OneRegistrationComplete = oneRegistrationComplete;
            this.NoRegistrations = noRegistrations;
            this.ProgramYearId = programYearId;
            this.UserId = userId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// If true indicates the user has at least one registration completed
        /// </summary>
        public bool OneRegistrationComplete { get; private set; }
        /// <summary>
        /// If true indicates the user does not have any registrations (complete or not complete)
        /// </summary>
        public bool NoRegistrations { get; private set; }
        /// <summary>
        /// User entity id
        /// </summary>
        public int UserId { get; private set; }
        /// <summary>
        /// ProgramYear entity id
        /// </summary>
        public int ProgramYearId { get; private set; }
 	    #endregion 
    }
}
