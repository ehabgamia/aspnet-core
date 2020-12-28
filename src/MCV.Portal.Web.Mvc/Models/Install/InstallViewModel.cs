using System.Collections.Generic;
using Abp.Localization;
using MCV.Portal.Install.Dto;

namespace MCV.Portal.Web.Models.Install
{
    public class InstallViewModel
    {
        public List<ApplicationLanguage> Languages { get; set; }

        public AppSettingsJsonDto AppSettingsJson { get; set; }
    }
}
