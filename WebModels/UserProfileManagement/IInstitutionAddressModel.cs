namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Institution Address Information
    /// </summary>
    public interface IInstitutionAddressModel : IEditable
    {
        /// <summary>
        /// Address identifier
        /// </summary>
        int AddressId { get;set; }
        ///// <summary>
        ///// Name of the institution
        ///// </summary>
        //string InstitutionName { get; set; }
        ///// <summary>
        ///// The position at this institution and address
        ///// </summary>
        //PositionInfoModel NewPosition { get; set; }
        /// <summary>
        /// Address associated with this positions
        /// </summary>
        AddressInfoModel Address { get; set; }     // street address
    }
}
