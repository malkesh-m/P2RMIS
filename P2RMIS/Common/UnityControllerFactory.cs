using System;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace Sra.P2rmis.Web.Common
{
    /// <summary>
    /// Controller factory constructs the controllers for the P2RMIS application.  Controller
    /// factory injects the business layer services and any other dependent objects into the controllers.
    /// </summary>
    public class UnityControllerFactory : DefaultControllerFactory
    {
        #region Attributes
        /// <summary>
        /// Unity container
        /// </summary>
        private IUnityContainer _container;
        /// <summary>
        /// TODO:: document me
        /// </summary>
        private IControllerFactory _innerFactory;
        #endregion
        #region Constructor; Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">Unity container</param>
        public UnityControllerFactory(IUnityContainer container)
            : this(container, new DefaultControllerFactory())
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">Unity container</param>
        /// <param name="innerFactory"-----></param>
        protected UnityControllerFactory(IUnityContainer container, IControllerFactory innerFactory)
        {
            _container = container;
            _innerFactory = innerFactory;
        }
        #endregion

        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            ///
            /// If the controller's name is font (i.e. FontAwesome) then don't create a constructor.
            ///
            if (
                (controllerName != "font") && (controllerName != "favicon.ico") && (controllerName != "img")
                )
            {
                try
                {
                    return (IController)_container.Resolve(GetControllerType(requestContext, controllerName));
                }
                catch (Exception)
                {
                    return _innerFactory.CreateController(requestContext, controllerName);
                }
            }
            return null;
        }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="controller">-----</param>
        public override void ReleaseController(IController controller)
        {
            _container.Teardown(controller);
        }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="requestContext">-----</param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public System.Web.SessionState.SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return System.Web.SessionState.SessionStateBehavior.Default;
        }
    }
}