using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ESFA.DC.LARS.Web.CustomFilters
{
    public class TelemetryActionFilter : IAsyncActionFilter
    {
        private readonly TelemetryClient _telemetryClient;

        public TelemetryActionFilter(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                var telemetryArgs = new Dictionary<string, string>();
                if (context.ActionArguments != null)
                {
                    foreach (var arg in context.ActionArguments)
                    {
                        if (arg.Value != null)
                        {
                            telemetryArgs.Add(arg.Key, arg.Value.ToString());
                        }
                        else
                        {
                            telemetryArgs.Add(arg.Key, "NULL");
                        }
                    }
                    _telemetryClient.TrackTrace(context.ActionDescriptor.DisplayName, telemetryArgs);
                }
            }
            catch { }
            finally
            {
                var resultContext = await next();
            }
        }
    }
}
