using DataServices;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Mvc5;

namespace bewitching
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            //var type = container.GetType(IDbConnectionSvc);

            //container.RegisterType<IDbFunctionSvc, DbFunctionSvc>(
            //    new InjectionConstructor(new ResolvedParameter<IDbFunctionSvc, DbFunctionSvc>));
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            container.RegisterType<IDbSqlFunctionSvc, DbSqlFunctionSvc>();
            container.RegisterType<IDbConnectionSvc, DbConnectionSvc>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}