using System.Net;
using ESFA.DC.LARS.API.Models;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Telemetry.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ESFA.DC.LARS.API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var telemetry = app.ApplicationServices.GetService<ITelemetry>();
                        var jsonSerializationService = app.ApplicationServices.GetService<IJsonSerializationService>();

                        telemetry.TrackEvent(
                            "Exception",
                            new System.Collections.Generic.Dictionary<string, string>()
                            {
                                { "StackTrace", contextFeature.Error.StackTrace },
                                { "Error", contextFeature.Error.Message }
                            },
                            null);
                        var errorDetail = new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        };

                        await context.Response.WriteAsync(jsonSerializationService.Serialize(errorDetail));
                    }
                });
            });
        }
    }
}
