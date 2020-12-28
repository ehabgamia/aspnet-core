using System.Collections.Generic;
using Abp.Application.Services.Dto;
using MCV.Portal.Configuration.Host.Dto;
using MCV.Portal.Editions.Dto;

namespace MCV.Portal.Web.Areas.App.Models.HostSettings
{
    public class HostSettingsViewModel
    {
        public HostSettingsEditDto Settings { get; set; }

        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }

        public List<ComboboxItemDto> TimezoneItems { get; set; }
    }
}