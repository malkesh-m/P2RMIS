
namespace Sra.P2rmis.Bll.Views
{
    /// <summary>
    /// Identifies the valid values for CritiqueRequired.
    /// </summary>
    public enum CritiqueRequiredType
    {
        None,               // No critique type specified
        NoIcon,             // No critique icon required
        CritiqueMissing,    // A critique is missing
        Critique,           // A critique exists
        RevisedCritique,    // A revised critique exists
        CritiqueUnavailable // Critique is not currently available
    }
}
