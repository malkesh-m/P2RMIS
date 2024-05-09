using System.Collections.Generic;
using Rhino.Mocks;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Web.Common.Interfaces;
using Sra.P2rmis.Web.Controllers;

namespace WebTest
{
    /// <summary>
    /// Base for unit tests.  Collection of "stuff" to simplify unit tests.
    /// </summary>
    public partial class WebBaseTest
    {
        /// <summary>
        /// The rhino mocks repository
        /// </summary>
        protected MockRepository theMock;                                                                   // theMock = new MockRepository();
        #region Controller Mocks
        protected SummaryStatementController summaryStatementControllerMock;                                // = theMock.PartialMock<SummaryStatementController>(<NULL OR SERVICE MOCK as params>, null, null, null);       
        #endregion       
        #region Services
        protected ICriteriaService criteriaServiceMoc;                                                      // = theMock.DynamicMock<ICriteriaService>();
        protected ISummaryManagementService summaryManagementServiceMock;                                   // = theMock.DynamicMock<ISummaryManagementService>();
        protected IWorkflowService theWorkflowServiceMock;                                                  // = theMock.DynamicMock<IWorkflowService>();
        #endregion
        #region Other objects
        protected ICustomIdentity identityMock;                                                             // = theMock.DynamicMock<ICustomIdentity>();
        #endregion
        #region Attributes
        protected int _goodId1 = 345;
        protected int _goodId2 = 999764;
        #endregion
        #region Helpers
        /// <summary>
        /// Generic helper method that constructs a container populated with a list
        /// of specific web models.
        /// </summary>
        /// <typeparam name="I">Interface type of WebModel</typeparam>
        /// <typeparam name="T">Concrete type of WebModel</typeparam>
        /// <param name="howMany">Number of WebModels to create</param>
        /// <returns>Populated WebModel</returns>
        public Container<I> BuildContainer<I, T>(int howMany) where T:I, new()
        {
            var result = new Container<I>();
            List<I> list = new List<I>();

            for (int i = 0; i < howMany; i++)
            {
                I x = new T();
                list.Add(x);
            }
            result.ModelList = list;

            return result;
        }
        #endregion
    }
}
