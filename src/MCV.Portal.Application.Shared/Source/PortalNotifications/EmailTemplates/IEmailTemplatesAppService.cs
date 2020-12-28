using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Source.PortalNotifications.EmailTemplates.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCV.Portal.Source.PortalNotifications.EmailTemplates
{
    public interface IEmailTemplatesAppService : IApplicationService
    {
        ListResultDto<EmailTemplatesListDto> GetEmailTemplatesListDto(GetEmailTemplateFilter input);

        Task CreateEmailTemplate(CreateEmailTemplateInput input);

        Task DeleteEmailTemplates(EntityDto input);

        Task<GetUpdateEmailTemplate> GetEmailTemplateForUpdate(EntityDto input);

        Task EditEmailTemplate(UpdateEmailTemplateInput input);
    }
}
