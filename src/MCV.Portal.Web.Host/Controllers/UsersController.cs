using Abp.AspNetCore.Mvc.Authorization;
using MCV.Portal.Authorization;
using MCV.Portal.Storage;
using Abp.BackgroundJobs;

namespace MCV.Portal.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}