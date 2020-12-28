using Abp.Modules;
using Abp.Reflection.Extensions;

namespace MCV.Portal
{
    public class PortalClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PortalClientModule).GetAssembly());
        }
    }
}
