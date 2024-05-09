using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
		
namespace BLLTest
{
    /// <summary>
    /// Unit tests for LookupService
    /// </summary>
    [TestClass()]
    public class LookupServiceTests : BLLBaseTest
    {
        #region Overhead
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            this.InitializeMocks();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            this.CleanUpMocks();
        }
        //
        #endregion
        #endregion
        #region Lookup Service Tests
        #region Gender Lookup Tests
        /// <summary>
        /// Test ListGender
        /// </summary>
        [TestMethod()]
        [Category("LookupService")]
        public void ListGenderTest()
        {
            //
            // Set up local data
            //
            var list = new List<Gender>();
            list.Add(new Gender { GenderId = 100, Gender1 = "Gender 1" });
            list.Add(new Gender { GenderId = 110, Gender1 = "Gender 2" });
            list.Add(new Gender { GenderId = 101, Gender1 = "Gender 3" });
            //
            // Test
            //
            MockUnitOfWorkGenderRepository();
            Expect.Call(this.theGenderRepositoryMock.GetAll()).Return(list);
            theMock.ReplayAll();
            var result = this.theTestLookupService.ListGender();
            //
            // Verify
            //
            Assert.AreEqual(list.Count, result.ModelList.Count(), "ListGender returned an incorrect number of entries");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 100 && x.Value == "Gender 1"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 110 && x.Value == "Gender 2"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 101 && x.Value == "Gender 3"), "did not find entry");

            Assert.IsTrue(result.ModelList.ElementAt(0).Value == "Gender 3", "ListGender is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(1).Value == "Gender 2", "ListGender is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(2).Value == "Gender 1", "ListGender is not ordered correctly");
        }		
        #endregion
        #region Ethnicity Lookup Tests
        /// <summary>
        /// Test ListEthnicity
        /// </summary>
        [TestMethod()]
        [Category("LookupService")]
        public void ListEthnicityTest()
        {
            //
            // Set up local data
            //
            var list = new List<Ethnicity>();
            list.Add(new Ethnicity { EthnicityId = 100, EthnicityLabel = "a label 6" });
            list.Add(new Ethnicity { EthnicityId = 110, EthnicityLabel = "a label 2" });
            list.Add(new Ethnicity { EthnicityId = 101, EthnicityLabel = "a label 3" });
            list.Add(new Ethnicity { EthnicityId = 33,  EthnicityLabel = "a label 1" });
            //
            // Test
            //
            MockUnitOfWorkEthnicityRepository();
            Expect.Call(this.theEthnicityRepositoryMock.GetAll()).Return(list);
            theMock.ReplayAll();
            var result = this.theTestLookupService.ListEthnicity();
            //
            // Verify
            //
            Assert.AreEqual(list.Count, result.ModelList.Count(), "ListEthnicity returned an incorrect number of entries");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 33 && x.Value == "a label 1"), "ListEthnicity - did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 110 && x.Value == "a label 2"), "ListEthnicity - did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 101 && x.Value == "a label 3"), "ListEthnicity - did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 100 && x.Value == "a label 6"), "ListEthnicity - did not find entry");

            Assert.IsTrue(result.ModelList.ElementAt(0).Value == "a label 1", "ListEthnicity is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(1).Value == "a label 2", "ListEthnicity is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(2).Value == "a label 3", "ListEthnicity is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(3).Value == "a label 6", "ListEthnicity is not ordered correctly");
        }
        #endregion
        #region Prefix Lookup Tests
        /// <summary>
        /// Test ListPrefix
        /// </summary>
        [TestMethod()]
        [Category("LookupService")]
        public void ListPrefixTest()
        {
            //
            // Set up local data
            //
            var list = new List<Prefix>();
            list.Add(new Prefix { PrefixId = 100, PrefixName = "1 name 1" });
            list.Add(new Prefix { PrefixId = 102, PrefixName = "5 name 1" });
            list.Add(new Prefix { PrefixId = 110, PrefixName = "2 name 1" });
            list.Add(new Prefix { PrefixId = 200, PrefixName = "4 name 1" });
            list.Add(new Prefix { PrefixId = 111, PrefixName = "3 name 1" });
            //
            // Test
            //
            MockUnitOfWorkPrefixRepository();
            Expect.Call(this.thePrefixRepositoryMock.GetAll()).Return(list);
            theMock.ReplayAll();
            var result = this.theTestLookupService.ListPrefix();
            //
            // Verify
            //
            Assert.AreEqual(list.Count, result.ModelList.Count(), "ListPrefix returned an incorrect number of entries");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 100 && x.Value == "1 name 1"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 110 && x.Value == "2 name 1"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 111 && x.Value == "3 name 1"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 200 && x.Value == "4 name 1"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 102 && x.Value == "5 name 1"), "did not find entry");
            //\
            // ordering to be tested after db change.
            //
            //Assert.IsTrue(result.ModelList.ElementAt(0).Value == "1 name 1", "ListPrefix is not ordered correctly");
            //Assert.IsTrue(result.ModelList.ElementAt(1).Value == "2 name 1", "ListPrefix is not ordered correctly");
            //Assert.IsTrue(result.ModelList.ElementAt(2).Value == "3 name 1", "ListPrefix is not ordered correctly");
            //Assert.IsTrue(result.ModelList.ElementAt(3).Value == "4 name 1", "ListPrefix is not ordered correctly");
            //Assert.IsTrue(result.ModelList.ElementAt(4).Value == "5 name 1", "ListPrefix is not ordered correctly");
        }
        #endregion
        #region ListPhoneType Lookup Tests
        /// <summary>
        /// Test ListPrefix
        /// </summary>
        [TestMethod()]
        [Category("LookupService")]
        public void ListPhoneTypeTest()
        {
            //
            // Set up local data
            //
            var list = new List<PhoneType>();
            list.Add(new PhoneType { PhoneTypeId = 1, PhoneType1 = "Type11" });
            list.Add(new PhoneType { PhoneTypeId = 2, PhoneType1 = "Type21" });
            list.Add(new PhoneType { PhoneTypeId = 3, PhoneType1 = "Type31" });
            list.Add(new PhoneType { PhoneTypeId = 4, PhoneType1 = "Type41" });
            list.Add(new PhoneType { PhoneTypeId = 5, PhoneType1 = "Type51" });
            list.Add(new PhoneType { PhoneTypeId = 6, PhoneType1 = "Type61" });
            list.Add(new PhoneType { PhoneTypeId = 7, PhoneType1 = "Type71" });
            //
            // Test
            //
            MockUnitOfWorkPhoneTypeRepository();
            Expect.Call(this.thePhoneTypeRepositoryMock.GetAll()).Return(list);
            theMock.ReplayAll();
            var result = this.theTestLookupService.ListPhoneType();
            //
            // Verify
            //
            Assert.AreEqual(list.Count, result.ModelList.Count(), "ListPhoneType returned an incorrect number of entries");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 1 && x.Value == "Type11"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 2 && x.Value == "Type21"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 3 && x.Value == "Type31"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 4 && x.Value == "Type41"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 5 && x.Value == "Type51"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 6 && x.Value == "Type61"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 7 && x.Value == "Type71"), "did not find entry");
        }
        #endregion
        #region ListStateByName Lookup Tests
        /// <summary>
        /// Test ListStateByName
        /// </summary>
        [TestMethod()]
        [Category("LookupService")]
        public void ListStateByNameTest()
        {
            //
            // Set up local data
            //
            var list = new List<State>();
            list.Add(new State { StateId = 4, StateName = "Zame4", StateAbbreviation = "ZB4" });
            list.Add(new State { StateId = 1, StateName = "Name1", StateAbbreviation = "AB1" });
            list.Add(new State { StateId = 3, StateName = "Name3", StateAbbreviation = "AB3" });
            list.Add(new State { StateId = 2, StateName = "Name2", StateAbbreviation = "AB2" });
            //
            // Test
            //
            MockUnitOfWorkStateRepository();
            Expect.Call(this.theStateRepositoryMock.GetAll()).Return(list);
            theMock.ReplayAll();
            var result = this.theTestLookupService.ListStateByName();
            //
            // Verify
            //
            Assert.AreEqual(list.Count, result.ModelList.Count(), "ListStateByName returned an incorrect number of entries");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 1 && x.Value == "Name1"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 2 && x.Value == "Name2"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 3 && x.Value == "Name3"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 4 && x.Value == "Zame4"), "did not find entry");
            //
            // Verify order
            //
            Assert.IsTrue(result.ModelList.ElementAt(0).Value == "Name1", "ListStateByName is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(1).Value == "Name2", "ListStateByName is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(2).Value == "Name3", "ListStateByName is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(3).Value == "Zame4", "ListStateByName is not ordered correctly");
        }
        #endregion
        #region ListCountryByName Lookup Tests
        /// <summary>
        /// Test ListCountryByName
        /// </summary>
        [TestMethod()]
        [Category("LookupService")]
        public void ListCountryByNameTest()
        {
            //
            // Set up local data
            //
            var list = new List<Country>();
            list.Add(new Country { CountryId = 41, CountryName = "Name4", CountryAbbreviation = "cAB4" });
            list.Add(new Country { CountryId =  1, CountryName = "Name1", CountryAbbreviation = "cAB1" });
            list.Add(new Country { CountryId = 21, CountryName = "Name2", CountryAbbreviation = "cAB2" });
            list.Add(new Country { CountryId = 51, CountryName = "Name5", CountryAbbreviation = "cAB5" });
            list.Add(new Country { CountryId = 31, CountryName = "Name3", CountryAbbreviation = "cAB3" });
            //
            // Test
            //
            MockUnitOfWorkCountryRepository();
            Expect.Call(this.theCountryRepositoryMock.GetAll()).Return(list);
            theMock.ReplayAll();
            var result = this.theTestLookupService.ListCountryByName();
            //
            // Verify
            //
            Assert.AreEqual(list.Count, result.ModelList.Count(), "ListCountryByName returned an incorrect number of entries");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 1 && x.Value == "Name1"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 21 && x.Value == "Name2"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 31 && x.Value == "Name3"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 41 && x.Value == "Name4"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 51 && x.Value == "Name5"), "did not find entry");
            //
            // Verify order
            //
            Assert.IsTrue(result.ModelList.ElementAt(0).Value == "Name1", "ListCountryByName is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(1).Value == "Name2", "ListCountryByName is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(2).Value == "Name3", "ListCountryByName is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(3).Value == "Name4", "ListCountryByName is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(4).Value == "Name5", "ListCountryByName is not ordered correctly");
        }
        #endregion
        #region ListDegree Lookup Tests
        /// <summary>
        /// Test ListDegree
        /// </summary>
        [TestMethod()]
        [Category("LookupService")]
        public void ListDegreeTest()
        {
            //
            // Set up local data
            //
            var list = new List<Degree>();
            list.Add(new Degree { DegreeId =  1, DegreeName = "Name1" });
            list.Add(new Degree { DegreeId = 21, DegreeName = "Name21" });
            list.Add(new Degree { DegreeId = 41, DegreeName = "Name41" });
            list.Add(new Degree { DegreeId = 51, DegreeName = "Name51" });
            list.Add(new Degree { DegreeId = 61, DegreeName = "Name61" });
            list.Add(new Degree { DegreeId = 31, DegreeName = "Name31" });
            //
            // Test
            //
            MockUnitOfWorkDegreeRepository();
            Expect.Call(this.theDegreeRepositoryMock.GetAll()).Return(list);
            theMock.ReplayAll();
            var result = this.theTestLookupService.ListDegree();
            //
            // Verify
            //
            Assert.AreEqual(list.Count, result.ModelList.Count(), "ListDegree returned an incorrect number of entries");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index ==  1 && x.Value == "Name1"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 21 && x.Value == "Name21"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 31 && x.Value == "Name31"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 41 && x.Value == "Name41"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 51 && x.Value == "Name51"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 61 && x.Value == "Name61"), "did not find entry");
            //
            // Verify order
            //
            Assert.IsTrue(result.ModelList.ElementAt(0).Value == "Name1", "ListDegree is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(1).Value == "Name21", "ListDegree is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(2).Value == "Name31", "ListDegree is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(3).Value == "Name41", "ListDegree is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(4).Value == "Name51", "ListDegree is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(5).Value == "Name61", "ListDegree is not ordered correctly");
            
        }
        #endregion
        #region ListProfileType Lookup Tests
        /// <summary>
        /// Test ListDegree
        /// </summary>
        [TestMethod()]
        [Category("LookupService")]
        public void ListProfileTypeTest()
        {
            //
            // Set up local data
            //
            var list = new List<ProfileType>();
            list.Add(new ProfileType { ProfileTypeId =  1, ProfileTypeName= "Profile Type 1" , SortOrder = 3});
            list.Add(new ProfileType { ProfileTypeId = 31, ProfileTypeName = "Profile Type 31", SortOrder = 0 });
            list.Add(new ProfileType { ProfileTypeId = 51, ProfileTypeName = "Profile Type 41", SortOrder = 2 });
            list.Add(new ProfileType { ProfileTypeId = 61, ProfileTypeName = "Profile Type 61", SortOrder = 1 });
            //
            // Test
            //
            MockUnitOfWorkProfileTypeRepository();
            Expect.Call(this.theProfileTypeRepositoryMock.GetAll()).Return(list);
            theMock.ReplayAll();
            var result = this.theTestLookupService.ListProfileType();
            //
            // Verify
            //
            Assert.AreEqual(list.Count, result.ModelList.Count(), "ListProfileType returned an incorrect number of entries");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 1 && x.Value == "Profile Type 1"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 31 && x.Value == "Profile Type 31"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 51 && x.Value == "Profile Type 41"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 61 && x.Value == "Profile Type 61"), "did not find entry");
            //
            // Verify order
            //
            Assert.IsTrue(result.ModelList.ElementAt(0).Value == "Profile Type 31", "ListProfileType is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(1).Value == "Profile Type 61", "ListProfileType is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(2).Value == "Profile Type 41", "ListProfileType is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(3).Value == "Profile Type 1", "ListProfileType is not ordered correctly");
        }
        #endregion
        #region ListService Lookup Tests
        /// <summary>
        /// Test ListService
        /// </summary>
        [TestMethod()]
        [Category("LookupService")]
        public void ListServiceTest()
        {
            //
            // Set up local data
            //
            var list = new List<MilitaryRank>();
            list.Add(new MilitaryRank { MilitaryRankId = 1, MilitaryRankName = "name 1", Service = "service 1" });
            list.Add(new MilitaryRank { MilitaryRankId = 2, MilitaryRankName = "name 2", Service = "service 2" });
            list.Add(new MilitaryRank { MilitaryRankId = 3, MilitaryRankName = "name 3", Service = "service 3" });
            list.Add(new MilitaryRank { MilitaryRankId = 4, MilitaryRankName = "name 4", Service = "service 3" });
            list.Add(new MilitaryRank { MilitaryRankId = 5, MilitaryRankName = "name 5", Service = "service 1" });
            list.Add(new MilitaryRank { MilitaryRankId = 6, MilitaryRankName = "name 6", Service = "service 2" });
            list.Add(new MilitaryRank { MilitaryRankId = 7, MilitaryRankName = "name 7", Service = "service 1" });
            list.Add(new MilitaryRank { MilitaryRankId = 8, MilitaryRankName = "name 8", Service = "service 3" });
            list.Add(new MilitaryRank { MilitaryRankId = 9, MilitaryRankName = "name 9", Service = "service 2" });
            list.Add(new MilitaryRank { MilitaryRankId = 10, MilitaryRankName = "name 0", Service = "service 4" });
            //
            // Test
            //
            MockUnitOfWorkMilitaryRankRepository();
            Expect.Call(this.theMilitaryRankRepositoryMock.GetAll()).Return(list);
            theMock.ReplayAll();
            var result = this.theTestLookupService.ListMilitaryService();
            //
            // Verify
            //
            Assert.AreEqual(4, result.ModelList.Count(), "ListMilitaryService returned an incorrect number of entries");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Value == "service 1"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Value == "service 2"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Value == "service 3"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Value == "service 4"), "did not find entry");
            //
            // Verify order
            //
            Assert.IsTrue(result.ModelList.ElementAt(0).Value == "service 1", "ListMilitaryService is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(1).Value == "service 2", "ListMilitaryService is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(2).Value == "service 3", "ListMilitaryService is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(3).Value == "service 4", "ListMilitaryService is not ordered correctly");
        }
        #endregion
        #region ListRank Lookup Tests
        /// <summary>
        /// Test ListMilitaryRanks
        /// </summary>
        [TestMethod()]
        [Category("LookupService")]
        public void ListRankTest()
        {
            //
            // Set up local data
            //
            var list = new List<MilitaryRank>();
            list.Add(new MilitaryRank { MilitaryRankId = 1, MilitaryRankName = "name 1", Service = "service 1", SortOrder = 0});
            list.Add(new MilitaryRank { MilitaryRankId = 2, MilitaryRankName = "name 2", Service = "service 2", SortOrder = 5 });
            list.Add(new MilitaryRank { MilitaryRankId = 3, MilitaryRankName = "name 3", Service = "service 3", SortOrder = 6 });
            list.Add(new MilitaryRank { MilitaryRankId = 4, MilitaryRankName = "name 4", Service = "service 3", SortOrder = 7 });
            list.Add(new MilitaryRank { MilitaryRankId = 5, MilitaryRankName = "name 5", Service = "service 1", SortOrder = 1 });
            list.Add(new MilitaryRank { MilitaryRankId = 6, MilitaryRankName = "name 6", Service = "service 2", SortOrder = 4 });
            list.Add(new MilitaryRank { MilitaryRankId = 7, MilitaryRankName = "name 7", Service = "service 1", SortOrder = 2 });
            list.Add(new MilitaryRank { MilitaryRankId = 8, MilitaryRankName = "name 8", Service = "service 3", SortOrder = 8 });
            list.Add(new MilitaryRank { MilitaryRankId = 9, MilitaryRankName = "name 9", Service = "service 2", SortOrder = 3 });
            list.Add(new MilitaryRank { MilitaryRankId = 10, MilitaryRankName = "name 0", Service = "service 4", SortOrder = 0 });
            //
            // Test
            //
            MockUnitOfWorkMilitaryRankRepository();
            Expect.Call(this.theMilitaryRankRepositoryMock.GetAll()).Return(list);
            theMock.ReplayAll();
            var result = this.theTestLookupService.ListMilitaryRanks("service 2");
            //
            // Verify
            //
            Assert.AreEqual(3, result.ModelList.Count(), "ListMilitaryRanks returned an incorrect number of entries");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Value == "name 2"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Value == "name 6"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Value == "name 9"), "did not find entry");
            //
            // Verify order
            //
            Assert.IsTrue(result.ModelList.ElementAt(0).Value == "name 9", "ListMilitaryRanks is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(1).Value == "name 6", "ListMilitaryRanks is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(2).Value == "name 2", "ListMilitaryRanks is not ordered correctly");
        }
        /// <summary>
        /// Test ListMilitaryRanks
        /// </summary>
        [TestMethod()]
        [Category("LookupService")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "LookupService.ListMilitaryRanks detected an invalid parameter: service was []")]
        public void ListRankNullRankTest()
        {
            //
            // Set up local data
            //
            var list = new List<MilitaryRank>();
            list.Add(new MilitaryRank { MilitaryRankId = 1, MilitaryRankName = "name 1", Service = "service 1" });
            list.Add(new MilitaryRank { MilitaryRankId = 2, MilitaryRankName = "name 2", Service = "service 2" });
            list.Add(new MilitaryRank { MilitaryRankId = 3, MilitaryRankName = "name 3", Service = "service 3" });
            list.Add(new MilitaryRank { MilitaryRankId = 4, MilitaryRankName = "name 4", Service = "service 3" });
            list.Add(new MilitaryRank { MilitaryRankId = 5, MilitaryRankName = "name 5", Service = "service 1" });
            list.Add(new MilitaryRank { MilitaryRankId = 6, MilitaryRankName = "name 6", Service = "service 2" });
            list.Add(new MilitaryRank { MilitaryRankId = 7, MilitaryRankName = "name 7", Service = "service 1" });
            list.Add(new MilitaryRank { MilitaryRankId = 8, MilitaryRankName = "name 8", Service = "service 3" });
            list.Add(new MilitaryRank { MilitaryRankId = 9, MilitaryRankName = "name 9", Service = "service 2" });
            list.Add(new MilitaryRank { MilitaryRankId = 10, MilitaryRankName = "name 0", Service = "service 4" });
            //
            // Test
            //
            MockUnitOfWorkMilitaryRankRepository();
            Expect.Call(this.theMilitaryRankRepositoryMock.GetAll()).Return(list);
            theMock.ReplayAll();
            var result = this.theTestLookupService.ListMilitaryRanks(null);
        }
        /// <summary>
        /// Test ListMilitaryRanks
        /// </summary>
        [TestMethod()]
        [Category("LookupService")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "LookupService.ListMilitaryRanks detected an invalid parameter: service was []")]
        public void ListRankEmptyRankTest()
        {
            //
            // Set up local data
            //
            var list = new List<MilitaryRank>();
            list.Add(new MilitaryRank { MilitaryRankId = 1, MilitaryRankName = "name 1", Service = "service 1" });
            list.Add(new MilitaryRank { MilitaryRankId = 2, MilitaryRankName = "name 2", Service = "service 2" });
            list.Add(new MilitaryRank { MilitaryRankId = 3, MilitaryRankName = "name 3", Service = "service 3" });
            list.Add(new MilitaryRank { MilitaryRankId = 4, MilitaryRankName = "name 4", Service = "service 3" });
            list.Add(new MilitaryRank { MilitaryRankId = 5, MilitaryRankName = "name 5", Service = "service 1" });
            list.Add(new MilitaryRank { MilitaryRankId = 6, MilitaryRankName = "name 6", Service = "service 2" });
            list.Add(new MilitaryRank { MilitaryRankId = 7, MilitaryRankName = "name 7", Service = "service 1" });
            list.Add(new MilitaryRank { MilitaryRankId = 8, MilitaryRankName = "name 8", Service = "service 3" });
            list.Add(new MilitaryRank { MilitaryRankId = 9, MilitaryRankName = "name 9", Service = "service 2" });
            list.Add(new MilitaryRank { MilitaryRankId = 10, MilitaryRankName = "name 0", Service = "service 4" });
            //
            // Test
            //
            MockUnitOfWorkMilitaryRankRepository();
            Expect.Call(this.theMilitaryRankRepositoryMock.GetAll()).Return(list);
            theMock.ReplayAll();
            var result = this.theTestLookupService.ListMilitaryRanks(string.Empty);
        }
        #endregion
        #region ListEmailAddressType Tests - no reference
        /// <summary>
        /// Test MilitaryStatus look up
        /// </summary>
        //[TestMethod()]
        //[Category("LookupService")]
        //public void ListEmailAddressTypeTest()
        //{
        //    //
        //    // Set up local data
        //    //
        //    var list = new List<EmailAddressType>();
        //    list.Add(new EmailAddressType { EmailAddressTypeId = 1, EmailAddressType1 = "Type 1" });
        //    list.Add(new EmailAddressType { EmailAddressTypeId = 3, EmailAddressType1 = "Type 2" });
        //    list.Add(new EmailAddressType { EmailAddressTypeId = 10, EmailAddressType1 = "Type 4" });
        //    //
        //    // Test
        //    //
        //    MockUnitOfWorkEmailAddressTypeRepository();
        //    Expect.Call(this.theEmailAddressTypeRepository.GetAll()).Return(list);
        //    theMock.ReplayAll();
        //    var result = this.theTestLookupService.ListEmailAddressType();
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(list.Count, result.ModelList.Count(), "ListEmailAddressType returned an incorrect number of entries");
        //    Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 1 && x.Value == "Type 1"), "did not find entry");
        //    Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 3 && x.Value == "Type 2"), "did not find entry");
        //    Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 10 && x.Value == "Type 4"), "did not find entry");
        //    //
        //    //  order is not specified at this point in time
        //    //
        //}
        #endregion
        #region ListMilitaryStatusType Tests
        /// <summary>
        /// Test MilitaryStatus look up
        /// </summary>
        [TestMethod()]
        [Category("LookupService")]
        public void ListMilitaryStatusTypeTest()
        {
            //
            // Set up local data
            //
            var list = new List<MilitaryStatusType>();
            list.Add(new MilitaryStatusType { MilitaryStatusTypeId = 1, StatusType = "AApe 1", SortOrder = 1 });
            list.Add(new MilitaryStatusType { MilitaryStatusTypeId = 2, StatusType = "ZZZZZ 1", SortOrder = 0 });
            //
            // Test
            //
            MockUnitOfWorkMilitaryStatusTypeRepository();           
            Expect.Call(this.theMilitaryStatusTypeRepository.GetAll()).Return(list);
            theMock.ReplayAll();
            var result = this.theTestLookupService.ListMilitaryStatusType();
            //
            // Verify
            //
            Assert.AreEqual(list.Count, result.ModelList.Count(), "ListMilitaryStatusType returned an incorrect number of entries");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 1 && x.Value == "AApe 1"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 2 && x.Value == "ZZZZZ 1"), "did not find entry");
            //
            // Verify order
            //
            Assert.IsTrue(result.ModelList.ElementAt(0).Value == "ZZZZZ 1", "ListMilitaryStatusType is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(1).Value == "AApe 1", "ListMilitaryStatusType is not ordered correctly");
        }
        #endregion
        #region ListAlternateContactType Tests
        /// <summary>
        /// Test AlternateContactType look up
        /// </summary>
        [TestMethod()]
        [Category("LookupService")]
        public void ListAlternateContactTypeTest()
        {
            //
            // Set up local data
            //
            var list = new List<AlternateContactType>();
            list.Add(new AlternateContactType { AlternateContactTypeId = 1, AlternateContactType1 = "Spouse", SortOrder = 1 });
            list.Add(new AlternateContactType { AlternateContactTypeId = 13, AlternateContactType1 = "Other", SortOrder = 2 });
            list.Add(new AlternateContactType { AlternateContactTypeId = 2, AlternateContactType1 = "Assistant", SortOrder = 0 });
            //
            // Test
            //
            MockUnitOfWorkAlternateContactTypeRepository();
            Expect.Call(this.theAlternateContactTypeRepository.GetAll()).Return(list);
            theMock.ReplayAll();
            var result = this.theTestLookupService.ListAlternateContactType();
            //
            // Verify
            //
            Assert.AreEqual(list.Count, result.ModelList.Count(), "ListAlternateContactType returned an incorrect number of entries");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 1 && x.Value == "Spouse"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 13 && x.Value == "Other"), "did not find entry");
            Assert.IsNotNull(result.ModelList.FirstOrDefault(x => x.Index == 2 && x.Value == "Assistant"), "did not find entry");
            //
            // Verify order
            //
            Assert.IsTrue(result.ModelList.ElementAt(0).Value == "Assistant", "ListAlternateContactType is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(1).Value == "Spouse", "ListAlternateContactType is not ordered correctly");
            Assert.IsTrue(result.ModelList.ElementAt(2).Value == "Other", "ListAlternateContactType is not ordered correctly");
        }
        #endregion
        #endregion
    }
}
