using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SVUSASS.Logging.IServices;
using SVUSASS.Logging.Services;
using SVUSASS.Web.API.Middlewares;

namespace SVUSASS.Web.API
{
    public class Startup
    {
        #region Properties
        public IConfiguration Configuration { get; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //The environment that we are working in
            var env = services.BuildServiceProvider().GetService<IHostingEnvironment>();

            //Add the wanted services to the DI pipeline
            if (env.IsDevelopment())
            {
                services.AddSingleton<ILoggingService, DefaultLoggingSservice>();
            }
            else
            {
                //REMOVED on 06/06/2020 due to throughing 500 server erros on production
                //services.AddSingleton<ILoggingService, SaveMyDataLoggingService>();
            }

            services.AddCors(options =>
            {
                //Add dev CORS Policy
                options.AddPolicy("Development", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
                //Addm Production CORS Policy
                options.AddPolicy("Production", buider =>
                {
                    buider.WithOrigins(Configuration.GetSection("CORS_Origins").Get<string[]>())
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           app.UseMiddleware<CustomLoggingMiddleware>();

            app.UseCors(env.IsDevelopment() ? "Development" : "Production");

            app.UseMvc();
        }
    }
}
