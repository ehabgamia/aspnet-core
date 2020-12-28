using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MCV.Portal.Source.Vacations.Dto
{
    public class CreateEmployeeVacationInput
    {
        [Required]
        public string Requester { get; set; }

        [Required]
        public int VacationTypeId { get; set; }

        [Required]
        public int EmpSAPCode { get; set; }

        [Required]
        public DateTime VacationFrom { get; set; }

        public DateTime VacationTo { get; set; }

        public decimal Period { get; set; }

        [Required]
        public int OnBehalfSAPCode { get; set; }

        public VacationStatus Status { get; set; }

        public string Reason { get; set; }
    }
}
