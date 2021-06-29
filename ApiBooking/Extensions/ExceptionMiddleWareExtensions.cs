using System.Net;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using ApiBooking.Models;

namespace ApiBooking.Extensions
{     // exstensionClass for overall Logging 
    public static class ExceptionMiddleWareExtensions
    {
        
        public static void ConfigureExceptionHandler( this IApplicationBuilder app, ILoggerManager logger)
        {
            // add pipeline
            app.UseExceptionHandler(appError =>
            {// register context object
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    // extract exception handler feature
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    // log if its not null
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong:{contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails
                        {// Models.ErrorDetails class
                            StatusCode = context.Response.StatusCode,
                            Message= "Internal Server Error"
                        }.ToString()) ; 
                   
                    }
                });     

            });
        }

    }
}
