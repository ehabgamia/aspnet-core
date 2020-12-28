using Abp.AspNetCore.Mvc.ViewComponents;

namespace MCV.Portal.Web.Views
{
    public abstract class PortalViewComponent : AbpViewComponent
    {
        protected PortalViewComponent()
        {
            LocalizationSourceName = PortalConsts.LocalizationSourceName;
        }
    }
}