using System;

namespace Sra.P2rmis.Dal.EntityChanges
{
    /// <summary>
    /// Object representing an EntityPropertChange
    /// </summary>
    public class PropertyChange
    {
        #region Constructor & Set up
        /// <summary>
        /// Constructor - sets both PropertyName & PropertType
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        /// <param name="propertyType">Type of property</param>
        public PropertyChange(Type propertyType, int userInfoChangeLogTypeIndex)
        {
            this.PropertType = propertyType;
            this.UserInfoChangeLogTypeIndex = userInfoChangeLogTypeIndex;
        }
        /// <summary>
        /// Constructor - sets both PropertyName & PropertType
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        /// <param name="propertyType">Type of property</param>
        /// <param name="overRide">Indicates the change should be recorded even if there is no change</param>
        public PropertyChange(Type propertyType, int userInfoChangeLogTypeIndex, bool overRide)
        {
            this.PropertType = propertyType;
            this.UserInfoChangeLogTypeIndex = userInfoChangeLogTypeIndex;
            this.OverRide = overRide;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// The property name
        /// </summary>
        public int UserInfoChangeLogTypeIndex { get; set; }
        /// <summary>
        /// The property type
        /// </summary>
        public Type PropertType { get; set; }
        /// <summary>
        /// Indicates if the property values should be recorded regardless of change
        /// </summary>                 
        public bool OverRide { get; set; }
        #endregion
    }
}
