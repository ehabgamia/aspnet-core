using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Source.AnnouncementsTypes.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCV.Portal.Source.AnnouncementsTypes
{
    public interface IAnnouncementTypeAppService : IApplicationService
    {
        ListResultDto<AnnouncementTypeListDto> GetAnnouncementTypes(GetAnnouncementTypesInput input);

        Task CreateAnnouncementType(CreateAnnouncementTypeInput input);

        Task DeleteAnnouncementType(EntityDto input);

        Task<GetAnnouncementTypeForEditOutput> GetAnnouncementTypeForEdit(GetAnnouncementTypeForEditInput input);

        Task EditAnnouncementType(EditAnnouncementTypeInput input);

    }
}
