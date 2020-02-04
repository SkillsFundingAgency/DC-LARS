namespace ESFA.DC.LARS.Web.Models
{
    public interface IAppVersionService
    {
        string Version { get; }

        string TargetFrameworkVersion { get; }
    }
}
