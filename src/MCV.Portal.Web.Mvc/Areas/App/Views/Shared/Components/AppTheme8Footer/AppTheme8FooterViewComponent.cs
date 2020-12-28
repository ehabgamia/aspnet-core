using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MCV.Portal.Web.Areas.App.Models.Layout;
using MCV.Portal.Web.Session;
using MCV.Portal.Web.Views;

namespace MCV.Portal.Web.Areas.App.Views.Shared.Components.AppTheme8Footer
{
    public class AppTheme8FooterViewComponent : PortalViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme8FooterViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerModel = new FooterViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(footerModel);
        }
    }
}
