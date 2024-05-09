namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Model representing the results of a GetOpenProgramsList() request.
    /// </summary>
    public class ProgramListResultModel
    {
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public ProgramYear ProgramFY { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public ClientProgram ClientProgram { get; set; }
    }
}
