using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCV.Portal.Source.Restaurant
{
    [Table("RestNonSchItems")]
    public class RestNonSchItem : FullAuditedEntity
    {
        [ForeignKey("RestItemId")]
        public virtual RestItem RestItems { get; set; }
        public virtual int RestItemId { get; set; }

        [ForeignKey("RestInfoID")]
        public virtual RestInfo RestInfos { get; set; }
        public virtual int RestInfoID { get; set; }

        public virtual double SpecialSalesPrice { get; set; }
    }
}
