using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ESFA.DC.LARS.Web.Models
{
    public interface IAppVersionService
    {
        string Version { get; }

        string TargetFrameworkVersion { get; }
    }
}
