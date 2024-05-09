using System;
using System.Linq;

namespace Sra.P2rmis.Dal
{
    public partial class ClientScoringScale
    {
        /// <summary>
        /// Provides lookup values for the types of scoring available in P2RMIS
        /// </summary>
        public class ScoringType
        {
            public const string Integer = "Integer";
            public const string Decimal = "Decimal";
            public const string Adjectival = "Adjectival";
        }
        /// <summary>
        /// Determines if the rating is within the LowValue & HighValue.
        /// </summary>
        /// <param name="rating">Rating to check</param>
        /// <returns></returns>
        public bool IsRatingValid(decimal rating)
        {
            //
            //  One would think that low would be less than high. (i.e. 1 < 5).  But nothing in 
            //  PRMIS is simple.  5 can be less than 1.  Go figure.  First we orient if we are
            // handling the case were the low value is less than the high value.
            //
            return (this.LowValue < this.HighValue)? 
            //
            // We are.  So life is good & normal.
            //
            (this.LowValue <= rating) && (rating <= this.HighValue): 
            //
            // We are not.  In this universe 1 > 5.
            //
            (this.HighValue <= rating) && (rating <= this.LowValue);
        }
        /// <summary>
        /// Convert the value to a string based on the scoring scale.
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Value converted to a string  based on the scoring scale</returns>
        public string ToString(decimal? value)
        {
            string result = string.Empty;
            if (value.HasValue)
            {
                //
                // Scorings can be either an integer
                //
                if (string.Equals(this.ScoreType, ScoringType.Integer, StringComparison.OrdinalIgnoreCase))
                {
                    result = Convert.ToInt32(value.Value).ToString();
                }
                //
                // Or Adjectival (i.e.: good; bad; ugly)
                //
                else if (string.Equals(this.ScoreType, ScoringType.Adjectival, StringComparison.OrdinalIgnoreCase))
                {
                    var v = this.ClientScoringScaleAdjectivals.First(x => (x.ClientScoringId == this.ClientScoringId) && (x.NumericEquivalent == Convert.ToInt32(value.Value)));
                    result = v.ScoreLabel?? string.Empty;
                }
                //
                // or finally just a normal decimal value
                //
                else if (string.Equals(this.ScoreType, ScoringType.Decimal, StringComparison.OrdinalIgnoreCase))
                {
                    result = value.ToString();
                }
                //
                // Well this is embarrassing.  It is some other type that we did not know existed.
                // So we just tell it to convert itself to a string and hope for the best.
                //
                else
                {
                    result = value.ToString();
                }
            }
            return result;
        }
        /// <summary>
        /// Compares two score type without regard to case.  If one looks in ClientScoringScale there are numerous
        /// instances where the ScoreType property is a different case for the same ScoreType.
        /// </summary>
        /// <param name="scoreTypeOne">First ScoreType value</param>
        /// <param name="scoreTypeTwo">Second ScoreType value</param>
        /// <returns>True if the ScoreTypes are the same (case insensitive); false otherwise</returns>
        public static bool IsSameScale(string scoreTypeOne, string scoreTypeTwo)
        {
            return string.Equals(scoreTypeOne, scoreTypeTwo, StringComparison.OrdinalIgnoreCase);
        }
    }
}
