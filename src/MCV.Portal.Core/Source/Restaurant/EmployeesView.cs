using Abp.Domain.Entities;

namespace MCV.Portal.Source.Restaurant
{
    public class EmployeesView : Entity
    {
        public virtual int emp_id { get; set; }
        public virtual string emp_username { get; set; }
        public virtual int? emp_ext { get; set; }
    }
}