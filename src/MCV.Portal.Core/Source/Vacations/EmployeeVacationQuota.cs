using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.Vacations
{
    [Table("VacationsGetEmpQuotaData")]
    public class EmployeeVacationQuota : Entity
    {
        [Column("AbsenceQuota")]
        public decimal AbsenceQuota { get; set; }

        [Column("Absence")]
        public decimal Absence { get; set; }

        [Column("AbsenceBalance")]
        public decimal AbsenceBalance { get; set; }

        [Column("emp_username")]
        public string emp_username { get; set; }
    }
}
