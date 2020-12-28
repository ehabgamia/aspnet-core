using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class RestItemsListDto
    {
        public int Id { get; set; }

        public virtual string ItemDesc { get; set; }

        public virtual string ItemCode { get; set; }

        public virtual double CostPrice { get; set; }

        public virtual double SalesPrice { get; set; }

        public virtual string Picture { get; set; }

        [ForeignKey("ItemsCategoryId")]
        public int ItemsCategoryId { get; set; }
        public string ItemsCategoryDesc { get; set; }

        public virtual int Count { get; set; }

        public virtual bool NotAvailable { get; set; }

        public virtual int RestRequestItemsId { get; set; }

        public virtual int RestNonSchId { get; set; }

        public virtual int RestInfoId { get; set; }

        public virtual double SpecialSalesPrice { get; set; }

        public virtual string RestaurantCategory { get; set; }

        public virtual int RestSchedulesId { get; set; }
    }
}
