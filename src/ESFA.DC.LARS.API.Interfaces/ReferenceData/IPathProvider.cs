namespace ESFA.DC.LARS.API.Interfaces.ReferenceData
{
    public interface IPathProvider
    {
        string GetFileLocation(string relativePath);
    }
}