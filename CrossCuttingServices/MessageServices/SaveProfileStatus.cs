
namespace Sra.P2rmis.CrossCuttingServices.MessageServices
{
    /// <summary>
    /// Status values returned from SaveProfile
    /// </summary>
    public enum SaveProfileStatus
    {
        /// <summary>
        /// All enums should have a default
        /// </summary>
        Default = 0,
        /// <summary>
        /// Reviewer was successfully assigned
        /// </summary>
        Success = 1,
        /// <summary>
        /// First Name field is to long
        /// </summary>
        FirstNameToLong,
        /// <summary>
        /// Middle initial field is to long
        /// </summary>
        MiddleInitialToLong,
        /// <summary>
        /// Last Name field is to long
        /// </summary>
        LastNameToLong,
        /// <summary>
        /// Nick Name field is to long
        /// </summary>
        NickNameToLong,
        /// <summary>
        /// Suffix field is to long
        /// </summary>
        SuffixToLong,
        /// <summary>
        /// Badge Name field is to long
        /// </summary>
        BadgeToLong,
        /// <summary>
        /// First name was not supplied
        /// </summary>
        FirstNameNotSupplied,
        /// <summary>
        /// Last name was not supplied
        /// </summary>
        LastNameNotSupplied,
        /// <summary>
        /// UserLogin field is to long
        /// </summary>
        UserLoginToLong,
        /// <summary>
        /// UserLogin was not supplied
        /// </summary>
        UserLoginNotSupplied,
        /// <summary>
        /// WebsiteAddress field is to long
        /// </summary>
        WebsiteAddressToLong,
        /// <summary>
        /// WebsiteAddress was not supplied
        /// </summary>
        WebsiteAddressNotSupplied,
        /// <summary>
        /// MilitaryRank id was invalid (Service and Rank)
        /// </summary>
        MilitaryRankInvalid,
        /// <summary>
        /// Military status id was invalid (Military Status)
        /// </summary>
        MilitaryStatusInvalid,
        /// <summary>
        /// Both military index values are not supplied.
        /// </summary>
        IncompleteMilitaryIndex,
        /// <summary>
        /// Major field is too long
        /// </summary>
        MajorTooLong,
        /// <summary>
        /// DegreeId was not supplied
        /// </summary>
        DegreeIdNotSupplied,
        /// <summary>
        /// Too many personal addresses were detected.  
        /// </summary>
        TooManyPersonalAddresses,
        ///
        /// Primary Phone required unless Profile type is misconduct and have at least one email address
        ///
        OneAndOnlyOnePhoneIsPrimary,
        /// <summary>
        /// One address is required
        /// </summary>
        OneAddressIsRequired
     }
}
