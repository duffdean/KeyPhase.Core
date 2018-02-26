using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using KeyPhase.Models;
using KeyPhase.Repository.Interface;
using KeyPhase.Repository;
using KeyPhase.Service.Interface;
using KeyPhase.Service;
using Newtonsoft.Json.Serialization;

namespace KeyPhase.Core
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
            services.AddMvc();

            //Had issue with json properties being defaulted to camelCase by .Net Core
            //Found solution at https://stackoverflow.com/questions/38202039/json-properties-now-lower-case-on-swap-from-asp-net-core-1-0-0-rc2-final-to-1-0
            services.AddMvc()
            .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
            .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //Second issue was with lazy loading in .net core 2
            //Solution found here : https://stackoverflow.com/questions/41489674/net-core-web-api-call-err-connection-reset-only-on-iis-other-calls-working

            services.AddDbContext<KeyPhaseDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("KeyPhaseDB")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IKPCombinedService, KPCombinedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
