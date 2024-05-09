using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.UserProfileManagement;

namespace Sra.P2rmis.Bll.UserProfileManagement
{
    /// <summary>
    /// Service Action method to Add/Modify/Delete UserSystemRole represented
    /// by WebModel GeneralInfoModel
    /// </summary>
    public class UserSystemRoleServiceAction : ServiceModelActionForWebModel<UserSystemRole, GeneralInfoModel>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public UserSystemRoleServiceAction()
        {
            //
            // Normally one would set Index here.  However since the model value is null able
            // we override EntityId() method instead.
            //
        }
        #endregion
        #region Attributes
        //
        // THE TYPE OF THE WEB MODEL NEEDS TO BE SUPPLIED.
        //
        //
        /// <summary>
        /// Return the Model as a specific model.
        /// </summary>
        protected GeneralInfoModel GeneralInfoModel
        {
            get { return Model as GeneralInfoModel; }
        }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the UserSystemRole entity with information from the GeneralInfo model.
        /// </summary>
        /// <param name="entity">UserSystemRole entity</param>
        protected override void Populate(UserSystemRole entity)
        {
            entity.Populate(GeneralInfoModel.SystemRoleId.Value, GeneralInfoModel.UserId);
        }
        /// <summary>
        /// Indicates if the WebModel is an add
        /// </summary>
        public override bool IsAdd
        {
            get
            {
                //
                // We need to add if 
                //  - the UserSystemRoleID does not have a value    AND
                //  - the SystemRoleId does have a value
                //
                return ((!GeneralInfoModel.UserSystemRoleId.HasValue) & (GeneralInfoModel.SystemRoleId.HasValue));
            }
        }
        #endregion
        #region Optional Overrides
        /// <summary>
        /// Action is not expected to delete items from the UserSystemRole table.
        /// </summary>
        protected override bool IsDelete
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// Override how the UserSystemRole is determined.
        /// </summary>
        /// <returns>UserSystemRole entity identifier; 0 if no entity exists.</returns>
        protected override int EntityId()
        {
            return GeneralInfoModel.UserSystemRoleId.Value;
        }
        /// <summary>
        /// Indicates if the WebModel has UserSystemRoledata
        /// </summary>
        protected override bool HasData
        {
            get { return GeneralInfoModel.SystemRoleId.HasValue; }
        }
        #endregion
    }
		
}
