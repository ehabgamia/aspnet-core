using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class GetRestReqForEditOutput
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

        public string Date { get; set; }

        public int RestInfosID { get; set; }

        public List<RestItemsListDto> targetItems { get; set; }

        public bool PersonalCost { get; set; }

        [ForeignKey("RestNonSchItemID")]
        public RestNonSchItemListDto RestNonSchItem { get; set; }
        public int RestNonSchItemID { get; set; }

        [ForeignKey("PaymentTypeID")]
        public PaymentTypesListDto PaymentTypes { get; set; }
        public virtual int? PaymentTypeID { get; set; }
        public virtual string PaymentTypeDesc { get; set; }

        public List<Days> GetDates { get; set; }

        public List<RestItemsListDto> SelectedRestItems { get; set; }

    }
}
