using Abp.Modules;
using Abp.Reflection.Extensions;

namespace MCV.Portal
{
    [DependsOn(typeof(PortalXamarinSharedModule))]
    public class PortalXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PortalXamarinIosModule).GetAssembly());
        }
    }
}