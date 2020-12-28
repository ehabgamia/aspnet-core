using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class RestNonSchItemListDto
    {
        public virtual int? RestNonSchItemId { get; set; }

        [ForeignKey("RestItemId")]
        public virtual RestItemsListDto RestItems { get; set; }
        public virtual int RestItemId { get; set; }

        [ForeignKey("RestInfoID")]
        public virtual RestInfosListDto RestInfos { get; set; }
        public virtual int RestInfoID { get; set; }

        public virtual double SpecialSalesPrice { get; set; }

    }
}
