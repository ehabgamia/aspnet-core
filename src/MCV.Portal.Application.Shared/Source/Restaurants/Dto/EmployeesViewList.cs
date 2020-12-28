using Abp.Domain.Entities;

namespace MCV.Portal.Source.Restaurant.Dto
{
    public class EmployeesViewList : Entity
    {
        public int emp_id { get; set; }
        public string emp_username { get; set; }
        public int emp_ext { get; set; }
    }
}