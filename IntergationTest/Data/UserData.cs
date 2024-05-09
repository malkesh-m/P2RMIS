using NUnit.Framework;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.UserProfileManagement;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace DBIntegrationTest.Data
{
    public class UserData
    {
        private const string _firstName = "TestFirstName";
        private const string _mi = "T";
        private const string _lastName = "TestLastName";
        private const string _nickName = "TestNickName";
        private const string _userName = "TestFLastName100";
        private const string _password = "P@$$w0rd12";
        private const string _badge = "TestBadge";
        private const int _male = 1;
        private const int _sraStaff = 3;
        private const string _personalEmail = "Testuser@Testdomain.com";
        private const int _acedamicRankIdNone = 5;
        private const string _kidney = "Polycystic kidney disease";
        private const int _miss = 2;
        private const int _alaskanNative = 2;
        private const int _webmaster = 10;


        public int TestUserId { get; } = 4;

        public GeneralInfoModel GeneralInfo
        {
            get
            {
                return new GeneralInfoModel
                {
                    FirstName = _firstName,
                    MI = _mi,
                    LastName = _lastName,
                    NickName = _nickName,
                    Username = _userName,
                    Badge = _badge,
                    PrefixId = _miss,
                    Suffix = string.Empty,
                    GenderId = _male,
                    EthinicityId = _alaskanNative,
                    ProfileTypeId = _sraStaff,
                    SystemRoleId = _webmaster,
                    AcademicRankId = _acedamicRankIdNone,
                    Expertise = _kidney
                };
            }

        }

        public List<WebsiteModel> WebSites
        {

            get
            {
                return new List<WebsiteModel>();
            }

        }
        public EmailAddressModel InstitutionEmailAddress
        {
            get
            {
                return new EmailAddressModel();
            }
        }
        public EmailAddressModel PersonalEmailAddress
        {
            get
            {
                return new EmailAddressModel
                {
                    Address = _personalEmail,
                    Primary = true
                };
            }
        }
        public List<AddressInfoModel> Addresses
        {
            get
            {
                var resedentialAdress = new AddressInfoModel
                {
                    Address1 = "Key West Ave",
                    Address3 = "Rockville",
                    Address4 = "MD",
                    StateId = 23,
                    AddressTypeId = 3
                };

                return new List<AddressInfoModel> { resedentialAdress };
            }
        }

        public ProfessionalAffiliationModel ProfessionalAffiliation
        {
            get
            {
                return new ProfessionalAffiliationModel
                {
                    ProfessionalAffiliationId = 1,
                    Name = "John Hopkins"
                };
            }
        }

        public W9AddressModel W9Addresses
        {
            get
            {
                return new W9AddressModel();
            }
        }

        public List<IListEntry> PhoneTypeDropdown
        {
            get
            {
                return new List<IListEntry>();
            }
        }

        public List<IListEntry> AlternateContactTypeDropdown
        {
            get
            {
                return new List<IListEntry>();
            }
        }

        public List<PhoneNumberModel> UserPhones
        {
            get
            {
                return new List<PhoneNumberModel>();
            }
        }
        public List<UserAlternateContactPersonModel> AlternateContactPersons
        {
            get
            {
                return new List<UserAlternateContactPersonModel>();
            }
        }

        public List<UserDegreeModel> UserDegrees
        {
            get
            {
                return new List<UserDegreeModel>();
            }
        }

        public IUserMilitaryRankModel MilitaryServiceAndRank
        {
            get
            {
                return new UserMilitaryRankModel();
            }
        }
        public int? MilitaryServiceId
        {
            get
            {
                return null;
            }
        }

        public IUserMilitaryStatusModel MilitaryStatus
        {
            get
            {
                return new UserMilitaryStatusModel(); ;
            }
        }

        public UserVendorModel VendorInfoIndividual
        {
            get
            {
                return new UserVendorModel();
            }
        }

        public UserVendorModel VendorInfoInstitutional
        {
            get
            {
                return new UserVendorModel();
            }
        }
        public List<UserProfileClientModel> UserClients
        {
            get
            {
                var userProfileClientModel = new UserProfileClientModel
                {
                    ClientAbrv = "BNBI",
                    ClientId = 1
                };

                return new List<UserProfileClientModel> { userProfileClientModel };                                                
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
        }

    }
}
