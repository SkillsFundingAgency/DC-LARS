using System;
using System.Collections.Generic;
using ESFA.DC.LARS.Web.Interfaces.Services;
using Microsoft.ApplicationInsights;

namespace ESFA.DC.LARS.Web.Services
{
    public class TelemetryWrapper : ITelemetryWrapper
    {
        private TelemetryClient _telemetryClient;

        public TelemetryWrapper(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public void Dispose()
        {
            _telemetryClient = null;
        }

        public void TrackEvent(string message)
        {
            _telemetryClient.TrackEvent(message);
        }

        public void TrackException(
            Exception exception,
            IDictionary<string, string> properties = null,
            IDictionary<string, double> metrics = null)
        {
            _telemetryClient.TrackException(exception, properties, metrics);
        }
    }
}