using System;
using System.Collections.Generic;

namespace ESFA.DC.LARS.Web.Interfaces.Services
{
    public interface ITelemetryWrapper : IDisposable
    {
        void TrackEvent(string message);

        void TrackException(
            Exception exception,
            IDictionary<string, string> properties = null,
            IDictionary<string, double> metrics = null);
    }
}