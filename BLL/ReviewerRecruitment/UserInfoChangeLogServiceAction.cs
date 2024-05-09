using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.EntityChanges;
using System;


namespace Sra.P2rmis.Bll.ReviewerRecruitment
{
    /// <summary>
    /// Service Action method to perform CRUD operations on UserInfoChangeLog entities.
    /// </summary>
    public class UserInfoChangeLogServiceAction : ServiceAction<UserInfoChangeLog>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public UserInfoChangeLogServiceAction() { }
        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        public void Populate(int userInfoChangeLogEntityId)
        {
            this.EntityIdentifier = userInfoChangeLogEntityId;
        }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the UserInfoChangeLog entity with information in the ServiceAction.
        /// </summary>
        protected override void Populate(UserInfoChangeLog entity)
        {
            entity.Populate(this.UserId);
        }
        /// <summary>
        /// Indicates if the PanelUserRegistrationDocumentItem has data.
        /// </summary>
        protected override bool HasData
        {
            get
            {
                //
                // By definition we are expecting that it will have data.  Even
                // null or the empty string should be stored.
                //
                return true;
            }
        }
        /// <summary>
        /// Indicates if the data represents a delete.  For this ServiceAction Deletes are not applicable.
        /// </summary>
        protected override bool IsDelete
        {
            get { return false; }
        }
        #endregion
    }

    /// <summary>
    /// Service Action for creating UserInfoChangeLog entities
    /// </summary>
    public class UserInfoChangeLogCreateServiceAction: UserInfoChangeLogServiceAction
    {
        #region Constructor & set up
        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        /// <param name="entityPropertyChange">Description of the property change</param>
        public void Populate(EntityPropertyChange entityPropertyChange, int userInfoEntityIdentifier)
        {
            this.OldValue = entityPropertyChange.OldValue;
            this.NewValue = entityPropertyChange.NewValue;
            this.EntityFieldName = entityPropertyChange.EntityFieldName;
            this.EntityTableName = entityPropertyChange.EntityTableName;
            this.EntityIdentifer = entityPropertyChange.EntityId;
            this.UserInfoEntityIdentifier = userInfoEntityIdentifier;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// UserInfo entity identifier
        /// </summary>
        protected int UserInfoEntityIdentifier { get; set; }
        /// <summary>
        /// The previous value for this field of this record
        /// </summary>
        protected string OldValue { get; set; }
        /// <summary>
        /// The new value for this field of this record
        /// </summary>
        protected string NewValue { get; set; }
        /// <summary>
        /// The ChangeType for this field of this record
        /// </summary>
        //protected EntityState ChangeType { get; set; }
        /// <summary>
        /// The field name
        /// </summary>
        protected string EntityFieldName { get; set; }
        /// <summary>
        /// The table name
        /// </summary>
        protected string EntityTableName { get; set; }
        /// <summary>
        /// The record's identifier.  
        /// 
        /// Note:  The ServiceAction has a property to hold the entity's 
        /// identifier.  However knowing that the ServiceAction will read this as a
        /// "modify" operation one needs create a separate property to hold it.  Also
        /// it is actual data that is stored in the UserInfoChangeLog entity.
        /// </summary>
        protected int EntityIdentifer { get; set; }
        /// <summary>
        /// Value for Missing CV
        /// </summary>
        private string MissingCV { get { return "Missing"; } }
        /// <summary>
        /// Value for Existence of CV
        /// </summary>
        private string ExistingCV { get { return "Present"; } }
        /// <summary>
        /// Value for a new CV
        /// </summary>
        private string NewCV { get { return "New CV Available"; } }
        /// <summary>
        /// Value for new degree and major value
        /// </summary>
        private void DegreeMajor()
        {
            int idEnd = this.OldValue.IndexOf(",");

            string oldId = this.OldValue.Substring(0, idEnd);
            string oldMajor = this.OldValue.Substring(idEnd + 1);

            idEnd = this.NewValue.IndexOf(",");
            string newId = this.NewValue.Substring(0, idEnd);
            string newMajor = this.NewValue.Substring(idEnd + 1);

            DegreeLookupConverter converter = LookupConverterFactory.Make(this.EntityTableName) as DegreeLookupConverter;

            Tuple<string, string> converted = converter.Convert(UnitOfWork.DegreeRepository, oldId, newId);
            this.OldValue = converted.Item1 + oldMajor;
            this.NewValue = converted.Item2 + newMajor;
        }

        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the UserInfoChangeLog entity with information in the ServiceAction.
        /// </summary>
        protected override void Populate(UserInfoChangeLog entity)
        {
            entity.Populate(OldValue, NewValue, EntityIdentifer, UserInfoEntityIdentifier, MapPropertyNameToUserInfoChangeTypeId());
        }
        #endregion
        #region Optional Overrides
        /// <summary>
        /// PreAdd processing. Data values are manipulated prior to populating the entity object.
        ///   - ProfessionalAffiliation:
        ///   - States:
        ///   - UserResume: Display values show existence & change status
        /// </summary>
        protected override void PreAdd()
        {
            if (this.EntityFieldName == ProfessionalAffiliation.Fields.ProfessionalAffiliationId)
            {
                ProfessionalAffilationLookupConverter converter = LookupConverterFactory.Make(this.EntityTableName) as ProfessionalAffilationLookupConverter;

                Tuple<string, string> converted = converter.Convert(UnitOfWork.ProfessionalAffiliationRepository, this.OldValue, this.NewValue);
                this.OldValue = converted.Item1;
                this.NewValue = converted.Item2;
            }
            else if (this.EntityFieldName == State.Fields.StateId)
            {
                StateLookupConverter converter = LookupConverterFactory.Make(this.EntityTableName) as StateLookupConverter;

                Tuple<string, string> converted = converter.Convert(UnitOfWork.StateRepository, this.OldValue, this.NewValue);
                this.OldValue = converted.Item1;
                this.NewValue = converted.Item2;
            }
            else if (UserResume.ChangeLogRequired.ContainsKey(this.EntityFieldName) && (this.EntityTableName == typeof(UserResume).Name))
            {
                this.OldValue = (string.IsNullOrEmpty(this.OldValue)) ? MissingCV : ExistingCV;
                this.NewValue = (this.OldValue == ExistingCV) ? NewCV : ExistingCV;
            }
            else if (this.EntityTableName == typeof(UserDegree).Name)
            {
                DegreeMajor();
            }
            else
            {
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Maps the table name and field name to the UserInfoChangeLogType
        /// </summary>
        /// <returns>UserInfoChangeLogType identifier</returns>
        protected int MapPropertyNameToUserInfoChangeTypeId()
        {
            int result = 0;

            if (EntityTableName == typeof(UserInfo).Name)
            {
                result = UserInfo.ChangeLogRequired[this.EntityFieldName].UserInfoChangeLogTypeIndex;
            }
            else if (EntityTableName == typeof(UserEmail).Name)
            {
                result = UserEmail.ChangeLogRequired[this.EntityFieldName].UserInfoChangeLogTypeIndex;
            }
            else if (EntityTableName == typeof(UserAddress).Name)
            {
                result = UserAddress.ChangeLogRequired[this.EntityFieldName].UserInfoChangeLogTypeIndex;
            }
            else if (EntityTableName == typeof(UserResume).Name)
            {
                result = UserResume.ChangeLogRequired[this.EntityFieldName].UserInfoChangeLogTypeIndex;
            }
            else if (EntityTableName == typeof(User).Name)
            {
                result = User.ChangeLogRequired[this.EntityFieldName].UserInfoChangeLogTypeIndex;
            }
            else if (EntityTableName == typeof(UserDegree).Name)
            {
                result = UserDegree.ChangeLogRequired[this.EntityFieldName].UserInfoChangeLogTypeIndex;
            }
            else
            {
                result = 0;
            }
            return result;
        }
        #endregion
    }
}
