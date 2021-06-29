using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApiBooking.Models;
using Newtonsoft.Json;
using NLog;
using System;
using System.IO;
using Contracts;
using LoggerService;
using ApiBooking.Extensions;

namespace ApiBooking
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }


    

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // add the interface and logger
           services.AddSingleton<ILoggerManager, LoggerManager>();

            // cors
            services.AddCors(options => options.AddPolicy("APIPOLICY", builder => {

                builder
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
            }));
            //seraliz
            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

         
            //connectionstring to context
            services.AddControllers();
            services.AddDbContext<BookingContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("ApiBookingContext")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Extensions folder
            app.ConfigureExceptionHandler(logger);


            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            //to be able to call api 
            app.UseCors("myPolicy");
            app.UseEndpoints(endpoints =>
            {    //to be able to call api 
                endpoints.MapControllers().RequireCors("APIPOLICY");
            });
        }
    }
}
