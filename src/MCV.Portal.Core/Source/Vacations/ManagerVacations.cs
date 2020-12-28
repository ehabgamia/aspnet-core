using Abp.Domain.Entities;
using MCV.Portal.Source.VacationTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.Vacations
{
    [Table("VacationsGetManagerVacations")]
    public class ManagerVacations : Entity
    {
        [ForeignKey("VacationTypeId")]
        public virtual VacationType VacationType { get; set; }
        public int VacationTypeId { get; set; }

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

        [ForeignKey("EmployeeVacationId")]
        public virtual EmployeeVacation EmployeeVacation { get; set; }
        public int EmployeeVacationId { get; set; }

        public string OnBehalfCCManagerVacation { get; set; }

        public string Requester { get; set; }

        public string Reason { get; set; }
    }
}
