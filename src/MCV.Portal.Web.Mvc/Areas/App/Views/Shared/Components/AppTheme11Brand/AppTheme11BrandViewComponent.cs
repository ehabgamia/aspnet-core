﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MCV.Portal.Web.Areas.App.Models.Layout;
using MCV.Portal.Web.Session;
using MCV.Portal.Web.Views;

namespace MCV.Portal.Web.Areas.App.Views.Shared.Components.AppTheme11Brand
{
    public class AppTheme11BrandViewComponent : PortalViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme11BrandViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(headerModel);
        }
    }
}
