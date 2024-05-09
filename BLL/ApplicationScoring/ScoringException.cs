using System;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// Exception identifying a scoring exception.  Currently there is only
    /// a single reason this exception is thrown, when a user tries to save
    /// a score to an application that cannot be scored.
    /// </summary>
    public class ScoringException : Exception
    {

    }
}
