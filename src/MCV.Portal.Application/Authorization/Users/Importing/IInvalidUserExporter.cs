using System.Collections.Generic;
using MCV.Portal.Authorization.Users.Importing.Dto;
using MCV.Portal.Dto;

namespace MCV.Portal.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
