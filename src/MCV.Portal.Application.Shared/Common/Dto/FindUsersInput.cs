using MCV.Portal.Dto;

namespace MCV.Portal.Common.Dto
{
    public class FindUsersInput : PagedAndFilteredInputDto
    {
        public int? TenantId { get; set; }
    }
}