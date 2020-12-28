using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MCV.Portal.Source.Search.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCV.Portal.Source.Search
{
    public interface ISearchAppService : IApplicationService
    {
        ListResultDto<SearchMainListDto> GetSearchResults(GetSearchInput input);
    }
}
