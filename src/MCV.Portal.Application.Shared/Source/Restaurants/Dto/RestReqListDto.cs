using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class RestReqListDto
    {
        public int Id { get; set; }

        public long UserId { get; set; }

        public string Username { get; set; }

        public long RequesterId { get; set; }

        public string Requestername { get; set; }

        [ForeignKey("RestSchedulesId")]
        public RestSchesListDto RestSchedules { get; set; }
        public int RestSchedulesId { get; set; }

        public string RestaurantInfo { get; set; }

        public string Day { get; set; }

        [ForeignKey("RestInfosID")]
        public RestInfosListDto RestInfos { get; set; }
        public virtual int? RestInfosID { get; set; }

        public int? ExtNum { get; set; }

        public virtual string StatusDesc { get; set; }
        public virtual int RequestStatusID { get; set; }

        [ForeignKey("PaymentTypeID")]
        public PaymentTypesListDto PaymentTypes { get; set; }
        public virtual int? PaymentTypeID { get; set; }
        public virtual string PaymentTypeDesc { get; set; }

        public DateTime CreationTime { get; set; }

        public string Time { get; set; }

    }
}
