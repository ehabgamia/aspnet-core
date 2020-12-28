using System.Collections.Generic;
using MCV.Portal.Authorization.Users.Dto;
using MCV.Portal.Dto;

namespace MCV.Portal.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}