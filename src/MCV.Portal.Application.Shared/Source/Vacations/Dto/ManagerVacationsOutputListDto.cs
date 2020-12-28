using MCV.Portal.Source.VacationTypes.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCV.Portal.Source.Vacations.Dto
{
    public class ManagerVacationsOutputListDto
    {
        public VacationTypesListDto VacationType { get; set; }

        public VacationStatus Status { get; set; }

        public int EmployeeSAPCode { get; set; }

        public DateTime VacationFrom { get; set; }

        public DateTime VacationTo { get; set; }

        public decimal Period { get; set; }

        public int OnBehalf { get; set; }

        public DateTime EntryDate { get; set; }

        public int VacationTypeID { get; set; }

        public string ForType { get; set; }

        public string Employee { get; set; }

        public string OnBehalfName { get; set; }

        public string OrgName { get; set; }

        public string JobTitle { get; set; }

        public EmployeeVacationsListDto EmployeeVacation { get; set; }

        public string OnBehalfCCManagerVacation { get; set; }

        public string Requester { get; set; }

        public string Reason { get; set; }
    }
}
