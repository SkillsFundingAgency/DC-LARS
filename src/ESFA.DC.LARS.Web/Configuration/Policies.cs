using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;

namespace ESFA.DC.LARS.Web.Configuration
{
    public static class Policies
    {
        private static int _timeOutInSeconds = 40;

        //public static PolicyWrap<HttpResponseMessage> PolicyStrategy => Policy.WrapAsync(RetryPolicy, TimeoutPolicy);

        //private static TimeoutPolicy<HttpResponseMessage> TimeoutPolicy
        //{
        //    get
        //    {
        //        return Policy.TimeoutAsync<HttpResponseMessage>(_timeOutInSeconds, (context, timeSpan, task) =>
        //        {
        //            Debug.WriteLine($"[App|Policy]: Timeout delegate fired after {timeSpan.Seconds} seconds");
        //            return Task.CompletedTask;
        //        });
        //    }
        //}

        //private static RetryPolicy<HttpResponseMessage> RetryPolicy
        //{
        //    get
        //    {
        //        var jitter = new Random();
        //        return HttpPolicyExtensions
        //            .HandleTransientHttpError()
        //            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        //            .WaitAndRetryAsync(3, retryAttempt =>
        //                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
        //                + TimeSpan.FromMilliseconds(jitter.Next(0, 100)));
        //    }
        //}

        //static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        //{
        //    return HttpPolicyExtensions
        //        .HandleTransientHttpError()
        //        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        //        .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        //}
    }
}