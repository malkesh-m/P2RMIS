using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.HelperClasses;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using Sra.P2rmis.Web.ViewModels;
using Sra.P2rmis.WebModels.ApplicationScoring;

namespace WebTest.ViewModels
{
    /// <summary>
    /// Unit tests for PhaseStateConverter
    /// </summary>
    [TestClass()]
    public class PhaseStateConverterTests
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
        #endregion
        #region Tests
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateConverter")]
        public void EnabledNormalView_Test()
        {
            //
            // Test & verify; first the states that are Enabled Normal View
            //
            bool result = false;
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase101);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase102);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase103);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase104);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase105);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase106);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase107);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase108);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase117);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase118);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase119);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase120);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase121);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase122);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase123);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase124);
            Assert.AreEqual(true, result);
            //
            // Now for states that are not Enabled Normal View
            //
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase109);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase110);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase111);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase112);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase113);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase114);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase115);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase116);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase125);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase126);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase127);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase128);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase129);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase130);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase131);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalView(StateResult.Phase132);
            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateConverter")]
        public void DisabledNormalView_Test()
        {
            //
            // Test & verify; first the states that are Disabled Normal View
            //
            bool result = false;
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase109);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase110);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase111);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase112);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase113);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase114);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase115);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase116);
            Assert.AreEqual(true, result);
            //
            // Now for states that are not Disabled Normal View
            //
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase101);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase102);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase103);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase104);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase105);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase106);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase107);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase108);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase117);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase118);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase119);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase120);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase121);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase122);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase123);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase124);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase125);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase126);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase127);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase128);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase129);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase130);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase131);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledNormalView(StateResult.Phase132);
            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateConverter")]
        public void DisabledAbnormalEdit_Test()
        {
            //
            // Test & verify; first the states that are Disabled Abnormal View
            //
            bool result = false;
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase125);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase126);
            Assert.AreEqual(true, result);
            //
            // Now for states that are not Disabled Normal View
            //
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase109);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase110);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase111);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase112);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase113);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase114);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase115);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase116);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase101);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase102);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase103);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase104);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase105);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase106);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase107);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase108);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase117);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase118);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase119);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase120);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase121);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase122);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase123);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase124);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase127);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase128);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase129);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase130);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase131);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.DisabledAbnormalEdit(StateResult.Phase132);
            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateConverter")]
        public void EnabledAbnormalEdit_Test()
        {
            //
            // Test & verify; first the states that are Disabled Abnormal View
            //
            bool result = false;
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase127);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase128);
            Assert.AreEqual(true, result);
            //
            // Now for states that are not Disabled Normal View
            //
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase125);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase126);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase109);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase110);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase111);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase112);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase113);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase114);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase115);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase116);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase101);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase102);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase103);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase104);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase105);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase106);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase107);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase108);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase117);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase118);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase119);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase120);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase121);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase122);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase123);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase124);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase129);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase130);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase131);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledAbnormalEdit(StateResult.Phase132);
            Assert.AreEqual(false, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateConverter")]
        public void EnabledNormalEdit_Test()
        {
            //
            // Test & verify; first the states that are Enabled Normal View
            //
            bool result = false;
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase129);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase130);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase131);
            Assert.AreEqual(true, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase132);
            Assert.AreEqual(true, result);
            //
            // Now for states that are not Disabled Normal View
            //
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase127);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase128);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase125);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase126);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase109);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase110);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase111);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase112);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase113);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase114);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase115);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase116);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase101);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase102);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase103);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase104);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase105);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase106);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase107);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase108);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase117);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase118);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase119);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase120);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase121);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase122);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase123);
            Assert.AreEqual(false, result);
            result = PhaseStateConverter.EnabledNormalEdit(StateResult.Phase124);
            Assert.AreEqual(false, result);
        }

        #endregion
    }

}
