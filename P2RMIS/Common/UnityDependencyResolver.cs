using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Sra.P2rmis.Web.Common
{
    /// <summary>
    /// TODO:: document me
    /// </summary>
    public class UnityDependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// TODO:: document me
        /// </summary>
        readonly IUnityContainer _container;
        /// <summary>
        /// Microsoft Unity Dependency Framework Dependency Resolver constructor.
        /// </summary>
        /// <param name="container">Unity container</param>
        public UnityDependencyResolver(IUnityContainer container)
        {
            this._container = container;
        }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch
            {
                return new List<object>();
            }
        }
    }
}
