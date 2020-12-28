using MCV.Portal.Authorization.Users.Dto;
using MCV.Portal.Source.Vacations;
using MCV.Portal.Source.VacationTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MCV.Portal.Source.VacationTypes.Dto
{
    public class VacationTypesListDto
    {
        public int Id { get; set; }

        public string TypeOfVacation { get; set; }

        public int SAPCodeType { get; set; }

        public int ServiceDeskId { get; set; }

        public UserListDto User { get; set; }

        public bool RequireDateTime { get; set; }

        public bool RequireTo { get; set; }

        public virtual VacationUnit Unit { get; set; }

        public virtual decimal Limit { get; set; }

    }
}
