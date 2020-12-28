using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Source.Vacations.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCV.Portal.Source.Vacations
{
    public interface IVacationAppService : IApplicationService
    {
        ListResultDto<EmployeeDto> GetEmployeeData(EmployeeDataInput input);

        ListResultDto<EmployeeVacationQuotaDto> GetEmployeeVacationData();

        Task CreateEmployeeVacation(CreateEmployeeVacationInput input);

        ListResultDto<EmployeeVacationsListDto> GetEmployeePendingVacations();

        ListResultDto<EmployeeVacationsListDto> GetEmployeeHistVacations();

        Task DeleteVacation(EntityDto input);

        ListResultDto<ManagerVacationsOutputListDto> GetManagerPendingVacations();

        ListResultDto<ManagerVacationsOutputListDto> GetManagerHistoryVacations();

        Task ManagerApproveVacation(EntityDto input);

        Task ManagerDeclineVacation(EntityDto input);
    }
}
