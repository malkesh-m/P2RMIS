namespace Sra.P2rmis.CrossCuttingServices.MessageServices
{
    /// <summary>
    /// Status values returned when saving a resume
    /// </summary>
    public enum SaveResumeStatus
    {
        /// <summary>
        /// All enums should have a default
        /// </summary>
        Default = 0,
        /// <summary>
        /// Resume was successfully saved or modified
        /// </summary>
        Success = 1,
        /// <summary>
        /// Resume exceeded maximum size.
        /// </summary>
        TooLarge,
        /// <summary>
        /// Resume was not a PDF file
        /// </summary>
        NotPdfFile,
        /// <summary>
        /// Resume save failure
        /// </summary>
        SaveFailure
    }
}
