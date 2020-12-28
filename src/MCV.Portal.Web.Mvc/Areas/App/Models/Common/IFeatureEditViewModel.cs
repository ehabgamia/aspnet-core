using System.Collections.Generic;
using Abp.Application.Services.Dto;
using MCV.Portal.Editions.Dto;

namespace MCV.Portal.Web.Areas.App.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}