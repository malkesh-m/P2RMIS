//using System;
namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Class containing the W9 address information
    /// </summary>
    public class W9AddressModel : IW9AddressModel
    {
        /// <summary>
        /// The physical address
        /// </summary>
        public IAddressInfoModel Address { get; set; }
        /// <summary>
        /// Indicates if a W9 address has been received for the user
        /// </summary>
        public bool W9AddressExists { get; set; }
        /// <summary>
        /// W9 verification status - 3 states 
        /// verified as accurate (true), verified as inaccurate (false) or not verified (null)
        /// </summary>
        private bool? w9Verified;
        public bool? W9Verified { get
            {
                return w9Verified;
            }
            set
            {
                w9Verified = value;
                W9Accurate = IsW9Accurate;
            }
        }
        /// <summary>
        /// W9 address is accurate
        /// </summary>
        public bool? W9Accurate { get; set; }
        /// <summary>
        /// Vendor name for W9
        /// </summary>
        public string VendorName { get; set; }
        /// <summary>
        /// Vendor Id 
        /// </summary>
        public string VendorId { get; set; }
        /// <summary>
        /// Indicates if the information is accurate
        /// </summary>
        protected bool IsW9Accurate 
        {
            get { return ((W9Verified.HasValue) && (W9Verified.Value)) ? true : false; }
        }
        /// <summary>
        /// Indicates if the information is inaccurate
        /// </summary>
        protected bool IsW9Inaccurate 
        { 
            get { return ((W9Verified.HasValue) && (!W9Verified.Value)) ? true : false; }
        }
    }
}
