using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using MCV.Portal.Source.Vacations;
using MCV.Portal.Source.Vacations.Dto;
using MCV.Portal.Source.VacationTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCV.Portal.Source.VacationTypes
{
    public class VacationTypesAppService : PortalAppServiceBase, IVacationTypesAppService
    {
        private readonly IRepository<VacationType> _vacationTypeRepository;
        private readonly IRepository<EmployeeData> _employeeDataRepository;

        public VacationType VacationType { get; set; }

        public VacationTypesAppService(
           IRepository<VacationType> vacationTypeRepository,
           IRepository<EmployeeData> employeeDataRepository)
        {
            _vacationTypeRepository = vacationTypeRepository;
            _employeeDataRepository = employeeDataRepository;
        }

        public ListResultDto<VacationTypesListDto> GetVacationTypes(EmployeeDataInput input)
        {
            var emp = _employeeDataRepository.GetAll().ToList().Where(x => x.Id == input.SAPCode);
            EmployeeData empdata = new EmployeeData();
            empdata = emp.FirstOrDefault();

            if (empdata.EmployeeType == 1)
            {
                List<int> notindluceded = new List<int> { 1013, 1011, 1017, 1001, 1018, 1021, 1019 };
                var vacationTypes = _vacationTypeRepository.GetAllIncluding(a => a.User)
                    .ToList()
                    .Where(x => !notindluceded.Contains(x.SAPCodeType));


                return new ListResultDto<VacationTypesListDto>(ObjectMapper.Map<List<VacationTypesListDto>>(vacationTypes));
            }
            else if (empdata.EmployeeType == 7)
            {
                List<int> notindluceded = new List<int> { 1013, 1011, 1006, 1017, 1014, 1019 };
                var vacationTypes = _vacationTypeRepository.GetAllIncluding(a => a.User)
                    .ToList()
                    .Where(x => !notindluceded.Contains(x.SAPCodeType) && x.ServiceDeskId != 901);

                return new ListResultDto<VacationTypesListDto>(ObjectMapper.Map<List<VacationTypesListDto>>(vacationTypes));
            }

            List<VacationTypesListDto> EmptyTypes = new List<VacationTypesListDto>();
            return new ListResultDto<VacationTypesListDto>(ObjectMapper.Map<List<VacationTypesListDto>>(EmptyTypes));
        }

    }
}
