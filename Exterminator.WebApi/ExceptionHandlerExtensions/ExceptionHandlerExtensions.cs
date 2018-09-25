using System;
using System.Collections.Generic;
using System.Net;
using Exterminator.Models;
using Exterminator.Models.Exceptions;
using Exterminator.Services.Interfaces;
using Exterminator.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Exterminator.WebApi.ExceptionHandlerExtensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            // TODO: Implement
            app.UseExceptionHandler(error =>
            {
                error.Run(async context => 
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    

                    if(exceptionHandlerFeature != null) {
                        var exception = exceptionHandlerFeature.Error;
                        var statusCode = (int) HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        if(exception is ResourceNotFoundException) {
                            statusCode = (int) HttpStatusCode.NotFound;
                        } else if (exception is ModelFormatException) {
                            statusCode = (int) HttpStatusCode.PreconditionFailed;
                        } else if (exception is ArgumentOutOfRangeException) {
                            statusCode = (int) HttpStatusCode.BadRequest;
                        }
                        // TODO: setja fleiri exception í if setningu hér

                        ExceptionModel exceptionModel = new ExceptionModel{
                            StatusCode = statusCode,
                            ExceptionMessage = exception.Message,
                            StackTrace = exception.StackTrace.ToString
                        };

                        var logService = app.ApplicationServices.GetService(typeof(ILogService)) as ILogService;
                        logService.LogToDatabase(exceptionModel);

                        await context.Response.WriteAsync(new ExceptionModel{
                            StatusCode = statusCode,
                            ExceptionMessage = exception.Message,
                            StackTrace = exception.StackTrace.ToString
                        }.ToString);

                        
                    }
                });
            });
        }
    }
}