﻿using AutoMapper;
using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace recenzent
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //PROTIP: konfiguracja kontenera DI
            DependencyInjectionConfig.Configure();

            //PROTIP: podpięcie AutoMappera na zasadzie wyszukiwania klas dziedziczących po Profile
            Mapper.Initialize(cfg =>
            {
                var baseType = typeof(Profile);
                var types = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t != baseType && baseType.IsAssignableFrom(t))
                    .ToList();

                foreach (var type in types)
                    cfg.AddProfile(Activator.CreateInstance(type) as Profile);
            });

            Mapper.AssertConfigurationIsValid();


            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
            if(cookie != null && cookie.Value != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pl");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pl");
            }
        }
    }
}
