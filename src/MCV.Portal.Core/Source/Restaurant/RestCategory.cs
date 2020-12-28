using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurant
{
    [Table("RestCategories")]
    public class RestCategory : FullAuditedEntity
    {
        public string RestCategoryDesc { get; set; }
    }
}
