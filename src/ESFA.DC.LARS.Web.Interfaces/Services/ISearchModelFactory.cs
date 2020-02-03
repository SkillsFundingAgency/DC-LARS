using ESFA.DC.LARS.Web.Models;

namespace ESFA.DC.LARS.Web.Interfaces.Services
{
    public interface ISearchModelFactory
    {
        SearchModel GetSearchModel(BasicSearchModel basicSearchModel);
    }
}