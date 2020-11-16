﻿using System.Reflection;

using Aspire.Utils;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AspireSetup
    {
        public static IServiceCollection AddAspireController(this IServiceCollection services, Assembly controllersAssembly)
        {
            services
                .AddControllers()
                .AddMvcOptions(configure =>
                {
                    configure.AllowEmptyInputInBodyModelBinding = false;
                    configure.Conventions.Add(new RouteTokenTransformerConvention(new CustomTransformerConvention()));
                })
                .ConfigureApplicationPartManager(setupAction =>
                {
                    setupAction.ApplicationParts.Add(new AssemblyPart(controllersAssembly));
                })
                .AddControllersAsServices()
                .AddNewtonsoftJson(setupAction =>
                {
                    setupAction.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });

            return services;
        }

        public static IApplicationBuilder UseAspireController(this IApplicationBuilder app)
        {
            app.UseExceptionHandler("/error");
            return app;
        }
    }
}
