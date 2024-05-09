using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sra.P2rmis.CrossCuttingServices;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;

namespace Sra.P2rmis.CrossCuttingServicesTest
{
    
    
    /// <summary>
    ///This is a test class for ViewHelpersTest and is intended
    ///to contain all ViewHelpersTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ViewHelpersTest
    {


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
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Average 1 decimal place
        ///</summary>
        [TestMethod()]
        public void AverageTest1()
        {
            List<decimal> list = new List<decimal>() { 3.1m, 1.9m, 1.1m, 1.7m, 2.1m };
            Decimal expected = 1.98m; 
            Decimal actual;
            actual = ViewHelpers.Average(list);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for Average 2 decimal place
        ///</summary>
        [TestMethod()]
        public void AverageTest2()
        {
            List<Decimal> list = new List<Decimal>() { 0, 0.35m, 1.40m, 0.05m, 1.10m, 0.15m };
            Decimal expected = 0.5083333333333333333333333333m;
            Decimal actual;
            actual = ViewHelpers.Average(list);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for Average 3 decimal place
        ///</summary>
        [TestMethod()]
        public void AverageTest3()
        {
            List<Decimal> list = new List<Decimal>() { 1, 3, 4, 7, 5, 3 };
            Decimal expected = 3.8333333333333333333333333333m;
            Decimal actual;
            actual = ViewHelpers.Average(list);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for ConstructName
        ///</summary>
        [TestMethod()]
        public void ConstructNameTest1()
        {
            string lastName = "Unnithan";
            string firstName = "Pushpa";
            string expected = "Unnithan, Pushpa";
            string actual;
            actual = ViewHelpers.ConstructName(lastName, firstName);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for ConstructName with some common special characters
        ///</summary>
        [TestMethod()]
        public void ConstructNameTest2()
        {
            string lastName = "Coffey, Jr.";
            string firstName = "Danny-boy";
            string expected = "Coffey, Jr., Danny-boy";
            string actual;
            actual = ViewHelpers.ConstructName(lastName, firstName);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for ConstructNameWithPrefix
        ///</summary>
        [TestMethod()]
        public void ConstructNameWithPrefixTest1()
        {
            string prefix = "Dr.";
            string firstName = "Pushpa"; 
            string lastName = "Unnithan"; 
            string expected = "Dr. Pushpa Unnithan"; 
            string actual;
            actual = ViewHelpers.ConstructNameWithPrefix(prefix, firstName, lastName);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for ConstructNameWithPrefix with some common special characters
        ///</summary>
        [TestMethod()]
        public void ConstructNameWithPrefixTest2()
        {
            string prefix = "Mr.";
            string firstName = "Danny-boy";
            string lastName = "Coffey, Jr.";
            string expected = "Mr. Danny-boy Coffey, Jr.";
            string actual;
            actual = ViewHelpers.ConstructNameWithPrefix(prefix, firstName, lastName);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// A test for ConstructNameWithComma
        /// </summary>
        [TestMethod()]
        public void ConstructNameWithCommaTest()
        {
            string firstName = "Craigy";
            string lastName = "Henson";
            string expected = "Henson, Craigy";
            string actual;
            actual = ViewHelpers.ConstructNameWithComma(firstName, lastName);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// A test for ConstructNameWithComma with special characters
        /// </summary>
        [TestMethod()]
        public void ConstructNameWithCommaTest2()
        {
            string firstName = "Craigy-nasty";
            string lastName = "Henson, Sr.";
            string expected = "Henson, Sr., Craigy-nasty";
            string actual;
            actual = ViewHelpers.ConstructNameWithComma(firstName, lastName);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// A test for ConstructNameWithComma with null parameter values
        /// </summary>
        [TestMethod()]
        public void ConstructNameWithCommaNullParameterTest()
        {
            string firstName = null;
            string lastName = null;
            string expected = "";
            string actual;
            actual = ViewHelpers.ConstructNameWithComma(firstName, lastName);
            Assert.AreEqual(expected, actual);
        }       
        /// <summary>
        ///A test for IsCritiqueSubmitted true
        ///</summary>
        [TestMethod()]
        public void IsCritiqueSubmittedTest1()
        {
            Nullable<DateTime> DateSubmitted = DateTime.Parse("2/10/2012");
            bool expected = true;
            bool actual;
            actual = ViewHelpers.IsCritiqueSubmitted(DateSubmitted);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for IsCritiqueSubmitted false
        ///</summary>
        [TestMethod()]
        public void IsCritiqueSubmittedTest2()
        {
            Nullable<DateTime> DateSubmitted = null;
            bool expected = false;
            bool actual;
            actual = ViewHelpers.IsCritiqueSubmitted(DateSubmitted);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for IsOpen true
        ///
        ///</summary>
        [TestMethod()]
        public void IsOpenTest1()
        {
            DateTime openDate = DateTime.Parse("1/1/1960");
            DateTime closeDate = DateTime.Parse("1/1/2050");
            bool expected = true; 
            bool actual;
            actual = ViewHelpers.IsOpen(openDate, closeDate);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for IsOpen false
        ///</summary>
        [TestMethod()]
        public void IsOpenTest2()
        {
            DateTime openDate = DateTime.Parse("5/25/2012");
            DateTime closeDate = DateTime.Parse("6/2/2012");
            bool expected = false;
            bool actual;
            actual = ViewHelpers.IsOpen(openDate, closeDate);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for MakeReviewerName standard
        ///</summary>
        [TestMethod()]
        public void MakeReviewerNameTest1()
        {
            string firstName = "Pushpa"; 
            string lastName = "Unnithan";
            string expected = "Pushpa Unnithan";
            string actual;
            actual = ViewHelpers.ConstructNameWithSpace(firstName, lastName);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for MakeReviewerName with special characters
        ///</summary>
        [TestMethod()]
        public void MakeReviewerNameTest2()
        {
            string firstName = "Danny-boy";
            string lastName = "Coffey, Jr.";
            string expected = "Danny-boy Coffey, Jr.";
            string actual;
            actual = ViewHelpers.ConstructNameWithSpace(firstName, lastName);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for MakeScoreboardReviewerName with some special characters
        ///</summary>
        [TestMethod()]
        public void MakeScoreboardReviewerNameTest1()
        {
            string firstName = "Pushpa";
            string lastName = "Unnithan";
            string expected = "P. Unnithan";
            string actual;
            actual = ViewHelpers.MakeScoreboardReviewerName(firstName, lastName);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for MakeScoreboardReviewerName with some special characters
        ///</summary>
        [TestMethod()]
        public void MakeScoreboardReviewerNameTest2()
        {
            string firstName = "Danny-boy";
            string lastName = "Coffey, Jr.";
            string expected = "D. Coffey, Jr.";
            string actual;
            actual = ViewHelpers.MakeScoreboardReviewerName(firstName, lastName);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for P2rmisRound simple
        ///</summary>
        [TestMethod()]
        public void P2rmisRoundTest1()
        {
            Decimal value = 1.4m;
            Decimal expected = 1.4m;
            Decimal actual;
            actual = ViewHelpers.P2rmisRound(value);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for P2rmisRound round up
        ///</summary>
        [TestMethod()]
        public void P2rmisRoundTest2()
        {
            Decimal value = 2.666666666667m;
            Decimal expected = 2.7m;
            Decimal actual;
            actual = ViewHelpers.P2rmisRound(value);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for P2rmisRound round down
        ///</summary>
        [TestMethod()]
        public void P2rmisRoundTest3()
        {
            Decimal value = 2.3333333333333m;
            Decimal expected = 2.3m;
            Decimal actual;
            actual = ViewHelpers.P2rmisRound(value);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for P2rmisRound round midpoint
        ///</summary>
        [TestMethod()]
        public void P2rmisRoundTest4()
        {
            Decimal value = 2.45m;
            Decimal expected = 2.5m;
            Decimal actual;
            actual = ViewHelpers.P2rmisRound(value);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for P2rmisRoundTwoDecimalPlaces simple
        ///</summary>
        [TestMethod()]
        public void P2rmisRoundTwoDecimalPlacesTest1()
        {
            Decimal value = 2.1m; 
            Decimal expected = 2.10m; 
            Decimal actual;
            actual = ViewHelpers.P2rmisRoundTwoDecimalPlaces(value);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for P2rmisRoundTwoDecimalPlaces round up
        ///</summary>
        [TestMethod()]
        public void P2rmisRoundTwoDecimalPlacesTest2()
        {
            Decimal value = 1.477777778m; 
            Decimal expected = 1.48m;
            Decimal actual;
            actual = ViewHelpers.P2rmisRoundTwoDecimalPlaces(value);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for P2rmisRoundTwoDecimalPlaces round down
        ///</summary>
        [TestMethod()]
        public void P2rmisRoundTwoDecimalPlacesTest3()
        {
            Decimal value = 1.2666667m;
            Decimal expected = 1.27m;
            Decimal actual;
            actual = ViewHelpers.P2rmisRoundTwoDecimalPlaces(value);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for P2rmisRoundTwoDecimalPlaces midpoint
        ///</summary>
        [TestMethod()]
        public void P2rmisRoundTwoDecimalPlacesTest4()
        {
            Decimal value = 0.445m; 
            Decimal expected = 0.45m; 
            Decimal actual;
            actual = ViewHelpers.P2rmisRoundTwoDecimalPlaces(value);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for rounding to 1 decimal
        ///</summary>
        [TestMethod()]
        public void RoundTest1()
        {
            Decimal value = 0.345m;
            int decimals = 1; 
            MidpointRounding mode = MidpointRounding.AwayFromZero; 
            Decimal expected = 0.3m; 
            Decimal actual;
            actual = ViewHelpers.Round(value, decimals, mode);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for rounding to 2 decimals
        ///</summary>
        [TestMethod()]
        public void RoundTest2()
        {
            Decimal value = 0.345m;
            int decimals = 2;
            MidpointRounding mode = MidpointRounding.AwayFromZero;
            Decimal expected = 0.35m;
            Decimal actual;
            actual = ViewHelpers.Round(value, decimals, mode);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for SetNonNull with NULL value passed in
        ///</summary>
        [TestMethod()]
        public void SetNonNullTest1()
        {
            string value = null;
            string expected = string.Empty; 
            string actual;
            actual = ViewHelpers.SetNonNull(value);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for SetNonNull with string value passed in
        ///</summary>
        [TestMethod()]
        public void SetNonNullTest2()
        {
            string value = "P2RMIS";
            string expected = "P2RMIS";
            string actual;
            actual = ViewHelpers.SetNonNull(value);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for SplitDelimitedString one value passed in
        ///</summary>
        [TestMethod()]
        public void SplitDelimitedStringTest1()
        {
            string StringToSplit = "SR"; 
            string[] expected = { "SR" };           
            string[] actual;
            actual = ViewHelpers.SplitDelimitedString(StringToSplit);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for SplitDelimitedString multiple values passed in
        ///</summary>
        [TestMethod()]
        public void SplitDelimitedStringTest2()
        {
            string StringToSplit = "SR;CR;TC";
            string[] expected = { "SR", "CR", "TC" };
            string[] actual;
            actual = ViewHelpers.SplitDelimitedString(StringToSplit);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for SplitDelimitedString multiple values passed in with special characters
        ///</summary>
        [TestMethod()]
        public void SplitDelimitedStringTest3()
        {
            string StringToSplit = "P. Unnithan, Ph.D.;D. Coffey, Jr.;C. Henson";
            string[] expected = { "P. Unnithan, Ph.D.", "D. Coffey, Jr.", "C. Henson" };
            string[] actual;
            actual = ViewHelpers.SplitDelimitedString(StringToSplit);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for StandardDeviation
        ///</summary>
        [TestMethod()]
        public void StandardDeviationTest()
        {
            List<decimal> list = new List<decimal>() { 1.2m, 1.6m, 2.0m };
            decimal meanValue = 1.6m;
            decimal expected = 0.4m; 
            decimal actual;
            actual = ViewHelpers.StandardDeviation(list, meanValue);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Test - correctly formatted date
        /// </summary>
        [TestMethod()]
        public void FormatDate_GoodTest()
        {
            DateTime d = new DateTime(1998, 5, 22);

            string s = ViewHelpers.FormatDate(d);

            Assert.AreEqual("5/22/1998", s, "The date was not formatted correctly");
        }
        /// <summary>
        /// Test - correctly formatted date
        /// </summary>
        [TestMethod()]
        public void FormatDate_NullTest()
        {
            string s = ViewHelpers.FormatDate(null);

            Assert.AreEqual(string.Empty, s, "The date was not formatted correctly");
        }
        #region IsAbleToSubmitCritique Tests
        private bool contentDoesExist { get; set; }
        private DateTime? submittedDate { get; set; }
        private int stepOrder { get; set; }
        private int maxStepOrder { get; set; }
        private DateTime startDate { get; set; }
        private DateTime endDate { get; set; }
        private DateTime? reOpenDate { get; set; }
        private DateTime? reOpenEndDate { get; set; }

        private const bool WithContext = true;
        private const bool NoContext = false;
        private const int StepEqual = 2;
        private const int StepNotEqual = 1;
        private DateTime? NoReopenDate = null;
        private DateTime? NoReopenEndDate = null;
        private DateTime? NoSubmittedDate = null;
        private DateTime? ASubmittedDate = GlobalProperties.P2rmisDateTimeNow;
        private DateTime DefaultStartDate = GlobalProperties.P2rmisDateTimeNow.AddDays(-3);
        private DateTime DefaultEndDate = GlobalProperties.P2rmisDateTimeNow.AddDays(3);
        private DateTime DefaultStartDateForReopen = GlobalProperties.P2rmisDateTimeNow.AddDays(-6);
        private DateTime DefaultEndDateForReopen = GlobalProperties.P2rmisDateTimeNow.AddDays(-4);
        private DateTime? DefaultReopenStartDate = GlobalProperties.P2rmisDateTimeNow.AddDays(-3);
        private DateTime? DefaultReopenEndDate = GlobalProperties.P2rmisDateTimeNow.AddDays(3);

        private bool HasManageCritiquePermissions = true;
        private bool NoManageCritiquePermissions = false;
        #region No submitted date; steps equal; no reopen date; with context       
        /// <summary>
        /// Test submittal before start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void BeforeStartDateTimeWithContextCurrentStep()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepEqual, StepEqual, GlobalProperties.P2rmisDateTimeNow.AddDays(1), DefaultEndDate, NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AtStartDateTimeWithContextCurrentStep()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepEqual, StepEqual, GlobalProperties.P2rmisDateTimeNow, DefaultEndDate, NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(true, result);
        }
        /// <summary>
        /// Test submittal at start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AfterStartDateTimeWithContextCurrentStep()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepEqual, StepEqual, DefaultStartDate, DefaultEndDate, NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(true, result);
        }
        /// <summary>
        /// Test submittal at end date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AtEndtDateTimeWithContextCurrentStep()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepEqual, StepEqual, DefaultStartDate, GlobalProperties.P2rmisDateTimeNow, NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at end date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AfterEndtDateTimeWithContextCurrentStep()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepEqual, StepEqual, DefaultStartDate, GlobalProperties.P2rmisDateTimeNow.AddDays(-1), NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        #endregion
        #region No submitted date; steps equal; no reopen date; no context
        /// <summary>
        /// Test submittal before start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void BeforeStartDateTimeNoContextCurrentStep()
        {
            SetIsAbleToSubmitCritiqueParameters(NoContext, NoSubmittedDate, StepEqual, StepEqual, GlobalProperties.P2rmisDateTimeNow.AddDays(1), DefaultEndDate, NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AtStartDateTimeNoContextCurrentStep()
        {
            SetIsAbleToSubmitCritiqueParameters(NoContext, NoSubmittedDate, StepEqual, StepEqual, GlobalProperties.P2rmisDateTimeNow, DefaultEndDate, NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AfterStartDateTimeNoContextCurrentStep()
        {
            SetIsAbleToSubmitCritiqueParameters(NoContext, NoSubmittedDate, StepEqual, StepEqual, DefaultStartDate, DefaultEndDate, NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at end date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AtEndtDateTimeNoContextCurrentStep()
        {
            SetIsAbleToSubmitCritiqueParameters(NoContext, NoSubmittedDate, StepEqual, StepEqual, DefaultStartDate, GlobalProperties.P2rmisDateTimeNow, NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at end date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AfterEndtDateTimeNoContextCurrentStep()
        {
            SetIsAbleToSubmitCritiqueParameters(NoContext, NoSubmittedDate, StepEqual, StepEqual, DefaultStartDate, GlobalProperties.P2rmisDateTimeNow.AddDays(-1), NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        #endregion
        #region No submitted date; steps not equal; no reopen date; with context
        /// <summary>
        /// Test submittal before start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void BeforeStartDateTimeWithContextNotCurrentStep()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepNotEqual, StepEqual, GlobalProperties.P2rmisDateTimeNow.AddDays(1), DefaultEndDate, NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AtStartDateTimeWithContextNotCurrentStep()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepNotEqual, StepEqual, GlobalProperties.P2rmisDateTimeNow, DefaultEndDate, NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AfterStartDateTimeWithContextNotCurrentStep()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepNotEqual, StepEqual, DefaultStartDate, DefaultEndDate, NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at end date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AtEndtDateTimeWithContextNotCurrentStep()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepNotEqual, StepEqual, DefaultStartDate, GlobalProperties.P2rmisDateTimeNow, NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at end date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AfterEndtDateTimeWithContextNotCurrentStep()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepNotEqual, StepEqual, DefaultStartDate, GlobalProperties.P2rmisDateTimeNow.AddDays(-1), NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        #endregion
        #region A submitted date; steps equal; no reopen date; with context       
        /// <summary>
        /// Test submittal before start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void BeforeStartDateTimeWithContextCurrentStepAndSubmittedDate()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, ASubmittedDate, StepEqual, StepEqual, GlobalProperties.P2rmisDateTimeNow.AddDays(1), DefaultEndDate, NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AtStartDateTimeWithContextCurrentStepAndSubmittedDate()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, ASubmittedDate, StepEqual, StepEqual, GlobalProperties.P2rmisDateTimeNow, DefaultEndDate, NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AfterStartDateTimeWithContextCurrentStepAndSubmittedDate()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, ASubmittedDate, StepEqual, StepEqual, DefaultStartDate, DefaultEndDate, NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at end date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AtEndtDateTimeWithContextCurrentStepAndSubmittedDate()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, ASubmittedDate, StepEqual, StepEqual, DefaultStartDate, GlobalProperties.P2rmisDateTimeNow, NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at end date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AfterEndtDateTimeWithContextCurrentStepAndSubmittedDate()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, ASubmittedDate, StepEqual, StepEqual, DefaultStartDate, GlobalProperties.P2rmisDateTimeNow.AddDays(-1), NoReopenDate, NoReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        #endregion
        #region No submitted date; steps equal;  reopen date; with context
        /// <summary>
        /// Test submittal before start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void BeforeStartDateTimeWithContextCurrentStepReopen()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, GlobalProperties.P2rmisDateTimeNow.AddDays(1), DefaultReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AtStartDateTimeWithContextCurrentStepReopen()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, GlobalProperties.P2rmisDateTimeNow, DefaultReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(true, result);
        }
        /// <summary>
        /// Test submittal at start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AfterStartDateTimeWithContextCurrentStepReopen()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, DefaultReopenStartDate, DefaultReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(true, result);
        }
        /// <summary>
        /// Test submittal at end date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AtEndtDateTimeWithContextCurrentStepReopen()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, DefaultReopenStartDate, GlobalProperties.P2rmisDateTimeNow);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal after end date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AfterEndtDateTimeWithContextCurrentStepReopen()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, DefaultReopenStartDate, GlobalProperties.P2rmisDateTimeNow.AddDays(-1));

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        #endregion
        #region No submitted date; steps equal;  reopen date; no context
        /// <summary>
        /// Test submittal before start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void BeforeStartDateTimeNoContextCurrentStepReopen()
        {
            SetIsAbleToSubmitCritiqueParameters(NoContext, NoSubmittedDate, StepEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, GlobalProperties.P2rmisDateTimeNow.AddDays(1), DefaultReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AtStartDateTimeNoContextCurrentStepReopen()
        {
            SetIsAbleToSubmitCritiqueParameters(NoContext, NoSubmittedDate, StepEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, GlobalProperties.P2rmisDateTimeNow, DefaultReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AfterStartDateTimeNoContextCurrentStepReopen()
        {
            SetIsAbleToSubmitCritiqueParameters(NoContext, NoSubmittedDate, StepEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, DefaultReopenStartDate, DefaultReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at end date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AtEndtDateTimeNoContextCurrentStepReopen()
        {
            SetIsAbleToSubmitCritiqueParameters(NoContext, NoSubmittedDate, StepEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, DefaultReopenStartDate, GlobalProperties.P2rmisDateTimeNow);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal after end date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AfterEndtDateTimeNoContextCurrentStepReopen()
        {
            SetIsAbleToSubmitCritiqueParameters(NoContext, NoSubmittedDate, StepEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, DefaultReopenStartDate, GlobalProperties.P2rmisDateTimeNow.AddDays(-1));

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        #endregion
        #region No submitted date; steps not equal;  reopen date; with context
        /// <summary>
        /// Test submittal before start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void BeforeStartDateTimeWithContextNotCurrentStepReopen()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepNotEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, GlobalProperties.P2rmisDateTimeNow.AddDays(1), DefaultReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AtStartDateTimeWithContextNotCurrentStepReopen()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepNotEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, GlobalProperties.P2rmisDateTimeNow, DefaultReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AfterStartDateTimeWithContextNotCurrentStepReopen()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepNotEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, DefaultReopenStartDate, DefaultReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at end date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AtEndtDateTimeWithContextNotCurrentStepReopen()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepNotEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, DefaultReopenStartDate, GlobalProperties.P2rmisDateTimeNow);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal after end date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AfterEndtDateTimeWithContextNotCurrentStepReopen()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, NoSubmittedDate, StepNotEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, DefaultReopenStartDate, GlobalProperties.P2rmisDateTimeNow.AddDays(-1));

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        #endregion
        #region submitted date; steps  equal;  reopen date; with context
        /// <summary>
        /// Test submittal before start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void BeforeStartDateTimeWithContextNotCurrentStepReopenWithSubmittedDate()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, ASubmittedDate, StepEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, GlobalProperties.P2rmisDateTimeNow.AddDays(1), DefaultReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AtStartDateTimeWithContextNotCurrentStepReopenWithSubmittedDate()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, ASubmittedDate, StepEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, GlobalProperties.P2rmisDateTimeNow, DefaultReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at start date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AfterStartDateTimeWithContextNotCurrentStepReopenWithSubmittedDate()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, ASubmittedDate, StepEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, DefaultReopenStartDate, DefaultReopenEndDate);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal at end date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AtEndtDateTimeWithContextNotCurrentStepReopenWithSubmittedDate()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, ASubmittedDate, StepEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, DefaultReopenStartDate, GlobalProperties.P2rmisDateTimeNow);

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test submittal after end date
        /// </summary>
        [TestMethod()]
        [Category("ViewHelpers.IsAbleToSubmitCritique")]
        public void AfterEndtDateTimeWithContextNotCurrentStepReopenWithSubmittedDate()
        {
            SetIsAbleToSubmitCritiqueParameters(WithContext, ASubmittedDate, StepEqual, StepEqual, DefaultStartDateForReopen, DefaultEndDateForReopen, DefaultReopenStartDate, GlobalProperties.P2rmisDateTimeNow.AddDays(-1));

            bool result = ViewHelpers.IsAbleToSubmitCritique(contentDoesExist, submittedDate, stepOrder, maxStepOrder, startDate, endDate, reOpenDate, reOpenEndDate, HasManageCritiquePermissions);

            Assert.AreEqual(false, result);
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Initializes values for IsAbleToSubmitCritique test
        /// </summary>
        private void SetIsAbleToSubmitCritiqueParameters(bool contentDoesExistValue, DateTime? submittedDateValue, int stepOrderValue, int maxStepOrderValue, DateTime startDateValue, DateTime endDateValue, DateTime? reOpenDateValue, DateTime? reOpenEndDateValue)
        {
            contentDoesExist = contentDoesExistValue;
            submittedDate = submittedDateValue;
            stepOrder = stepOrderValue;
            maxStepOrder = maxStepOrderValue;
            startDate = startDateValue;
            endDate = endDateValue;
            reOpenDate = reOpenDateValue;
            reOpenEndDate = reOpenEndDateValue;
        }
        #endregion
        #endregion
#region ConcatenateStringWithCommaTest

        /// <summary>
        /// Tests ConcatenateStringWithComma with both strings not null.
        /// </summary>
        [TestMethod()]
        public void ConcatenateStringWithCommaTest_NoNull()
        {
            string s1 = "Ok";
            string s2 = "Thanks";
            string result = ViewHelpers.ConcatenateStringWithComma(s1, s2);

            Assert.AreEqual(result, "Ok, Thanks");
        }
        /// <summary>
        /// Tests ConcatenateStringWithComma with first string null.
        /// </summary>
        [TestMethod()]
        public void ConcatenateStringWithCommaTest_FirstStringNull()
        {
            string s1 = null;
            string s2 = "Thanks";
            string result = ViewHelpers.ConcatenateStringWithComma(s1, s2);

            Assert.AreEqual(result, "Thanks");
        }
        /// <summary>
        /// Tests ConcatenateStringWithComma with second string null.
        /// </summary>
        [TestMethod()]
        public void ConcatenateStringWithCommaTest_SecondStringNull()
        {
            string s1 = "Ok";
            string s2 = null;
            string result = ViewHelpers.ConcatenateStringWithComma(s1, s2);

            Assert.AreEqual(result, "Ok");
        }
        /// <summary>
        /// Tests ConcatenateStringWithComma with both strings null.
        /// </summary>
        [TestMethod()]
        public void ConcatenateStringWithCommaTest_BothNull()
        {
            string s1 = null;
            string s2 = null;
            string result = ViewHelpers.ConcatenateStringWithComma(s1, s2);

            Assert.AreEqual(result, string.Empty);
        }

#endregion 
    }
}
