using Abp.Application.Services.Dto;
using Abp.BackgroundJobs;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Abp.UI;
using MCV.Portal.Friendships;
using MCV.Portal.Source.AnnouncementsTypes.Dto;
using MCV.Portal.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCV.Portal.Source.AnnouncementsTypes
{
    public class AnnouncementTypeAppService : PortalAppServiceBase, IAnnouncementTypeAppService
    {

        private readonly IRepository<AnnouncementType> _announcementTypeRepository;

        public AnnouncementType AnnouncementType { get; set; }

        public AnnouncementTypeAppService(
            IRepository<AnnouncementType> announcementTypeRepository)
        {
            _announcementTypeRepository = announcementTypeRepository;
        }

        public async Task CreateAnnouncementType(CreateAnnouncementTypeInput input)
        {
            var announcementType = ObjectMapper.Map<AnnouncementType>(input);
            await _announcementTypeRepository.InsertAsync(announcementType);
        }

        public async Task DeleteAnnouncementType(EntityDto input)
        {
            await _announcementTypeRepository.DeleteAsync(input.Id);
        }

        public async Task<GetAnnouncementTypeForEditOutput> GetAnnouncementTypeForEdit(GetAnnouncementTypeForEditInput input)
        {
            var announcementType = await _announcementTypeRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetAnnouncementTypeForEditOutput>(announcementType);
        }

        public async Task EditAnnouncementType(EditAnnouncementTypeInput input)
        {
            var announcementType = await _announcementTypeRepository.GetAsync(input.Id);
            announcementType.Name = input.Name;
            announcementType.IconPath = input.IconPath;
            await _announcementTypeRepository.UpdateAsync(announcementType);
        }


        public ListResultDto<AnnouncementTypeListDto> GetAnnouncementTypes(GetAnnouncementTypesInput input)
        {
            var announcementTypes = _announcementTypeRepository
            .GetAllIncluding(a => a.User)
            .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p => p.Name.Contains(input.Filter)
            )
            .OrderBy(p => p.Name)
            .ToList();

            var announcementTypesCount = announcementTypes.Count();


            return new ListResultDto<AnnouncementTypeListDto>(ObjectMapper.Map<List<AnnouncementTypeListDto>>(announcementTypes));
        }

    }
}
