using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using MCV.Portal.Source.Vacations;
using MCV.Portal.Source.Vacations.Dto;
using MCV.Portal.Source.VacationTypes.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCV.Portal.Source.VacationTypes
{
    public class VacationsAppService : PortalAppServiceBase, IVacationAppService
    {
        private readonly IRepository<EmployeeData> _employeeDataRepository;
        private readonly IRepository<EmployeeVacationQuota> _employeeVacationQuota;
        private readonly IRepository<EmployeeVacation> _employeeVacationsRepository;
        private readonly IRepository<VacationType> _vacationTypeRepository;
        private readonly IRepository<EmployeeVacationsView> _employeeVacationsViewRepository;
        private readonly IRepository<ManagerVacations> _managerVacationsRepository;


        public VacationsAppService(
           IRepository<EmployeeData> employeeDataRepository,
           IRepository<EmployeeVacationQuota> employeeVacationQuota,
           IRepository<EmployeeVacation> employeeVacationsRepository,
           IRepository<VacationType> vacationTypeRepository,
           IRepository<EmployeeVacationsView> employeeVacationsViewRepository,
           IRepository<ManagerVacations> managerVacationsRepository)
        {
            _employeeDataRepository = employeeDataRepository;
            _employeeVacationQuota = employeeVacationQuota;
            _employeeVacationsRepository = employeeVacationsRepository;
            _vacationTypeRepository = vacationTypeRepository;
            _employeeVacationsViewRepository = employeeVacationsViewRepository;
            _managerVacationsRepository = managerVacationsRepository;
        }

        public ListResultDto<EmployeeDto> GetEmployeeData(EmployeeDataInput input)
        {
            var emp = _employeeDataRepository.GetAll().ToList().Where(x => x.Id == input.SAPCode);
            return new ListResultDto<EmployeeDto>(ObjectMapper.Map<List<EmployeeDto>>(emp));
        }

        public ListResultDto<EmployeeVacationQuotaDto> GetEmployeeVacationData()
        {
            var MySapCode = GetCurrentUser().SAPCode;
            var vacationquota = _employeeVacationQuota.GetAll().ToList().Where(x => x.Id == MySapCode);
            return new ListResultDto<EmployeeVacationQuotaDto>(ObjectMapper.Map<List<EmployeeVacationQuotaDto>>(vacationquota));
        }

        public async Task CreateEmployeeVacation(CreateEmployeeVacationInput input)
        {
            input.Status = VacationStatus.PendingForManager;
            var vacationtype = _vacationTypeRepository.GetAll().Where(x => x.TypeOfVacation == "Permission").FirstOrDefault();


            if (input.VacationTypeId == vacationtype.Id)
            {
                double period = EmployeeVacationRulesCheck(input, vacationtype);
                if (period > Convert.ToDouble(vacationtype.Limit))
                {
                    throw new Abp.UI.UserFriendlyException("Error!", "Sorry, you can't take more than " + Convert.ToDouble(vacationtype.Limit) + " " + vacationtype.Unit.ToString() + " as permissions.");
                }
                else
                {
                    var employeevacation = ObjectMapper.Map<EmployeeVacation>(input);
                    await _employeeVacationsRepository.InsertAsync(employeevacation);
                }
            }
            else
            {
                var employeevacation = ObjectMapper.Map<EmployeeVacation>(input);
                await _employeeVacationsRepository.InsertAsync(employeevacation);
            }
        }

        public double EmployeeVacationRulesCheck(CreateEmployeeVacationInput input, VacationType vacationtype)
        {

            var employeeperiod = _employeeVacationsRepository
                .GetAll()
                .ToList()
                .Where(x => x.EmpSAPCode == input.EmpSAPCode
                && x.VacationTypeId == input.VacationTypeId
                && x.VacationFrom.Month == input.VacationFrom.Month
                && x.VacationFrom.Year == input.VacationFrom.Year
                && (x.Status == VacationStatus.Done || x.Status == VacationStatus.ManagerApproved));

            TimeSpan ts;
            double period = 0;
            foreach (var vac in employeeperiod)
            {
                ts = (vac.VacationTo - vac.VacationFrom);
                if (vacationtype.Unit == VacationUnit.Minute)
                {
                    period += ts.TotalMinutes;
                }
                else if (vacationtype.Unit == VacationUnit.Hour)
                {
                    period += ts.TotalHours;
                }
                else if (vacationtype.Unit == VacationUnit.Day)
                {
                    period += ts.TotalDays;
                }

            }

            ts = (input.VacationTo - input.VacationFrom);
            if (vacationtype.Unit == VacationUnit.Minute)
            {
                period += ts.TotalMinutes;
            }
            else if (vacationtype.Unit == VacationUnit.Hour)
            {
                period += ts.TotalHours;
            }
            else if (vacationtype.Unit == VacationUnit.Day)
            {
                period += ts.TotalDays;
            }

            return period;
        }

        public ListResultDto<EmployeeVacationsListDto> GetEmployeePendingVacations()
        {
            var MySapCode = GetCurrentUser().SAPCode;
            var employeePendingVacations = _employeeVacationsViewRepository
                .GetAllIncluding(a => a.VacationType)
                .Where(x => x.EmpSAPCode == MySapCode && x.Status == VacationStatus.PendingForManager);
            //_employeeVacationsRepository.GetAll().Where(x => x.EmpSAPCode == MySapCode && x.Status == VacationStatus.PendingForManager);

            return new ListResultDto<EmployeeVacationsListDto>(ObjectMapper.Map<List<EmployeeVacationsListDto>>(employeePendingVacations));
        }

        public ListResultDto<EmployeeVacationsListDto> GetEmployeeHistVacations()
        {
            var MySapCode = GetCurrentUser().SAPCode;
            var employeehistvacations = _employeeVacationsViewRepository
                .GetAllIncluding(a => a.VacationType)
                .Where(x => x.EmpSAPCode == MySapCode && x.Status != VacationStatus.PendingForManager);
            //_employeeVacationsRepository.GetAll().Where(x => x.EmpSAPCode == MySapCode && x.Status != VacationStatus.PendingForManager);

            return new ListResultDto<EmployeeVacationsListDto>(ObjectMapper.Map<List<EmployeeVacationsListDto>>(employeehistvacations));
        }

        public async Task DeleteVacation(EntityDto input)
        {
            await _employeeVacationsRepository.DeleteAsync(input.Id);
        }

        public ListResultDto<ManagerVacationsOutputListDto> GetManagerPendingVacations()
        {
            var ManagerUserName = GetCurrentUser().UserName;
            var managervacations = _managerVacationsRepository
                .GetAllIncluding(a => a.VacationType, a => a.EmployeeVacation)
                .Where(x => x.OnBehalfCCManagerVacation == ManagerUserName && x.Status == VacationStatus.PendingForManager);

            return new ListResultDto<ManagerVacationsOutputListDto>(ObjectMapper.Map<List<ManagerVacationsOutputListDto>>(managervacations));
        }

        public ListResultDto<ManagerVacationsOutputListDto> GetManagerHistoryVacations()
        {
            var ManagerUserName = GetCurrentUser().UserName;
            var managervacations = _managerVacationsRepository
                .GetAllIncluding(a => a.VacationType, a => a.EmployeeVacation)
                .Where(x => x.OnBehalfCCManagerVacation == ManagerUserName && (x.Status != VacationStatus.PendingForManager));

            return new ListResultDto<ManagerVacationsOutputListDto>(ObjectMapper.Map<List<ManagerVacationsOutputListDto>>(managervacations));
        }

        public async Task ManagerApproveVacation(EntityDto input)
        {
            var empvacation = await _employeeVacationsRepository.GetAsync(input.Id);
            empvacation.Status = VacationStatus.ManagerApproved;
            await _employeeVacationsRepository.UpdateAsync(empvacation);
        }

        public async Task ManagerDeclineVacation(EntityDto input)
        {
            var empvacation = await _employeeVacationsRepository.GetAsync(input.Id);
            empvacation.Status = VacationStatus.ManagerReject;
            await _employeeVacationsRepository.UpdateAsync(empvacation);
        }

    }
}
