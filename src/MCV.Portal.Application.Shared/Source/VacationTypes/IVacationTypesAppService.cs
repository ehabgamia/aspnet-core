using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Source.Vacations.Dto;
using MCV.Portal.Source.VacationTypes.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCV.Portal.Source.VacationTypes
{
    public interface IVacationTypesAppService : IApplicationService
    {
        ListResultDto<VacationTypesListDto> GetVacationTypes(EmployeeDataInput input);

    }
}
