using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EntityFramework.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tempus.Utils.AspNetCore;

namespace Completo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization();
            
            services
                .AddMvc(opts =>
                {
                    opts.Filters.Add(typeof(JsonApiValidationActionFilter));
                    opts.ModelBindingMessageProvider.Translate();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFeatureFolders();
            
            services
                .AddDbContext<LojaContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("LojaContext")));

            services.AddMediatR(typeof(Startup));

            RegisterValidators(services);
        }

        private void RegisterValidators(IServiceCollection services)
        {
            var assembly = typeof(Startup).GetTypeInfo().Assembly;
            var validatorType = typeof(IValidator);

            var validators = assembly
                .GetExportedTypes()
                .Where(t => validatorType.IsAssignableFrom(t) && !t.IsInterface);

            foreach (var validator in validators)
            {
                services.AddTransient(validator);

                var validatorInterfaces = validator
                    .GetInterfaces()
                    .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IValidator<>));

                foreach (var validatorInterface in validatorInterfaces)
                {
                    services.AddTransient(validatorInterface, validator);
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                loggerFactory.AddConsole();
                loggerFactory.AddDebug();
            }
            else
            {
                loggerFactory.AddFile("Logs/myapp-{Date}.txt");
                app.UseExceptionHandler("/Home/Error");
            }

            var supportedCultures = new[]
            {
                new CultureInfo("pt-BR")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });


            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}");
            });
        }
    }
}
