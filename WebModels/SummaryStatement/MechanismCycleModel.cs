namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Class representing mechanism and cycle information
    /// </summary>
    public class MechanismCycleModel : IMechanismCycleModel
    {
        /// <summary>
        /// Award Mechanism Abbreviation
        /// </summary>
        public string MechanismAbbreviation { get; set; }
        /// <summary>
        /// Receipt cycle the mechanism was released
        /// </summary>
        public int Cycle { get; set; }
    }
}
