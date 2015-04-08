using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace ILS.Web.Extensions
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private UnityContainer container;
		public UnityDependencyResolver(UnityContainer container)
		{
			this.container = container;
		}

        public object GetService(Type serviceType)
        {
            return TryGetService(serviceType);
        }

        public object TryGetService(Type serviceType)
        {
            try
            {
                return this.container.Resolve(serviceType);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return TryGetServices(serviceType);
        }

        public IEnumerable<object> TryGetServices(Type serviceType)
        {
            try
            {
                return this.container.ResolveAll(serviceType);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}