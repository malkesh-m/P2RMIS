using System.Collections.Generic;
using NUnit.Framework;
using Sra.P2rmis.CrossCuttingServices;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;



namespace Sra.P2rmis.NotificationServicesTest
{    
    /// <summary>
    ///This is a test class for MergeFieldReplacerTest and is intended
    ///to contain all MergeFieldReplacerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MergeFieldReplacerTest
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
        ///A test for GetMergeData
        ///</summary>
        [TestMethod()]
        public void GetMergeDataTest()
        {
            MergeField mergeFields = new MergeField();
            mergeFields.ToEmailAddress = "primo@sra.com";
            mergeFields.ToFirstName = "Zeus";
            mergeFields.ToFullMailingAddress = "123 Main St., Fairfax, VA 22033";
            mergeFields.ToFullName = "Zeus Jockley";
            mergeFields.ToLastName = "Jockley";
            mergeFields.ToPrefix = "Mr.";
            mergeFields.ToRole = "Deity";
            mergeFields.ToSuffix = "Jr.";
            mergeFields.FromEmailAddress = "godsRus@deities.gov";
            mergeFields.FromFirstName = "Mercury";
            mergeFields.FromFullMailingAddress = "123 Deity Way, Athens, Greece";
            mergeFields.FromFullName = "Mercury Planet";
            mergeFields.FromLastName = "Planet";
            mergeFields.FromPrefix = "Dr.";
            mergeFields.FromRole = "Messenger";
            mergeFields.FromSuffix = "Sr.";
            mergeFields.ApplicationTitle = "Grant for Deity Services";
            mergeFields.CritiqueDeadline = "4/15/2012";
            mergeFields.MeetingNameAndDate = "5/26/2012";
            mergeFields.PanelName = "Deity Panel";
            mergeFields.RoleOnPanel = "Chief Deity";
            Dictionary<string, string> expected = new Dictionary<string, string>();
            expected.Add("{^from-prefix^}", mergeFields.FromPrefix);
            expected.Add("{^from-first-name^}", mergeFields.FromFirstName);
            expected.Add("{^from-last-name^}", mergeFields.FromLastName);
            expected.Add("{^from-suffix^}", mergeFields.FromSuffix);
            expected.Add("{^from-full-name^}", mergeFields.FromFullName);
            expected.Add("{^from-email-address^}", mergeFields.FromEmailAddress);
            expected.Add("{^from-full-mailing-address^}", mergeFields.FromFullMailingAddress);
            expected.Add("{^from-role^}", mergeFields.FromRole);
            expected.Add("{^panel-name^}", mergeFields.PanelName);
            expected.Add("{^role-on-panel^}", mergeFields.RoleOnPanel);
            expected.Add("{^application-title^}", mergeFields.ApplicationTitle);
            expected.Add("{^critique-deadline^}", mergeFields.CritiqueDeadline);
            expected.Add("{^meeting-name-and-date^}", mergeFields.MeetingNameAndDate);
            expected.Add("{^to-prefix^}", mergeFields.ToPrefix);
            expected.Add("{^to-first-name^}", mergeFields.ToFirstName);
            expected.Add("{^to-last-name^}", mergeFields.ToLastName);
            expected.Add("{^to-suffix^}", mergeFields.ToSuffix);
            expected.Add("{^to-full-name^}", mergeFields.ToFullName);
            expected.Add("{^to-email-address^}", mergeFields.ToEmailAddress);
            expected.Add("{^to-full-mailing-address^}", mergeFields.ToFullMailingAddress);
            expected.Add("{^to-role^}", mergeFields.ToRole);
            Dictionary<string, string> actual = MergeFieldReplacer.GetMergeData(mergeFields);
            foreach (KeyValuePair<string, string> kvp in expected)
            {
                if (actual.ContainsKey(kvp.Key))
                {
                    Assert.AreEqual(kvp.Value, actual[kvp.Key]);
                }
                else
                {
                    Assert.Fail("Actual dictionary missing key: ", kvp.Key);
                }
            }
        }

        /// <summary>
        ///A test for GetMergeData - test with empty/missing fields
        ///</summary>
        [TestMethod()]
        public void GetMergeDataTest1()
        {
            // removed to-full-mailing-address and from-suffix
            MergeField mergeFields = new MergeField();
            mergeFields.ToEmailAddress = "primo@sra.com";
            mergeFields.ToFirstName = "Zeus";
            mergeFields.ToFullName = "Zeus Jockley";
            mergeFields.ToLastName = "Jockley";
            mergeFields.ToPrefix = "Mr.";
            mergeFields.ToRole = "Deity";
            mergeFields.ToSuffix = "Jr.";
            mergeFields.FromEmailAddress = "godsRus@deities.gov";
            mergeFields.FromFirstName = "Mercury";
            mergeFields.FromFullMailingAddress = "123 Deity Way, Athens, Greece";
            mergeFields.FromFullName = "Mercury Planet";
            mergeFields.FromLastName = "Planet";
            mergeFields.FromPrefix = "Dr.";
            mergeFields.FromRole = "Messenger";
            mergeFields.ApplicationTitle = "Grant for Deity Services";
            mergeFields.CritiqueDeadline = "4/15/2012";
            mergeFields.MeetingNameAndDate = "5/26/2012";
            mergeFields.PanelName = "Deity Panel";
            mergeFields.RoleOnPanel = "Chief Deity";
            Dictionary<string, string> expected = new Dictionary<string, string>();
            expected.Add("{^from-prefix^}", mergeFields.FromPrefix);
            expected.Add("{^from-first-name^}", mergeFields.FromFirstName);
            expected.Add("{^from-last-name^}", mergeFields.FromLastName);
            expected.Add("{^from-suffix^}", mergeFields.FromSuffix);
            expected.Add("{^from-full-name^}", mergeFields.FromFullName);
            expected.Add("{^from-email-address^}", mergeFields.FromEmailAddress);
            expected.Add("{^from-full-mailing-address^}", mergeFields.FromFullMailingAddress);
            expected.Add("{^from-role^}", mergeFields.FromRole);
            expected.Add("{^panel-name^}", mergeFields.PanelName);
            expected.Add("{^role-on-panel^}", mergeFields.RoleOnPanel);
            expected.Add("{^application-title^}", mergeFields.ApplicationTitle);
            expected.Add("{^critique-deadline^}", mergeFields.CritiqueDeadline);
            expected.Add("{^meeting-name-and-date^}", mergeFields.MeetingNameAndDate);
            expected.Add("{^to-prefix^}", mergeFields.ToPrefix);
            expected.Add("{^to-first-name^}", mergeFields.ToFirstName);
            expected.Add("{^to-last-name^}", mergeFields.ToLastName);
            expected.Add("{^to-suffix^}", mergeFields.ToSuffix);
            expected.Add("{^to-full-name^}", mergeFields.ToFullName);
            expected.Add("{^to-email-address^}", mergeFields.ToEmailAddress);
            expected.Add("{^to-full-mailing-address^}", mergeFields.ToFullMailingAddress);
            expected.Add("{^to-role^}", mergeFields.ToRole);
            Dictionary<string, string> actual = MergeFieldReplacer.GetMergeData(mergeFields);
            foreach (KeyValuePair<string, string> kvp in expected)
            {
                if (actual.ContainsKey(kvp.Key))
                {
                    Assert.AreEqual(kvp.Value, actual[kvp.Key]);
                }
                else
                {
                    Assert.Fail("Actual dictionary missing key: ", kvp.Key);
                }
            }
        }

        /// <summary>
        ///A test for Replace
        ///</summary>
        [TestMethod()]
        public void ReplaceTest()
        {
            string CrLf = "\r\n";
            string msgBodyText = @"From: {^from-prefix^} {^from-first-name^} {^from-last-name^}" + CrLf +
                @"{^from-role^}" + CrLf +
                @"{^from-full-mailing-address^}" + CrLf +
                CrLf +
                @"To: {^to-prefix^} {^to-first-name^} {^to-last-name^}" + CrLf +
                @"{^to-full-mailing-address^}" + CrLf +
                CrLf +
                @"Re: {^application-title^}" + CrLf +
                CrLf +
                @"Dear {^to-prefix^} {^to-last-name^}," + CrLf +
                @"Thank you very much for your grant application entitled ""{^application-title^}"".  The meeting to consider your application" + CrLf +
                @"will be held on {^meeting-name-and-date^}.  Our critique panel, named {^panel-name^} will inform you of our decision by" + CrLf +
                @"{^critique-deadline^}.  This decision will be sent to you via email at {^to-email-address^} and via postal mail at" + CrLf +
                @"{^to-full-mailing-address^}.  Should you have any concerns about my participation on the panel as {^from-role^}, please" + CrLf +
                @"send your correspondence to the following email address: {^from-email-address^}." + CrLf +
                CrLf +
                @"Sincerely," + CrLf +
                @"{^from-full-name^}" + CrLf +
                @"{^role-on-panel^}";
            MergeField mergeFields = new MergeField();
            mergeFields.ToEmailAddress = "primo@sra.com";
            mergeFields.ToFirstName = "Zeus";
            mergeFields.ToFullMailingAddress = "123 Main St., Fairfax, VA 22033";
            mergeFields.ToFullName = "Zeus Jockley";
            mergeFields.ToLastName = "Jockley";
            mergeFields.ToPrefix = "Mr.";
            mergeFields.ToRole = "Deity";
            mergeFields.ToSuffix = "Jr.";
            mergeFields.FromEmailAddress = "godsRus@deities.gov";
            mergeFields.FromFirstName = "Mercury";
            mergeFields.FromFullMailingAddress = "123 Deity Way, Athens, Greece";
            mergeFields.FromFullName = "Mercury Planet";
            mergeFields.FromLastName = "Planet";
            mergeFields.FromPrefix = "Dr.";
            mergeFields.FromRole = "Messenger";
            mergeFields.ApplicationTitle = "Grant for Deity Services";
            mergeFields.CritiqueDeadline = "4/15/2012";
            mergeFields.MeetingNameAndDate = "5/26/2012";
            mergeFields.PanelName = "Deity Panel";
            mergeFields.RoleOnPanel = "Chief Deity";
            string expected = @"From: Dr. Mercury Planet" + CrLf +
                @"Messenger" + CrLf +
                @"123 Deity Way, Athens, Greece" + CrLf +
                CrLf +
                @"To: Mr. Zeus Jockley" + CrLf +
                @"123 Main St., Fairfax, VA 22033" + CrLf +
                CrLf +
                @"Re: Grant for Deity Services" + CrLf +
                CrLf +
                @"Dear Mr. Jockley," + CrLf +
                @"Thank you very much for your grant application entitled ""Grant for Deity Services"".  The meeting to consider your application" + CrLf +
                @"will be held on 5/26/2012.  Our critique panel, named Deity Panel will inform you of our decision by" + CrLf +
                @"4/15/2012.  This decision will be sent to you via email at primo@sra.com and via postal mail at" + CrLf +
                @"123 Main St., Fairfax, VA 22033.  Should you have any concerns about my participation on the panel as Messenger, please" + CrLf +
                @"send your correspondence to the following email address: godsRus@deities.gov." + CrLf +
                CrLf +
                @"Sincerely," + CrLf +
                @"Mercury Planet" + CrLf +
                @"Chief Deity";
            string actual = MergeFieldReplacer.Replace(msgBodyText, mergeFields);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Replace - ensuring exception is properly thrown
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(UnknownMergeFieldException))]
        public void ReplaceTest1()
        {
            string CrLf = "\r\n";
            string msgBodyText = @"From: {^from-prefix^} {^from-first-name^} {^from-last-name^}" + CrLf +
                @"{^from-role^}" + CrLf +
                @"{^from-full-mailing-address^}" + CrLf +
                CrLf +
                @"To: {^to-prefix^} {^to-first-name^} {^to-last-name^}" + CrLf +
                @"{^to-full-mailing-address^}" + CrLf +
                CrLf +
                @"Re: {^application-title^}" + CrLf +
                CrLf +
                @"Dear {^to-prefix^} {^to-last-name^}," + CrLf +
                @"Thank you very much for your grant application entitled ""{^application-title^}"".  The meeting to consider your application" + CrLf +
                @"will be held on {^meeting-name-and-date^}.  Our critique panel, named {^panel-name^} will inform you of our decision by" + CrLf +
                @"{^critique-deadline^}.  This {^unknown-merge-field^} will be sent to you via email at {^to-email-address^} and via postal mail at" + CrLf +
                @"{^to-full-mailing-address^}.  Should you have any concerns about my participation on the panel as {^from-role^}, please" + CrLf +
                @"send your correspondence to the following email address: {^from-email-address^}." + CrLf +
                CrLf +
                @"Sincerely," + CrLf +
                @"{^from-full-name^}" + CrLf +
                @"{^role-on-panel^}";
            MergeField mergeFields = new MergeField();
            mergeFields.ToEmailAddress = "primo@sra.com";
            mergeFields.ToFirstName = "Zeus";
            mergeFields.ToFullMailingAddress = "123 Main St., Fairfax, VA 22033";
            mergeFields.ToFullName = "Zeus Jockley";
            mergeFields.ToLastName = "Jockley";
            mergeFields.ToPrefix = "Mr.";
            mergeFields.ToRole = "Deity";
            mergeFields.ToSuffix = "Jr.";
            mergeFields.FromEmailAddress = "godsRus@deities.gov";
            mergeFields.FromFirstName = "Mercury";
            mergeFields.FromFullMailingAddress = "123 Deity Way, Athens, Greece";
            mergeFields.FromFullName = "Mercury Planet";
            mergeFields.FromLastName = "Planet";
            mergeFields.FromPrefix = "Dr.";
            mergeFields.FromRole = "Messenger";
            mergeFields.ApplicationTitle = "Grant for Deity Services";
            mergeFields.CritiqueDeadline = "4/15/2012";
            mergeFields.MeetingNameAndDate = "5/26/2012";
            mergeFields.PanelName = "Deity Panel";
            mergeFields.RoleOnPanel = "Chief Deity";
            string actual = MergeFieldReplacer.Replace(msgBodyText, mergeFields);
        }
    }
}
