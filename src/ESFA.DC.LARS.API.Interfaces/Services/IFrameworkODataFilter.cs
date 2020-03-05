namespace ESFA.DC.LARS.API.Interfaces.Services
{
    public interface IFrameworkODataFilter
    {
        string GetFilter(int frameworkCode, int programType, int pathwayCode);
    }
}