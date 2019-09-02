using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Models;
using EntityFramework.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tempus.Utils.AspNetCore;

namespace EntityFramework
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
            services.AddMvc(options=>
            {
                options.Filters.Add<JsonApiValidationActionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services
                .AddDbContext<LojaContext>();

            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IProdutoService, ProdutoService>();
            services.AddTransient<ValidarProduto>();

            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IConnectionStringBuilder, ConnectionStringBuilder>();

            services.AddMemoryCache();
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

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseMvc(options =>
            {
                options.MapRoute("default", "{tenant}/{controller}/{action}");
            });
        }
    }
}
