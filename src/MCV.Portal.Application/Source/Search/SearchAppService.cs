using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using MCV.Portal.Source.Search.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCV.Portal.Source.Search
{
    public class SearchAppService : PortalAppServiceBase, ISearchAppService
    {

        private readonly IRepository<Search> _searchRepository;

        public SearchAppService(
           IRepository<Search> searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public ListResultDto<SearchMainListDto> GetSearchResults(GetSearchInput input)
        {
            var searchResults = _searchRepository
                .GetAll().WhereIf(!input.Filter.IsNullOrEmpty(), p => p.KeyWords.Contains(input.Filter)
           )
           .ToList();

            var searchResultsCount = searchResults.Count();


            return new ListResultDto<SearchMainListDto>(ObjectMapper.Map<List<SearchMainListDto>>(searchResults));
        }
    }
}
