using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using MCV.Portal.Source.PortalNotifications.EmailTemplates.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MCV.Portal.Source.PortalNotifications.EmailTemplates
{
    public class EmailTemplatesAppService : PortalAppServiceBase, IEmailTemplatesAppService
    {
        private readonly IRepository<EmailTemplate> _emailTemplatesRepository;

        public EmailTemplatesAppService(IRepository<EmailTemplate> emailTemplatesRepository)
        {
            _emailTemplatesRepository = emailTemplatesRepository;
        }

        public async Task CreateEmailTemplate(CreateEmailTemplateInput input)
        {
            input.MessageBody = ParseBetween(input.MessageBody,"SafeValue must use [property]=binding: ", "(see http://g.co/ng/security#xss)");
            var EmailTemplateInput = ObjectMapper.Map<EmailTemplate>(input);
            await _emailTemplatesRepository.InsertAsync(EmailTemplateInput);
        }

        public string ParseBetween(string Subject, string Start, string End)
        {
            return Regex.Match(Subject, Regex.Replace(Start, @"[][{}()*+?.\\^$|]", @"\$0") + @"\s*(((?!" + Regex.Replace(Start, @"[][{}()*+?.\\^$|]", @"\$0") + @"|" + Regex.Replace(End, @"[][{}()*+?.\\^$|]", @"\$0") + @").)+)\s*" + Regex.Replace(End, @"[][{}()*+?.\\^$|]", @"\$0"), RegexOptions.IgnoreCase).Value.Replace(Start, "").Replace(End, "");
        }

        public async Task DeleteEmailTemplates(EntityDto input)
        {
            await _emailTemplatesRepository.DeleteAsync(input.Id);
        }

        public async Task EditEmailTemplate(UpdateEmailTemplateInput input)
        {
            input.MessageBody = ParseBetween(input.MessageBody, "SafeValue must use [property]=binding: ", "(see http://g.co/ng/security#xss)");
            await _emailTemplatesRepository.UpdateAsync(ObjectMapper.Map<EmailTemplate>(input));
        }

        public async Task<GetUpdateEmailTemplate> GetEmailTemplateForUpdate(EntityDto input)
        {
            var EmailTemplate = await _emailTemplatesRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetUpdateEmailTemplate>(EmailTemplate);
        }

        public ListResultDto<EmailTemplatesListDto> GetEmailTemplatesListDto(GetEmailTemplateFilter input)
        {
            var emailTemplates = _emailTemplatesRepository
                .GetAll()
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.Subject.Contains(input.Filter) || p.MessageBody.Contains(input.Filter)
                ).ToList();

            return new ListResultDto<EmailTemplatesListDto>(ObjectMapper.Map<List<EmailTemplatesListDto>>(emailTemplates));
        }
    }
}
