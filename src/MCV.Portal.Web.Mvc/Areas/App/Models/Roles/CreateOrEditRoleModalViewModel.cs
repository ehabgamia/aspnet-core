using Abp.AutoMapper;
using MCV.Portal.Authorization.Roles.Dto;
using MCV.Portal.Web.Areas.App.Models.Common;

namespace MCV.Portal.Web.Areas.App.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode => Role.Id.HasValue;
    }
}