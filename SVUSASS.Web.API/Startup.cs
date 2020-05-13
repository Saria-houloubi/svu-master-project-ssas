using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SVUSASS.Logging.IServices;
using SVUSASS.Logging.Services;

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

            services.AddSingleton<ILoggingService, DefaultLoggingSservice>();

            services.AddCors(options =>
            {
                //Add dev CORS Policy
                options.AddPolicy("Development2", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
                //Addm Production CORS Policy
                options.AddPolicy("Development", buider =>
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

            app.UseCors(env.EnvironmentName);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
