namespace Sra.P2rmis.WebModels.HelperClasses
{
    /// <summary>
    /// Model for a program list drop down.
    /// </summary>
    public interface IProgramModel
    {
        /// <summary>
        /// Unique identifier for the program
        /// </summary>
        string ProgramAbbrv { get; set; }
        /// <summary>
        /// the programs abbreviation 
        /// </summary>
        string ProgramName { get; set; }
    }
}
