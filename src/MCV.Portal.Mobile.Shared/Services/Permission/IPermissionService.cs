namespace MCV.Portal.Services.Permission
{
    public interface IPermissionService
    {
        bool HasPermission(string key);
    }
}