using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MCV.Portal.MultiTenancy.HostDashboard.Dto;

namespace MCV.Portal.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}