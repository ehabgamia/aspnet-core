using System;
using System.Collections.Generic;
using System.Text;

namespace MCV.Portal.Source.Vacations.Dto
{
    public class EmployeeVacationQuotaDto
    {
        public decimal AbsenceQuota { get; set; }

        public decimal Absence { get; set; }

        public decimal AbsenceBalance { get; set; }

        public string emp_username { get; set; }
    }
}
