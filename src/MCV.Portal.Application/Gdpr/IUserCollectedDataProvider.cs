using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using MCV.Portal.Dto;

namespace MCV.Portal.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
