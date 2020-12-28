using System.Collections.Generic;
using MCV.Portal.Caching.Dto;

namespace MCV.Portal.Web.Areas.App.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}