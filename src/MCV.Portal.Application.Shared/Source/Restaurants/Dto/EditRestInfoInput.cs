using Abp;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class EditRestInfoInput
    {
        public int Id { get; set; }

        public virtual string Country { get; set; }

        public virtual string City { get; set; }

        public virtual string Area { get; set; }

        public virtual string Building { get; set; }

        public virtual string AdminPerson { get; set; }

        public virtual bool isAvailable { get; set; }

        public virtual string ExtNum { get; set; }

        [ForeignKey("RestCategoryId")]
        public int RestCategoryId { get; set; }
        public string RestCategory { get; set; }

        public virtual ListResultDto<RestSchesListDto> RestSchedules { get; set; }

        public List<NameValue<string>> selectedEmployees { get; set; }

        public virtual ICollection<RestInfoAdminsDto> RestInfoAdmins { get; set; }


    }
}
