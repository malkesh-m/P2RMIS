using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    public partial class ApplicationPersonnel : IStandardDateFields
    {
        /// <summary>
        /// Default value for Linq search.
        /// </summary>
        private static ApplicationPersonnel _default;
        public static ApplicationPersonnel Default
        {
            get
            {
                //
                // Lazy load the default
                //
                if (_default == null)
                {
                    _default = new ApplicationPersonnel();
                }
                return _default;
            }
        }
    }
}
