using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WheatherForecast.Repositories.Concrete;
using WheatherForecast.Repositories.Interfaces;
using WheatherForecast.Services;

namespace WheatherForecast.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IForecastProvider>().To<OpenWeatherMapProvider>().InTransientScope();
            _kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InTransientScope();
            _kernel.Bind<IWeatherService>().To<WeatherService>().InTransientScope();
            _kernel.Bind<IForecastConverter>().To<ForecastConverter>().InTransientScope();
        }
    }
}