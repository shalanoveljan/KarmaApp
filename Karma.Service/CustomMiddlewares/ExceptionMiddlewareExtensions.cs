using Karma.Service.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.CustomMiddlewares
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    int statusCode = 500;
                    string message = "Server internal error";

                    //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>().Error;
                    if(contextFeature is ItemNotFoundException)
                    {
                        statusCode = 404;
                        message = contextFeature.Message;
                    }
                    context.Response.ContentType = "application/json";
                    
                        await context.Response.WriteAsync(new 
                        {
                            StatusCode = statusCode,
                            Message = contextFeature.Message
                        }.ToString());
                    
                });
            });
        }
    }
}
