using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurant
{
    [Table("RestItems")]
    public class RestItem : FullAuditedEntity
    {
        public virtual string ItemDesc { get; set; }

        public virtual string ItemCode { get; set; }

        public virtual double CostPrice { get; set; }

        public virtual double SalesPrice { get; set; }

        public virtual string Picture { get; set; }

        [ForeignKey("ItemsCategoryId")]
        public virtual ItemsCategory ItemsCategory { get; set; }
        public int ItemsCategoryId { get; set; }
    }
}
