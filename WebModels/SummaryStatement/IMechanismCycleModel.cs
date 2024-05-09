
namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Class representing mechanism and cycle information
    /// </summary>
    public interface IMechanismCycleModel
    {
        /// <summary>
        /// Award Mechanism Abbreviation
        /// </summary>
        string MechanismAbbreviation { get; set; }
        /// <summary>
        /// Receipt cycle the mechanism was released
        /// </summary>
        int Cycle { get; set; }
    }
}
