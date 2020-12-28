using Microsoft.AspNetCore.Mvc;
using MCV.Portal.Web.Controllers;

namespace MCV.Portal.Web.Public.Controllers
{
    public class AboutController : PortalControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}