using Microsoft.Extensions.Configuration;

namespace MCV.Portal.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
