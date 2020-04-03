using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ESFA.DC.LARS.API.CustomFilters
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
                foreach (var arg in context.ActionArguments)
                {
                    telemetryArgs.Add(arg.Key, arg.Value.ToString());
                }

                _telemetryClient.TrackTrace(context.ActionDescriptor.DisplayName, telemetryArgs);
            }
            catch { }

            var resultContext = await next();
        }
    }
}
