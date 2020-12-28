using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using MCV.Portal.Web.Controllers;

namespace MCV.Portal.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class WelcomeController : PortalControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}