using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Web.Controllers;
using Sra.P2rmis.Web.Controllers.PanelManagement;
using Sra.P2rmis.WebModels.PanelManagement;

namespace WebTest
{
    /// <summary>
    /// Base for unit tests.  Collection of "stuff" to simplify panel management unit tests.
    /// </summary>
    public partial class WebBaseTest
    {
        #region Mocks & mock objects
        //
        // PanelManagementController
        //
        protected PanelManagementController panelManagementControllerMock;                                  // = mocks.PartialMock<PanelManagementController>(<NULL OR SERVICE MOCK as param>);
        //
        // Object used to mock the session state
        //
        protected HttpSessionStateBase mockBaseSessionState;
        /// <summary>
        /// Object used to set mock the controller's session state
        /// </summary>
        protected HttpContextMock mockBaseHttpContext;
        #endregion
        #region Services
        protected IPanelManagementListService thePanelManagementListServiceMock;                            // = theMock.DynamicMock<IPanelManagementListService>();
        protected ISummaryProcessingService theSummaryProcessingServiceMock;                                // = theMock.DynmaicMock<ISummaryProcessingService>();
        protected IMailService theMailServiceMock;                                                          // = theMock.DynmaicMock<IMailService>();
        protected IPanelManagementService thePanelManagementServiceMock;                                    // = theMock.DynmaicMock<IPanelManagementService>();
        #endregion
        #region RhinoMocks Wrappers
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for GetUserId
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<int> MockGetUserId(int value)
        {
            return Expect.Call(this.panelManagementControllerMock.GetUserId()).Return(value);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for HasSelectProgramPanelPermission
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<bool> MockGetSelectProgramPanelPermission(bool value) 
        {
            return Expect.Call(this.panelManagementControllerMock.HasSelectProgramPanelPermission()).Return(value);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for PanelManagementListService.PanelSignifications()
        /// </summary>
        /// <param name="userId">User identifier to use</param>
        /// <param name="container">Container to return</param>
        public IMethodOptions<Container<IPanelSignificationsModel>> MockListPanelSignifications(int userId, Container<IPanelSignificationsModel> container)
        {
            return Expect.Call(this.thePanelManagementListServiceMock.ListPanelSignifications(userId)).Return(container);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for PanelManagementListService.PanelSignifications()
        /// </summary>
        /// <param name="userId">User identifier to use</param>
        /// <param name="programYearId">Program/Year identifier to use</param>
        /// <param name="container">Container to return</param>
        public IMethodOptions<Container<IPanelSignificationsModel>> MockListPanelSignifications(int userId, int programYearId, Container<IPanelSignificationsModel> container)
        {
            return Expect.Call(this.thePanelManagementListServiceMock.ListPanelSignifications(userId, programYearId)).Return(container);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for PanelManagementListService.ProgramYears()
        /// </summary>
        /// <param name="userId">User identifier to use</param>
        /// <param name="container">Container to return</param>
        public IMethodOptions<Container<IProgramYearModel>> MockListProgramYears(int userId, Container<IProgramYearModel> container)
        {
            return Expect.Call(this.thePanelManagementListServiceMock.ListProgramYears(userId)).Return(container);
        }
        /// <summary>
        /// Wrapper method throws an exception when GetUserId() is called.
        /// </summary>
        /// <param name="controllerMock">Controller mock</param>
        public void ForceExceptionFromGetUserId(BaseController controllerMock)
        {
            controllerMock.Stub(s => s.GetUserId()).Throw(new TestException());
        }
        /// <summary>
        /// Wrapper method throws an exception when HandleExceptionViaElmah() is called.
        /// Used in conjunction with ForceExceptionFromGetUserId() or other method to indicate
        /// the try/catch block is coded.
        /// </summary>
        /// <param name="controllerMock">Controller mock</param>
        public void ElmahErrorTest(BaseController controllerMock)
        {
            controllerMock.Stub(s => s.HandleExceptionViaElmah(null)).IgnoreArguments().Throw(new ElmahTestException());
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for SetPanelSession
        /// </summary>
        /// <param name="value">Value to return</param>
        public void MockSetPanelSession(int value)
        {
            Expect.Call(delegate { this.panelManagementControllerMock.SetPanelSession(value); });
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for GetPanelSession
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<int> MockGetPanelSession(int value)
        {
            return Expect.Call(this.panelManagementControllerMock.GetPanelSession()).Return(value);
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for SetProgramYearSession
        /// </summary>
        /// <param name="value">Value to return</param>
        public void MockSetProgramYearSession(int value)
        {
            Expect.Call(delegate { this.panelManagementControllerMock.SetProgramYearSession(value); });
        }
        /// <summary>
        /// Wrapper to set RhinoMock Expect-Return for GetProgramYearSession
        /// </summary>
        /// <param name="value">Value to return</param>
        public IMethodOptions<int> MockGetProgramYearSession(int value)
        {
            return Expect.Call(this.panelManagementControllerMock.GetProgramYearSession()).Return(value);
        }
        /// <summary>
        /// Initialize all mocks object in the base.
        /// </summary>
        protected void InitializeMocks()
        {
            Console.WriteLine("InitializeMocks()");

            //
            // Create mock object
            //
            mockBaseSessionState = new HttpSessionMock();
            mockBaseHttpContext = new HttpContextMock(mockBaseSessionState);
            //
            // Now create the mocks
            //
            theMock = new MockRepository();
            thePanelManagementListServiceMock = theMock.DynamicMock<IPanelManagementListService>();
            theSummaryProcessingServiceMock = theMock.DynamicMock<ISummaryProcessingService>();
            theMailServiceMock = theMock.DynamicMock<IMailService>();
            thePanelManagementServiceMock = theMock.DynamicMock<IPanelManagementService>();
            theWorkflowServiceMock = theMock.DynamicMock<IWorkflowService>();
            panelManagementControllerMock = theMock.PartialMock<PanelManagementController>(thePanelManagementListServiceMock, theSummaryProcessingServiceMock, theMailServiceMock, thePanelManagementServiceMock, null);
            //
            // finally set the controllers context
            //
            panelManagementControllerMock.ControllerContext = new ControllerContext(mockBaseHttpContext.Context, new System.Web.Routing.RouteData(), panelManagementControllerMock);
        }
        /// <summary>
        /// Clean up mocks after the test.
        /// </summary>
        protected void CleanUpMocks()
        {
            Console.WriteLine("CleanUpMocks()");
            panelManagementControllerMock.ControllerContext = null;
            panelManagementControllerMock = null;

            mockBaseSessionState = null;
            mockBaseHttpContext = null;

            theWorkflowServiceMock = null;
            thePanelManagementServiceMock = null;
            theMailServiceMock = null;
            thePanelManagementListServiceMock = null;
            theSummaryProcessingServiceMock = null;
            theMock = null;
        }
        #endregion
        #region Classes
        /// <summary>
        /// Test exceptions for mocking
        /// </summary>
        public class TestException : Exception { }
        public class ElmahTestException : Exception { }
        /// <summary>
        /// Mock object for session state.
        /// </summary>
        public class HttpSessionMock : HttpSessionStateBase
        {
            readonly Dictionary<string, object> _sessionDictionary = new Dictionary<string, object>();
            public override object this[string name]
            {
                get
                {
                    object obj = null;
                    _sessionDictionary.TryGetValue(name, out obj);
                    return obj;
                }
                set
                {
                    Console.WriteLine(" SETTING " + value);
                    _sessionDictionary[name] = value;
                }
            }
        }
        /// <summary>
        /// Mock HttpContext
        /// </summary>
        public class HttpContextMock
        {
            private readonly HttpContextBase _contextBase;
            private readonly HttpRequestBase _requestBase;
            private readonly HttpResponseBase _responseBase;
            private readonly HttpSessionStateBase _sessionStateBase;
            private readonly HttpServerUtilityBase _serverUtilityBase;

            public HttpContextBase Context { get { return _contextBase; } }
            public HttpRequestBase Request { get { return _requestBase; } }
            public HttpResponseBase Response { get { return _responseBase; } }
            public HttpSessionStateBase Session { get { return _sessionStateBase; } }
            public HttpServerUtilityBase Server { get { return _serverUtilityBase; } }

            /// <summary>
            /// Mock context controller
            /// </summary>
            /// <param name="mockSessionStateBase">Session state mock</param>
            public HttpContextMock(HttpSessionStateBase mockSessionStateBase)
            {
                _contextBase = MockRepository.GenerateStub<HttpContextBase>();
                _requestBase = MockRepository.GenerateStub<HttpRequestBase>();
                _responseBase = MockRepository.GenerateStub<HttpResponseBase>();
                _serverUtilityBase = MockRepository.GenerateStub<HttpServerUtilityBase>();
                _sessionStateBase = mockSessionStateBase;

                _contextBase.Stub(x => x.Request).Return(_requestBase);
                _contextBase.Stub(x => x.Response).Return(_responseBase);
                _contextBase.Stub(x => x.Session).Return(_sessionStateBase);
                _contextBase.Stub(x => x.Session).Return(_sessionStateBase);
                _contextBase.Stub(x => x.Server).Return(_serverUtilityBase);
                _requestBase.Stub(x => x.IsAuthenticated).Return(true);
            }
        }
        #endregion
    }
}
