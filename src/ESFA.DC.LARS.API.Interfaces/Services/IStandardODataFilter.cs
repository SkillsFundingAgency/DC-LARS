using ESFA.DC.LARS.API.Models;

namespace ESFA.DC.LARS.API.Interfaces.Services
{
    public interface IStandardODataFilter
    {
        string ApplyFilter(StandardSearchModel searchModel);
    }
}
