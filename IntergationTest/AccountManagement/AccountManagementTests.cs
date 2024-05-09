using NUnit.Framework;
using Sra.P2rmis.Bll.AccessAccountManagement;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using DBIntegrationTest.Base;
using DBIntegrationTest.Data;
namespace DBIntegrationTest.AccessAccountManagement
{
    [TestClass()]
    public class AccountManagementTests : DBIntegrationBase
    {
        private IUserProfileManagementService theProfileService;
        private IAccessAccountManagementService theAccessAccountService;
        private UserData userData;
        private UnitOfWork uow;
        private int? profileUserId = null;

        [OneTimeSetUp]
        public void CreateProfile()
        {
            uow = new UnitOfWork();
            uow.UnitofWork(_context);
            theProfileService = new UserProfileManagementService(uow);

            userData = new UserData();            
            ICollection<SaveProfileStatus> status = theProfileService.CreateProfile(userData.GeneralInfo,
                                           userData.WebSites,
                                           userData.InstitutionEmailAddress,
                                           userData.PersonalEmailAddress,
                                           userData.Addresses,
                                           userData.ProfessionalAffiliation,
                                           userData.W9Addresses,
                                           userData.PhoneTypeDropdown,
                                           userData.AlternateContactTypeDropdown,
                                           userData.UserPhones,
                                           userData.AlternateContactPersons,
                                           userData.UserDegrees,
                                           userData.MilitaryServiceAndRank, userData.MilitaryStatus,
                                           userData.MilitaryServiceId,
                                           userData.VendorInfoIndividual,
                                           userData.VendorInfoInstitutional, userData.UserClients, userData.TestUserId, true, ref profileUserId);

            Assert.IsTrue(profileUserId > 0, "LogInValidUserTest Profile Service Save Failed");
            Assert.IsTrue(status.Any(t => t.Equals(SaveProfileStatus.Success)), "LogInValidUserTest Profile Service Save Failed");
        }
       
        [TestMethod()]
        public void LogInValidUserTest()
        {
            var user = theProfileService.GetUser((int)profileUserId);
            theAccessAccountService = new AccessAccountManagementService(uow);
            theAccessAccountService.SavePassword(user.UserId, userData.Password);
            var validUser = theAccessAccountService.ValidateUser(user.UserLogin, userData.Password);
            Assert.IsTrue(validUser, "LogInValidUserTest test failed");
        }

        [TestMethod()]
        public void LogInInvalidUserTest()
        {
                      
            var user = theProfileService.GetUser((int)profileUserId);
            theAccessAccountService = new AccessAccountManagementService(uow);
            var validUser = theAccessAccountService.ValidateUser(user.UserLogin, "randomfailpassword");
            Assert.IsFalse(validUser, "LogInInvalidUserTest  user test failed");
        }
        [TestMethod()]
        public void ValidateUserIdTest()
        {
            var user = theProfileService.GetUser((int)profileUserId);
            theAccessAccountService = new AccessAccountManagementService(uow);
            var validUserName = theAccessAccountService.IsValidUserName(user.UserLogin);
            Assert.IsTrue(validUserName, "ValidateUserIdTest test failed");
        }
        [TestMethod()]
        public void LockUserTest()
        {           
            User user = _context.Users.Include(u => u.UserAccountStatus.Select(y => y.AccountStatusReason))
                .Where(u => u.UserID == (int)profileUserId)
                .FirstOrDefault();

            theAccessAccountService = new AccessAccountManagementService(uow);
            var locked = theAccessAccountService.IsUserLockedOut((int)profileUserId);

            Assert.IsFalse(locked, "LockUserTest Failed User is Locked");
            theAccessAccountService.LockoutUser(userData.TestUserId, (int)profileUserId);
            user = _context.Users.Include(u => u.UserAccountStatus.Select(y => y.AccountStatusReason))
                .Where(u => u.UserID == (int)profileUserId)
                .FirstOrDefault();
            locked = theAccessAccountService.IsUserLockedOut((int)profileUserId);
            Assert.IsTrue(locked, "LockUserTest Failed User is Locked");
        }
        
    }
}


