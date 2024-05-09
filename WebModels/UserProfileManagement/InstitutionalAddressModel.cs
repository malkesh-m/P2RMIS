namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Institution Address Information
    /// </summary>
    public class InstitutionAddressModel : Editable, IInstitutionAddressModel
    {
        /// <summary>
        /// Minimum entries
        /// </summary>
        public const int MinimumEntries = 1;
        /// <summary>
        /// Initialize model
        /// </summary>
        /// <param name="model">the InstitutionAddressModel</param>
        public static void InitializeModel(InstitutionAddressModel model) 
        {
            if (model.Address == null)
            {
                model.Address = new AddressInfoModel();
            }
            //if (model.NewPosition == null)
            //{
            //    model.NewPosition = new PositionInfoModel();
            //}
        }
        /// <summary>
        /// Address identifier
        /// </summary>
        public int AddressId { get; set; }
        /// <summary>
        /// Name of the institution
        /// </summary>
        public string InstitutionName { get; set; }
        ///// <summary>
        ///// The position at this institution and address
        ///// </summary>
        //public PositionInfoModel NewPosition { get; set; }
        ///// <summary>
        /// Address associated with this positions
        /// </summary>
        public AddressInfoModel Address { get; set; }     // street address
        /// <summary>
        /// Indicates if the position is deleted
        /// </summary>
        /// <returns>Returns true if the item is to be deleted, false otherwise</returns>
        public override bool IsDeleted()
        { 
            return this.Address.IsDeleted(); 
        }
        /// <summary>
        /// Indicates if the position has data.
        /// </summary>
        /// <returns>True if the model has any data; false otherwise</returns>
        /// <remarks>needs unit test</remarks>
        public override bool HasData()
        {
            return ( 
                        //(!string.IsNullOrWhiteSpace(InstitutionName)) ||
                        //(!string.IsNullOrEmpty(NewPosition.DepartmentTitle))||
                        //(!string.IsNullOrEmpty(NewPosition.PositionTitle))||
                        (!string.IsNullOrEmpty(Address.Address1))||
                        (!string.IsNullOrEmpty(Address.Address2))||
                        (!string.IsNullOrEmpty(Address.City))||
                        (Address.StateId > 0)||
                        (!string.IsNullOrEmpty(Address.Zip))
                   );
        }
    }
}
