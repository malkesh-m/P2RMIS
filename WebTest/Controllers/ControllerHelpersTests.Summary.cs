using System.Collections.Generic;
using NUnit.Framework;
using Sra.P2rmis.Web.Controllers;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;

namespace WebTest.Controllers
{
    /// <summary>
    /// Unit test for ControllerHelpers class, specifically helper methods for summary controllers
    /// </summary>
    [TestClass()]
    [Category("ControllerHelpers")]
    public partial class ControllerHelpersTests : WebBaseTest
    {
        #region OrderContentForPresentation Tests
        /// <summary>
        /// Test a single key
        /// </summary>
        [TestMethod()]
        public void OrderContentForPresentation_SingleKeyTest()
        {
            // 
            // set up local data
            //
            List<IStepContentModel> l = new List<IStepContentModel>();
            IEnumerable<IStepContentModel> collection = SetUpDataForOrderContentForPresentationTest(l, 4, "name 1", (decimal)4.567, true, true);
            //
            // Execute the method under test
            //
            IDictionary<string, List<IStepContentModel>> result = ControllerHelpers.OrderContentForPresentation(collection);
            //
            // Test the assertions
            //
            Assert.IsNotNull(result, "result was null and should not have been");
            Assert.AreEqual(1, result.Keys.Count, "Incorrect number of keys");
            Assert.AreEqual(4, result["name 1 (Average Score: 4.6)"].Count, "Incorrect number of list entries");
        }
        public void OrderContentForPresentation_TwoKeyTest()
        {
            // 
            // set up local data
            //
            List<IStepContentModel> l = new List<IStepContentModel>();
            IEnumerable<IStepContentModel> collection1 = SetUpDataForOrderContentForPresentationTest(l, 4, "name 1", (decimal)4.567, true, true);
            IEnumerable<IStepContentModel> collection = SetUpDataForOrderContentForPresentationTest(new List<IStepContentModel>(collection1), 4, "name 3", (decimal)4.567, true, true);
            //
            // Execute the method under test
            //
            IDictionary<string, List<IStepContentModel>> result = ControllerHelpers.OrderContentForPresentation(collection);
            //
            // Test the assertions
            //
            Assert.IsNotNull(result, "result was null and should not have been");
            Assert.AreEqual(2, result.Keys.Count, "Incorrect number of keys");
            Assert.AreEqual(4, result["name 1 (Average Score: 4.6)"].Count, "Incorrect number of list entries");
            Assert.AreEqual(4, result["name 3 (Average Score: 4.6)"].Count, "Incorrect number of list entries");
        }
        //TODO:  This needs additional unit tests. 
        #endregion
        #region Helpers
        /// <summary>
        /// set up local data for test
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IStepContentModel> SetUpDataForOrderContentForPresentationTest(List<IStepContentModel> l, int thisMany, string elementName, decimal score, bool overAll, bool hasText)
        {
            for (int i = 0; i < thisMany; i++)
			{
                l.Add(new StepContentModel { ElementName = elementName, ElementContentAverageScore = score, ElementOverallFlag = overAll, ElementTextFlag = hasText });
            }
            return l;
        }
        #endregion
    }
}
