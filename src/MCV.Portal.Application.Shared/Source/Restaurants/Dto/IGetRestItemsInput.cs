using Abp.Application.Services.Dto;

namespace MCV.Portal.Source.Restaurants.Dto
{
    public interface IGetRestItemsInput : ISortedResultRequest
    {
        string Filter { get; set; }
    }
}
