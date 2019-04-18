using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Net;
using NLog;

namespace Store.Exceptions
{
    public class ExceptionHandler
    {
        public static Logger Logger { get; set; } = NLog.LogManager.GetCurrentClassLogger();

        public static void ConfigExceptionHandler(IApplicationBuilder app)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;
                   if (error != null && error.Error is NotImplementedException)
                    {
                        Logger.Error(error);
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = (int)HttpStatusCode.NotImplemented,
                            Msg = "NotImplementedException"
                        }));
                    }
                    else if (error != null && error.Error != null)
                    {
                        Logger.Fatal(error);
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = 500,
                            Msg = error.Error.Message
                        }));
                    }
                    else
                    {
                        await next();
                    }
                });
            });
        }
    }
}
