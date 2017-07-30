using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WheatherForecast.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            configuration.EnableCors(cors);

            configuration.MapHttpAttributeRoutes();

            configuration.Routes.MapHttpRoute("DefaultWebApi", "api/v1/{controller}/{id}",
                new {id = RouteParameter.Optional});
        }
    }
}