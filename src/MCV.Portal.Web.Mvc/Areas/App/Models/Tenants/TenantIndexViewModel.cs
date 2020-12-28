using System.Collections.Generic;
using MCV.Portal.Editions.Dto;

namespace MCV.Portal.Web.Areas.App.Models.Tenants
{
    public class TenantIndexViewModel
    {
        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }
    }
}