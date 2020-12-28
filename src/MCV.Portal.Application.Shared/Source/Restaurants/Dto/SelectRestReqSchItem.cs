using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class SelectRestReqSchItem
    {
        public int Id { get; set; }

        [ForeignKey("RestScheduleItemsId")]
        public RestSchesItemListDto RestScheduleItems { get; set; }
        public virtual int RestScheduleItemsId { get; set; }

        [ForeignKey("RestRequestsId")]
        public RestReqListDto RestRequests { get; set; }
        public virtual int RestRequestsId { get; set; }

        public int count { get; set; }
    }
}
