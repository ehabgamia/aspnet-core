using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class CreateRestReqInput
    {
        public int Id { get; set; }

        [Required]
        public long UserId { get; set; }

        public long RequesterId { get; set; }

        [ForeignKey("RestSchedulesId")]
        public RestSchesListDto RestSchedules { get; set; }
        public int? RestSchedulesId { get; set; }

        public List<RestItemsListDto> RestItemsListDto { get; set; }

        [ForeignKey("RestInfosID")]
        public RestInfosListDto RestInfos { get; set; }
        public virtual int? RestInfosID { get; set; }

        [ForeignKey("PaymentTypeID")]
        public PaymentTypesListDto PaymentTypes { get; set; }
        public virtual int? PaymentTypeID { get; set; }

    }
}
