


using System.Reflection;

namespace Ombi.Helpers
{
    public class AssemblyHelper
    {
        public static string GetAssemblyVersion()
        {
            return
                Assembly.GetEntryAssembly()
                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                    .InformationalVersion;
        }
    }
}