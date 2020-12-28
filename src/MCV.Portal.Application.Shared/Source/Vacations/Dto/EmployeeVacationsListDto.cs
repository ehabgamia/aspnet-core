using Abp.Extensions;
using Abp.Runtime.Validation;
using MCV.Portal.Dto;
using MCV.Portal.Source.VacationTypes.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCV.Portal.Source.Vacations.Dto
{
    public class EmployeeVacationsListDto
    {
        public int Id { get; set; }

        public DateTime CreationTime { get; set; }

        public VacationTypesListDto VacationType { get; set; }

        public VacationStatus Status { get; set; }

        public int EmpSAPCode { get; set; }

        public string Requester { get; set; }

        public DateTime VacationFrom { get; set; }

        public DateTime VacationTo { get; set; }

        public string OnBehalfEmp { get; set; }

        public string Reason { get; set; }

        public string Manager { get; set; }

        public bool IsDeleted { get; set; }
    }
}
