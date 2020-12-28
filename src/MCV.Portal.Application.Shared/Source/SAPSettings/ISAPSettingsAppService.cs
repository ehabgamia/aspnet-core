using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Source.SAPSettings.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCV.Portal.Source.SAPSettings
{
    public interface ISAPSettingsAppService : IApplicationService
    {
        ListResultDto<SAPSettingListDto> GetSAPSettings(GetSAPSettingsInput input);
        Task CreateSAPSetting(CreateSAPSettingInput input);
        Task DeleteSAPSetting(EntityDto input);
        Task<GetSAPSettingForEditOutput> GetSAPSettingForEdit(GetSAPSettingForEditInput input);
        
        Task EditSAPSetting(EditSAPSettingInput input);
            
    }
}
