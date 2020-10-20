using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESFA.DC.LARS.Web.Interfaces.Services
{
    public interface IClientService
    {
        Task<TResult> PostAsync<TContent, TResult>(string url, TContent content);

        Task<TResult> GetAsync<TResult>(string url, IDictionary<string, object> parameters);
    }
}