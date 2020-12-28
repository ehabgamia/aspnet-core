using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using MCV.Portal.Source.SAPSettings.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCV.Portal.Source.SAPSettings
{
    public class SAPSettingsAppService : PortalAppServiceBase, ISAPSettingsAppService
    {
        private readonly IRepository<SAPSetting> _sapSettingRepository;
        public SAPSettingsAppService(IRepository<SAPSetting> sapSettingRepository)
        {
            _sapSettingRepository = sapSettingRepository;
        }
        public ListResultDto<SAPSettingListDto> GetSAPSettings(GetSAPSettingsInput input)
        {
            var SAPSetting = _sapSettingRepository
            .GetAll().ToList();

            return new ListResultDto<SAPSettingListDto>(ObjectMapper.Map<List<SAPSettingListDto>>(SAPSetting));
        }
        public async Task CreateSAPSetting(CreateSAPSettingInput input)
        {
            var sapSetting = ObjectMapper.Map<SAPSetting>(input);
            await _sapSettingRepository.InsertAsync(sapSetting);
        }
        public async Task DeleteSAPSetting(EntityDto input)
        {
            await _sapSettingRepository.DeleteAsync(input.Id);
        }
        public async Task<GetSAPSettingForEditOutput> GetSAPSettingForEdit(GetSAPSettingForEditInput input)
        {
            var sapSetting = await _sapSettingRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetSAPSettingForEditOutput>(sapSetting);
        }
        
        public async Task EditSAPSetting(EditSAPSettingInput input)
        {
            var SAPSetting = await _sapSettingRepository.GetAsync(input.Id);
            SAPSetting.ConnectionName = input.ConnectionName;
            SAPSetting.ServerHost = input.ServerHost;
            SAPSetting.SystemNumber = input.SystemNumber;
            SAPSetting.SystemID = input.SystemID;
            SAPSetting.UserName = input.UserName;
            SAPSetting.Password = input.Password;
            SAPSetting.Client = input.Client;
            SAPSetting.Language = input.Language;
            SAPSetting.PoolSize = input.PoolSize;
            SAPSetting.DefaultConnection = input.DefaultConnection;

            await _sapSettingRepository.UpdateAsync(SAPSetting);
        }
        
    }
}
