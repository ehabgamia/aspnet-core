using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Tenants.Dashboard.Dto;

namespace MCV.Portal.Tenants.Dashboard
{
    public interface ITenantDashboardAppService : IApplicationService
    {
        
        GetMemberActivityOutput GetMemberActivity();

        GetDashboardDataOutput GetDashboardData(GetDashboardDataInput input);

        GetDailySalesOutput GetDailySales();

        GetProfitShareOutput GetProfitShare();

        GetSalesSummaryOutput GetSalesSummary(GetSalesSummaryInput input);

        GetTopStatsOutput GetTopStats();

        GetRegionalStatsOutput GetRegionalStats();

        GetGeneralStatsOutput GetGeneralStats();

        ListResultDto<MyBirthday> CheckMyBirthday();
    }
}
