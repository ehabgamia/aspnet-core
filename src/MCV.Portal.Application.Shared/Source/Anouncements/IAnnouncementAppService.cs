using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Source.Anouncements.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCV.Portal.Source.Anouncements
{
    public interface IAnnouncementAppService : IApplicationService
    {
        ListResultDto<AnnouncementsListDto> GetAnnouncements(GetAnnouncements input);

        ListResultDto<AnnouncementsListDto> GetTopAnnouncements(GetNumberOfAnnouncements input);

        Task CreateAnnouncement(CreateAnnouncementInput input);

        Task DeleteAnnouncement(EntityDto input);

        Task<GetAnnouncementForEditOutput> GetAnnouncementForEdit(GetAnnouncementForEditInput input);

        Task EditAnnouncement(EditAnnouncementInput input);

    }
}
