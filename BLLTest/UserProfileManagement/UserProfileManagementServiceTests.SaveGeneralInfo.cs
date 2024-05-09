using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.UserProfileManagement;
using TestMethod = NUnit.Framework.TestAttribute;

namespace BLLTest.UserProfileManagement
{
    public partial class UserProfileManagementServiceTests
    {
        string firstNameValue = "first";
        string miValue = "middle";
        string lastNameValue = "last";
        string nicknameValue = "nick";
        int prefixValue = 4;
        string badgeNameValue = "badge name";
        int genderIdValue = 12;
        int ethnicityIdValue = 5;
        string suffixTextValue = "suffix text value";
        int profileTypeIdvalue = 8;
        #region Update - UserInfo entity
        /// <summary>
        /// Test - expected fields changed or not changed
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService.Save")]
        public void UpdateGeneralInfoUpdateTest()
        {
            //
            // Set up local data
            //
            UserInfo ui = new UserInfo();
            UserProfile up = new UserProfile();
            ui.UserProfiles.Add(up);
            GeneralInfoModel general = InitializeGeneralInfo();
            IList<WebsiteModel> website = new List<WebsiteModel>();
            //
            // Test
            this.theTestUserProfileManagementService.Update(ui, general, _goodUserId);
            //
            // Verify
            //
            Assert.AreEqual(firstNameValue, ui.FirstName, "Unexpected property change");
            Assert.AreEqual(miValue, ui.MiddleName, "Unexpected property change");
            Assert.AreEqual(lastNameValue, ui.LastName, "Unexpected property change");
            Assert.AreEqual(nicknameValue, ui.NickName, "Unexpected property change");
            Assert.AreEqual(prefixValue, ui.PrefixId, "Unexpected property change");
            Assert.AreEqual(badgeNameValue, ui.BadgeName, "Unexpected property change");
            Assert.AreEqual(genderIdValue, ui.GenderId, "Unexpected property change");
            Assert.AreEqual(ethnicityIdValue, ui.EthnicityId, "Unexpected property change");
            Assert.AreEqual(suffixTextValue, ui.SuffixText, "Unexpected property change");
            Assert.AreEqual(1, ui.UserProfiles.Count(), "Unexpected property change");
            Assert.AreEqual(up, ui.UserProfiles.First(), "Unexpected property change");
            Assert.IsNull(ui.CreatedBy, "Unexpected property change");
            Assert.IsNull(ui.ModifiedBy, "Unexpected property change");
            Assert.IsNull(ui.CreatedDate, "Unexpected property change");
            Assert.IsNull(ui.ModifiedDate, "Unexpected property change");
            Assert.IsNull(ui.DeletedBy, "Unexpected property change");
            Assert.IsNull(ui.DeletedDate, "Unexpected property change");

            Assert.AreEqual(0, ui.UserInfoID, "property changed when it should not have");
            Assert.AreEqual(0, ui.UserID, "property changed when it should not have");
            Assert.IsNull(ui.MilitaryRankId, "property changed when it should not have");
            Assert.IsNull(ui.MilitaryStatusTypeId, "property changed when it should not have");
            Assert.IsNull(ui.Institution, "property changed when it should not have");
            Assert.IsNull(ui.Department, "property changed when it should not have");
            Assert.IsNull(ui.Position, "property changed when it should not have");
            Assert.IsNull(ui.MilitaryStatusTypeId, "property changed when it should not have");
        }
        /// <summary>
        /// Test - expected fields changed on create
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService.Save")]
        public void UpdateGeneralInfoCreateTest()
        {
            //
            // Set up local data
            //
            UserInfo ui = new UserInfo();
            GeneralInfoModel general = InitializeGeneralInfo();
            IList<WebsiteModel> website = new List<WebsiteModel>();
            MockUnitOfWorkUserProfileRepository();
            Expect.Call(delegate { theUserProfileRepositoryMock.Add(null); }).IgnoreArguments();
            theMock.ReplayAll();
            //
            // Test
            this.theTestUserProfileManagementService.Update(ui, general, _goodUserId);
            //
            // Verify
            //
            Assert.AreEqual(firstNameValue, ui.FirstName, "Unexpected property change");
            Assert.AreEqual(miValue, ui.MiddleName, "Unexpected property change");
            Assert.AreEqual(lastNameValue, ui.LastName, "Unexpected property change");
            Assert.AreEqual(nicknameValue, ui.NickName, "Unexpected property change");
            Assert.AreEqual(prefixValue, ui.PrefixId, "Unexpected property change");
            Assert.AreEqual(badgeNameValue, ui.BadgeName, "Unexpected property change");
            Assert.AreEqual(genderIdValue, ui.GenderId, "Unexpected property change");
            Assert.AreEqual(ethnicityIdValue, ui.EthnicityId, "Unexpected property change");
            Assert.AreEqual(suffixTextValue, ui.SuffixText, "Unexpected property change");
            Assert.AreEqual(1, ui.UserProfiles.Count(), "Unexpected property change");
            Assert.IsNull(ui.CreatedBy, "Unexpected property change");
            Assert.IsNull(ui.ModifiedBy, "Unexpected property change");
            Assert.IsNull(ui.CreatedDate, "Unexpected property change");
            Assert.IsNull(ui.ModifiedDate, "Unexpected property change");
            Assert.IsNull(ui.DeletedBy, "Unexpected property change");
            Assert.IsNull(ui.DeletedDate, "Unexpected property change");

            IList<object[]> argsPerCall = theUserProfileRepositoryMock.GetArgumentsForCallsMadeOn(x => x.Add(null));
            UserProfile uPEntityCreated = argsPerCall[0][0] as UserProfile;
            Assert.IsNotNull(uPEntityCreated.CreatedBy, "Unexpected property change");
            Assert.IsNotNull(uPEntityCreated.ModifiedBy, "Unexpected property change");
            Assert.IsNotNull(uPEntityCreated.CreatedDate, "Unexpected property change");
            Assert.IsNotNull(uPEntityCreated.ModifiedDate, "Unexpected property change");
            Assert.IsNull(ui.DeletedBy, "Unexpected property change");
            Assert.IsNull(ui.DeletedDate, "Unexpected property change");
            theUserProfileRepositoryMock.AssertWasCalled(x => x.Add(uPEntityCreated));

            Assert.AreEqual(0, ui.UserInfoID, "property changed when it should not have");
            Assert.AreEqual(0, ui.UserID, "property changed when it should not have");
            Assert.IsNull(ui.MilitaryRankId, "property changed when it should not have");
            Assert.IsNull(ui.MilitaryStatusTypeId, "property changed when it should not have");
            Assert.IsNull(ui.Institution, "property changed when it should not have");
            Assert.IsNull(ui.Department, "property changed when it should not have");
            Assert.IsNull(ui.Position, "property changed when it should not have");
            Assert.IsNull(ui.MilitaryStatusTypeId, "property changed when it should not have");
        }
        #endregion
        #region Helpers
        private GeneralInfoModel InitializeGeneralInfo()
        {
            return new GeneralInfoModel { FirstName = firstNameValue,
                                          MI = miValue, 
                                          LastName = lastNameValue, 
                                          NickName = nicknameValue, 
                                          PrefixId = prefixValue, 
                                          Badge = badgeNameValue,
                                          GenderId = genderIdValue,
                                          EthinicityId = ethnicityIdValue,
                                          Suffix = suffixTextValue,
                                          ProfileTypeId = profileTypeIdvalue
                                          };
        }
        #endregion
		
    }
}
