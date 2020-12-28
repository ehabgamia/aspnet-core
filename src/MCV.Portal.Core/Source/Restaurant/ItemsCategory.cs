using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurant
{
    [Table("ItemsCategories")]
    public class ItemsCategory : FullAuditedEntity
    {
        public string CategoryDesc { get; set; }
    }
}
