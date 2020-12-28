using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using MCV.Portal.Source.AnnouncementsTypes;
using MCV.Portal.Source.Anouncements;
using MCV.Portal.Source.Anouncements.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCV.Portal.Source.Announcements
{
    public class AnnouncementAppService : PortalAppServiceBase, IAnnouncementAppService
    {

        private readonly IRepository<Announcement> _announcementRepository;

        public Announcement Announcement { get; set; }

        public AnnouncementAppService(
           IRepository<Announcement> announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task CreateAnnouncement(CreateAnnouncementInput input)
        {
            var announcement = ObjectMapper.Map<Announcement>(input);
            await _announcementRepository.InsertAsync(announcement);
        }

        public async Task DeleteAnnouncement(EntityDto input)
        {
            await _announcementRepository.DeleteAsync(input.Id);
        }

        public async Task EditAnnouncement(EditAnnouncementInput input)
        {
            var announcement = await _announcementRepository.GetAsync(input.Id);
            announcement.Subject = input.Subject;
            announcement.Message = input.Message;
            announcement.ExpiryDate = input.ExpiryDate;
            announcement.EntryDate = input.EntryDate;
            announcement.AnnouncementTypeId = input.AnnouncementTypeId;
            await _announcementRepository.UpdateAsync(announcement);
        }

        public async Task<GetAnnouncementForEditOutput> GetAnnouncementForEdit(GetAnnouncementForEditInput input)
        {
            var announcement = await _announcementRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetAnnouncementForEditOutput>(announcement);
        }

        public ListResultDto<AnnouncementsListDto> GetAnnouncements(GetAnnouncements input)
        {
           var announcements = _announcementRepository
            .GetAllIncluding(a => a.AnnouncementType, a => a.User)
            .WhereIf(
               !input.Filter.IsNullOrEmpty(),
               p => p.Message.Contains(input.Filter)
            )
            .OrderBy(p => p.Subject)
            .ToList();

           
            var announcementCount = announcements.Count();

            return new ListResultDto<AnnouncementsListDto>(ObjectMapper.Map<List<AnnouncementsListDto>>(announcements));
        }

        public ListResultDto<AnnouncementsListDto> GetTopAnnouncements(GetNumberOfAnnouncements input)
        {
            var announcements = _announcementRepository
              .GetAllIncluding( a => a.AnnouncementType, a => a.User)
              .OrderBy(p => p.Subject)
              .ToList();

            var announcementCount = announcements.Count();

            return new ListResultDto<AnnouncementsListDto>(ObjectMapper.Map<List<AnnouncementsListDto>>(announcements));
        }
    }
}
