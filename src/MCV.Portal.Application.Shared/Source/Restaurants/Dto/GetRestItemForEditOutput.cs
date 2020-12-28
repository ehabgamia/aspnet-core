using MCV.Portal.Source.Restaurant;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public class GetRestItemForEditOutput
    {
        public int Id { get; set; }

        public virtual string ItemDesc { get; set; }

        public virtual string ItemCode { get; set; }

        public virtual double CostPrice { get; set; }

        public virtual double SalesPrice { get; set; }

        public virtual string Picture { get; set; }

        [ForeignKey("ItemsCategoryId")]
        public int ItemsCategoryId { get; set; }
        public virtual string ItemsCategory { get; set; }

    }
}
