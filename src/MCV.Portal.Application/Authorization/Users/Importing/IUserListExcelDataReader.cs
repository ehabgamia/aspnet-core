using System.Collections.Generic;
using MCV.Portal.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace MCV.Portal.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
