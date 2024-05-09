namespace Sra.P2rmis.CrossCuttingServices.MessageServices
{
    /// <summary>
    /// Status values returned when saving a password and/or security question(s)
    /// </summary>
    public enum SaveSecurityQuestionStatus
    {
        /// <summary>
        /// All enums should have a default
        /// </summary>
        Default = 0,
        /// <summary>
        /// Password and Security questions were successfully saved or modified.
        /// </summary>
        Success = 1,
        /// <summary>
        /// Password only was successfully saved or modified.
        /// </summary>
        PasswordSuccess = 2,
        /// <summary>
        /// Security Questions only were successfully saved or modified.
        /// </summary>
        SecurityQuestionSuccess = 3,
        /// <summary>
        /// No action attempted, neither password nor secuirty questions were updated
        /// </summary>
        NoActionAttempted = 4
    }
}