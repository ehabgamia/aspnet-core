using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MCV.Portal.Configure;
using MCV.Portal.Startup;
using MCV.Portal.Test.Base;

namespace MCV.Portal.GraphQL.Tests
{
    [DependsOn(
        typeof(PortalGraphQLModule),
        typeof(PortalTestBaseModule))]
    public class PortalGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PortalGraphQLTestModule).GetAssembly());
        }
    }
}