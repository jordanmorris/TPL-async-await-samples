using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Mvc.Ioc
{
    internal class StructureMapDependencyResolver : IDependencyResolver
    {
        private static StructureMap.Container _container;

        public StructureMapDependencyResolver(StructureMap.Container container)
        {
            _container = container;
        }


        public object GetService(Type serviceType)
        {
            try
            {
                return _container.GetInstance(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.GetAllInstances(serviceType).Cast<object>();
            }
            catch
            {
                return null;
            }
        }
    }
}