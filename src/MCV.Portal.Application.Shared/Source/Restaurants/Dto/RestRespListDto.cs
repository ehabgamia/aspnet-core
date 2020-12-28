using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class RestRespListDto
    {
        public virtual int Id { get; set; }

        [ForeignKey("RestRequestItemsId")]
        public RestReqItemListDto RestRequestItems { get; set; }
        public virtual int RestRequestItemsId { get; set; }

        public virtual bool NotAvailable { get; set; }
    }
}
