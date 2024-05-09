using System.Collections.Generic;

namespace Sra.P2rmis.Web.ViewModels.ProgramRegistration
{
    /// <summary>
    /// Business category lookup
    /// </summary>
    public class BusinessCategoryLookup
    {
        /// <summary>
        /// List of category groups
        /// </summary>
        public List<CategoryGroup> CategoryGroups { get; private set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public BusinessCategoryLookup()
        {
            CategoryGroups = new List<CategoryGroup>{
                new CategoryGroup{
                    GroupName=Constants.CategoryName.SmallBusiness,
                    Categories=new List<KeyValuePair<string, string>>{
                        new KeyValuePair<string, string>(Constants.KeyName.SmallBusiness, Constants.KeyDisplayName.SmallBusiness),
                        new KeyValuePair<string, string>(Constants.KeyName.WomanOwned, Constants.KeyDisplayName.WomanOwned),
                        new KeyValuePair<string, string>(Constants.KeyName.VeteranOwned, Constants.KeyDisplayName.VeteranOwned),
                        new KeyValuePair<string, string>(Constants.KeyName.ServiceDisabledVeteranOwned, Constants.KeyDisplayName.ServiceDisabledVeteranOwned),
                        new KeyValuePair<string, string>(Constants.KeyName.HubZone, Constants.KeyDisplayName.HubZone),
                        new KeyValuePair<string, string>(Constants.KeyName.Disadvantaged, Constants.KeyDisplayName.Disadvantaged)
                    }
                },
                new CategoryGroup{
                    GroupName=Constants.CategoryName.LargeBusiness,
                    Categories=new List<KeyValuePair<string, string>>{
                        new KeyValuePair<string, string>(Constants.KeyName.LargeBusiness, Constants.KeyDisplayName.LargeBusiness)
                    }
                },
                new CategoryGroup{
                    GroupName=Constants.CategoryName.NoBusiness,
                    Categories=new List<KeyValuePair<string, string>>{
                        new KeyValuePair<string, string>(Constants.KeyName.NoBusinessCategory, Constants.KeyDisplayName.NoBusiness)
                    }
                }
            };
        }
        /// <summary>
        /// Category group
        /// </summary>
        public class CategoryGroup
        {
            /// <summary>
            /// Group name
            /// </summary>
            public string GroupName { get; set; }
            /// <summary>
            /// Categories
            /// </summary>
            public List<KeyValuePair<string, string>> Categories { get; set; }
        }
        static public class Constants
        {
            static public class CategoryName
            {
                static public string SmallBusiness { get { return "Small Business"; } }
                static public string LargeBusiness { get { return "Large Business"; } }
                static public string NoBusiness { get { return "No Business Category"; } }
            }

            static public class KeyName
            { 
                static public string SmallBusiness { get { return "Small Business"; } }
                static public string WomanOwned { get { return "Woman-Owned"; } }
                static public string VeteranOwned { get { return "Veteran-Owned"; } }
                static public string ServiceDisabledVeteranOwned { get { return "Service-Disabled Veteran Owned"; } }
                static public string HubZone { get { return "HubZone"; } }
                static public string Disadvantaged { get { return "Disadvantaged"; } }
                static public string LargeBusiness { get { return "Large Business"; } }
                static public string NoBusinessCategory { get { return "No Business Category"; } }
            }
            static public class KeyDisplayName
            {
                static public string SmallBusiness { get { return "Small Business"; } }
                static public string WomanOwned { get { return "Woman-Owned"; } }
                static public string VeteranOwned { get { return "Veteran-Owned"; } }
                static public string ServiceDisabledVeteranOwned { get { return "Service-Disabled Veteran Owned (certification required)"; } }
                static public string HubZone { get { return "HubZone (certification required)"; } }
                static public string Disadvantaged { get { return "Disadvantaged (certification required)"; } }
                static public string LargeBusiness { get { return "Large Business"; } }
                static public string NoBusiness { get { return "No Business"; } }
            }

        }
    }
}