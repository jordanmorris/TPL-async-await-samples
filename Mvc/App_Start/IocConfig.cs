using System.Web.Http;
using System.Web.Mvc;
using Mvc.Ioc;
using StructureMap.Graph;

namespace Mvc
{
    internal class IocConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            var container = new StructureMap.Container(x =>
            {
                x.Scan(s =>
                {
                    s.TheCallingAssembly();
                    s.WithDefaultConventions();
                });
            });
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }

}