using System.ComponentModel.DataAnnotations;

namespace Sra.P2rmis.Dal
{
    [MetadataType(typeof(UserMetaData))]   
    public partial class UserInfo
    {

        public string FullUserName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        //public string FullNameDegree
        //{
        //    get
        //    {
        //        return LastName + ", " + FirstName + ", " + LookupDegree.DegreeName;
        //    }
        //}

        public string FullNameLastCommaFirst
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        /// <summary>
        /// Default value for Linq search.
        /// </summary>
        private static UserInfo _default;
        public static UserInfo Default
        {
            get
            {
                //
                // Lazy load the default
                //
                if (_default == null)
                {
                    _default = new UserInfo();
                    _default.UserEmails.Add(new UserEmail {Email = null, PrimaryFlag = false });
                }
                return _default;
            }
        }

       

        public class UserMetaData
        {
            
            [Required]
            [MaxLength(50)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [MaxLength(100)]
            [Display(Name="Last Name")]
            public string LastName { get; set; }

            //[Required]
            //[MaxLength(100)]
            //[Display(Name = "Emergency Contact Name")]
            //public string ECName { get; set; }

            //[Required]
            //[MaxLength(20)]
            //[Display(Name = "Emergency Contact Phone")]
            //public string ECPhone { get; set; }

            //[Required]
            //[MaxLength(100)]
            //[Display(Name = "Emergency Contact Email")]
            //public string ECEmail { get; set; }

        }
    }


}
