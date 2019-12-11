using System.IO;
using System.Reflection;
using ESFA.DC.LARS.API.Interfaces.ReferenceData;

namespace ESFA.DC.LARS.API.ReferenceData
{
    public class PathProvider : IPathProvider
    {
        public string GetFileLocation(string relativePath)
        {
            var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", string.Empty));
            return $@"{root}\{relativePath}";
        }
    }
}