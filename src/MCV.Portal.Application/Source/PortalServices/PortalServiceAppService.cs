using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using MCV.Portal.Source.PortalServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCV.Portal.Source.PortalServices
{
    public class PortalServiceAppService : PortalAppServiceBase, IPortalServiceAppservice
    {
        private readonly IRepository<PortalService> _portalServiceRepository;

        public PortalServiceAppService(IRepository<PortalService> portalServiceRepository)
        {
            _portalServiceRepository = portalServiceRepository;
        }

        public async Task CreatePortalService(CreatePortalServiceInput input)
        {
            var PortalServiceInput = ObjectMapper.Map<PortalService>(input);
            await _portalServiceRepository.InsertAsync(PortalServiceInput);
        }

        public async Task DeletePortalService(EntityDto input)
        {
            await _portalServiceRepository.DeleteAsync(input.Id);
        }

        public async Task EditPortalService(UpdatePortalServiceInput input)
        {
            await _portalServiceRepository.UpdateAsync(ObjectMapper.Map<PortalService>(input));
        }

        public ListResultDto<PortalServicesListDto> GetPortalServiceList(GetPortalServiceFilter input)
        {
            var portalServices = _portalServiceRepository
                .GetAll()
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.ServiceName.Contains(input.Filter) || p.ServiceDescription.Contains(input.Filter)
                ).ToList();

            return new ListResultDto<PortalServicesListDto>(ObjectMapper.Map<List<PortalServicesListDto>>(portalServices));
        }

        public async Task<GetPortalServiceForUpdate> GetPortalServiceForUpdate(EntityDto input)
        {
            var PortalService = await _portalServiceRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetPortalServiceForUpdate>(PortalService);
        }
    }
}
