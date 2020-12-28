using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Source.PortalServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCV.Portal.Source.PortalServices
{
    public interface IPortalServiceAppservice : IApplicationService
    {
        ListResultDto<PortalServicesListDto> GetPortalServiceList(GetPortalServiceFilter input);

        Task CreatePortalService(CreatePortalServiceInput input);

        Task DeletePortalService(EntityDto input);

        Task<GetPortalServiceForUpdate> GetPortalServiceForUpdate(EntityDto input);

        Task EditPortalService(UpdatePortalServiceInput input);
    }
}
