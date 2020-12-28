using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Authorization.Permissions.Dto;

namespace MCV.Portal.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
