using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class RestReqItemListDto
    {
        public int Id { get; set; }

        [ForeignKey("RestScheduleItemsId")]
        public RestSchesItemListDto RestScheduleItems { get; set; }
        public virtual int RestScheduleItemsId { get; set; }

        [ForeignKey("RestRequestsId")]
        public RestReqListDto RestRequests { get; set; }
        public virtual int RestRequestsId { get; set; }

        public virtual int Count { get; set; }

        public virtual bool Approve { get; set; }
        public virtual bool Recject { get; set; }

        [ForeignKey("RestNonSchItemId")]
        public RestNonSchItemListDto RestNonSchItem { get; set; }
        public virtual int? RestNonSchItemId { get; set; }
    }
}
