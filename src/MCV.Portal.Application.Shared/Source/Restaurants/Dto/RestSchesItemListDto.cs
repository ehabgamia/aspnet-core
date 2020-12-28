using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class RestSchesItemListDto
    {
        public int Id { get; set; }

        [ForeignKey("RestSchedulesId")]
        public RestSchesItemListDto RestSchedules { get; set; }
        public int RestSchedulesId { get; set; }

        public int RestItemsId { get; set; }

        [ForeignKey("RestItemsId")]
        public RestItemsListDto RestItems { get; set; }

    }
}
