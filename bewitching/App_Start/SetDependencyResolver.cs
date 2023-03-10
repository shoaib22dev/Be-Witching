using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bewitching.App_Start
{
    public class SetDependencyResolver : IDependencyResolver
    {
        protected IServiceProvider serviceProvider;

        public SetDependencyResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public object GetService(Type serviceType)
        {
            return this.serviceProvider.GetService(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.serviceProvider.GetServices(serviceType);
        }
    }
}