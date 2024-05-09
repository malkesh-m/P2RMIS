using System;

namespace Sra.P2rmis.WebModels.Lists
{
    /// <summary>
    /// Webmodel drop down content for discrete lists
    /// </summary>
    public class DiscreteListEntry: ListEntry, IEquatable<DiscreteListEntry>
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="index">Index value</param>
        /// <param name="value">Display value</param>
        public DiscreteListEntry(int index, string value) : base(index, value) { }
        #endregion
        #region IEquatable interface implementation
        /// <summary>
        /// Determines if two DiscreteListEntry objects are equal
        /// </summary>
        /// <param name="other">DiscreteListEntry object to compare</param>
        /// <returns>True if the objects are equal; false otherwise</returns>
        public bool Equals(DiscreteListEntry other)
        {

            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return Index.Equals(other.Index) && Value.Equals(other.Value);
        }


        /// <summary>
        /// If Equals() returns true for a pair of objects 
        /// then GetHashCode() must return the same value for these objects.
        /// </summary>
        /// <returns>Object hash code</returns>
        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null.
            int hashProductName = Value == null ? 0 : Value.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = Index.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashProductCode;
        } 
        #endregion
    }
}
