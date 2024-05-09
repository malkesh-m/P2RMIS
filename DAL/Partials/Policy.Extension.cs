using Sra.P2rmis.Dal.Interfaces;
namespace Sra.P2rmis.Dal
{
    public partial class Policy : IStandardDateFields
    {
        /// <summary>
        /// Perform a shallow copy.
        /// </summary>
        /// <returns></returns>
        public Policy ShallowCopy()
        {
            return (Policy)this.MemberwiseClone();
        }
    }
}
